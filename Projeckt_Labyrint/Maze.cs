using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Projeckt_Labyrint
{
    public class Maze
    {
        
        char[,] maze;
        public int X { get; set; }
        public int Y { get; set; }
        public int BeginX { get; set; }
        public int BeginY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public event Action changed;

        public Maze()
        {      
            
        }
        public bool isEmpty()
        {
            if (maze != null)
                return false;
            return true;
        }

        public void Load(string file)
        {
            string[] lines;
            lines = File.ReadAllLines(file, Encoding.UTF8);

            var matchs = Regex.Matches(lines[0], "[0-9]+");
            X = int.Parse(matchs[0].Value);
            Y = int.Parse(matchs[1].Value);
            maze = new char[Y, X];

            for (int i = 0; i < Y; i++)
            {
                string buff = lines[i + 1];
                for (int j = 0; j < X; j++)
                {
                    maze[i, j] = buff[j];
                    if (maze[i, j] == 'B')
                    {
                        BeginX = j;
                        BeginY = i;
                    }
                    else if (maze[i,j] == 'E')
                    {
                        EndX = j;
                        EndY = i;
                    }
                    else if (maze[i, j] == '+' || maze[i, j] == '|' || maze[i,j] == '-')
                        SetTial(j, i, 'X');
                }
            }
            changed();
        }

        public void Save(string file)
        {
            string[] lines = new string[Y + 1];
            ClearMaze();
            string line = X + "x" + Y;
            lines[0] = line;
            for (int i = 0; i < Y; i++)
            {
                line = "";
                for (int j = 0; j < X; j++)
                {
                    line += maze[i, j];
                }
                lines[i + 1] = line;

            }
            File.WriteAllLines(file, lines);
        }

        public char Tial(int TargetX, int TargetY)
        {
            return maze[TargetY, TargetX];
        }

        public void SetTial(int TargetX, int TargetY, char Tial)
        {
            maze[TargetY, TargetX] = Tial;
            changed();
        }

        public void NewMaze(int x, int y)
        {
            X = x;
            Y = y;
            maze = new char[Y, X];
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    maze[j,i] = 'X';
                }
            }
            changed();
        }

        public void ClearMaze()
        {
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    if(Tial(j,i) == ',' || Tial(j,i) == '*')
                    {
                        SetTial(j, i, ' ');
                    }
                }
            }
        }
    }
}
