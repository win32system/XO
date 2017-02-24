namespace GameClient
{
    partial class RoomDialog
    {
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
            this.btn_XO1 = new System.Windows.Forms.Button();
            this.btn_XO2 = new System.Windows.Forms.Button();
            this.btn_XO3 = new System.Windows.Forms.Button();
            this.btn_XO4 = new System.Windows.Forms.Button();
            this.btn_XO5 = new System.Windows.Forms.Button();
            this.btn_XO6 = new System.Windows.Forms.Button();
            this.btn_XO7 = new System.Windows.Forms.Button();
            this.btn_XO8 = new System.Windows.Forms.Button();
            this.btn_XO9 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_XO1
            // 
            this.btn_XO1.Location = new System.Drawing.Point(94, 54);
            this.btn_XO1.Name = "btn_XO1";
            this.btn_XO1.Size = new System.Drawing.Size(94, 91);
            this.btn_XO1.TabIndex = 0;
            this.btn_XO1.Tag = "1";
            this.btn_XO1.UseVisualStyleBackColor = true;
            this.btn_XO1.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // btn_XO2
            // 
            this.btn_XO2.Location = new System.Drawing.Point(194, 54);
            this.btn_XO2.Name = "btn_XO2";
            this.btn_XO2.Size = new System.Drawing.Size(94, 91);
            this.btn_XO2.TabIndex = 1;
            this.btn_XO2.Tag = "2";
            this.btn_XO2.UseVisualStyleBackColor = true;
            this.btn_XO2.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // btn_XO3
            // 
            this.btn_XO3.Location = new System.Drawing.Point(294, 54);
            this.btn_XO3.Name = "btn_XO3";
            this.btn_XO3.Size = new System.Drawing.Size(94, 91);
            this.btn_XO3.TabIndex = 2;
            this.btn_XO3.Tag = "3";
            this.btn_XO3.UseVisualStyleBackColor = true;
            this.btn_XO3.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // btn_XO4
            // 
            this.btn_XO4.Location = new System.Drawing.Point(94, 151);
            this.btn_XO4.Name = "btn_XO4";
            this.btn_XO4.Size = new System.Drawing.Size(94, 91);
            this.btn_XO4.TabIndex = 3;
            this.btn_XO4.Tag = "4";
            this.btn_XO4.UseVisualStyleBackColor = true;
            this.btn_XO4.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // btn_XO5
            // 
            this.btn_XO5.Location = new System.Drawing.Point(194, 151);
            this.btn_XO5.Name = "btn_XO5";
            this.btn_XO5.Size = new System.Drawing.Size(94, 91);
            this.btn_XO5.TabIndex = 4;
            this.btn_XO5.Tag = "5";
            this.btn_XO5.UseVisualStyleBackColor = true;
            this.btn_XO5.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // btn_XO6
            // 
            this.btn_XO6.Location = new System.Drawing.Point(294, 151);
            this.btn_XO6.Name = "btn_XO6";
            this.btn_XO6.Size = new System.Drawing.Size(94, 91);
            this.btn_XO6.TabIndex = 5;
            this.btn_XO6.Tag = "6";
            this.btn_XO6.UseVisualStyleBackColor = true;
            this.btn_XO6.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // btn_XO7
            // 
            this.btn_XO7.Location = new System.Drawing.Point(94, 248);
            this.btn_XO7.Name = "btn_XO7";
            this.btn_XO7.Size = new System.Drawing.Size(94, 91);
            this.btn_XO7.TabIndex = 6;
            this.btn_XO7.Tag = "7";
            this.btn_XO7.UseVisualStyleBackColor = true;
            this.btn_XO7.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // btn_XO8
            // 
            this.btn_XO8.Location = new System.Drawing.Point(194, 248);
            this.btn_XO8.Name = "btn_XO8";
            this.btn_XO8.Size = new System.Drawing.Size(94, 91);
            this.btn_XO8.TabIndex = 7;
            this.btn_XO8.Tag = "8";
            this.btn_XO8.UseVisualStyleBackColor = true;
            this.btn_XO8.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // btn_XO9
            // 
            this.btn_XO9.Location = new System.Drawing.Point(294, 248);
            this.btn_XO9.Name = "btn_XO9";
            this.btn_XO9.Size = new System.Drawing.Size(94, 91);
            this.btn_XO9.TabIndex = 8;
            this.btn_XO9.Tag = "9";
            this.btn_XO9.UseVisualStyleBackColor = true;
            this.btn_XO9.Click += new System.EventHandler(this.SendMoveXO);
            // 
            // RoomDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 440);
            this.Controls.Add(this.btn_XO9);
            this.Controls.Add(this.btn_XO8);
            this.Controls.Add(this.btn_XO7);
            this.Controls.Add(this.btn_XO6);
            this.Controls.Add(this.btn_XO5);
            this.Controls.Add(this.btn_XO4);
            this.Controls.Add(this.btn_XO3);
            this.Controls.Add(this.btn_XO2);
            this.Controls.Add(this.btn_XO1);
            this.Name = "RoomDialog";
            this.Text = "RoomDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_XO1;
        private System.Windows.Forms.Button btn_XO2;
        private System.Windows.Forms.Button btn_XO3;
        private System.Windows.Forms.Button btn_XO4;
        private System.Windows.Forms.Button btn_XO5;
        private System.Windows.Forms.Button btn_XO6;
        private System.Windows.Forms.Button btn_XO7;
        private System.Windows.Forms.Button btn_XO8;
        private System.Windows.Forms.Button btn_XO9;
    }
}