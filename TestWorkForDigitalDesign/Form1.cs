using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TestWorkForDigitalDesign.Classes;

/*На сейфе множество поворачиваемых рукояток, которые могут быть расположены горизонтально или вертикально. 
Рукоятки расположены квадратом, как 2-мерный массив NxN. 
Есть одно правило - при повороте рукоятки (кликом мышки меняется положение рукоятки с вертикального 
в горизонтальный и обратно), поворачиваются все рукоятки в одной строке и в одном столбце. 
Сейф открывается, только если удается расположить все ручки параллельно друг другу 
(т.е. все вертикально или все горизонтально). Изначально поле должно быть запутано, но так, чтобы было решение. 
Число N должно быть настраиваемое.*/

namespace TestWorkForDigitalDesign
{
    public partial class Form1 : Form
    {
        private List<Lever> Levers = new List<Lever>();
        private HashSet<Position> OrderCommands = new HashSet<Position>();
        private int N = 0;
        private int CountCommands;
        public Form1()
        {
            InitializeComponent();
        }

        private void trackbar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = String.Format("Текущее значение: {0}", trackBar1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwitchingScene();
            N = trackBar1.Value;
            button2.Location = new Point(20, N * 50 + 10);
            button3.Location = new Point(100, N * 50 + 10);
            CountCommands = trackBar2.Value;
            InitializeLeversList();
            this.Height = N*50+100;
            this.Width = N*50+15;
            Random rnd = new Random();
            while (OrderCommands.Count < CountCommands)
            {
                int i = rnd.Next(0, N-1);
                int j = rnd.Next(0, N-1);
                bool success = OrderCommands.Add(new Position()
                {
                    i = i, j = j
                });
                if (success)
                {
                    RotateRowAndColumn(i,j);
                }
            }
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            for (int i = 0; i < Levers.Count; i++)
            {
                Lever lever = Levers.ElementAt(i);
                graphics.DrawImage(lever.Img, lever.x, lever.y, Lever.size, Lever.size);
            }
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Location.X <= N * Lever.size && e.Location.Y <= N * Lever.size)
            {
                int i = e.Location.Y/Lever.size;
                int j = e.Location.X/ Lever.size;
                RotateRowAndColumn(i,j);
                if (Levers.All(element => element.isRotate) || Levers.All(element => !element.isRotate))
                {
                    SwitchingScene();
                    N = 0;
                    Levers.Clear();
                    OrderCommands.Clear();
                    this.Height = 600;
                    this.Width = 600;
                }
                Invalidate();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label4.Text = String.Format("Текущее значение: {0}", trackBar2.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SwitchingScene();
            N = 0;
            Levers.Clear();
            OrderCommands.Clear();
            this.Height = 600;
            this.Width = 600;
            Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Levers.Clear();
            InitializeLeversList();
            foreach (var position in OrderCommands)
            {
                RotateRowAndColumn(position.i, position.j);
            }
            Invalidate();
        }

        private void InitializeLeversList()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Levers.Add(new Lever(i, j));
                }
            }
        }

        private void SwitchingScene()
        {
            label1.Visible = !label1.Visible;
            label2.Visible = !label2.Visible;
            button1.Visible = !button1.Visible;
            button2.Visible = !button2.Visible;
            button3.Visible = !button3.Visible;
            trackBar1.Visible = !trackBar1.Visible;
            trackBar2.Visible = !trackBar2.Visible;
            label3.Visible = !label3.Visible;
            label4.Visible = !label4.Visible;
        }

        private void RotateRowAndColumn(int i, int j)
        {
            for (int k = 0; k < N; k++)
            {
                Levers[i * N + k].RotateLever();
                Levers[k * N + j].RotateLever();
            }
            Levers[i * N + j].RotateLever();
        }
    }
}
