using System;
using System.Windows;
using Variant4.Models;

namespace Variant4.Views
{
    public partial class EditRequestView : Window
    {
        private readonly User _currentUser;
        private readonly PatientRequest? _requestToEdit;

        public EditRequestView(User user, PatientRequest? request = null)
        {
            InitializeComponent();
            _currentUser = user;
            _requestToEdit = request;

            if (request != null)
            {
                Title = "Редактировать заявку";
                ArticleBox.Text = request.Article;
                TitleBox.Text = request.Title;
                TypeBox.Text = request.Type;
                DescriptionBox.Text = request.Description;
                StatusBox.SelectedIndex = request.Status;
                ArticleBox.IsEnabled = false; // Артикул не меняем
            }
            else
            {
                Title = "Новая заявка";
                StatusBox.SelectedIndex = 0;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string article = ArticleBox.Text.Trim();
            string title = TitleBox.Text.Trim();
            string type = TypeBox.Text.Trim();
            string description = DescriptionBox.Text.Trim();
            int status = StatusBox.SelectedIndex;

            if (string.IsNullOrWhiteSpace(article) || string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Артикул и Название обязательны.");
                return;
            }

            using var db = new Variant4Context();

            if (_requestToEdit == null)
            {
                if (db.PatientRequests.Any(r => r.Article == article))
                {
                    MessageBox.Show("Заявка с таким артикулом уже существует.");
                    return;
                }

                var newRequest = new PatientRequest
                {
                    Article = article,
                    Title = title,
                    Type = type,
                    Description = description,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    Status = status,
                    UserId = _currentUser.Id
                };

                db.PatientRequests.Add(newRequest);
            }
            else
            {
                var existing = db.PatientRequests.Find(_requestToEdit.Id);
                if (existing != null)
                {
                    existing.Title = title;
                    existing.Type = type;
                    existing.Description = description;
                    existing.Status = status;
                }
            }

            db.SaveChanges();
            Close();
        }
    }
}
