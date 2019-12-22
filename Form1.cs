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
{   //Form1_Click don't work every 2nd game
    //points has'nt been reset 
    public partial class Form1 : Form
    {
        private const int LASSO_LENGTH = 6;
        private const int CATTLE_NUMBER = LASSO_LENGTH/2;
        private const int BTN_POINT_X = 700;
        private const int BTN_POINT_Y = 420;
        private const int BTN_SIZE = 70;
        private const int BTN_SHIFT = 35;

        private Point LASSO_ORIGIN = new Point(BTN_POINT_X, BTN_POINT_Y);
        private Point CATTLE_ORIGIN = new Point(420, 280);
        private Button[] lasso, cattle;
        private bool inverseOrder = false;
        private static int points = 0;
        //graphics
        private Point POINTS_LOCATION = new Point(20, 20);
        GroupBox pointsGroupBox;
        TextBox pointsTextBox;

        private Point NEW_GAME_BTN_LOCATION = new Point(20, 60);
        private Size NEW_GAME_BTN_SIZE = new Size(130, 60);

        int[] blueIndex = new int[] { 1, 3, 5, 6, 7, 10};

        public Form1()
        {
            InitializeComponent();  
            InitLasso();
            InitCattle();
            AddGroupBox();
            InitNewGameBtn();
        }

        private void InitLasso()
        {
            lasso = new Button[LASSO_LENGTH];

            this.SuspendLayout();
            for (int i = 0; i < LASSO_LENGTH; ++i)
            {
                this.lasso[i] = new Button();
                this.lasso[i].Location = new Point(LASSO_ORIGIN.X - BTN_SHIFT * i, LASSO_ORIGIN.Y - BTN_SHIFT * i);
                this.lasso[i].Name = "btn" + i;
                this.lasso[i].Size = new Size(BTN_SIZE, BTN_SIZE);
                this.lasso[i].TabIndex = i;
                this.lasso[i].Text = "btn" + i;
                this.lasso[i].UseVisualStyleBackColor = true;
                this.lasso[i].Click += new EventHandler(this.lasso_Click);
                if(isBlue(lasso[i].TabIndex)){ lasso[i].BackColor = Color.Blue; }
                lasso[i].BringToFront();
                Controls.Add(lasso[i]);
            }
            this.ResumeLayout(false);
        }

        private void InitNewGameBtn()
        {
            Button newGameBtn = new Button();
            SuspendLayout();
            newGameBtn.Location = NEW_GAME_BTN_LOCATION;
            newGameBtn.Name = "newGameBtn";
            newGameBtn.Size = NEW_GAME_BTN_SIZE;
            newGameBtn.Text = "New Game!";
            newGameBtn.UseVisualStyleBackColor = true;
            newGameBtn.Click += new EventHandler(NewGameBtn_Click);
            Controls.Add(newGameBtn);
            ResumeLayout(false);
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

        private void lasso_Click(object sender, EventArgs e)
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
            //int colorSign = isBlue(clickedBtn.TabIndex) ? -1 : 1;
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

            if (cattlesAreCovered())
            {
                gameoverMsg();
                subtractHandlers();
            }
            ++points;
            updateGroupBox(); 
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
            for (int i = 0; i < LASSO_LENGTH/2; i++)
            {
                cattle[i].SendToBack();
            }
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LASSO_LENGTH; i++)
            {
                Controls.Remove(lasso[i]);
                lasso[i].Dispose();
            }
            for (int i = 0; i < CATTLE_NUMBER; i++)
            {
                Controls.Remove(cattle[i]);
                cattle[i].Dispose();
            }
            inverseOrder = false;
            InitLasso();
            InitCattle();
            points = 0;
            updateGroupBox();
        }

        private void InitCattle()
        {
            cattle = new Button[LASSO_LENGTH/2];
            Point prevCattleLocation = CATTLE_ORIGIN;
            this.SuspendLayout();

            for (int i = 0; i < LASSO_LENGTH/2; ++i)
            {
                this.cattle[i] = new Button();
                this.cattle[i].Name = "cattle" + i;
                this.cattle[i].Size = new Size(BTN_SIZE, BTN_SIZE);
                this.cattle[i].TabIndex = 50+i;
                this.cattle[i].Text = "cattle" + i;
                this.cattle[i].UseVisualStyleBackColor = true;
                this.cattle[i].BackColor = Color.SaddleBrown;

                //init cattle location based on random lasso actions            
                if (i == 0)
                {
                    cattle[i].Location = new Point(prevCattleLocation.X, prevCattleLocation.Y);
                }
                else if(i == 1)
                {
                    cattle[i].Location = new Point(prevCattleLocation.X + randSign() * BTN_SHIFT * 2,
                                                        prevCattleLocation.Y + randSign() * BTN_SHIFT * 2);
                }
                else {
                    int j = 0;
                    while (j < i-1)
                    {
                        Point newCattleLocation = new Point(prevCattleLocation.X + randSign() * BTN_SHIFT + randSign() * BTN_SHIFT,
                                                        prevCattleLocation.Y + randSign() * BTN_SHIFT + randSign() * BTN_SHIFT);
                        if (newCattleLocation == cattle[j].Location)
                        {
                            newCattleLocation = new Point(prevCattleLocation.X + randSign() * BTN_SHIFT + randSign() * BTN_SHIFT,
                                                        prevCattleLocation.Y + randSign() * BTN_SHIFT + randSign() * BTN_SHIFT);
                        }
                        else
                        {
                            cattle[i].Location = newCattleLocation;
                            j++;
                        }
                    }
                }
                prevCattleLocation = cattle[i].Location;
                Controls.Add(cattle[i]);
            }

            this.ResumeLayout(false);
        }

        public int randSign()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int result = rnd.Next(0, 2);
            if (result == 0)
            {
                return -1;
            }
            else {
                return 1;
            }
        }

        public bool cattlesAreCovered()
        {
            bool cattleIsCovered;
            for (int i = 0; i < CATTLE_NUMBER; i++)
            {
                cattleIsCovered = false;
                for (int j = 0; j < LASSO_LENGTH; j++)
                {
                    if (cattle[i].Location == lasso[j].Location)
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

        public void subtractHandlers()
        {
            for (int i = 0; i < LASSO_LENGTH; i++)
            {
                lasso[i].Click -= new EventHandler(lasso_Click);
            }
        }

        public void gameoverMsg()
        {
            Graphics formGraphics = this.CreateGraphics();
            string GAME_END_TEXT = "COMPLETE!!!";
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            int GEtextX = Screen.PrimaryScreen.Bounds.Width / 2;
            int GEtextY = Screen.PrimaryScreen.Bounds.Height / 2;
            formGraphics.DrawString(GAME_END_TEXT, new Font("Arial", 16), drawBrush, GEtextX, GEtextY, new StringFormat());
            drawBrush.Dispose();
            formGraphics.Dispose();
        }

        public void updateGroupBox()
        {
            pointsTextBox.ResetText();
            pointsTextBox.Text = "POINTS: " + points;
        }

        private void AddGroupBox()
        {
            pointsGroupBox = new GroupBox();
            pointsTextBox = new TextBox();
            pointsTextBox.Location = POINTS_LOCATION;
            Controls.Add(pointsTextBox);
            pointsTextBox.Text = "POINTS: " + points;
        }
        

    }
}
