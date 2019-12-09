using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeEdit
{
    public class CanvasComponent: ComponentBase
    {
        [Parameter]
        public Type ComponentType { get; set; }
        private string @class;
        public CanvasComponent()
        {
            this.@class = "";
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int seq = 0;
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", this.@class);
            builder.AddAttribute(seq++, "draggable", "true");
            builder.AddAttribute(seq++, "ondragstart", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, this.OnDragStart));
            builder.OpenComponent(seq++, ComponentType);
            builder.CloseComponent();
            builder.CloseElement();

            base.BuildRenderTree(builder);
        }

        private void OnDragStart()
        {
            State.Payload = this.ComponentType;
            State.Components.Remove(this.ComponentType);
        }
    }
}
