using Npgsql;

namespace LopushokApp
{
    public partial class ChangeCostForm : Form
    {
        public ChangeCostForm()
        {
            InitializeComponent();
            textBoxNumber.Text = GetAverageCostProduct().ToString("0.00");
            textBoxNumber.KeyPress += TextBoxNumber_KeyPress;
            buttonChangeCost.Click += ButtonChangeCost_Click;
        }

        public string Cost
        {
            get { return textBoxNumber.Text; }
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

        private void ButtonChangeCost_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNumber.Text))
                MessageBox.Show("Введите числовое значение!", "Пустое поле", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                DialogResult = DialogResult.OK;
        }

        private void TextBoxNumber_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
