namespace LopushokApp
{
    partial class AuthorizationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizationForm));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            textBoxLogin = new TextBox();
            textBoxPassword = new TextBox();
            buttonLogin = new Button();
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
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(250, 147);
            label1.Name = "label1";
            label1.Size = new Size(57, 37);
            label1.TabIndex = 3;
            label1.Text = "Логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(250, 204);
            label2.Name = "label2";
            label2.Size = new Size(64, 37);
            label2.TabIndex = 4;
            label2.Text = "Пароль";
            // 
            // textBoxLogin
            // 
            textBoxLogin.Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxLogin.Location = new Point(352, 147);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.Size = new Size(195, 41);
            textBoxLogin.TabIndex = 5;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPassword.Location = new Point(352, 204);
            textBoxPassword.MaximumSize = new Size(195, 41);
            textBoxPassword.MinimumSize = new Size(195, 41);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Size = new Size(195, 41);
            textBoxPassword.TabIndex = 6;
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.Cyan;
            buttonLogin.Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLogin.Location = new Point(250, 289);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(297, 50);
            buttonLogin.TabIndex = 7;
            buttonLogin.Text = "ВОЙТИ";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // AuthorizationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonLogin);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AuthorizationForm";
            Text = "Авторизация";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private TextBox textBoxLogin;
        private TextBox textBoxPassword;
        private Button buttonLogin;
    }
}