using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeckt_Labyrint
{
    class DeepsFirst
    {
        Stack<Node> nodes = new Stack<Node>();
        Maze myMaze;
        public bool Complete { get; set; }

        public DeepsFirst(Maze labyrint)
        {
            Complete = false;
            myMaze = labyrint;
        }

        public void findWay()
        {
            Clear();

            bool isStarted = false;
            char tail = myMaze.Tial(nodes.Peek().X, nodes.Peek().Y);
            while ((nodes.Count != 1 || isStarted != true) && tail != 'E')
            {
                isStarted = true;
                Node next = new Node(myMaze.BeginX, myMaze.BeginY);
                Node old = nodes.Peek();
                next.X = myMaze.BeginX;
                next.Y = myMaze.BeginY;
                // Go Right
                if (!next.Visitet && old.X + 1 < myMaze.X && (myMaze.Tial(old.X + 1, old.Y) == ' ' || myMaze.Tial(old.X + 1, old.Y) == 'E'))
                {
                    next.Visitet = true;
                    next.X = old.X + 1;
                    next.Y = old.Y;
                    nodes.Push(next);
                }

                // Go Down
                else if (!next.Visitet && old.Y + 1 < myMaze.Y && (myMaze.Tial(old.X, old.Y + 1) == ' ' || myMaze.Tial(old.X, old.Y + 1) == 'E'))
                {
                    next.Visitet = true;
                    next.X = old.X;
                    next.Y = old.Y + 1;
                    nodes.Push(next);
                }
                // Go left
                else if (!next.Visitet && old.X - 1 >= 0 && (myMaze.Tial(old.X - 1, old.Y) == ' ' || myMaze.Tial(old.X - 1, old.Y) == 'E'))
                {
                    next.Visitet = true;
                    next.X = old.X - 1;
                    next.Y = old.Y;
                    nodes.Push(next);
                }
                // Go up
                else if (!next.Visitet && old.Y - 1 >= 0 && (myMaze.Tial(old.X, old.Y - 1) == ' ' || myMaze.Tial(old.X, old.Y - 1) == 'E'))
                {
                    next.Visitet = true;
                    next.X = old.X;
                    next.Y = old.Y - 1;
                    nodes.Push(next);
                }
                else
                // No where to go
                {
                    nodes.Pop();
                }

                if (nodes.Count > 0)
                {
                    tail = myMaze.Tial(nodes.Peek().X, nodes.Peek().Y);
                }
                else
                    return;
                if (tail == ' ')
                    myMaze.SetTial(next.X, next.Y, ',');
                if (tail == 'E')
                {
                    Complete = true;
                }
            }
            if (Complete)
            {
                foreach (Node node in nodes)
                {
                    if (myMaze.Tial(node.X, node.Y) == ',')
                    {
                        myMaze.SetTial(node.X, node.Y, '*');
                    }
                }
            }
        }

        public void Clear()
        {
            myMaze.ClearMaze();
            nodes.Clear();
            nodes.Push(new Node(myMaze.BeginX, myMaze.BeginY));
        }
    }
}