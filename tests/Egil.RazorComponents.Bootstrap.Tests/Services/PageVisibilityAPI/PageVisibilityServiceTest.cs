using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI
{
    public class PageVisibilityServiceTest
    {
        class StubJsRuntime : IJSRuntime
        {
            private readonly List<(string identifier, object[] args)> _invocations = new List<(string identifier, object[] args)>();

            public IReadOnlyList<(string identifier, object[] args)> Invocations => _invocations.AsReadOnly();

            public Task<TValue> InvokeAsync<TValue>(string identifier, params object[] args)
            {
                _invocations.Add((identifier, args));
                return Task.FromResult<TValue>(default!);
            }
        }

        private (PageVisibilityAPIService, Mock<IJSRuntime>) SetupSutAndJsRuntime(MockBehavior mockBehavior = MockBehavior.Strict)
        {
            var jsRuntimeMock = new Mock<IJSRuntime>(mockBehavior);
            var sut = new PageVisibilityAPIService(jsRuntimeMock.Object, pvs => default);
            return (sut, jsRuntimeMock);
        }

        [Fact(DisplayName = "When user subscribes, api adapter is injected into browser and adapter is subscribed to")]
        public void MyTestMethod2()
        {
            var (sut, jsRuntimeMock) = SetupSutAndJsRuntime();
            var sequence = new MockSequence();
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>())).Returns(Task.FromResult(new object()));

            sut.OnPageVisibilityChanged += (sender, e) => { };

            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("eval", It.IsAny<string>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.subscribe", It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>()), Times.Once);
        }

        [Fact(DisplayName = "When another user subscribes and service is already subscribed to api adapter, no new subscription to api adapter is created")]
        public void MyTestMethod3()
        {
            var (sut, jsRuntimeMock) = SetupSutAndJsRuntime();
            var sequence = new MockSequence();
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>())).Returns(Task.FromResult(new object()));

            sut.OnPageVisibilityChanged += (sender, e) => { };
            sut.OnPageVisibilityChanged += (sender, e) => { };

            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("eval", It.IsAny<string>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.subscribe", It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>()), Times.Once);
            jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "When SetVisibilityState is called with a new isVisisble value, subscribers are notified")]
        public void MyTestMethod4()
        {
            var (sut, _) = SetupSutAndJsRuntime(MockBehavior.Loose);
            var actualStatusUpdates = new List<bool>();
            sut.OnPageVisibilityChanged += (sender, e) => { actualStatusUpdates.Add(e.IsPageVisible); };

            sut.SetVisibilityState(false);

            actualStatusUpdates.ShouldBe(new List<bool> { false });
        }

        [Fact(DisplayName = "When SetVisibilityState is called with a existing isVisisble value, subscribers are not notified")]
        public void MyTestMethod5()
        {
            var (sut, _) = SetupSutAndJsRuntime(MockBehavior.Loose);
            var actualStatusUpdates = new List<bool>();
            sut.OnPageVisibilityChanged += (sender, e) => { actualStatusUpdates.Add(e.IsPageVisible); };

            sut.SetVisibilityState(sut.IsPageVisible);

            actualStatusUpdates.ShouldBeEmpty();
        }

        [Fact(DisplayName = "When there are no more subscribers left, the service unsubscribes from the api adapter")]
        public void MyTestMethod6()
        {
            static void Subscriber(object sender, PageVisibilityChangedEventArgs args) { };
            var (sut, jsRuntimeMock) = SetupSutAndJsRuntime();
            var sequence = new MockSequence();
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>())).Returns(Task.FromResult(new object()));

            sut.OnPageVisibilityChanged += Subscriber;
            sut.OnPageVisibilityChanged -= Subscriber;

            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("eval", It.IsAny<string>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.subscribe", It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.unsubscribe"), Times.Once);
        }

        [Fact(DisplayName = "When user subscribes again to service that has previously had subscribers but have none now, api adapter is not injected again")]
        public void MyTestMethod7()
        {
            static void Subscriber(object sender, PageVisibilityChangedEventArgs args) { };
            var (sut, jsRuntimeMock) = SetupSutAndJsRuntime();
            var sequence = new MockSequence();
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>())).Returns(Task.FromResult(new object()));

            sut.OnPageVisibilityChanged += Subscriber;
            sut.OnPageVisibilityChanged -= Subscriber;
            sut.OnPageVisibilityChanged += Subscriber;

            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("eval", It.IsAny<string>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.subscribe", It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>()), Times.Exactly(2));
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.unsubscribe"), Times.Once);
            jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "If two users subscribe at the same time, the api service injection and subscribtion to it is only run once")]
        public void MyTestMethod8()
        {
            var (sut, jsRuntimeMock) = SetupSutAndJsRuntime();
            var injectTask = new Task<object>(() => new object());
            var sequence = new MockSequence();
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<string>())).Returns(injectTask);
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>())).Returns(Task.FromResult(new object()));

            sut.OnPageVisibilityChanged += (sender, e) => { };
            sut.OnPageVisibilityChanged += (sender, e) => { };
            injectTask.RunSynchronously();

            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("eval", It.IsAny<string>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.subscribe", It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>()), Times.Once);
            jsRuntimeMock.VerifyNoOtherCalls();
        }

        [Fact(DisplayName = "When Dispose is called, the service unsubscribes from api adapter, if subscribed")]
        public void MyTestMethod9()
        {
            var (sut, jsRuntimeMock) = SetupSutAndJsRuntime();
            var sequence = new MockSequence();
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>())).Returns(Task.FromResult(new object()));
            sut.OnPageVisibilityChanged += (sender, e) => { };

            sut.Dispose();

            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("eval", It.IsAny<string>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.subscribe", It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.unsubscribe"), Times.Once);
            jsRuntimeMock.VerifyNoOtherCalls();
        }


        [Fact(DisplayName = "If the last user unsubscribes at the same time as Dispose is called, unsubscribe to api adapter run only once")]
        public void MyTestMethod10()
        {
            var (sut, jsRuntimeMock) = SetupSutAndJsRuntime();
            var unsubscribeTask = new Task<object>(() => new object());
            var sequence = new MockSequence();
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>())).Returns(Task.FromResult(new object()));
            jsRuntimeMock.InSequence(sequence).Setup(x => x.InvokeAsync<object>(It.IsAny<string>())).Returns(unsubscribeTask);
            sut.OnPageVisibilityChanged += (sender, e) => { };

            sut.OnPageVisibilityChanged -= (sender, e) => { };
            sut.Dispose();
            unsubscribeTask.RunSynchronously();

            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("eval", It.IsAny<string>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.subscribe", It.IsAny<DotNetObjectRef<PageVisibilityAPIService>>()), Times.Once);
            jsRuntimeMock.Verify(x => x.InvokeAsync<object>("window.pageVisibilityApiRazorAdapter.unsubscribe"), Times.Once);
            jsRuntimeMock.VerifyNoOtherCalls();
        }

    }
}
