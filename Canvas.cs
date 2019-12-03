using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazeEdit
{
    public class Canvas : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        private List<string> lastRendered;

        //public Canvas(IJSRuntime jsruntime)
        //{
        //    this._jsruntime = jsruntime;
        //    this.lastRendered = new List<string>();
        //}

        public Canvas()
        {
            this.lastRendered = new List<string>();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            this.JSRuntime.InvokeVoidAsync($"loadCanvas");
            return base.OnAfterRenderAsync(firstRender);
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int seq = 0;

            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "blazeedit-canvas");
            builder.AddAttribute(seq++, "ondragover", "event.preventDefault();");
            builder.AddAttribute(seq++, "ondrop", Microsoft.AspNetCore.Components.EventCallback.Factory.Create(this, this.OnDrop));

            this.lastRendered.Clear();

            foreach (var component in State.Components)
            {
                builder.OpenElement(seq++, "div");

                var @class = $"blazeedit-id-{seq}";
                builder.AddAttribute(seq++, "class", @class);

                builder.OpenComponent<CanvasComponent>(seq++);
                builder.AddAttribute(seq++, "ComponentType", component);

                builder.CloseComponent();

                this.lastRendered.Add(@class);

                builder.CloseElement();

            }

            builder.CloseElement();

            base.BuildRenderTree(builder);
        }

        private async void OnDrop(DragEventArgs e)
        {
            var yOffset = await this.JSRuntime.InvokeAsync<int>($"getCanvasTopOffset");
            var yDragOffset = e.ClientY - yOffset;

            if (State.Payload == null) return;

            int sum = 0;

            for(int i = 0; i < this.lastRendered.Count; i++)
            {
                //var lastY = await this.JSRuntime.InvokeAsync<f>($"getLastCanvasMouseY");

                var height = await this.JSRuntime.InvokeAsync<int>($"getHeightFromRef", this.lastRendered[i]);
                sum += height;

                if (sum > yDragOffset)
                {
                    State.Components.Insert(i, State.Payload);
                    State.Payload = null;
                    break;
                }
            }

            if (this.lastRendered.Count <= 0 || State.Payload != null)
            {
                State.Components.Add(State.Payload);
                State.Payload = null;
            }

            //State.Components.Insert(index, State.Payload);

            //State.Components.Add(State.Payload);
            

            this.StateHasChanged();
        }
    }
}
