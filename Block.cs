using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeEdit
{
    public class Block: ComponentBase
    {
        public Block()
        {

        }
        public Block(Type blockType)
        {
            this.BlockType = blockType;
        }

        [Parameter]
        public Type BlockType { get; set; }

        private string @class;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "onmouseenter", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, this.OnMouseEnter));
            builder.AddAttribute(2, "onmouseleave", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, this.OnMouseLeave));;
            builder.AddAttribute(3, "draggable", "true");
            builder.AddAttribute(4, "ondragstart", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, this.OnDragStart));
            builder.AddAttribute(5, "class", this.@class);
            builder.AddContent(6, this.BlockType.Name);
            builder.CloseElement();

            base.BuildRenderTree(builder);
        }

        private void OnDragStart(DragEventArgs e)
        {
            State.Payload = this.BlockType;
        }

        private void OnMouseEnter()
        {
            this.@class = "hightlight";
            this.StateHasChanged();
        }

        private void OnMouseLeave()
        {
            this.@class = "";
            this.StateHasChanged();
        }
    }
}
