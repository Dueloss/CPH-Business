﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeckt_Labyrint
{
    public partial class MazeControl : UserControl
    {
        public Maze MyMaze = new Maze();
        DeepsFirst way;

        public delegate void NewMazeSizeEventHandler(object source, MazeSizeArgs args);

        public event NewMazeSizeEventHandler NewMazeSized;

        public MazeControl()
        {
            InitializeComponent();
            MyMaze.changed += labChange;
        }

        protected virtual void OnNewMazeXed(Maze myMaze)
        {
            if (NewMazeSized != null)
                NewMazeSized(this, new MazeSizeArgs() { MyMaze = myMaze });
        }


        private void labChange()
        {
            this.Invalidate();
        }

        private void MazePaint(object sender, PaintEventArgs e)
        {
            if (!MyMaze.isEmpty())
            {
                e.Graphics.Clear(this.BackColor);
                Pen p = new Pen(Color.Red, 3);

                int width = DisplayRectangle.Width;
                int height = DisplayRectangle.Height;
                int left = DisplayRectangle.Left;
                int top = DisplayRectangle.Top;

                int xPix = width / MyMaze.X;
                int yPix = height / MyMaze.Y;

                for (int i = 0; i < MyMaze.Y; i++)
                {
                    for (int j = 0; j < MyMaze.X; j++)
                    {
                        Rectangle r = new Rectangle(j * xPix, i * yPix, xPix, yPix);
                        switch (MyMaze.Tial(j, i))
                        {
                            case 'B':
                                e.Graphics.FillRectangle(new SolidBrush(Color.Green), r);
                                break;
                            case 'E':
                                e.Graphics.FillRectangle(new SolidBrush(Color.Red), r);
                                break;
                            case 'X':
                                e.Graphics.FillRectangle(new SolidBrush(Color.Blue), r);
                                break;
                            case ',':
                                e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), r);
                                break;
                            case '*':
                                e.Graphics.FillRectangle(new SolidBrush(Color.Pink), r);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        
        private void MazeResize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void MazeControl_Load(object sender, EventArgs e)
        {
            
            
        }

        public void Solve()
        {
            if (!MyMaze.isEmpty())
            {
                if (way == null)
                {
                    way = new DeepsFirst(MyMaze);
                }
                way.findWay();

                Invalidate();
            }
        }

        private new void MouseClick(object sender, MouseEventArgs e)
        {
            if(way != null)
                way.Clear();
            int width = DisplayRectangle.Width;
            int height = DisplayRectangle.Height;
            
            int x = e.Location.X / (width / MyMaze.X);
            int y = e.Location.Y / (height / MyMaze.Y);

            if (e.Button == MouseButtons.Right)
            {
                switch (MyMaze.Tial(x, y))
                {
                    case ' ':
                        MyMaze.SetTial(MyMaze.EndX, MyMaze.EndY, 'X');
                        MyMaze.SetTial(x, y, 'E');
                        MyMaze.EndX = x;
                        MyMaze.EndY = y;

                        break;
                    case 'X':
                        MyMaze.SetTial(x, y, ' ');
                        break;
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                switch (MyMaze.Tial(x, y))
                {
                    case 'X':
                        MyMaze.SetTial(MyMaze.BeginX, MyMaze.BeginY, 'X');
                        MyMaze.SetTial(x, y, 'B');
                        MyMaze.BeginX = x;
                        MyMaze.BeginY = y;

                        break;
                    case ' ':
                        MyMaze.SetTial(x, y, 'X');
                        break;
                }
            }

        }
        public void LoadLab(string file)
        {
            MyMaze.Load(file);
            OnNewMazeXed(MyMaze);
        }

        public void Clear()
        {
            if (way != null)
                way.Clear();
        }

        public void SaveLab(string file)
        {
            MyMaze.Save(file);
        }

        public void NewMaze(int x, int y)
        {
            MyMaze.NewMaze(x, y);
            OnNewMazeXed(MyMaze);
        }
    }

    public class MazeSizeArgs : EventArgs
    {
        public Maze MyMaze { get; set; }
    }
}
