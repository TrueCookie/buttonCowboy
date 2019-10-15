using System;
using System.Drawing;
using System.Windows.Forms;

namespace buttonCowboy
{
    partial class Form1
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
            
            for (int i = 0; i < LASSO_LENGTH; ++i) {
                this.lasso[i] = new Button();
                this.lasso[i].Location = new System.Drawing.Point(BTN_POINT_X - BTN_SHIFT * i, BTN_POINT_Y - BTN_SHIFT * i);
                this.lasso[i].Name = "btn"+i;
                this.lasso[i].Size = new System.Drawing.Size(BTN_SIZE, BTN_SIZE);
                this.lasso[i].TabIndex = i;
                this.lasso[i].Text = "btn" + i;
                this.lasso[i].UseVisualStyleBackColor = true;
                Controls.Add(lasso[i]);
            }

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1079, 614);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

    }


}

