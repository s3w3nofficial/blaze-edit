using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazeEdit
{
    public class Canvas : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        private List<string> lastRendered;

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
                builder.AddAttribute(seq++, "ComponentType", component.GetType());

                var props = component.GetType().GetProperties().Where(p => p.GetCustomAttributes().Any(a => a.GetType() == typeof(Attributes.Blazable)));

                foreach(var prop in props)
                {
                    var val = prop.GetValue(component);
                    //builder.AddAttribute(seq++, prop.Name, prop.GetValue(component));
                }


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
                    State.Components.Insert(i, Activator.CreateInstance(State.Payload) as ComponentBase);
                    State.Payload = null;
                    break;
                }
            }

            if (this.lastRendered.Count <= 0 || State.Payload != null)
            {
                State.Components.Add(Activator.CreateInstance(State.Payload) as ComponentBase);
                State.Payload = null;
            }

            //State.Components.Insert(index, State.Payload);

            //State.Components.Add(State.Payload);
            

            this.StateHasChanged();
        }
    }
}
