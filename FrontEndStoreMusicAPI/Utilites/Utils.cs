
using FrontEndStoreMusicAPI.View;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using FrontEndStoreMusicAPI;

namespace FrontEndStoreMusicAPI.Utilites
{
    public static class Utils
    {
        public static void ClearValuesOfUserRegisterWindow()
        {
            Register.r.RegisterFirstName.Clear();
            Register.r.RegisterLastName.Clear();
            Register.r.RegisterEmail.Clear();
            Register.r.RegisterPassword.Clear();
            Register.r.RegisterConfirmPassword.Clear();
            Register.r.RegisterNationality.Clear();
            Register.r.RegisterDateOfBirth.Clear();
            Register.r.RegisterRole.Clear();
        }
    }
}
