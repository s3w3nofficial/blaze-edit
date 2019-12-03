using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlazeEdit
{
    public class BlazeEdit: ComponentBase
    {
        [Parameter]
        public List<Type> Types { 
            get => State.Blocks.Select(b => b.BlockType).ToList(); 
            set => State.Blocks = value.Select(v => new Block(v)).ToList();
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "blazeedit");

            builder.OpenComponent<Canvas>(2);
            builder.CloseComponent();

            builder.OpenComponent<BlockManager>(3);
            builder.CloseComponent();

            builder.CloseElement();

            base.BuildRenderTree(builder);
        }
    }
}
