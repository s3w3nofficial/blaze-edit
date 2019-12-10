using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeEdit
{
    internal static class State
    {
        public static List<Block> Blocks { get; set; } = new List<Block>();
        public static List<ComponentBase> Components { get; set; } = new List<ComponentBase>();
        public static Type Payload { get; set; }
    }
}
