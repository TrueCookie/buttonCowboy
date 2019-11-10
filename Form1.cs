using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace buttonCowboy
{
    public partial class Form1 : Form
    {
        private const int LASSO_LENGTH = 12;
        private const int BTN_POINT_X = 720;
        private const int BTN_POINT_Y = 420;
        private const int BTN_SIZE = 70;
        private const int BTN_SHIFT = 30;
        //private Lasso lasso;  
        private Point origin = new Point(BTN_POINT_X, BTN_POINT_Y);
        private Button[] lasso;
        private int turnBegin;
        private int turnEnd;
        private bool inverseOrder = false;

        private void initLasso()    //should i do write this method just in this class, reallly?
        {
            lasso = new Button[LASSO_LENGTH];

            this.SuspendLayout();

            for (int i = 0; i < LASSO_LENGTH; ++i)
            {
                this.lasso[i] = new Button();
                this.lasso[i].Location = new System.Drawing.Point(origin.X - BTN_SHIFT * i, origin.Y - BTN_SHIFT * i);
                this.lasso[i].Name = "btn" + i;
                this.lasso[i].Size = new System.Drawing.Size(BTN_SIZE, BTN_SIZE);
                this.lasso[i].TabIndex = i;
                this.lasso[i].Text = "btn" + i;
                this.lasso[i].UseVisualStyleBackColor = true;
                this.lasso[i].Click += new System.EventHandler(this.node_Click);
                Controls.Add(lasso[i]);
            }
            this.ResumeLayout(false);
        }

        /*private void initLasso()
        {
            lasso = new Lasso(LASSO_LENGTH, origin, node_Click);
            this.SuspendLayout();
            this.Controls.Add(this.lasso);
            this.ResumeLayout(false);
        }*/

        public Form1()
        {
            InitializeComponent();  
            initLasso();
        }

        private void node_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            clickedBtn.BackColor = Color.Chocolate;
            if (inverseOrder)
            {
                turnBegin = clickedBtn.TabIndex;
                turnEnd = 1;
            }
            else
            {
                turnBegin = clickedBtn.TabIndex;
                turnEnd = LASSO_LENGTH - 2;
            }
            Point prevLocation = new Point(lasso[turnBegin].Location.X, lasso[turnBegin].Location.Y);
            Point curLocation;
            for (int i = turnBegin; i <= turnEnd; ++i)
            {
                curLocation = new Point(lasso[i+1].Location.X, lasso[i+1].Location.Y);
                if (lasso[i+1].Location.X > prevLocation.X
                    && lasso[i + 1].Location.Y < prevLocation.Y)
                {
                    lasso[i + 1].Location = new Point(lasso[i].Location.X + BTN_SHIFT, lasso[i].Location.Y + BTN_SHIFT);
                }
                else if (lasso[i + 1].Location.X > prevLocation.X
                   && lasso[i + 1].Location.Y > prevLocation.Y)
                {
                    lasso[i + 1].Location = new Point(lasso[i].Location.X - BTN_SHIFT, lasso[i].Location.Y + BTN_SHIFT);
                }
                else if (lasso[i + 1].Location.X < prevLocation.X
                   && lasso[i + 1].Location.Y > prevLocation.Y)
                {
                    lasso[i + 1].Location = new Point(lasso[i].Location.X - BTN_SHIFT, lasso[i].Location.Y - BTN_SHIFT);
                }
                else if (lasso[i + 1].Location.X < prevLocation.X
                   && lasso[i + 1].Location.Y < prevLocation.Y)
                {
                    lasso[i + 1].Location = new Point(lasso[i].Location.X + BTN_SHIFT, lasso[i].Location.Y - BTN_SHIFT);
                }
                prevLocation = curLocation;
            }
        }

    }
}
