namespace LopushokApp
{
    partial class ChangeCostForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeCostForm));
            pictureBox1 = new PictureBox();
            textBoxNumber = new TextBox();
            buttonChangeCost = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 15);
            pictureBox1.Margin = new Padding(3, 6, 3, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(131, 74);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBoxNumber.Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxNumber.Location = new Point(84, 118);
            textBoxNumber.Name = "textBoxNumber";
            textBoxNumber.Size = new Size(213, 41);
            textBoxNumber.TabIndex = 2;
            // 
            // button1
            // 
            buttonChangeCost.BackColor = Color.Cyan;
            buttonChangeCost.Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonChangeCost.Location = new Point(84, 181);
            buttonChangeCost.Name = "buttonChangeCost";
            buttonChangeCost.Size = new Size(213, 40);
            buttonChangeCost.TabIndex = 3;
            buttonChangeCost.Text = "Изменить стоимость";
            buttonChangeCost.UseVisualStyleBackColor = false;
            // 
            // ChangeCost
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(406, 253);
            Controls.Add(buttonChangeCost);
            Controls.Add(textBoxNumber);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ChangeCost";
            Text = "Изменить стоимость на ...";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox textBoxNumber;
        private Button buttonChangeCost;
    }
}