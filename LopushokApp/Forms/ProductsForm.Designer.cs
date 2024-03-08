namespace LopushokApp
{
    partial class ProductsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductsForm));
            pictureBox1 = new PictureBox();
            textBoxSearch = new TextBox();
            comboBoxSort = new ComboBox();
            comboBoxFilter = new ComboBox();
            mainPanel = new Panel();
            panel0 = new Panel();
            labelCost = new Label();
            labelMaterials = new Label();
            labelProductTypeName = new Label();
            pictureBoxProduct = new PictureBox();
            labelVendorCode = new Label();
            buttonChangeCost = new Button();
            buttonAddProduct = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            labelBack = new Label();
            labelForward = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProduct).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(42, 15);
            pictureBox1.Margin = new Padding(3, 6, 3, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(131, 74);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(44, 125);
            textBoxSearch.Margin = new Padding(3, 6, 3, 6);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(377, 41);
            textBoxSearch.TabIndex = 1;
            textBoxSearch.Text = "Введите для поиска";
            textBoxSearch.Click += TextBoxSearch_Click;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // comboBoxSort
            // 
            comboBoxSort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxSort.FormattingEnabled = true;
            comboBoxSort.Items.AddRange(new object[] { "Не выбрано", "Наименование (по возрастанию)", "Наименование (по убыванию)", "Номер цеха (по возрастанию)", "Номер цеха (по убыванию)", "Минимальная стоимость (по возрастанию)", "Минимальная стоимость (по убыванию)" });
            comboBoxSort.Location = new Point(544, 125);
            comboBoxSort.Margin = new Padding(3, 6, 3, 6);
            comboBoxSort.Name = "comboBoxSort";
            comboBoxSort.Size = new Size(209, 45);
            comboBoxSort.TabIndex = 2;
            comboBoxSort.Text = "Сортировка";
            comboBoxSort.SelectedIndexChanged += comboBoxSort_SelectedIndexChanged;
            // 
            // comboBoxFilter
            // 
            comboBoxFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxFilter.FormattingEnabled = true;
            comboBoxFilter.Items.AddRange(new object[] { "Все типы" });
            comboBoxFilter.Location = new Point(780, 125);
            comboBoxFilter.Margin = new Padding(3, 6, 3, 6);
            comboBoxFilter.Name = "comboBoxFilter";
            comboBoxFilter.Size = new Size(209, 45);
            comboBoxFilter.TabIndex = 3;
            comboBoxFilter.Text = "Фильтрация";
            comboBoxFilter.SelectedIndexChanged += comboBoxFilter_SelectedIndexChanged;
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.AutoScroll = true;
            mainPanel.Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            mainPanel.Location = new Point(42, 211);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(993, 527);
            mainPanel.TabIndex = 13;
            // 
            // panel0
            // 
            panel0.BorderStyle = BorderStyle.FixedSingle;
            panel0.Controls.Add(labelCost);
            panel0.Controls.Add(labelMaterials);
            panel0.Controls.Add(labelProductTypeName);
            panel0.Controls.Add(pictureBoxProduct);
            panel0.Controls.Add(labelVendorCode);
            panel0.Location = new Point(41, 211);
            panel0.Name = "panel0";
            panel0.Size = new Size(948, 168);
            panel0.TabIndex = 14;
            panel0.Visible = false;
            // 
            // labelCost
            // 
            labelCost.AutoSize = true;
            labelCost.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            labelCost.Location = new Point(824, 22);
            labelCost.Name = "labelCost";
            labelCost.Size = new Size(107, 42);
            labelCost.TabIndex = 4;
            labelCost.Text = "Стоимость";
            // 
            // labelMaterials
            // 
            labelMaterials.Location = new Point(153, 90);
            labelMaterials.Name = "labelMaterials";
            labelMaterials.Size = new Size(778, 66);
            labelMaterials.TabIndex = 3;
            labelMaterials.Text = "Материалы";
            // 
            // labelProductTypeName
            // 
            labelProductTypeName.AutoSize = true;
            labelProductTypeName.Font = new Font("Gabriola", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelProductTypeName.Location = new Point(153, 13);
            labelProductTypeName.Name = "labelProductTypeName";
            labelProductTypeName.Size = new Size(378, 51);
            labelProductTypeName.TabIndex = 1;
            labelProductTypeName.Text = "Тип продукта Наименование продукта";
            // 
            // pictureBoxProduct
            // 
            pictureBoxProduct.Location = new Point(23, 33);
            pictureBoxProduct.Name = "pictureBoxProduct";
            pictureBoxProduct.Size = new Size(110, 94);
            pictureBoxProduct.TabIndex = 0;
            pictureBoxProduct.TabStop = false;
            // 
            // labelVendorCode
            // 
            labelVendorCode.AutoSize = true;
            labelVendorCode.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            labelVendorCode.Location = new Point(153, 51);
            labelVendorCode.Name = "labelVendorCode";
            labelVendorCode.Size = new Size(86, 42);
            labelVendorCode.TabIndex = 2;
            labelVendorCode.Text = "Артикул";
            // 
            // buttonChangeCost
            // 
            buttonChangeCost.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonChangeCost.BackColor = Color.Cyan;
            buttonChangeCost.Location = new Point(41, 779);
            buttonChangeCost.Name = "buttonChangeCost";
            buttonChangeCost.Size = new Size(239, 46);
            buttonChangeCost.TabIndex = 15;
            buttonChangeCost.Text = "Изменить стоимость на ...";
            buttonChangeCost.UseVisualStyleBackColor = false;
            buttonChangeCost.Visible = false;
            buttonChangeCost.Click += buttonChangeCost_Click;
            // 
            // buttonAddProduct
            // 
            buttonAddProduct.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonAddProduct.BackColor = Color.Cyan;
            buttonAddProduct.Location = new Point(41, 850);
            buttonAddProduct.Name = "buttonAddProduct";
            buttonAddProduct.Size = new Size(239, 46);
            buttonAddProduct.TabIndex = 16;
            buttonAddProduct.Text = "Добавить продукцию";
            buttonAddProduct.UseVisualStyleBackColor = false;
            buttonAddProduct.Click += buttonAddProduct_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(866, 852);
            label1.Name = "label1";
            label1.Size = new Size(23, 42);
            label1.TabIndex = 17;
            label1.Text = "1";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(895, 852);
            label2.Name = "label2";
            label2.Size = new Size(26, 42);
            label2.TabIndex = 18;
            label2.Text = "2";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(927, 852);
            label3.Name = "label3";
            label3.Size = new Size(26, 42);
            label3.TabIndex = 19;
            label3.Text = "3";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(959, 852);
            label4.Name = "label4";
            label4.Size = new Size(27, 42);
            label4.TabIndex = 20;
            label4.Text = "4";
            label4.Click += label4_Click;
            // 
            // labelBack
            // 
            labelBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelBack.AutoSize = true;
            labelBack.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            labelBack.Location = new Point(843, 852);
            labelBack.Name = "labelBack";
            labelBack.Size = new Size(27, 42);
            labelBack.TabIndex = 21;
            labelBack.Text = "<";
            labelBack.Click += labelBack_Click;
            // 
            // labelForward
            // 
            labelForward.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelForward.AutoSize = true;
            labelForward.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            labelForward.Location = new Point(992, 852);
            labelForward.Name = "labelForward";
            labelForward.Size = new Size(27, 42);
            labelForward.TabIndex = 22;
            labelForward.Text = ">";
            labelForward.Click += labelForward_Click;
            // 
            // ProductsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1047, 943);
            Controls.Add(labelForward);
            Controls.Add(labelBack);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonAddProduct);
            Controls.Add(buttonChangeCost);
            Controls.Add(panel0);
            Controls.Add(mainPanel);
            Controls.Add(comboBoxFilter);
            Controls.Add(comboBoxSort);
            Controls.Add(textBoxSearch);
            Controls.Add(pictureBox1);
            Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 6, 3, 6);
            Name = "ProductsForm";
            Text = "Продукты";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel0.ResumeLayout(false);
            panel0.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProduct).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private PictureBox pictureBox1;
        private TextBox textBoxSearch;
        private ComboBox comboBoxSort;
        private ComboBox comboBoxFilter;
        private Panel mainPanel;
        private Panel panel0;
        private Label labelCost;
        private Label labelMaterials;
        private Label labelProductTypeName;
        private PictureBox pictureBoxProduct;
        private Label labelVendorCode;
        private Button buttonChangeCost;
        private Button buttonAddProduct;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label labelBack;
        private Label labelForward;
    }
}