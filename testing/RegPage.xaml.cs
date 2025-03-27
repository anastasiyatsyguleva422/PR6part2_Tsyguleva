using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace testing
{
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        public void SetComboBoxRole(int index)
        {
            CmbRole.SelectedIndex = index;
        }

        public void SetGender(bool isMale)
        {
            if (isMale)
            {
                RadioMale.IsChecked = true;
                RadioFemale.IsChecked = false;
            }
            else
            {
                RadioFemale.IsChecked = true;
                RadioMale.IsChecked = false;
            }
        }
        public bool Reg(string fio, string log, string pas, string phone)
        {
            if (string.IsNullOrEmpty(fio) ||
                string.IsNullOrEmpty(log) ||
                string.IsNullOrEmpty(pas) ||
                string.IsNullOrEmpty(phone) ||
                (RadioMale.IsChecked == false && RadioFemale.IsChecked == false) ||
                CmbRole.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            string selectedRole = (CmbRole.SelectedItem as ComboBoxItem)?.Content.ToString().Trim();
            Dictionary<string, string> roleMapping = new Dictionary<string, string>
            {
                { "Администратор", "Admin" },
                { "Посетитель", "Visitor" },
                { "Деятель", "Artist" },
                { "Организатор", "Organizer" }
            };

            if (roleMapping.ContainsKey(selectedRole))
            {
                selectedRole = roleMapping[selectedRole];
            }
            else
            {
                MessageBox.Show("Выбрана некорректная роль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    if (db.Users.Any(u => u.Login == log))
                    {
                        MessageBox.Show("Этот логин уже занят!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }

                    string selectedGender = RadioMale.IsChecked == true ? "Мужской" : "Женский";

                    User newUser = new User
                    {
                        FIO = fio,
                        Login = log,
                        Password = pas,
                        Gender = selectedGender,
                        Role = selectedRole,
                        PhoneNumber = phone,
                        PhotoUrl = TextBoxPhoto.Text
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new AuthPage());
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBoxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "\\d");
        }

        private void TextBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxPhone.TextChanged -= TextBoxPhone_TextChanged;
            string phone = new string(TextBoxPhone.Text.Where(char.IsDigit).ToArray());

            if (!phone.StartsWith("8"))
            {
                phone = "8" + phone;
            }

            if (phone.Length > 11)
                phone = phone.Substring(0, 11);

            string formattedPhone = "8";
            if (phone.Length > 1) formattedPhone += $" ({phone.Substring(1, Math.Min(3, phone.Length - 1))}";
            if (phone.Length > 4) formattedPhone += $") {phone.Substring(4, Math.Min(3, phone.Length - 4))}";
            if (phone.Length > 7) formattedPhone += $"-{phone.Substring(7, Math.Min(2, phone.Length - 7))}";
            if (phone.Length > 9) formattedPhone += $"-{phone.Substring(9, Math.Min(2, phone.Length - 9))}";

            TextBoxPhone.Text = formattedPhone;
            TextBoxPhone.CaretIndex = TextBoxPhone.Text.Length;
            TextBoxPhone.TextChanged += TextBoxPhone_TextChanged;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFIO.Clear();
            TextBoxLogin.Clear();
            PasswordBox.Clear();
            TextBoxPhone.Clear();
            TextBoxPhoto.Clear();
            RadioMale.IsChecked = false;
            RadioFemale.IsChecked = false;
            CmbRole.SelectedIndex = -1;
            NavigationService.Navigate(new AuthPage());
        }
    }
}
