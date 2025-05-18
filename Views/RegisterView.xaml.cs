using System;
using System.Linq;
using System.Windows;
using Variant4.Models;

namespace Variant4.Views
{
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            using var db = new Variant4Context();

            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password;
            string fullName = FullNameBox.Text.Trim();
            string phone = PhoneBox.Text.Trim();

            if (db.Users.Any(u => u.Login == login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует.");
                return;
            }

            var user = new User
            {
                Login = login,
                Password = password,
                FullName = fullName,
                PhoneNumber = phone,
                RegisteredDate = DateOnly.FromDateTime(DateTime.Now)
            };

            db.Users.Add(user);
            db.SaveChanges();

            MessageBox.Show("Регистрация успешна.");
            Close();
        }
    }
}
