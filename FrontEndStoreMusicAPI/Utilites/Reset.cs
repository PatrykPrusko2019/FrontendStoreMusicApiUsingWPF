using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Utilites
{
    public static class Reset
    {
        public static void ClearValuesOfUserRegisterWindow()
        {
            RegisterWindow.r.RegisterFirstName.Clear();
            RegisterWindow.r.RegisterLastName.Clear();
            RegisterWindow.r.RegisterEmail.Clear();
            RegisterWindow.r.RegisterPassword.Clear();
            RegisterWindow.r.RegisterConfirmPassword.Clear();
            RegisterWindow.r.RegisterNationality.Clear();
            RegisterWindow.r.RegisterDateOfBirth.Clear();
            RegisterWindow.r.RegisterRole.Clear();
           
        }


    }
}
