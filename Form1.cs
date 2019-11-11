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
        private const int LASSO_LENGTH = 6;
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

        int[] blueIndex = new int[] { 1, 3, 5, 6, 7, 10};

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
                if(isBlue(lasso[i].TabIndex)){ this.lasso[i].BackColor = Color.Blue; }
                Controls.Add(lasso[i]);
            }
            this.ResumeLayout(false);
        }

        public bool isBlue(int btnIndex) {
            for (int i = 0; i < blueIndex.Length; i++) {
                if (blueIndex[i] == btnIndex) {
                    return true;
                }
            }
            return false;
        }

        public bool isEnd(int i) {
            if (inverseOrder)
            {
                return i > 1;
            }
            else
            {
                return i <= LASSO_LENGTH - 2;
            }
        }

        private void node_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            int orderSign = 1;
            Point prevLocation = new Point(lasso[turnBegin].Location.X, lasso[turnBegin].Location.Y);
            Point curLocation;
            turnBegin = clickedBtn.TabIndex;
            turnEnd = LASSO_LENGTH - 1;

            /*if (inverseOrder)
            {
                turnEnd = 1;
                orderSign = -1;
            }
            else
            {
                turnEnd = LASSO_LENGTH - 2;
                orderSign = 1;
            }*/

            /*int i = turnBegin;
            while (!isEnd(i))
            {
                curLocation = new Point(lasso[i + 1].Location.X, lasso[i + 1].Location.Y);

                int pointX, pointY;
                int posSignX, posSignY, colorSign;
                colorSign = isBlue(clickedBtn.TabIndex) ? -1 : 1;

                posSignY = (lasso[i + orderSign*1].Location.X > prevLocation.X) ? 1 : -1;
                pointY = lasso[i].Location.Y + colorSign * posSignY * BTN_SHIFT;

                posSignX = (lasso[i + orderSign * 1].Location.Y > prevLocation.Y) ? -1 : 1;
                pointX = lasso[i].Location.X + colorSign * posSignX * BTN_SHIFT;

                lasso[i + orderSign * 1].Location = new Point(pointX, pointY);

                prevLocation = curLocation;

                i = inverseOrder ? i - 1 : i + 1;
            }*/

            for (int i = turnBegin; i < turnEnd; ++i)
            {
                curLocation = new Point(lasso[i + 1].Location.X, lasso[i + 1].Location.Y);
                
                int pointX, pointY;
                int posSignX, posSignY, colorSign;
                colorSign = isBlue(clickedBtn.TabIndex) ? -1 : 1;
                   
                posSignY = (lasso[i + 1].Location.X > prevLocation.X) ? 1 : -1;
                pointY = lasso[i].Location.Y + colorSign * posSignY * BTN_SHIFT;

                posSignX = (lasso[i + 1].Location.Y > prevLocation.Y) ? -1 : 1;
                pointX = lasso[i].Location.X + colorSign * posSignX * BTN_SHIFT;

                lasso[i + 1].Location = new Point(pointX, pointY);

                prevLocation = curLocation;
            }

        }

        private void Form1_Click(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < LASSO_LENGTH; i++) {
                if (inverseOrder)
                {
                    lasso[i].SendToBack();
                    lasso[i].TabIndex = LASSO_LENGTH-1 - i;
                }
                else
                {
                    lasso[i].BringToFront();
                    lasso[i].TabIndex = i;
                }
            }
            inverseOrder = !inverseOrder;
        }
    }
}
