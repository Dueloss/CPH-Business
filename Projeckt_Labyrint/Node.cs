using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeckt_Labyrint
{
    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visitet { get; set; } 
        public Node(int xNode, int yNode)
        {
            X = xNode;
            Y = yNode;
            Visitet = false;
        }
    }
}
