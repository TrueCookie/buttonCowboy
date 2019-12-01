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

        public bool isEnd(int index) {
            if (inverseOrder)
            {
                return index == 0;
            }
            else
            {
                return index == LASSO_LENGTH - 1;
            }
        }

        private void node_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            int turnOrigin = clickedBtn.TabIndex;
            Point oldPrevLocation = new Point(lasso[turnOrigin].Location.X, lasso[turnOrigin].Location.Y);

            int orderSign;
            if (inverseOrder)
            {
                orderSign = -1;
            }
            else
            {
                orderSign = 1;
            }
            int colorSign = isBlue(clickedBtn.TabIndex) ? -1 : 1;
            Point curLocation;

            int i = turnOrigin;
            //btn should base on the old pos of prev btn,
            //but calc new location from new pos of prev btn

            /*
             * oldPrevLocation - old pos of prev btn   
             * prevLocation - pos of prev btn   
             * curLocation - location of this btn
             */
            while (!isEnd(i))
            {
                curLocation = new Point(lasso[i + orderSign].Location.X, lasso[i + orderSign].Location.Y);
                Point prevLocation = new Point(lasso[i].Location.X, lasso[i].Location.Y);
                if (isBlue(clickedBtn.TabIndex)) {//if blue
                    if (curLocation.X > oldPrevLocation.X & curLocation.Y < oldPrevLocation.Y) {
                        lasso[i + orderSign].Location = new Point(prevLocation.X - BTN_SHIFT, prevLocation.Y - BTN_SHIFT);
                    }
                    else if (curLocation.X > oldPrevLocation.X & curLocation.Y > oldPrevLocation.Y)
                    {
                        lasso[i + orderSign].Location = new Point(prevLocation.X + BTN_SHIFT, prevLocation.Y - BTN_SHIFT);
                    }
                    else if (curLocation.X < oldPrevLocation.X & curLocation.Y > oldPrevLocation.Y)
                    {
                        lasso[i + orderSign].Location = new Point(prevLocation.X + BTN_SHIFT, prevLocation.Y + BTN_SHIFT);
                    }
                    else if (curLocation.X < oldPrevLocation.X & curLocation.Y < oldPrevLocation.Y)
                    {
                        lasso[i + orderSign].Location = new Point(prevLocation.X - BTN_SHIFT, prevLocation.Y + BTN_SHIFT);
                    }
                } else{//if white
                    if (curLocation.X > oldPrevLocation.X & curLocation.Y < oldPrevLocation.Y)
                    {
                        lasso[i + orderSign].Location = new Point(prevLocation.X + BTN_SHIFT, prevLocation.Y + BTN_SHIFT);
                    }
                    else if (curLocation.X > oldPrevLocation.X & curLocation.Y > oldPrevLocation.Y)
                    {
                        lasso[i + orderSign].Location = new Point(prevLocation.X - BTN_SHIFT, prevLocation.Y + BTN_SHIFT);
                    }
                    else if (curLocation.X < oldPrevLocation.X & curLocation.Y > oldPrevLocation.Y)
                    {
                        lasso[i + orderSign].Location = new Point(prevLocation.X - BTN_SHIFT, prevLocation.Y - BTN_SHIFT);
                    }
                    else if (curLocation.X < oldPrevLocation.X & curLocation.Y < oldPrevLocation.Y)
                    {
                        lasso[i + orderSign].Location = new Point(prevLocation.X + BTN_SHIFT, prevLocation.Y - BTN_SHIFT);
                    }
                }
                oldPrevLocation = curLocation;

                i = inverseOrder ? i - 1 : i + 1;
            }
        }

        private void Form1_Click(object sender, MouseEventArgs e)
        {
            inverseOrder ^= true;
            for (int i = 0; i < LASSO_LENGTH; i++) {
                if (inverseOrder)
                {
                    lasso[i].BringToFront();
                }
                else
                {
                    lasso[i].SendToBack();
                }
            }
        }
    }
}
