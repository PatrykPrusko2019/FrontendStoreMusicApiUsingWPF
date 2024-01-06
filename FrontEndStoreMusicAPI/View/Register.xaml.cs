using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.View;
using MusicStoreApi.Models;
using System.Windows;

namespace FrontEndStoreMusicAPI
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();

        }

        private void Button_ReturnToLogin(object sender, RoutedEventArgs e)
        {
            MainWindowLogin login = new MainWindowLogin();
            this.Visibility = Visibility.Hidden;
            login.Show();
        }

        private void Button_CreateNewAccount(object sender, RoutedEventArgs e)
        {

                RegisterUserDto registerUserDto = new RegisterUserDto();
                registerUserDto.FirstName = RegisterFirstName.Text;
                registerUserDto.LastName = RegisterLastName.Text;
                registerUserDto.Email = RegisterEmail.Text;
                registerUserDto.Password = RegisterPassword.Text;
                registerUserDto.ConfirmPassword = RegisterConfirmPassword.Text;
                registerUserDto.Nationality = RegisterNationality.Text;

                // registerUserDto.DateOfBirth = new DateTime(RegisterDateOfBirth.Text);
                registerUserDto.RoleId = registerUserDto.RoleId;

                IRegisterService registerService = new RegisterService();
                registerService.Register(registerUserDto);


            
            //using (HttpClient client = new HttpClient()) 
            //{
            //    string firstName = RegisterFirstName.Text;
            //    RegisterUserDto registerUserDto = new RegisterUserDto();
            //    registerUserDto.FirstName = firstName;

            //     var response = await client.GetAsync("https://localhost:7195/api/account/register");
            //    response.EnsureSuccessStatusCode();
            //    if (response.IsSuccessStatusCode)
            //    {
            //        accountController.RegisterUser(registerUserDto);
            //    }
            //    else
            //    {
            //        MessageBox.Show($"Server error code {response.StatusCode}");
            //    }
            //}
        }
    }
}
