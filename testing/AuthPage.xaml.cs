using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace testing
{
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        public bool Auth(string log, string pas)
        {
            if (string.IsNullOrEmpty(log) || string.IsNullOrEmpty(pas))
            {
                MessageBox.Show("Ошибка: не все поля ввода инициализированы!");
                return false;
            }
            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Login == log);

                    if (user == null)
                        throw new InvalidOperationException("Пользователь не найден в базе данных");

                    MessageBox.Show($"Здравствуйте, {user.Role} {user.FIO}!");
                    return true;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка: {ex.Message}");
                // Логирование ошибки
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }
       
        private void RegisterPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegPage());
        }

        private void LoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == "Введите логин")
            {
                LoginTextBox.Text = "";
                LoginTextBox.Foreground = Brushes.Black;
            }
        }

        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                LoginTextBox.Text = "Введите логин";
                LoginTextBox.Foreground = Brushes.Gray;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Auth(LoginTextBox.Text, PasswordBox.Password);

            //if (LoginTextBox == null || PasswordBox == null)
            //{
            //    MessageBox.Show("Ошибка: не все поля ввода инициализированы!");
            //    return;
            //}

            //string login = LoginTextBox.Text;
            //string password = PasswordBox.Password;

            //if (Auth(login, password))
            //{
            //    using (var db = new AppDbContext())
            //    {
            //        var user = db.Users.FirstOrDefault(u => u.Login == login);
            //        MessageBox.Show($"Здравствуйте, {user.Role} {user.FIO}!");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Неверные логин или пароль!");
            //}
        }
    }
}