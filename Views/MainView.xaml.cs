using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Variant4.Models;

namespace Variant4.Views
{
    public partial class MainView : Window
    {
        private readonly User _currentUser;

        public MainView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            UserNameBlock.Text = user.FullName;
            LoadRequests();
        }

        private void LoadRequests()
        {
            using var db = new Variant4Context();
            var requests = db.PatientRequests
                .OrderByDescending(r => r.CreatedDate)
                .Select(r => new
                {
                    r.Id,
                    r.Article,
                    r.Title,
                    r.Type,
                    r.Status,
                    r.CreatedDate,
                    User = r.User
                })
                .ToList();

            RequestsGrid.ItemsSource = requests;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var win = new EditRequestView(_currentUser);
            win.ShowDialog();
            LoadRequests();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is not null)
            {
                dynamic selected = RequestsGrid.SelectedItem;
                int id = selected.Id;

                using var db = new Variant4Context();
                var request = db.PatientRequests.FirstOrDefault(r => r.Id == id);
                if (request != null)
                {
                    var editWindow = new EditRequestView(_currentUser, request);
                    editWindow.ShowDialog();
                    LoadRequests(); // обновим таблицу
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is not null)
            {
                dynamic selected = RequestsGrid.SelectedItem;
                int id = selected.Id;

                var confirm = MessageBox.Show("Удалить эту заявку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    using var db = new Variant4Context();
                    var request = db.PatientRequests.FirstOrDefault(r => r.Id == id);
                    if (request != null)
                    {
                        db.PatientRequests.Remove(request);
                        db.SaveChanges();
                        LoadRequests();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления.");
            }
        }

        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is not null)
            {
                dynamic selected = RequestsGrid.SelectedItem;
                int id = selected.Id;
                using var db = new Variant4Context();
                var request = db.PatientRequests.FirstOrDefault(r => r.Id == id);
                if (request != null)
                {
                    request.UserId = _currentUser.Id;
                    db.SaveChanges();
                    LoadRequests();
                }
            }
        }

        private void Unassign_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is not null)
            {
                dynamic selected = RequestsGrid.SelectedItem;
                int id = selected.Id;
                using var db = new Variant4Context();
                var request = db.PatientRequests.FirstOrDefault(r => r.Id == id);
                if (request != null && request.UserId == _currentUser.Id)
                {
                    request.UserId = null;
                    db.SaveChanges();
                    LoadRequests();
                }
            }
        }
    }
}
