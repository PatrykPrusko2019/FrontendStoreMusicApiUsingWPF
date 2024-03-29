﻿using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View;
using System;
using System.Linq;
using System.Windows;

namespace FrontEndStoreMusicAPI
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public static RegisterWindow c;
        public RegisterWindow()
        {
            InitializeComponent();
            c = this;
        }

        private void Button_ReturnToLogin(object sender, RoutedEventArgs e)
        {
            MainWindowLogin login = new MainWindowLogin();
            this.Visibility = Visibility.Hidden;
            login.Show();
        }

        private async void Button_CreateNewAccount(object sender, RoutedEventArgs e)
        {
            string[] insertedDate = RegisterDateOfBirth.Text.Split('-').Reverse().ToArray();
            string date = string.Join("-", insertedDate);
            
            int registerRole = 0;
            if ( DateTime.TryParse(date, out DateTime dateOfBirth) &&
                int.TryParse(RegisterRole.Text, out registerRole) && registerRole > 0 && registerRole < 4  ) 
            {
                RegisterUserDto registerUserDto = new RegisterUserDto()
                {
                    FirstName = RegisterFirstName.Text,
                    LastName = RegisterLastName.Text,
                    Email = RegisterEmail.Text,
                    Password = RegisterPassword.Password,
                    ConfirmPassword = RegisterConfirmPassword.Password,
                    Nationality = RegisterNationality.Text,
                    DateOfBirth = dateOfBirth,
                    RoleId = registerRole
                };
                IRegisterService registerService = new RegisterService();
                bool result = await registerService.Register(registerUserDto);
                if (result)
                {
                    MainWindowLogin login = new MainWindowLogin();
                    this.Visibility = Visibility.Hidden;
                    login.Show();
                }
                
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Invalid Date Of Birth -> correct format is: day-month-year or Role -> correct choose number: 1 (USER), 2 (PREMIUM_USER), 3 (ADMIN)");
                return;
            }

        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            Reset.ClearValuesOfUserRegisterWindow();
        }
    }
}
