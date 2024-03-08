using LopushokApp.Entities;
using Npgsql;
using System.Globalization;

namespace LopushokApp
{
    public partial class AddEditForm : Form
    {
        private bool isProgramEvent = false;
        private bool isDataChanged = false;
        private int? productId = null;
        private string? filePath = null;
        private Panel? panelForDelete;
        private readonly int userGroup;

        public AddEditForm(int? productId, int userGroup)
        {
            InitializeComponent();
            this.userGroup = userGroup;
            comboBoxType.Items.Add("Не выбрано");
            comboBoxType.Items.AddRange(ProductsForm.GetTypes());
            panelMaterials.Controls.Remove(panelExample);
            if (productId != null)
            {
                this.productId = productId;
                LoadProduct();
                buttonSave.Text = "Сохранить";
                buttonDelete.Enabled = true;
            }
            pictureBoxImage.Click += PictureBoxImage_Click;
            textBoxTitle.TextChanged += TextBox_TextChanged;
            textBoxArticle.TextChanged += TextBox_TextChanged;
            textBoxDescription.TextChanged += TextBox_TextChanged;
            textBoxMinCost.TextChanged += TextBox_TextChanged;
            textBoxPersonCount.TextChanged += TextBox_TextChanged;
            textBoxWorkshopNumber.TextChanged += TextBox_TextChanged;
            comboBoxType.SelectedIndexChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            if (!isProgramEvent)
                isDataChanged = true;
        }

        private void PictureBoxImage_Click(object? sender, EventArgs e)
        {
            isDataChanged = true;
            var directory = Environment.CurrentDirectory;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = directory;
            dialog.ShowDialog();
            filePath = dialog.FileName;

            filePath = filePath.Replace(directory, "");
            pictureBoxImage.Image = ProductsForm.ConvertToBitmap(filePath);
        }

        private void LoadProduct()
        {
            var product = GetProductById();
            isProgramEvent = true;
            textBoxTitle.Text = product.Title;
            textBoxArticle.Text = product.Article;
            comboBoxType.SelectedIndex = (int)product.TypeId;
            textBoxPersonCount.Text = product.PersonCount + "";
            textBoxWorkshopNumber.Text = product.WorkShopNumber + "";
            textBoxMinCost.Text = product.Cost + "";
            if (product.Image != null)
            {
                pictureBoxImage.Image = ProductsForm.ConvertToBitmap(product.Image);
                filePath = product.Image;
            }
            else pictureBoxImage.Image = ProductsForm.ConvertToBitmap("\\picture.png");
            textBoxDescription.Text = (product.Description != null) ? product.Description : "";
            LoadMaterials();
            isProgramEvent = false;
        }

        private void LoadMaterials()
        {
            var materials = GetMaterialsByProductId();

            if (materials.Count > 0)
            {
                var location = new Point(0, 0);
                foreach (Material item in materials)
                {
                    Panel panel = new Panel
                    {
                        Location = location,
                        Name = item.Id + "",
                        BorderStyle = BorderStyle.FixedSingle,
                        Size = new Size(583, 83)
                    };
                    panel.MouseClick += Panel_MouseClick;

                    var comboBox = new ComboBox
                    {
                        Location = new Point(16, 18),
                        Size = new Size(356, 45),
                        MaxDropDownItems = 8,
                        IntegralHeight = false,
                        Name = "comboBox"
                    };
                    comboBox.Items.AddRange(GetMaterials());
                    comboBox.SelectedIndex = comboBox.Items.IndexOf(item.Title);
                    comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

                    var textBox = new TextBox
                    {
                        Location = new Point(436, 22),
                        Size = new Size(125, 41),
                        Text = item.Count + "",
                        Name = "textBox"
                    };
                    textBox.Click += TextBoxCount_Click;
                    textBox.TextChanged += TextBoxCount_TextChanged;
                    panel.Controls.AddRange(new Control[] { comboBox, textBox });
                    panelMaterials.Controls.Add(panel);
                    location = new Point(location.X, location.Y + panel.Size.Height + 10);
                }
            }
        }

        private void TextBoxCount_TextChanged(object? sender, EventArgs e)
        {
            if (isProgramEvent)
                return;
            isDataChanged = true;
            var textBox = (TextBox)sender;
            if (textBox.Text == "")
                textBox.Text = "Количество";
        }

        private void TextBoxCount_Click(object? sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text == "Количество")
            {
                isProgramEvent = true;
                textBox.Text = "";
                isProgramEvent = false;
            }
        }

        private void Panel_MouseClick(object? sender, MouseEventArgs e)
        {
            Panel panel = (Panel)sender;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                ToolStripMenuItem menuItem = new("Удалить");
                menuItem.Click += new EventHandler(DeleteMaterial);
                contextMenu.Items.Add(menuItem);
                panelForDelete = panel;
                panel.ContextMenuStrip = contextMenu;
            }
        }

        private void DeleteMaterial(object? sender, EventArgs e)
        {
            if (productId != null && panelForDelete.Name != "")
                DeleteProductMaterialByMaterialId(int.Parse(panelForDelete.Name));
            panelMaterials.Controls.Remove(panelForDelete);
            panelForDelete = null;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (isDataChanged)
            {
                DialogResult result = MessageBox.Show(
                    "На страницу остались несохраненные изменения, Вы уверены, что хотите выйти?",
                    "Несохраненные изменения",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    BackToProducts();
                }
            }
            else BackToProducts();
        }

        private void BackToProducts()
        {
            Program.ChangeForm(this, new ProductsForm(userGroup));
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Вы уверены, что хотите удалить продукт? Будет выполнено удаление продукта без возможности восстановления",
                    "Удаление продукта",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (GetProductFromProductSale() != null)
                {
                    MessageBox.Show("Продукт не может быть удален, так существует информация о его продаже агентами!", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DeleteProduct();
                DeleteProductCostHistory();
                DeleteProductMaterial();
                BackToProducts();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxArticle.Text) || string.IsNullOrEmpty(textBoxTitle.Text) || string.IsNullOrEmpty(textBoxMinCost.Text))
            {
                MessageBox.Show("Поля Артикул, Наименование и Минимальная стоимость для агента не могут быть пустыми!", "Заполните поля", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var cost = string.Format("0,00", textBoxMinCost.Text);
                if (decimal.Parse(cost) < 0)
                {
                    MessageBox.Show("Поле Минимальная стоимость для агента не может содержать отрицательное значение!", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Поле Минимальная стоимость для агента может содержать только числовое значение с точностью до сотых!", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!int.TryParse(textBoxArticle.Text, out int a))
            {
                MessageBox.Show("Поле Артикул может содержать только целочисленные значения!", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((!string.IsNullOrEmpty(textBoxPersonCount.Text) && !int.TryParse(textBoxPersonCount.Text, out int p))
                || !string.IsNullOrEmpty(textBoxWorkshopNumber.Text) && !int.TryParse(textBoxWorkshopNumber.Text, out int n))
            {
                MessageBox.Show("Поля Количество человек для произвоздства и Номер производственного цеха могут содержать только целочисленные значения!", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (Panel panel in panelMaterials.Controls)
            {
                if (((ComboBox)panel.Controls["comboBox"]).SelectedIndex == -1 || string.IsNullOrEmpty(((TextBox)panel.Controls["textBox"]).Text))
                {
                    MessageBox.Show("Поля с данными об используемых материалах не могут быть пустыми!", "Заполните поля", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!double.TryParse(((TextBox)panel.Controls["textBox"]).Text, out double count) && panel.Visible == true)
                {
                    MessageBox.Show("Поле Количество используемых материалов может содержать только числовые значения с запятой в качестве разделителя целой и десятичной частей!", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            var id = GetProductByArticle(textBoxArticle.Text);
            if (id != null)
            {
                if (productId != null && id == productId)
                    UpdateProduct();
                else
                {
                    MessageBox.Show("Товар с таким артикулом уже существует, укажите другой артикул!", "Совпадение артикула", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
                if (productId != null)
                UpdateProduct();
            else productId = AddProduct();
            foreach (Panel panel in panelMaterials.Controls)
            {
                if (panel.Name != "panel")
                    UpdateProductMaterial(int.Parse(panel.Name), ((ComboBox)panel.Controls["comboBox"]).GetItemText(((ComboBox)panel.Controls["comboBox"]).SelectedItem), double.Parse(((TextBox)panel.Controls["textBox"]).Text));
                else AddProductMaterial(((ComboBox)panel.Controls["comboBox"]).GetItemText(((ComboBox)panel.Controls["comboBox"]).SelectedItem), double.Parse(((TextBox)panel.Controls["textBox"]).Text));
            }
            BackToProducts();
        }

        public Product GetProductById()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select product.*, producttype.title from product left join producttype on product.producttypeid=producttype.id where product.id={productId}", Program.con);
            var reader = cmd.ExecuteReader();
            var result = new Product();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = new Product()
                    {
                        Id = (int)reader.GetValue(0),
                        Title = (string)reader.GetValue(1),
                        Article = (string)reader.GetValue(3),
                        Cost = (decimal)reader.GetValue(8)
                    };
                    if (reader.GetValue(5) != DBNull.Value)
                        result.Image = (string)reader.GetValue(5);
                    if (reader.GetValue(4) != DBNull.Value)
                        result.Description = (string)reader.GetValue(4);
                    if (reader.GetValue(9) != DBNull.Value)
                        result.Type = (string)reader.GetValue(9);
                    if (reader.GetValue(7) != DBNull.Value)
                        result.WorkShopNumber = (int)reader.GetValue(7);
                    if (reader.GetValue(6) != DBNull.Value)
                        result.PersonCount = (int)reader.GetValue(6);
                    if (reader.GetValue(2) != DBNull.Value)
                        result.TypeId = (int)reader.GetValue(2);
                    else result.TypeId = 0;
                }
            }
            reader.Close();
            Program.con.Close();
            return result;
        }

        private List<Material> GetMaterialsByProductId()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select productmaterial.materialid, material.title, productmaterial.count from productmaterial inner join material on productmaterial.materialid=material.id where productmaterial.productid ={productId}", Program.con);
            var reader = cmd.ExecuteReader();
            var result = new List<Material>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Material()
                    {
                        Id = (int)reader.GetValue(0),
                        Title = (string)reader.GetValue(1),
                        Count = (double)reader.GetValue(2)
                    });
                }

            }
            reader.Close();
            Program.con.Close();
            return result;
        }

        public static string[] GetMaterials()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select title from material", Program.con);
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


        private void UpdateProduct()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"update product set articlenumber ='{textBoxArticle.Text}', title = '{textBoxTitle.Text}'," +
                $"producttypeid = {((comboBoxType.SelectedIndex == 0) ? "null" : comboBoxType.SelectedIndex)}, " +
                $"description = {((textBoxDescription.Text == "") ? "null" : "'" + textBoxDescription.Text + "'")}, " +
                $"image = {((filePath != null) ? "'" + filePath + "'" : "null")}, " +
                $"productionpersoncount={((textBoxPersonCount.Text == "") ? "null" : textBoxPersonCount.Text)}, " +
                $"productionworkshopnumber={((textBoxWorkshopNumber.Text == "") ? "null" : textBoxWorkshopNumber.Text)}, " +
                $"mincostforagent ={textBoxMinCost.Text.Replace(',', '.')} where id={productId}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }
        private int AddProduct()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"insert into product(articlenumber, title, producttypeid, description, image, productionpersoncount," +
                $"productionworkshopnumber, mincostforagent) values ('{textBoxArticle.Text}', '{textBoxTitle.Text}', " +
                $"{((comboBoxType.SelectedIndex == -1 || comboBoxType.SelectedIndex == 0) ? "null" : comboBoxType.SelectedIndex)}, " +
                $"{((textBoxDescription.Text == "") ? "null" : "'" + textBoxDescription.Text + "'")}, " +
                $"{((filePath != null) ? "'" + filePath + "'" : "null")}, " +
                $"{((textBoxPersonCount.Text == "") ? "null" : textBoxPersonCount.Text)}, " +
                $"{((textBoxWorkshopNumber.Text == "") ? "null" : textBoxWorkshopNumber.Text)}, " +
                $"{textBoxMinCost.Text.Replace(',', '.')}) returning id", Program.con);
            int result = (int)cmd.ExecuteScalar();
            Program.con.Close();
            return result;
        }

        private void AddProductMaterial(string material, double count)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"insert into productmaterial values({productId}, " +
                $"(select id from material where title = '{material}'), {count.ToString("0.00", CultureInfo.GetCultureInfo("en-US"))})", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private void UpdateProductMaterial(int materialId, string material, double count)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"update productmaterial set materialid = (select id from material where title = '{material}'), " +
                $"count = {count} where productid = {productId} and materialid = {materialId}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private void DeleteProduct()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"delete from product where id ={productId}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private void DeleteProductMaterial()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"delete from productmaterial where productid ={productId}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private void DeleteProductMaterialByMaterialId(int id)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"delete from productmaterial where productid ={productId} and materialid = {id}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private void DeleteProductCostHistory()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"delete from productcosthistory where productid ={productId}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private int? GetProductFromProductSale()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select id from productsale where productid = {productId}", Program.con);
            int? result = (int?)cmd.ExecuteScalar();
            Program.con.Close();
            return result;
        }

        private int? GetProductByArticle(string article)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select id from product where articlenumber = '{article}'", Program.con);
            int? result = (int?)cmd.ExecuteScalar();
            Program.con.Close();
            return result;
        }

        private void buttonAddMaterial_Click(object sender, EventArgs e)
        {
            isDataChanged = true;
            var location = new Point(0, buttonAddMaterial.Location.Y);
            Panel panel = new Panel
            {
                Location = location,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(583, 83),
                Name = "panel"
            };
            panel.MouseClick += Panel_MouseClick;

            var comboBox = new ComboBox
            {
                Location = new Point(16, 18),
                Size = new Size(356, 45),
                MaxDropDownItems = 8,
                IntegralHeight = false,
                Name = "comboBox"
            };
            comboBox.Items.AddRange(GetMaterials());
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            var textBox = new TextBox
            {
                Location = new Point(436, 22),
                Size = new Size(125, 41),
                Text = "Количество",
                Name = "textBox"
            };
            textBox.Click += TextBoxCount_Click;
            textBox.TextChanged += TextBoxCount_TextChanged;
            panel.Controls.AddRange(new Control[] { comboBox, textBox });
            panelMaterials.Controls.Add(panel);
            location = new Point(location.X, location.Y + panel.Size.Height + 10);
        }

        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            isDataChanged = true;
        }
    }
}
