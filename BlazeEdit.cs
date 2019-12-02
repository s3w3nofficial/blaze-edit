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
        public List<Type> Types { get => State.Blocks; set => State.Blocks = value; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<BlockManager>(0);
            builder.CloseComponent();

            base.BuildRenderTree(builder);
        }
    }
}
