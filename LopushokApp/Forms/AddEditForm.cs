using LopushokApp.Entities;
using Npgsql;
using System.Globalization;

namespace LopushokApp
{
    public partial class AddEditForm : Form
    {
        private int? productId = null;
        private string? filePath = null;
        private readonly int userGroup;

        public AddEditForm(int? productId, int userGroup)
        {
            InitializeComponent();
            this.userGroup = userGroup;
            comboBoxType.Items.Add("Не выбрано");
            comboBoxType.Items.AddRange(ProductsForm.GetTypes());
            DataGridViewComboBoxColumn comboBox = (DataGridViewComboBoxColumn)dataGridViewMaterials.Columns[1];
            comboBox.Items.AddRange(GetMaterials());
            dataGridViewMaterials.EditingControlShowing += DataGridViewMaterials_EditingControlShowing;
            if (productId != null)
            {
                this.productId = productId;
                LoadProduct();
                buttonSave.Text = "Сохранить";
                buttonDelete.Visible = true;
            }
            pictureBoxImage.Click += PictureBoxImage_Click;
        }

        private void DataGridViewMaterials_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox cb)
            {
                cb.DropDownStyle = ComboBoxStyle.DropDown;
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 6;
            }
        }

        private void PictureBoxImage_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem load = new("Выбрать новое");
            load.Click += Load_Click;
            ToolStripMenuItem delete = new("Удалить");
            delete.Click += Delete_Click;
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.AddRange(new ToolStripItem[] { load, delete });
            pictureBoxImage.ContextMenuStrip = menu;
        }

        private void Delete_Click(object? sender, EventArgs e)
        {
            filePath = null;
            pictureBoxImage.Image = ProductsForm.ConvertToBitmap(filePath);
        }

        private void Load_Click(object? sender, EventArgs e)
        {
            var directory = Environment.CurrentDirectory;
            directory = directory.Replace("\\bin\\Debug\\net6.0-windows", "");
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            filePath = dialog.FileName;
            filePath = filePath.Replace(directory, "");
            pictureBoxImage.Image = ProductsForm.ConvertToBitmap(filePath);
        }

        private void LoadProduct()
        {
            var product = GetProductById();
            textBoxTitle.Text = product.Title;
            textBoxArticle.Text = product.Article;
            comboBoxType.SelectedIndex = (int)product.TypeId;
            textBoxPersonCount.Text = product.PersonCount + "";
            textBoxWorkshopNumber.Text = product.WorkShopNumber + "";
            textBoxMinCost.Text = product.Cost + "";
            pictureBoxImage.Image = ProductsForm.ConvertToBitmap(product.Image);
            if (product.Image != null)
                filePath = product.Image;
            textBoxDescription.Text = product.Description ?? "";
            LoadMaterials();
        }

        private void LoadMaterials()
        {
            var materials = GetMaterialsByProductId();
            dataGridViewMaterials.Rows.Clear();
            if (materials.Count > 0)
            {
                foreach (Material item in materials)
                    dataGridViewMaterials.Rows.Add(item.Id, item.Title, item.Count + "");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Возможно на странице есть несохраненные изменения, при возврате к списку продуктов они будут утеряны, Вы уверены, что хотите уйти?",
                "Несохраненные изменения",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                BackToProducts();
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
                try
                {
                    DeleteProduct();
                    BackToProducts();
                }
                catch (PostgresException)
                {
                    MessageBox.Show("Продукт не может быть удален, так существует информация о его продаже агентами!", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxArticle.Text) || string.IsNullOrEmpty(textBoxTitle.Text) || string.IsNullOrEmpty(textBoxMinCost.Text))
            {
                MessageBox.Show("Поля Артикул, Наименование и Минимальная стоимость для агента не могут быть пустыми!", "Заполните поля", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!int.TryParse(textBoxArticle.Text, out int a))
            {
                MessageBox.Show("Поле Артикул может содержать только цифры!", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!decimal.TryParse(textBoxMinCost.Text, out decimal c))
            {
                MessageBox.Show("Поле Минимальная стоимость дял агента может содержать только числовые значения с точностью до сотых, в качестве разделителя используйте запятую!", "Неверное значение поля", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (c < 0)
            {
                MessageBox.Show("Поле Минимальная стоимость дял агента не может содержать отрицательные значения!", "Неверное значение поля", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((!string.IsNullOrEmpty(textBoxPersonCount.Text) && !int.TryParse(textBoxPersonCount.Text, out int p))
                || !string.IsNullOrEmpty(textBoxWorkshopNumber.Text) && !int.TryParse(textBoxWorkshopNumber.Text, out int n))
            {
                MessageBox.Show("Поля Количество человек для произвоздства и Номер производственного цеха могут содержать только целочисленные значения!", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (DataGridViewRow row in dataGridViewMaterials.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells[1].Value == null || string.IsNullOrEmpty((string)row.Cells[2].Value))
                    {
                        MessageBox.Show("Поля с данными об используемых материалах не могут быть пустыми!", "Заполните поля", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!double.TryParse((string)row.Cells[2].Value, out double count))
                    {
                        MessageBox.Show("Поле Количество используемых материалов может содержать только числовые значения с запятой в качестве разделителя целой и десятичной частей!", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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
            foreach (DataGridViewRow row in dataGridViewMaterials.Rows)
                if (row.Index != dataGridViewMaterials.Rows.Count - 1)
                {
                    if (row.Cells[0].Value == null)
                        AddProductMaterial((string)row.Cells[1].Value, double.Parse((string)row.Cells[2].Value));
                    else UpdateProductMaterial((int)row.Cells[0].Value, (string)row.Cells[1].Value, double.Parse((string)row.Cells[2].Value));
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
            var cost = decimal.Parse(textBoxMinCost.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-EN"));
            var cmd = new NpgsqlCommand($"update product set articlenumber ='{textBoxArticle.Text}', title = '{textBoxTitle.Text}'," +
                $"producttypeid = {((comboBoxType.SelectedIndex == 0) ? "null" : comboBoxType.SelectedIndex)}, " +
                $"description = {((textBoxDescription.Text == "") ? "null" : "'" + textBoxDescription.Text + "'")}, " +
                $"image = {((filePath != null) ? "'" + filePath + "'" : "null")}, " +
                $"productionpersoncount={((textBoxPersonCount.Text == "") ? "null" : textBoxPersonCount.Text)}, " +
                $"productionworkshopnumber={((textBoxWorkshopNumber.Text == "") ? "null" : textBoxWorkshopNumber.Text)}, " +
                $"mincostforagent ={cost} where id={productId}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private int AddProduct()
        {
            var cost = decimal.Parse(textBoxMinCost.Text).ToString("0.00", CultureInfo.GetCultureInfo("en-EN"));
            Program.con.Open();
            var cmd = new NpgsqlCommand($"insert into product(articlenumber, title, producttypeid, description, image, productionpersoncount," +
                $"productionworkshopnumber, mincostforagent) values ('{textBoxArticle.Text}', '{textBoxTitle.Text}', " +
                $"{((comboBoxType.SelectedIndex == -1 || comboBoxType.SelectedIndex == 0) ? "null" : comboBoxType.SelectedIndex)}, " +
                $"{((textBoxDescription.Text == "") ? "null" : "'" + textBoxDescription.Text + "'")}, " +
                $"{((filePath != null) ? "'" + filePath + "'" : "null")}, " +
                $"{((textBoxPersonCount.Text == "") ? "null" : textBoxPersonCount.Text)}, " +
                $"{((textBoxWorkshopNumber.Text == "") ? "null" : textBoxWorkshopNumber.Text)}, " +
                $"{cost}) returning id", Program.con);
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
                $"count = {count.ToString("0.00", CultureInfo.GetCultureInfo("en-US"))} where productid = {productId} and materialid = {materialId}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private void DeleteProduct()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"delete from product where id ={productId}", Program.con);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (PostgresException e)
            {
                throw e;
            }
            finally
            {
                Program.con.Close();
            }
        }

        private void DeleteProductMaterialByMaterialId(int id)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"delete from productmaterial where productid ={productId} and materialid = {id}", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }

        private int? GetProductByArticle(string article)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select id from product where articlenumber = '{article}'", Program.con);
            int? result = (int?)cmd.ExecuteScalar();
            Program.con.Close();
            return result;
        }

        private void buttonDeleteMaterial_Click(object sender, EventArgs e)
        {
            if (dataGridViewMaterials.SelectedRows.Count > 0)
                foreach (DataGridViewRow row in dataGridViewMaterials.SelectedRows)
                {
                    var index = row.Index;
                    if (!dataGridViewMaterials.Rows[index].IsNewRow)
                    {
                        var id = (int?)dataGridViewMaterials.Rows[index].Cells[0].Value;
                        if (productId != null && id != null)
                            DeleteProductMaterialByMaterialId((int)id);
                        dataGridViewMaterials.Rows.RemoveAt(index);
                    }
                }
        }
    }
}
