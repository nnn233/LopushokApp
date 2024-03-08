using LopushokApp.Entities;
using Npgsql;

namespace LopushokApp
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Поля не могут быть пусты!", "Заполните поля", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var user = GetUserByLogin(textBoxLogin.Text.Trim());
            if (user == null)
                MessageBox.Show("Пользователя с таким логином не существует!", "Неверный логин", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (user.Password != textBoxPassword.Text.Trim())
                MessageBox.Show("Введен неверный пароль!", "Неверный пароль", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else Program.ChangeForm(this, new ProductsForm(user.UserGroup));
        }

        public User? GetUserByLogin(string login)
        {
            Program.con.Open();
            var cmd = new NpgsqlCommand($"select * from public.user where login = '{login}'", Program.con);
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var user = new User()
                    {
                        Id = (int)reader.GetValue(0),
                        UserGroup = (int)reader.GetValue(1),
                        Login = (string)reader.GetValue(2),
                        Password = (string)reader.GetValue(3)
                    };
                    Program.con.Close();
                    return user;
                }
                Program.con.Close();
                return null;
            }
            else
            {
                Program.con.Close();
                return null;
            }
        }
    }
}
