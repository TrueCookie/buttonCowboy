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

        private Button[] lasso;

        private void initLasso()
        {
            lasso = new Button[LASSO_LENGTH];

            this.SuspendLayout();

            for (int i = 0; i < LASSO_LENGTH; ++i)
            {
                this.lasso[i] = new Button();
                this.lasso[i].Location = new System.Drawing.Point(BTN_POINT_X - BTN_SHIFT * i, BTN_POINT_Y - BTN_SHIFT * i);
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
        //private const int LASSO_LENGTH = 12;
        //private const int BTN_POINT_X = 720;
        //private const int BTN_POINT_Y = 420;
        ////private Lasso lasso;

        private Point origin = new Point(BTN_POINT_X, BTN_POINT_Y);

        //private void initLasso()
        //{
        //    Lasso lasso = new Lasso(LASSO_LENGTH, origin, node_Click);
        //    this.SuspendLayout();
        //    this.Controls.Add(lasso);
        //    this.ResumeLayout(false);
        //}

        //public Lasso Lasso { get => lasso; set => lasso = value; } 

        public Form1()
        {
            InitializeComponent();  
            initLasso();    //should i do write this method just in this class, reallly?
        }

        private void node_Click(object sender, EventArgs e)
        {
            Button clicked_button = (Button)sender;
            //lasso.spin();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
