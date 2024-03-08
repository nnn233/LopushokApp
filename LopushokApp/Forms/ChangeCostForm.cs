using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LopushokApp
{
    public partial class ChangeCostForm : Form
    {
        List<int> productsId;
        public ChangeCostForm(List<int> productsId)
        {
            this.productsId = productsId;
            InitializeComponent();
            textBoxNumber.Text = GetAverageCostProduct().ToString("0.00");
            textBoxNumber.KeyPress += TextBoxNumber_KeyPress;
            buttonChangeCost.Click += ButtonChangeCost_Click;
        }

        private decimal GetAverageCostProduct()
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand("select avg(mincostforagent) from product", Program.con);
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

        private void UpdateCostProduct(decimal sum)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"update product set mincostforagent = mincostforagent +{sum.ToString("0.00", CultureInfo.GetCultureInfo("en_US"))} where id in ({string.Join(", ", productsId)})", Program.con);
            cmd.ExecuteNonQuery();
            Program.con.Close();
        }


        private void ButtonChangeCost_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNumber.Text))
                MessageBox.Show("Сообщение","Введите числовое значение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                UpdateCostProduct(Decimal.Parse(textBoxNumber.Text));
                Close();
            }
        }

        private void TextBoxNumber_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && !Char.IsControl(e.KeyChar))
                e.Handled=true;
        }
    }
}
