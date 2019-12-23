using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace buttonCowboy
{
    class Lasso : Control
    {
        private const int BTN_SIZE = 70;
        private const int BTN_SHIFT = 35;

        private Button[] buttons;
        private Point origin;
        private int length;
        private Cattle targetCattle;
        private int[] blueIndex = new int[] { 1, 3, 5, 6, 7, 10 };
        private bool inverseOrder = false;

        public Lasso(Point origin, int length, ref Cattle targetCattles)
        {
            this.origin = origin;
            this.length = length;
            this.targetCattle = targetCattles;

            buttons = new Button[length];

            SuspendLayout();
            for (int i = 0; i < length; ++i)
            {
                buttons[i] = new Button();
                buttons[i].Location = new Point(origin.X - BTN_SHIFT * i, origin.Y - BTN_SHIFT * i);
                buttons[i].Name = "btn" + i;
                buttons[i].Size = new Size(BTN_SIZE, BTN_SIZE);
                buttons[i].TabIndex = i;
                buttons[i].UseVisualStyleBackColor = true;
                buttons[i].Click += new EventHandler(lasso_Click);
                if (isBlue(buttons[i].TabIndex))
                {
                    buttons[i].BackColor = Color.Blue;
                }
                buttons[i].BringToFront();
                Controls.Add(buttons[i]);
            }
            ResumeLayout(false);
        }

        public bool isBlue(int btnIndex)
        {
            for (int i = 0; i < blueIndex.Length; i++)
            {
                if (blueIndex[i] == btnIndex)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isEnd(int index)
        {
            if (inverseOrder)
            {
                return index == 0;
            }
            else
            {
                return index == length - 1;
            }
        }

        public void lasso_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            int turnOrigin = clickedBtn.TabIndex;
            Point oldPrevLocation = new Point(buttons[turnOrigin].Location.X, buttons[turnOrigin].Location.Y);

            int orderSign;
            if (inverseOrder)
            {
                orderSign = -1;
            }
            else
            {
                orderSign = 1;
            }

            Point curLocation;
            int i = turnOrigin;
            while (!isEnd(i))
            {
                curLocation = new Point(buttons[i + orderSign].Location.X, buttons[i + orderSign].Location.Y);
                Point prevLocation = new Point(buttons[i].Location.X, buttons[i].Location.Y);

                if (isBlue(clickedBtn.TabIndex))
                {//if blue
                    if (curLocation.X > oldPrevLocation.X & curLocation.Y < oldPrevLocation.Y)
                    {
                        buttons[i + orderSign].Location = new Point(prevLocation.X - BTN_SHIFT, prevLocation.Y - BTN_SHIFT);
                    }
                    else if (curLocation.X > oldPrevLocation.X & curLocation.Y > oldPrevLocation.Y)
                    {
                        buttons[i + orderSign].Location = new Point(prevLocation.X + BTN_SHIFT, prevLocation.Y - BTN_SHIFT);
                    }
                    else if (curLocation.X < oldPrevLocation.X & curLocation.Y > oldPrevLocation.Y)
                    {
                        buttons[i + orderSign].Location = new Point(prevLocation.X + BTN_SHIFT, prevLocation.Y + BTN_SHIFT);
                    }
                    else if (curLocation.X < oldPrevLocation.X & curLocation.Y < oldPrevLocation.Y)
                    {
                        buttons[i + orderSign].Location = new Point(prevLocation.X - BTN_SHIFT, prevLocation.Y + BTN_SHIFT);
                    }
                }
                else
                {//if white
                    if (curLocation.X > oldPrevLocation.X & curLocation.Y < oldPrevLocation.Y)
                    {
                        buttons[i + orderSign].Location = new Point(prevLocation.X + BTN_SHIFT, prevLocation.Y + BTN_SHIFT);
                    }
                    else if (curLocation.X > oldPrevLocation.X & curLocation.Y > oldPrevLocation.Y)
                    {
                        buttons[i + orderSign].Location = new Point(prevLocation.X - BTN_SHIFT, prevLocation.Y + BTN_SHIFT);
                    }
                    else if (curLocation.X < oldPrevLocation.X & curLocation.Y > oldPrevLocation.Y)
                    {
                        buttons[i + orderSign].Location = new Point(prevLocation.X - BTN_SHIFT, prevLocation.Y - BTN_SHIFT);
                    }
                    else if (curLocation.X < oldPrevLocation.X & curLocation.Y < oldPrevLocation.Y)
                    {
                        buttons[i + orderSign].Location = new Point(prevLocation.X + BTN_SHIFT, prevLocation.Y - BTN_SHIFT);
                    }
                }
                oldPrevLocation = curLocation;

                i = inverseOrder ? i - 1 : i + 1;
            }

            if (targetCattle.cattlesAreCovered())
            {
                gameoverMsg();
                subtractHandlers();
            }
            ++points;
            updateGroupBox();
        }
    }
}
