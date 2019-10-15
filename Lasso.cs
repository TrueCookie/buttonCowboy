using System;
using System.Drawing;
using System.Windows.Forms;

    public class Lasso
    {
        private const int BTN_SIZE = 70;
        private const int BTN_SHIFT = 35;

        private Button[] nodes;
        private int nodeNum = 0;

        public Lasso(int lenght, Point beginPoint)
        {
            nodes = new Button[lenght];
            foreach (Button node in nodes)
            {                
                ++nodeNum;
                node.Location = new Point(beginPoint.X - BTN_SHIFT * nodeNum, beginPoint.Y - BTN_SHIFT * nodeNum);
                node.Name = "node";
                node.Size = new System.Drawing.Size(BTN_SIZE, BTN_SIZE);
                node.TabIndex = 0;
                node.UseVisualStyleBackColor = true;
                node.BackColor = Color.Aqua;    //TODO: add method for init color
            }
        }
        //public Button[] Nodes { get => nodes; set => nodes = value; }
    }
