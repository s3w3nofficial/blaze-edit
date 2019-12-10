using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeEdit.Models
{
    class ComponentNode
    {
        public Type Type { get; set; }
        public List<ComponentNode> Nodes { get; set; }

        public ComponentNode()
        {
            this.Nodes = new List<ComponentNode>();
        }
    }
}
