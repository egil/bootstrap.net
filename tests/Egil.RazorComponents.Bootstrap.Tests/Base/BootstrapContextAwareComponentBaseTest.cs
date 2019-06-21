using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Moq;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public class BootstrapContextAwareComponentBaseTest
    {
        public class FakeComponent : BootstrapContextAwareComponentBase
        {
            public virtual new void BuildRenderTree(RenderTreeBuilder builder) { base.BuildRenderTree(builder); }
            public virtual new void OnInit() { base.OnInit(); }
        }

        [Fact(DisplayName = "BuilderRenderTree will use DefaultRenderFragment when CustomRenderFragment is null")]
        public void MyTestMethod1()
        {
            var treeBuilder = CreateRenderTreeBuilderStub();
            var sutMock = new Mock<FakeComponent>() { CallBase = true };

            sutMock.Object.BuildRenderTree(treeBuilder);
            sutMock.Verify(x => x.DefaultRenderFragment(treeBuilder), Times.Once);
        }

        [Fact(DisplayName = "BuilderRenderTree will use CustomRenderFragment when its not null")]
        public void MyTestMethod2()
        {
            var treeBuilder = CreateRenderTreeBuilderStub();
            var sutMock = new Mock<FakeComponent>() { CallBase = true };
            var renderFragmentMock = new Mock<RenderFragment>();
            sutMock.Object.CustomRenderFragment = renderFragmentMock.Object;

            sutMock.Object.BuildRenderTree(treeBuilder);

            renderFragmentMock.Verify(x => x.Invoke(treeBuilder), Times.Once);
        }

        // TODO: Figure out a good way to test these without making OnRegisterChildContextRules, OnBootstrapComponentInit and OnChildInit internal
        //[Fact(DisplayName = "OnRegisterChildContextRules and OnBootstrapComponentInit is always called during compnents OnInit")]
        //public void MyTestMethod3()
        //{
        //    var sutMock = new Mock<FakeComponent>() { CallBase = true };

        //    sutMock.Object.OnInit();

        //    sutMock.Verify(x => x.OnRegisterChildContextRules(), Times.Once);
        //    sutMock.Verify(x => x.OnBootstrapComponentInit(), Times.Once);
        //}

        //[Fact(DisplayName = "When Parent is not null and parent has context, Parent.OnChildInit and ChildContext.UpdateChild is called")]
        //public void MyTestMethod4()
        //{
        //    var sut = new FakeComponent();
        //    var (parentMock, contextMock) = SetupParentMock();
        //    sut.Parent = parentMock.Object;

        //    sut.OnInit();

        //    parentMock.Verify(x => x.OnChildInit(sut), Times.Once);
        //    contextMock.Verify(x => x.UpdateChild<BootstrapContextAwareComponentBase>(sut), Times.Once);
        //}

        //[Fact(DisplayName = "When IgnoreParentContext is true, OnChildInit and ChildContext.UpdateChild are never called")]
        //public void MyTestMethod5()
        //{
        //    var sut = new FakeComponent();
        //    var (parentMock, contextMock) = SetupParentMock();
        //    sut.Parent = parentMock.Object;
        //    sut.IgnoreParentContext = true;

        //    sut.OnInit();

        //    parentMock.Verify(x => x.OnChildInit(sut), Times.Never);
        //    contextMock.Verify(x => x.UpdateChild<BootstrapContextAwareComponentBase>(sut), Times.Never);
        //}

        //[Fact(DisplayName = "When parent is not null, but doesnt have a child context, OnChildInit and ChildContext.UpdateChild are never called")]
        //public void MyTestMethod6()
        //{
        //    var sut = Mock.Of<FakeComponent>();
        //    var (parentMock, contextMock) = SetupParentMock();
        //    parentMock.SetupGet(x => x.HasContext).Returns(false);
        //    sut.Parent = parentMock.Object;

        //    sut.OnInit();

        //    parentMock.Verify(x => x.OnChildInit(sut), Times.Never);
        //    contextMock.Verify(x => x.UpdateChild<BootstrapContextAwareComponentBase>(sut), Times.Never);
        //}

        private static (Mock<FakeComponent>, Mock<BootstrapContext>) SetupParentMock()
        {
            var parentMock = new Mock<FakeComponent>();
            var contextMock = new Mock<BootstrapContext>();
            parentMock.SetupGet(x => x.Context).Returns(contextMock.Object);
            parentMock.SetupGet(x => x.HasContext).Returns(true);
            return (parentMock, contextMock);
        }

        private static RenderTreeBuilder CreateRenderTreeBuilderStub()
        {
            var rendere = new Mock<Renderer>(Mock.Of<IServiceProvider>()).Object;
            var mockTreeBuilder = new Mock<RenderTreeBuilder>(rendere);
            return mockTreeBuilder.Object;
        }
    }
}
