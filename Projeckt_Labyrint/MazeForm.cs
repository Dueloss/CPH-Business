using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Projeckt_Labyrint
{
    public partial class MazeForm : Form
    {
        public MazeForm()
        {
            InitializeComponent();
            mazeControl1.NewMazeSized += newSize;
        }
        private void newSize(object source, MazeSizeArgs e)
        {
            textBox1.Text = e.MyMaze.X.ToString();
            textBox2.Text = e.MyMaze.Y.ToString();
        }
        private void btnSolve_Click(object sender, EventArgs e)
        {
            mazeControl1.Solve();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            open.InitialDirectory = @"\Labyrint\";
            open.FilterIndex = 1;

            open.Multiselect = false;
            string file;
            if (open.ShowDialog() == DialogResult.OK)
            {
                file = open.FileName;
                mazeControl1.LoadLab(file);
            }
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            mazeControl1.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = @"\Labyrint\";
            save.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            save.FilterIndex = 1;

            string file;
            if(save.ShowDialog() == DialogResult.OK)
            {
                file = save.FileName;
                mazeControl1.SaveLab(file);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int x))
                x = 10;
            if (!int.TryParse(textBox2.Text, out int y))
                y = 10;
            mazeControl1.NewMaze(x,y);
        }
    }
}
