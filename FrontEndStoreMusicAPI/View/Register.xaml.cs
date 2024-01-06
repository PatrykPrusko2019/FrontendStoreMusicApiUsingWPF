﻿using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.View;
using MusicStoreApi.Models;
using System;
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
            int registerRole = 0;
            if ( DateTime.TryParse(RegisterDateOfBirth.Text, out DateTime dateOfBirth) &&
                int.TryParse(RegisterRole.Text, out registerRole) && registerRole > 0 && registerRole < 4  ) 
            {
                RegisterUserDto registerUserDto = new RegisterUserDto()
                {
                    FirstName = RegisterFirstName.Text,
                    LastName = RegisterLastName.Text,
                    Email = RegisterEmail.Text,
                    Password = RegisterPassword.Text,
                    ConfirmPassword = RegisterConfirmPassword.Text,
                    Nationality = RegisterNationality.Text,
                    DateOfBirth = dateOfBirth,
                    RoleId = registerRole
                };
                IRegisterService registerService = new RegisterService();
                registerService.Register(registerUserDto);
            }
            else
            {
                MessageBox.Show("Invalid Date Of Birth -> correct format is: year/month/day or Role -> correct choose number: 1 (USER), 2 (PREMIUM_USER), 3 (ADMIN)");
                return;
            }

        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            RegisterFirstName.Clear();
            RegisterLastName.Clear();
            RegisterEmail.Clear();
            RegisterPassword.Clear();
            RegisterConfirmPassword.Clear();
            RegisterNationality.Clear();
            RegisterDateOfBirth.Clear();
            RegisterRole.Clear();
        }
    }
}
