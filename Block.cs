using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeEdit
{
    public class Block: ComponentBase
    {
        [Parameter]
        public string Name { get; set; }

        private string @class;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "onmouseenter", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, this.OnMouseEnter));
            builder.AddAttribute(2, "onmouseleave", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, this.OnMouseLeave));
            builder.AddAttribute(3, "class", this.@class);
            builder.AddContent(4, Name);
            builder.CloseElement();

            base.BuildRenderTree(builder);
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
