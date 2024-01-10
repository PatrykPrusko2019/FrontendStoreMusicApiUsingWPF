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
