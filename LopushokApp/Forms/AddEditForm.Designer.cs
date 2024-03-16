namespace LopushokApp
{
    partial class AddEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditForm));
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            pictureBoxImage = new PictureBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            comboBoxType = new ComboBox();
            textBoxArticle = new TextBox();
            textBoxPersonCount = new TextBox();
            textBoxTitle = new TextBox();
            textBoxWorkshopNumber = new TextBox();
            textBoxMinCost = new TextBox();
            textBoxDescription = new TextBox();
            label8 = new Label();
            buttonDelete = new Button();
            buttonSave = new Button();
            buttonBack = new Button();
            dataGridViewMaterials = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Material = new DataGridViewComboBoxColumn();
            Count = new DataGridViewTextBoxColumn();
            buttonDeleteMaterial = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMaterials).BeginInit();
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
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(41, 231);
            label1.Name = "label1";
            label1.Size = new Size(86, 42);
            label1.TabIndex = 2;
            label1.Text = "Артикул";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(41, 296);
            label2.Name = "label2";
            label2.Size = new Size(128, 42);
            label2.TabIndex = 3;
            label2.Text = "Наименование";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(41, 364);
            label3.Name = "label3";
            label3.Size = new Size(120, 42);
            label3.TabIndex = 4;
            label3.Text = "Тип продукта";
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Image = (Image)resources.GetObject("pictureBoxImage.Image");
            pictureBoxImage.Location = new Point(986, 596);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(196, 152);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.TabIndex = 5;
            pictureBoxImage.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(41, 426);
            label4.Name = "label4";
            label4.Size = new Size(302, 42);
            label4.TabIndex = 6;
            label4.Text = "Количество человек для производства";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(41, 495);
            label5.Name = "label5";
            label5.Size = new Size(254, 42);
            label5.TabIndex = 7;
            label5.Text = "Номер производственного цеха";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(41, 577);
            label6.Name = "label6";
            label6.Size = new Size(298, 42);
            label6.TabIndex = 8;
            label6.Text = "Минимальная стоимость для агента";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Gabriola", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(41, 648);
            label7.Name = "label7";
            label7.Size = new Size(170, 42);
            label7.TabIndex = 9;
            label7.Text = "Подробное описание";
            // 
            // comboBoxType
            // 
            comboBoxType.FormattingEnabled = true;
            comboBoxType.Location = new Point(359, 361);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(346, 45);
            comboBoxType.TabIndex = 2;
            // 
            // textBoxArticle
            // 
            textBoxArticle.BackColor = SystemColors.Window;
            textBoxArticle.Location = new Point(359, 231);
            textBoxArticle.Name = "textBoxArticle";
            textBoxArticle.Size = new Size(346, 41);
            textBoxArticle.TabIndex = 0;
            // 
            // textBoxPersonCount
            // 
            textBoxPersonCount.Location = new Point(359, 426);
            textBoxPersonCount.Name = "textBoxPersonCount";
            textBoxPersonCount.Size = new Size(346, 41);
            textBoxPersonCount.TabIndex = 3;
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(359, 296);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(346, 41);
            textBoxTitle.TabIndex = 1;
            // 
            // textBoxWorkshopNumber
            // 
            textBoxWorkshopNumber.Location = new Point(359, 495);
            textBoxWorkshopNumber.Name = "textBoxWorkshopNumber";
            textBoxWorkshopNumber.Size = new Size(346, 41);
            textBoxWorkshopNumber.TabIndex = 4;
            // 
            // textBoxMinCost
            // 
            textBoxMinCost.Location = new Point(359, 577);
            textBoxMinCost.Name = "textBoxMinCost";
            textBoxMinCost.Size = new Size(346, 41);
            textBoxMinCost.TabIndex = 5;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Location = new Point(359, 648);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(346, 132);
            textBoxDescription.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Gabriola", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(756, 227);
            label8.Name = "label8";
            label8.Size = new Size(132, 51);
            label8.TabIndex = 10;
            label8.Text = "Материалы";
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.Cyan;
            buttonDelete.Location = new Point(986, 116);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(128, 46);
            buttonDelete.TabIndex = 12;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Visible = false;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.Cyan;
            buttonSave.Location = new Point(1195, 116);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(128, 46);
            buttonSave.TabIndex = 13;
            buttonSave.Text = "Добавить";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonBack
            // 
            buttonBack.BackColor = Color.Cyan;
            buttonBack.Location = new Point(41, 116);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(128, 46);
            buttonBack.TabIndex = 14;
            buttonBack.Text = "<- К продуктам";
            buttonBack.UseVisualStyleBackColor = false;
            buttonBack.Click += buttonBack_Click;
            // 
            // dataGridViewMaterials
            // 
            dataGridViewMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMaterials.BackgroundColor = Color.White;
            dataGridViewMaterials.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMaterials.Columns.AddRange(new DataGridViewColumn[] { Id, Material, Count });
            dataGridViewMaterials.Location = new Point(757, 295);
            dataGridViewMaterials.Name = "dataGridViewMaterials";
            dataGridViewMaterials.RowHeadersWidth = 51;
            dataGridViewMaterials.RowTemplate.Height = 29;
            dataGridViewMaterials.ScrollBars = ScrollBars.Vertical;
            dataGridViewMaterials.Size = new Size(595, 247);
            dataGridViewMaterials.TabIndex = 15;
            // 
            // Id
            // 
            Id.HeaderText = "";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // Material
            // 
            Material.HeaderText = "Материал";
            Material.MinimumWidth = 6;
            Material.Name = "Material";
            // 
            // Count
            // 
            Count.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Count.HeaderText = "Количество";
            Count.MinimumWidth = 6;
            Count.Name = "Count";
            Count.Width = 123;
            // 
            // buttonDeleteMaterial
            // 
            buttonDeleteMaterial.BackColor = Color.Cyan;
            buttonDeleteMaterial.Location = new Point(894, 231);
            buttonDeleteMaterial.Name = "buttonDeleteMaterial";
            buttonDeleteMaterial.Size = new Size(183, 44);
            buttonDeleteMaterial.TabIndex = 16;
            buttonDeleteMaterial.Text = "Удалить материал";
            buttonDeleteMaterial.UseVisualStyleBackColor = false;
            buttonDeleteMaterial.Click += buttonDeleteMaterial_Click;
            // 
            // AddEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(1378, 808);
            Controls.Add(buttonDeleteMaterial);
            Controls.Add(dataGridViewMaterials);
            Controls.Add(buttonBack);
            Controls.Add(buttonSave);
            Controls.Add(buttonDelete);
            Controls.Add(label8);
            Controls.Add(textBoxDescription);
            Controls.Add(textBoxMinCost);
            Controls.Add(textBoxWorkshopNumber);
            Controls.Add(textBoxTitle);
            Controls.Add(textBoxPersonCount);
            Controls.Add(textBoxArticle);
            Controls.Add(comboBoxType);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(pictureBoxImage);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Font = new Font("Gabriola", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 6, 3, 6);
            MaximumSize = new Size(1396, 855);
            MinimumSize = new Size(1396, 855);
            Name = "AddEditForm";
            Text = "Добавить продукт";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMaterials).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureBox pictureBoxImage;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox comboBoxType;
        private TextBox textBoxArticle;
        private TextBox textBoxPersonCount;
        private TextBox textBoxTitle;
        private TextBox textBoxWorkshopNumber;
        private TextBox textBoxMinCost;
        private TextBox textBoxDescription;
        private Label label8;
        private Button buttonDelete;
        private Button buttonSave;
        private Button buttonBack;
        private DataGridView dataGridViewMaterials;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewComboBoxColumn Material;
        private DataGridViewTextBoxColumn Count;
        private Button buttonDeleteMaterial;
    }
}