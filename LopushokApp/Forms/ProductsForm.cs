using LopushokApp.Entities;
using Npgsql;

namespace LopushokApp
{
    public partial class ProductsForm : Form
    {
        private bool isProgramEvent = false;
        private int productId;
        private int start = 0;
        private int position = 1;
        private readonly int userGroup;

        public ProductsForm(int userGroup)
        {
            InitializeComponent();
            this.userGroup = userGroup;
            if (userGroup != 1)
                buttonAddProduct.Visible = false;
            LoadProducts(GetProducts());
            label1.Font = new Font(label1.Font, FontStyle.Underline);
            comboBoxFilter.Items.AddRange(GetTypes());
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
                        MinimumSize = new Size(948, 0),
                        MaximumSize = new Size(948, 0),
                    };
                    if (userGroup == 1)
                        panel.MouseClick += Panel_MouseClick;

                    Label title = new Label();
                    title.Font = new Font("Gabriola", 16);
                    title.Location = new Point(153, 13);
                    title.Text = product.Type + " " + product.Title;
                    title.AutoSize = true;

                    if (!goodSoldProducts.Contains(product.Id))
                        title.ForeColor = Color.PaleVioletRed;
                    else title.ForeColor = Color.Black;

                    Label article = new Label();
                    article.Font = new Font("Gabriola", 14);
                    article.Text = product.Article;
                    article.Location = new Point(153, 51);
                    article.AutoSize = true;
                    var picture = new PictureBox();
                    picture.Location = new Point(23, 33);
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    picture.Size = new Size(110, 94);
                    if (product.Image != null)
                        picture.Image = ConvertToBitmap(product.Image);
                    else picture.Image = ConvertToBitmap("\\picture.png");
                    Label cost = new Label
                    {
                        Font = new Font("Gabriola", 14),
                        //Text = GetCostMaterialsByProductId(product.Id)+"",
                        Text = product.Cost + "",
                        Location = new Point(824, 22),
                        AutoSize = true
                    };
                    Label materials = new Label
                    {
                        Font = new Font("Gabriola", 12),
                        Location = new Point(153, 90),
                        AutoSize = true,
                        MaximumSize = new Size(778, 0),
                        Text = (GetMaterialsByProductId(product.Id) == "") ? "" : "Материалы: " + GetMaterialsByProductId(product.Id)
                    };
                    panel.Controls.AddRange(new Control[] { title, article, picture, cost, materials });
                    mainPanel.Controls.Add(panel);
                    location = new Point(location.X, location.Y + panel.Size.Height + 10);
                    startIndex++;
                }
            }
            else MessageBox.Show("В базе данных нет продуктов с указанными параметрами!");
        }

        private void Panel_MouseClick(object? sender, MouseEventArgs e)
        {
            Panel panel = (Panel)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (panel.BackColor == Color.Green)
                    panel.BackColor = Color.White;
                else panel.BackColor = Color.Green;

                bool isHighlighted = false;
                foreach (Panel item in mainPanel.Controls)
                {
                    if (item.BackColor == Color.Green)
                        isHighlighted = true;
                }
                buttonChangeCost.Visible = isHighlighted;
            }
            else
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                ToolStripMenuItem menuItem = new("Редактировать");
                menuItem.Click += new EventHandler(EditProduct);
                contextMenu.Items.Add(menuItem);
                productId = int.Parse(panel.Name);
                panel.ContextMenuStrip = contextMenu;
            }
        }

        private void EditProduct(object? sender, EventArgs e)
        {
            Form form = new AddEditForm(productId, userGroup);
            form.Text = "Редактировать продукт";
            Program.ChangeForm(this, form);
        }

        public static Image ConvertToBitmap(string fileName)
        {
            Image image;
            try
            {
                using (Stream bmpStream = File.Open(Environment.CurrentDirectory + fileName, FileMode.Open))
                {
                    image = Image.FromStream(bmpStream);
                }
            }
            catch (Exception _)
            {
                using (Stream bmpStream = File.Open(Environment.CurrentDirectory + "\\picture.png", FileMode.Open))
                {
                    image = Image.FromStream(bmpStream);
                }
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

        private decimal GetCostMaterialsByProductId(int id)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select material.cost*productmaterial.count from productmaterial inner join material on productmaterial.materialid=material.id where productmaterial.productid ={id}", Program.con);
            var reader = cmd.ExecuteReader();
            decimal result = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = (decimal)reader.GetValue(0);
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
            var cmd = new NpgsqlCommand("select product.*, producttype.title from product inner join producttype on product.producttypeid=producttype.id", Program.con);
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
                        sql += "order by product.title desc"; break;
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
                if (item.BackColor == Color.Green)
                    ids.Add(int.Parse(item.Name));
            }
            Form changeCost = new ChangeCostForm(ids);
            Enabled = false;
            changeCost.Show();
            changeCost.FormClosed += ChangeCost_FormClosed;
        }

        private void ChangeCost_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Enabled = true;
            LoadProducts(GetProductsWithSelection());
            BackToFirstPage();
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            Program.ChangeForm(this, new AddEditForm(null, userGroup));
        }

        private void labelBack_Click(object sender, EventArgs e)
        {
            if (start == 0)
                return;
            position--;
            start = (position - 1) * 20;
            LoadProducts(GetProductsWithSelection());
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
            if ((int.Parse(label2.Text) - 1) * 20 >= GetCountWithSelection())
                return;
            position = 2;
            start = (position - 1) * 20;
            LoadProducts(GetProductsWithSelection());
            changeLabelUnderline(label2);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if ((int.Parse(label1.Text) - 1) * 20 >= GetCountWithSelection())
                return;
            position = 1;
            start = (position - 1) * 20;
            LoadProducts(GetProductsWithSelection());
            changeLabelUnderline(label1);
        }

        private void labelForward_Click(object sender, EventArgs e)
        {
            if (position * 20 >= GetCountWithSelection())
                return;
            position++;
            start = (position - 1) * 20;
            LoadProducts(GetProductsWithSelection());
            foreach (Control control in Controls)
            {
                if (control is Label)
                {
                    if (int.TryParse(control.Text, out int a))
                        if (a == position)
                            changeLabelUnderline((Label)control);
                }
            }
            if (position > 4)
                changeLabelNumbers(1);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if ((int.Parse(label3.Text) - 1 )* 20 >= GetCountWithSelection())
                return;
            position = 3;
            start = (position - 1) * 20;
            LoadProducts(GetProductsWithSelection());
            changeLabelUnderline(label3);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if ((int.Parse(label4.Text) - 1) * 20 >= GetCountWithSelection())
                return;
            position = 4;
            start = (position - 1) * 20;
            LoadProducts(GetProductsWithSelection());
            changeLabelUnderline(label4);
        }

        private void changeLabelUnderline(Label labelUnderline)
        {
            foreach (Control control in Controls)
            {
                if (control is Label)
                {
                    control.Font = new Font(control.Font, FontStyle.Regular);
                }
            }
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
            position = 1;
            start = (position - 1) * 20;
            LoadProducts(GetProductsWithSelection());
            changeLabelUnderline(label1);
            if (position < int.Parse(label1.Text))
                changeLabelNumbers(1 - int.Parse(label1.Text));
        }
    }
}
