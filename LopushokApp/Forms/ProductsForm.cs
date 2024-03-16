using LopushokApp.Entities;
using Npgsql;
using System.Globalization;

namespace LopushokApp
{
    public partial class ProductsForm : Form
    {
        private bool isProgramEvent = false;
        private int start = 0;
        private int position = 1;
        private readonly int userGroup;
        private readonly Color color = Color.FromArgb(206, 255, 249);

        public ProductsForm(int userGroup)
        {
            InitializeComponent();
            this.userGroup = userGroup;
            if (userGroup != 1)
                buttonAddProduct.Visible = false;
            LoadProducts(GetProducts());
            label1.Font = new Font(label1.Font, FontStyle.Underline);
            comboBoxFilter.Items.AddRange(GetTypes());
            SizeChanged += ProductsForm_SizeChanged;
        }

        private void ProductsForm_SizeChanged(object? sender, EventArgs e)
        {
            LoadProducts(GetProductsWithSelection());
        }

        private void LoadProducts(List<Product> list)
        {
            mainPanel.Controls.Clear();
            buttonChangeCost.Visible = false;
            if (list.Count > 0)
            {
                var location = new Point(0, 0);
                var goodSoldProducts = GetSoldProductIdInLastMonth();
                var startIndex = start;
                var endIndex = start + 20;
                while (startIndex < endIndex && startIndex < list.Count)
                {
                    Product product = list[startIndex];
                    Panel panel = new Panel
                    {
                        Location = location,
                        Name = product.Id + "",
                        BorderStyle = BorderStyle.FixedSingle,
                        AutoSize = true,
                        MinimumSize = new Size(mainPanel.Size.Width - 30, 0),
                        MaximumSize = new Size(mainPanel.Size.Width - 30, 0),
                    };
                    if (userGroup == 1)
                    {
                        panel.MouseDoubleClick += Panel_MouseDoubleClick;
                        panel.Click += Panel_Click;
                    }

                    Label title = new Label
                    {
                        Location = new Point(153, 13),
                        Text = product.Type + " " + product.Title,
                        AutoSize = true
                    };

                    if (!goodSoldProducts.Contains(product.Id))
                        title.ForeColor = Color.PaleVioletRed;
                    else title.ForeColor = Color.Black;

                    Label article = new Label
                    {
                        Text = product.Article,
                        Location = new Point(153, 51),
                        AutoSize = true
                    };
                    var picture = new PictureBox
                    {
                        Location = new Point(23, 33),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(110, 94)
                    };
                    if (product.Image != null)
                        picture.Image = ConvertToBitmap(product.Image);
                    else picture.Image = ConvertToBitmap("\\picture.png");
                    Label cost = new Label
                    {
                        Text = (GetCostMaterialsByProductId(product.Id) != 0) ? string.Format("{0:f2}", GetCostMaterialsByProductId(product.Id)) : product.Cost + "",
                        Location = new Point(824, 22),
                        AutoSize = true
                    };
                    Label materials = new Label
                    {
                        Location = new Point(153, 90),
                        AutoSize = true,
                        Text = (GetMaterialsByProductId(product.Id) == "") ? "" : "Материалы: " + GetMaterialsByProductId(product.Id)
                    };
                    materials.MaximumSize = new Size(panel.Size.Width - materials.Location.X - 30, 0);
                    panel.Controls.AddRange(new Control[] { title, article, picture, cost, materials });
                    mainPanel.Controls.Add(panel);
                    cost.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    startIndex++;
                }
            }
            else MessageBox.Show("В базе данных нет продуктов с указанными параметрами!");
        }

        private void Panel_Click(object? sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel.BackColor == color)
                panel.BackColor = Color.White;
            else panel.BackColor = color;
            bool isHighlighted = false;
            foreach (Panel item in mainPanel.Controls)
            {
                if (item.BackColor == color)
                    isHighlighted = true;
            }
            buttonChangeCost.Visible = isHighlighted;
        }

        private void Panel_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;
            Form form = new AddEditForm(int.Parse(panel.Name), userGroup);
            form.Text = "Редактировать продукт";
            Program.ChangeForm(this, form);
        }

        public static Image ConvertToBitmap(string fileName)
        {
            Image image;
            try
            {
                using Stream bmpStream = File.Open(Environment.CurrentDirectory + fileName, FileMode.Open);
                image = Image.FromStream(bmpStream);
            }
            catch (Exception _)
            {
                using Stream bmpStream = File.Open(Environment.CurrentDirectory + "\\picture.png", FileMode.Open);
                image = Image.FromStream(bmpStream);
            }
            return image;
        }

        private string GetMaterialsByProductId(int id)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select material.title from productmaterial inner join material on productmaterial.materialid=material.id where productmaterial.productid ={id}", Program.con);
            var reader = cmd.ExecuteReader();
            var result = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add((string)reader.GetValue(0));
                }

            }
            reader.Close();
            Program.con.Close();
            return string.Join(", ", result);
        }

        private double GetCostMaterialsByProductId(int id)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select material.cost*productmaterial.count from productmaterial inner join material on productmaterial.materialid=material.id where productmaterial.productid ={id}", Program.con);
            var reader = cmd.ExecuteReader();
            double result = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = (double)reader.GetValue(0);
                }

            }
            reader.Close();
            Program.con.Close();
            return result;
        }

        private List<int> GetSoldProductIdInLastMonth()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select productid from productsale where saledate<='{DateOnly.FromDateTime(DateTime.Now.AddMonths(-1))}'", Program.con);
            var reader = cmd.ExecuteReader();
            var result = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add((int)reader.GetValue(0));
                }

            }
            reader.Close();
            Program.con.Close();
            return result;
        }

        public List<Product> GetProducts()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand("select product.*, producttype.title from product left join producttype on product.producttypeid=producttype.id", Program.con);
            var reader = cmd.ExecuteReader();
            var result = new List<Product>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var item = new Product()
                    {
                        Id = (int)reader.GetValue(0),
                        Title = (string)reader.GetValue(1),
                        Article = (string)reader.GetValue(3),
                        Cost = (decimal)reader.GetValue(8)
                    };
                    if (reader.GetValue(5) != DBNull.Value)
                        item.Image = (string)reader.GetValue(5);
                    if (reader.GetValue(9) != DBNull.Value)
                        item.Type = (string)reader.GetValue(9);
                    result.Add(item);
                }
            }
            reader.Close();
            Program.con.Close();
            return result;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!isProgramEvent)
            {
                if (string.IsNullOrEmpty(textBoxSearch.Text))
                    textBoxSearch.Text = "Введите для поиска";
                else
                {
                    LoadProducts(GetProductsWithSelection());
                    BackToFirstPage();
                }
            }
        }

        private List<Product> GetProductsWithSelection()
        {
            Program.con.Open();
            var text = (textBoxSearch.Text.ToLower() == "введите для поиска") ? "" : textBoxSearch.Text.ToLower();
            string sql = "select product.*, producttype.title from product left join producttype on product.producttypeid=producttype.id" +
                    $" where (lower(product.title) like '%{text}%' or lower(description) like '%{text}%') ";
            if (comboBoxFilter.SelectedIndex != -1 && comboBoxFilter.SelectedIndex != 0)
                sql += $"and producttypeid = {comboBoxFilter.SelectedIndex} ";
            if (comboBoxSort.SelectedIndex != -1)
            {
                switch (comboBoxSort.SelectedIndex)
                {
                    case 1:
                        sql += "order by product.title";
                        break;
                    case 2:
                        sql += "order by product.title desc";
                        break;
                    case 3:
                        sql += "order by productionworkshopnumber";
                        break;
                    case 4:
                        sql += "order by productionworkshopnumber desc";
                        break;
                    case 5:
                        sql += "order by mincostforagent";
                        break;
                    case 6:
                        sql += "order by mincostforagent desc";
                        break;
                }
            }
            var cmd = new NpgsqlCommand(sql, Program.con);
            var reader = cmd.ExecuteReader();
            var result = new List<Product>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var item = new Product()
                    {
                        Id = (int)reader.GetValue(0),
                        Title = (string)reader.GetValue(1),
                        Article = (string)reader.GetValue(3),
                        Cost = (decimal)reader.GetValue(8)
                    };
                    if (reader.GetValue(5) != DBNull.Value)
                        item.Image = (string)reader.GetValue(5);
                    if (reader.GetValue(9) != DBNull.Value)
                        item.Type = (string)reader.GetValue(9);
                    result.Add(item);
                }
            }
            reader.Close();
            Program.con.Close();
            return result;
        }

        private Int64 GetCountWithSelection()
        {
            Program.con.Open();
            var text = (textBoxSearch.Text.ToLower() == "введите для поиска") ? "" : textBoxSearch.Text.ToLower();
            string sql = "select count(id) from product" +
                    $" where (lower(product.title) like '%{text}%' or lower(description) like '%{text}%') ";
            if (comboBoxFilter.SelectedIndex != -1 && comboBoxFilter.SelectedIndex != 0)
                sql += $"and producttypeid = {comboBoxFilter.SelectedIndex} ";
            var cmd = new NpgsqlCommand(sql, Program.con);
            var result = (Int64)cmd.ExecuteScalar();
            Program.con.Close();
            return result;
        }


        private void TextBoxSearch_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == "Введите для поиска")
            {
                isProgramEvent = true;
                textBoxSearch.Text = "";
                isProgramEvent = false;
            }
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts(GetProductsWithSelection());
            BackToFirstPage();
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts(GetProductsWithSelection());
            BackToFirstPage();
        }

        public static string[] GetTypes()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand("select title from producttype", Program.con);
            var reader = cmd.ExecuteReader();
            var result = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add((string)reader.GetValue(0));
                }
            }
            reader.Close();
            Program.con.Close();
            return result.ToArray();
        }

        private void buttonChangeCost_Click(object sender, EventArgs e)
        {
            List<int> ids = new List<int>();
            foreach (Panel item in mainPanel.Controls)
            {
                if (item.BackColor == color)
                    ids.Add(int.Parse(item.Name));
            }
            var changeCost = new ChangeCostForm();
            if (changeCost.ShowDialog(this) == DialogResult.OK)
            {
                UpdateCostProduct(Decimal.Parse(changeCost.Cost), ids);
                LoadProducts(GetProductsWithSelection());
                BackToFirstPage();
            }
        }

        private void UpdateCostProduct(decimal sum, List<int> productsId)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"update product set mincostforagent = mincostforagent +{sum.ToString("0.00", CultureInfo.GetCultureInfo("en_US"))} where id in ({string.Join(", ", productsId)})", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }


        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            Program.ChangeForm(this, new AddEditForm(null, userGroup));
        }

        private void labelBack_Click(object sender, EventArgs e)
        {
            if (start == 0)
                return;
            ChangePage(position - 1);
            foreach (Control control in Controls)
            {
                if (control is Label)
                {
                    if (int.TryParse(control.Text, out int a))
                        if (a == position)
                            changeLabelUnderline((Label)control);
                }
            }
            if (position < int.Parse(label1.Text))
                changeLabelNumbers(-1);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (ChangePage(int.Parse(label2.Text)))
                changeLabelUnderline(label2);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (ChangePage(int.Parse(label1.Text)))
                changeLabelUnderline(label1);
        }

        private void labelForward_Click(object sender, EventArgs e)
        {
            ChangePage(position + 1);
            if (position > 4)
                changeLabelNumbers(1);
            foreach (Control control in Controls)
            {
                if (control is Label)
                {
                    if (int.TryParse(control.Text, out int a))
                        if (a == position)
                            changeLabelUnderline((Label)control);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (ChangePage(int.Parse(label3.Text)))
                changeLabelUnderline(label3);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (ChangePage(int.Parse(label4.Text)))
                changeLabelUnderline(label4);
        }

        private bool ChangePage(int pos)
        {
            if (pos - 1 != 0 && (pos - 1) * 20 >= GetCountWithSelection())
                return false;
            position = pos;
            start = (position - 1) * 20;
            LoadProducts(GetProductsWithSelection());
            return true;
        }

        private void changeLabelUnderline(Label labelUnderline)
        {
            foreach (Control control in Controls)
                if (control is Label)
                    control.Font = new Font(control.Font, FontStyle.Regular);
            labelUnderline.Font = new Font(labelUnderline.Font, FontStyle.Underline);
        }

        private void changeLabelNumbers(int i)
        {
            label1.Text = int.Parse(label1.Text) + i + "";
            label2.Text = int.Parse(label2.Text) + i + "";
            label3.Text = int.Parse(label3.Text) + i + "";
            label4.Text = int.Parse(label4.Text) + i + "";
        }

        private void BackToFirstPage()
        {
            ChangePage(1);
            changeLabelUnderline(label1);
            if (position < int.Parse(label1.Text))
                changeLabelNumbers(1 - int.Parse(label1.Text));
        }
    }
}
