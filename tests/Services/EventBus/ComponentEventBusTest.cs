using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Services.EventBus
{
    public class ComponentEventBusTest
    {
        class DummyComponent : ComponentBase
        {
        }

        class DummyEventType : IEventType
        {
            private DummyEventType() { }

            public string Key { get; } = nameof(DummyEventType);

            public bool CacheLatest { get; } = false;

            public static readonly DummyEventType Instance = new DummyEventType();
        }

        class DummyEvent : IEvent<DummyEventType, DummyComponent>
        {
            public DummyEventType Type { get; } = DummyEventType.Instance;

            public DummyComponent Source { get; } = new DummyComponent();
        }

        [Fact]
        public async Task CanSubscribeAndReceiveEvents()
        {
            var sut = new ComponentEventBus();
            var calledTimes = 0;

            sut.Subscribe<DummyEventType, DummyComponent>(DummyEventType.Instance, (evt) => calledTimes++);
            sut.Subscribe<DummyEventType, DummyComponent>(DummyEventType.Instance, (evt) => calledTimes++);

            await sut.PublishAsync(new DummyEvent());

            calledTimes.ShouldBe(2);
        }

        [Fact]
        public void CanSubscribeAndUnsubscribe()
        {
            var sut = new ComponentEventBus();
            Action<IEvent<DummyEventType, DummyComponent>> cb1 = (e) => throw new Exception("SHOULD NOT HAPPEN");
            Action<IEvent<DummyEventType, DummyComponent>> cb2 = (e) => { };
            Action<IEvent<DummyEventType, DummyComponent>> cb3 = (e) => throw new Exception("SHOULD NOT HAPPEN");

            sut.Subscribe(DummyEventType.Instance, cb1);
            sut.Subscribe(DummyEventType.Instance, cb2);
            sut.Subscribe(DummyEventType.Instance, cb3);

            sut.Unsubscribe(DummyEventType.Instance, cb1);
            sut.Unsubscribe(DummyEventType.Instance, cb3);

            Should.NotThrow(async () => await sut.PublishAsync(new DummyEvent()));
        }

        [Fact]
        public void WhenAllHandlersUnsubscribeTheEventIsRemoved()
        {
            var sut = new ComponentEventBus();
            Action<IEvent<DummyEventType, DummyComponent>> cb1 = (e) => throw new Exception("SHOULD NOT HAPPEN");

            sut.Subscribe(DummyEventType.Instance, cb1);
            sut.Unsubscribe(DummyEventType.Instance, cb1);

            Should.NotThrow(async () => await sut.PublishAsync(new DummyEvent()));
        }

        class DummyCachedEventType : IEventType
        {
            private DummyCachedEventType() { }

            public string Key { get; } = nameof(DummyCachedEventType);

            public bool CacheLatest { get; } = true;

            public static readonly DummyCachedEventType Instance = new DummyCachedEventType();
        }

        class DummyCachedEvent : IEvent<DummyCachedEventType, DummyComponent>
        {
            public DummyCachedEventType Type { get; } = DummyCachedEventType.Instance;

            public DummyComponent Source { get; } = new DummyComponent();
        }

        [Fact]
        public async Task WhenEvenTypeHasCacheLatestNewSubscribersGetsLastEventUpponSubscription()
        {
            var sut = new ComponentEventBus();
            var evtExpected = new DummyCachedEvent();
            await sut.PublishAsync(evtExpected);

            sut.Subscribe<DummyCachedEventType, DummyComponent>(DummyCachedEventType.Instance, (evtActual) => evtActual.ShouldBe(evtExpected));
        }

        [Fact]
        public async Task CachedEventsAreDiscardedWhenSourceComponentDiposes()
        {
            var sut = new ComponentEventBus();
            var evt = new DummyCachedEvent();

            await sut.PublishAsync(evt);
            evt.Source.Dispose();

            sut.Subscribe<DummyCachedEventType, DummyComponent>(DummyCachedEventType.Instance, (e) => throw new Exception("SHOULD NOT HAPPEN"));
        }
    }
}
