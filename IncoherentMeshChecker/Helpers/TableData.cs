using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncoherentMeshChecker.Helpers
{
    public class NodeTable
    {
        public int Node { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class ElementTable
    {
        public int Element { get; set; }
        public int Node1 { get; set; }
        public int Node2 { get; set; }
        public int Node3 { get; set; }
        public int Node4 { get; set; }
    }
}
