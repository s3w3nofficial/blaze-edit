using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeEdit
{
    public class BlockManager: ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int seq = 0;
            foreach (var type in State.Blocks)
            {
                if (!type.GetInterfaces().Any(t => t == typeof(IComponent))) continue;

                builder.OpenComponent<Block>(seq++);
                builder.AddAttribute(seq++, "Name", type.Name);
                builder.CloseComponent();
            }

            base.BuildRenderTree(builder);
        }
    }
}
