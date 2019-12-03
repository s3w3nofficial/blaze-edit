using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeEdit
{
    internal static class State
    {
        public static List<Block> Blocks { get; set; } = new List<Block>();
        public static List<Type> Components { get; set; } = new List<Type>();
        public static Type Payload { get; set; }
    }
}
