using AdminApp.Common.Abstractions;
using AdminApp.Stores;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AdminApp.ViewModel;
public partial class SignInViewModel : BaseViewModel, IDataErrorInfo
{
    private readonly NavigationStore _navigationStore;
    public SignInViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private bool isPasswordVisible;

    [ObservableProperty]
    private string notificationMessage;

    [ObservableProperty]
    private bool isNotificationVisible;

    //validation
    public string Error => null;

    public string this[string columnName]
    {
        get
        {
            string result = string.Empty;

            switch (columnName)
            {
                case nameof(Email):
                    {
                        if (string.IsNullOrEmpty(Email))
                        {
                            EmailBorderColor = Brushes.Red;
                            EmailError = "Please, enter the email";
                            IsEmailErrorVisible = true;
                        }
                        else if (!Email.Contains("@"))
                        {
                            EmailBorderColor = Brushes.Red;
                            EmailError = "You've entered an invalid email";
                            IsEmailErrorVisible = true;
                        }
                        else
                        {
                            EmailBorderColor = Brushes.Gray;
                            EmailError = string.Empty;
                            IsEmailErrorVisible = false;
                        }
                        break;
                    }
                case nameof(Password):
                    {
                        if (string.IsNullOrEmpty(Password))
                        {
                            PasswordBorderColor = Brushes.Red;
                            PasswordError = "Please, enter the password";
                            IsPasswordVisible = true;
                        }
                        else if (Password.Length < 6 || Password.Length > 15)
                        {
                            PasswordBorderColor = Brushes.Red;
                            PasswordError = "Password must be between 6 and 15 characters";
                            IsPasswordErrorVisible = true;
                        }
                        else
                        {
                            PasswordBorderColor = Brushes.Gray;
                            PasswordError = string.Empty;
                            IsPasswordErrorVisible = false;
                        }
                        break;
                    }

            }
            return result;
        }
    }

    private void TogglePasswordVisibility(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            Password = passwordBox.Password;
            passwordBox.Password = IsPasswordVisible ? Password : new string('*', Password.Length);
        }
    }

    [RelayCommand]
    private async Task SignIn() => await WorkerSignIn(Email, Password);

    private async Task WorkerSignIn(string email, string password)
    {
        _navigationStore.CurrentViewModel = new HomeViewModel();

        if (Validate())
        {
            // await accountHttpClient.
            bool accountExists = Email == "user@example.com" && Password == "password123";
            bool profileActive = true; // Simulating a check for active profile

            if (accountExists && profileActive)
            {
                NotificationMessage = "You've signed in successfully";
                IsNotificationVisible = true;
                // Navigate to home page logic here
            }
            else
            {
                NotificationMessage = "Either an email or a password is incorrect";
                IsNotificationVisible = true;
            }
        }
    }

    [ObservableProperty]
    private bool isEmailErrorVisible;

    [ObservableProperty]
    private bool isPasswordErrorVisible;

    // Error messages bindings
    [ObservableProperty]
    private string emailError;

    [ObservableProperty]
    private string passwordError;

    // Border color bindings
    [ObservableProperty]
    private Brush emailBorderColor = Brushes.Gray;

    [ObservableProperty]
    private Brush passwordBorderColor = Brushes.Gray;

    private bool Validate()
    {
        return string.IsNullOrEmpty(EmailError) && string.IsNullOrEmpty(PasswordError);
    }

    private void OpenMainWindow()
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            var currentWindow = App.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        });
    }
}