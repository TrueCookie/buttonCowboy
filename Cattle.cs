using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace buttonCowboy
{
    class Cattle
    {
        private Button[] buttons;

        private void Cattle(Point origin, int number)
        {
            buttons = new Button[number];
            Point prevCattleLocation = origin;

            for (int i = 0; i < number; ++i)
            {
                buttons[i] = new Button();
                buttons[i].Name = "cattle" + i;
                buttons[i].Size = new Size(BTN_SIZE, BTN_SIZE);
                buttons[i].TabIndex = 50 + i;
                buttons[i].UseVisualStyleBackColor = true;
                buttons[i].BackColor = Color.SaddleBrown;
                buttons[i].SendToBack();
                //init cattle location based on random lasso actions            
                if (i == 0)
                {
                    buttons[i].Location = new Point(prevCattleLocation.X, prevCattleLocation.Y);
                }
                else if (i == 1)
                {
                    buttons[i].Location = new Point(prevCattleLocation.X + randSign() * BTN_SHIFT,
                                                        prevCattleLocation.Y + randSign() * BTN_SHIFT);
                }
                else
                {
                    int j = 0;
                    while (j < i - 1)
                    {
                        Point newCattleLocation = new Point(prevCattleLocation.X + randSign() * BTN_SHIFT,
                                                        prevCattleLocation.Y + randSign() * BTN_SHIFT);
                        if (newCattleLocation == buttons[j].Location)
                        {
                            newCattleLocation = new Point(prevCattleLocation.X + randSign() * BTN_SHIFT,
                                                        prevCattleLocation.Y + randSign() * BTN_SHIFT);
                        }
                        else
                        {
                            buttons[i].Location = newCattleLocation;
                            j++;
                        }
                    }
                }
                prevCattleLocation = buttons[i].Location;
                Controls.Add(buttons[i]);
            }

            ResumeLayout(false);
        }

        public bool cattlesAreCovered()
        {
            bool cattleIsCovered;
            for (int i = 0; i < CATTLE_NUMBER; i++)
            {
                cattleIsCovered = false;
                for (int j = 0; j < LASSO_LENGTH; j++)
                {
                    if (buttons[i].Location == lasso[j].Location)
                    {
                        cattleIsCovered = true;
                        break;
                    }
                }
                if (cattleIsCovered == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
