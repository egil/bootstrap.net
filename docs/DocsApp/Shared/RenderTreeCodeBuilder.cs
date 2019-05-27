//using Microsoft.AspNetCore.Components.Rendering;
//using Microsoft.AspNetCore.Components.RenderTree;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DocsApp.Shared
//{
//    public class RenderTreeCodeBuilder : RenderTreeBuilder
//    {
//        public RenderTreeCodeBuilder(Renderer renderer) : base(renderer)
//        {
//        }

//        //
//        // Summary:
//        //     Appends a frame representing an attribute.
//        //     The attribute is associated with the most recently added element.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   frame:
//        //     A Microsoft.AspNetCore.Components.RenderTree.RenderTreeFrame holding the name
//        //     and value of the attribute.
//        public new void AddAttribute(int sequence, in RenderTreeFrame frame)
//        {
//            base.AddAttribute(sequence, frame);
//        }
//        //
//        // Summary:
//        //     Appends a frame representing a string-valued attribute. The attribute is associated
//        //     with the most recently added element. If the value is null, or the System.Boolean
//        //     value false and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        public new void AddAttribute(int sequence, string name, object value)
//        {

//        }
//        //
//        // Summary:
//        //     Appends a frame representing an Microsoft.AspNetCore.Components.EventCallback
//        //     attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is null and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        //
//        // Remarks:
//        //     This method is provided for infrastructure purposes, and is used to support generated
//        //     code that uses Microsoft.AspNetCore.Components.EventCallbackFactory.
//        public void AddAttribute<T>(int sequence, string name, EventCallback<T> value);
//        //
//        // Summary:
//        //     Appends a frame representing an Microsoft.AspNetCore.Components.EventCallback
//        //     attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is null and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        //
//        // Remarks:
//        //     This method is provided for infrastructure purposes, and is used to support generated
//        //     code that uses Microsoft.AspNetCore.Components.EventCallbackFactory.
//        public void AddAttribute(int sequence, string name, EventCallback value);
//        //
//        // Summary:
//        //     Appends a frame representing a delegate-valued attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is null and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        //
//        // Remarks:
//        //     This method is provided for infrastructure purposes, and is used to be Microsoft.AspNetCore.Components.UIEventArgsRenderTreeBuilderExtensions
//        //     to provide support for delegates of specific types. For a good programming experience
//        //     when using a custom delegate type, define an extension method similar to Microsoft.AspNetCore.Components.UIEventArgsRenderTreeBuilderExtensions.AddAttribute(Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder,System.Int32,System.String,System.Action{Microsoft.AspNetCore.Components.UIChangeEventArgs})
//        //     that calls this method.
//        public void AddAttribute(int sequence, string name, MulticastDelegate value);
//        //
//        // Summary:
//        //     Appends a frame representing a System.Func`2-valued attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is null and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        public void AddAttribute(int sequence, string name, Func<UIEventArgs, Task> value);
//        //
//        // Summary:
//        //     Appends a frame representing a System.Func`1-valued attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is null and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        public void AddAttribute(int sequence, string name, Func<Task> value);
//        //
//        // Summary:
//        //     Appends a frame representing an System.Action-valued attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is null and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        public void AddAttribute(int sequence, string name, Action value);
//        //
//        // Summary:
//        //     Appends a frame representing a string-valued attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is null and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        public void AddAttribute(int sequence, string name, string value);
//        //
//        // Summary:
//        //     Appends a frame representing an System.Action`1-valued attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is null and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        public void AddAttribute(int sequence, string name, Action<UIEventArgs> value);
//        //
//        // Summary:
//        //     Appends a frame representing a bool-valued attribute.
//        //     The attribute is associated with the most recently added element. If the value
//        //     is false and the current element is not a component, the frame will be omitted.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   name:
//        //     The name of the attribute.
//        //
//        //   value:
//        //     The value of the attribute.
//        public void AddAttribute(int sequence, string name, bool value);
//        //
//        // Summary:
//        //     Appends a frame representing an instruction to capture a reference to the parent
//        //     component.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   componentReferenceCaptureAction:
//        //     An action to be invoked whenever the reference value changes.
//        public void AddComponentReferenceCapture(int sequence, Action<object> componentReferenceCaptureAction);
//        //
//        // Summary:
//        //     Appends a frame representing text content.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   textContent:
//        //     Content for the new text frame.
//        public void AddContent(int sequence, object textContent);
//        //
//        // Summary:
//        //     Appends a frame representing markup content.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   markupContent:
//        //     Content for the new markup frame.
//        public void AddContent(int sequence, MarkupString markupContent);
//        //
//        // Summary:
//        //     Appends frames representing an arbitrary fragment of content.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   fragment:
//        //     Content to append.
//        //
//        //   value:
//        //     The value used by fragment.
//        public void AddContent<T>(int sequence, RenderFragment<T> fragment, T value);
//        //
//        // Summary:
//        //     Appends frames representing an arbitrary fragment of content.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   fragment:
//        //     Content to append.
//        public void AddContent(int sequence, RenderFragment fragment);
//        //
//        // Summary:
//        //     Appends a frame representing text content.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   textContent:
//        //     Content for the new text frame.
//        public void AddContent(int sequence, string textContent);
//        //
//        // Summary:
//        //     Appends a frame representing an instruction to capture a reference to the parent
//        //     element.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   elementReferenceCaptureAction:
//        //     An action to be invoked whenever the reference value changes.
//        public void AddElementReferenceCapture(int sequence, Action<ElementRef> elementReferenceCaptureAction);
//        //
//        // Summary:
//        //     Appends a frame representing markup content.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   markupContent:
//        //     Content for the new markup frame.
//        public void AddMarkupContent(int sequence, string markupContent);
//        //
//        // Summary:
//        //     Clears the builder.
//        public void Clear();
//        //
//        // Summary:
//        //     Marks a previously appended component frame as closed. Calls to this method must
//        //     be balanced with calls to Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder.OpenComponent``1(System.Int32).
//        public void CloseComponent();
//        //
//        // Summary:
//        //     Marks a previously appended element frame as closed. Calls to this method must
//        //     be balanced with calls to Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder.OpenElement(System.Int32,System.String).
//        public void CloseElement();
//        //
//        // Summary:
//        //     Returns the Microsoft.AspNetCore.Components.RenderTree.RenderTreeFrame values
//        //     that have been appended.
//        //
//        // Returns:
//        //     An array range of Microsoft.AspNetCore.Components.RenderTree.RenderTreeFrame
//        //     values.
//        public ArrayRange<RenderTreeFrame> GetFrames();
//        //
//        // Summary:
//        //     Appends a frame representing a child component.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        // Type parameters:
//        //   TComponent:
//        //     The type of the child component.
//        public void OpenComponent<TComponent>(int sequence) where TComponent : IComponent;
//        //
//        // Summary:
//        //     Appends a frame representing a child component.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   componentType:
//        //     The type of the child component.
//        public void OpenComponent(int sequence, Type componentType);
//        //
//        // Summary:
//        //     Appends a frame representing an element, i.e., a container for other frames.
//        //     In order for the Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder
//        //     state to be valid, you must also call Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder.CloseElement
//        //     immediately after appending the new element's child frames.
//        //
//        // Parameters:
//        //   sequence:
//        //     An integer that represents the position of the instruction in the source code.
//        //
//        //   elementName:
//        //     A value representing the type of the element.
//        public void OpenElement(int sequence, string elementName);
//    }
//}
