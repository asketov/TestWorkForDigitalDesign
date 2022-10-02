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
        private List<Lever> levers = new List<Lever>();
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
            label1.Visible = false;
            label2.Visible = false;
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            trackBar1.Visible = false;
            trackBar2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            N = trackBar1.Value;
            button2.Location = new Point(20, N * 50 + 10);
            button3.Location = new Point(100, N * 50 + 10);
            CountCommands = trackBar2.Value;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Lever lever = new Lever(i, j);
                    levers.Add(lever);
                }
            }
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
                    for (int k = 0; k < N; k++)
                    {
                        levers[i * N + k].RotateLever();
                        levers[k * N + j].RotateLever();
                    }
                    levers[i * N + j].RotateLever();
                }
            }
            Invalidate();
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            for (int i = 0; i < levers.Count; i++)
            {
                Lever lever = levers.ElementAt(i);
                graphics.DrawImage(lever.Img, lever.x, lever.y, Lever.size, Lever.size);
            }
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Location.X <= N * Lever.size && e.Location.Y <= N * Lever.size)
            {
                int i = e.Location.Y/Lever.size;
                int j = e.Location.X/ Lever.size;
                for (int k = 0; k < N; k++)
                {
                    levers[i * N + k].RotateLever();
                    levers[k * N + j].RotateLever();
                }
                levers[i*N + j].RotateLever();
                if (levers.All(element => element.isRotate) || levers.All(element => !element.isRotate))
                {
                    label1.Visible = true;
                    label2.Visible = true;
                    button1.Visible = true;
                    button2.Visible = false;
                    button3.Visible = false;
                    trackBar1.Visible = true;
                    trackBar2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    N = 0;
                    levers.Clear();
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
            label1.Visible = true;
            label2.Visible = true;
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            trackBar1.Visible = true;
            trackBar2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            N = 0;
            levers.Clear();
            OrderCommands.Clear();
            this.Height = 600;
            this.Width = 600;
            Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            levers.Clear();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Lever lever = new Lever(i, j);
                    levers.Add(lever);
                }
            }
            foreach (var position in OrderCommands)
            {
                for (int k = 0; k < N; k++)
                {
                    levers[position.i * N + k].RotateLever();
                    levers[k * N + position.j].RotateLever();
                }
                levers[position.i * N + position.j].RotateLever();
            }
            Invalidate();
        }
    }
}
