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
        //private Lasso lasso;

        private Point origin = new Point(BTN_POINT_X, BTN_POINT_Y);

        private void initLasso()
        {
            Lasso lasso = new Lasso(LASSO_LENGTH, origin, node_Click);
            this.SuspendLayout();
            this.Controls.Add(lasso);
            this.ResumeLayout(false);
        }

        //public Lasso Lasso { get => lasso; set => lasso = value; } 

        public Form1()
        {
            InitializeComponent();
            initLasso();
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
