using Microsoft.Win32;
using System.Linq;
using System.Windows;
using Variant4.Models;

namespace Variant4.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            using var db = new Variant4Context();
            var login = LoginBox.Text;
            var password = PasswordBox.Password;

            var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                var mainView = new MainView(user);
                mainView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var reg = new RegisterView();
            reg.ShowDialog();
        }
    }
}
