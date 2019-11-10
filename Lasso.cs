using System;
using System.Drawing;
using System.Windows.Forms;

namespace buttonCowboy
{
    public class Lasso : Control
    {
        private const int BTN_SIZE = 70;
        private const int BTN_SHIFT = 35;
        private Button[] nodes;

        public Lasso(int lenght, Point beginPoint, Action<object, EventArgs> node_Click)
        {
            nodes = new Button[lenght];
            /*int i = 0;
            nodes = new Button[12];
            foreach (Button node in nodes)
            {
                //node = new Button();
                node.Location = new System.Drawing.Point(beginPoint.X - BTN_SHIFT * i, beginPoint.Y - BTN_SHIFT * i);
                node.Name = "btn" + i;
                node.Size = new System.Drawing.Size(BTN_SIZE, BTN_SIZE);
                node.TabIndex = i;
                node.Text = "btn" + i;
                node.UseVisualStyleBackColor = true;
                node.Click += new EventHandler(node_Click);
                //Controls.Add(nodes[i]);
                i++;
            }*/
            for (int i = 0; i < lenght; ++i)
            {
                nodes[i] = new Button();
                nodes[i].Location = new System.Drawing.Point(beginPoint.X - BTN_SHIFT * i, beginPoint.Y - BTN_SHIFT * i);
                nodes[i].Name = "btn" + i;
                nodes[i].Size = new System.Drawing.Size(BTN_SIZE, BTN_SIZE);
                nodes[i].TabIndex = i;
                nodes[i].Text = "btn" + i;
                nodes[i].UseVisualStyleBackColor = true;
                //nodes[i].Click += new EventHandler(node_Click);
                Controls.Add(nodes[i]);
            }
        }

        //public Lasso Lasso { get => lasso; set => lasso = value; } 

        public void spin()
        {
            foreach (Button node in nodes)
            {
                node.BackColor = Color.DarkOrange;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}