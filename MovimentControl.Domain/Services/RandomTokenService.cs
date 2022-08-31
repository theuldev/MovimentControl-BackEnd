using MovimentControl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimentControl.Domain.Services
{
    public class RandomTokenService
    {
        public static string GenerateToken2Fa()
        {
            string token = "";
            var random = new Random();




            for (int i = 0; i < 4; i++)
            {
                var _bytes = random.Next(0, 9);
                token += _bytes.ToString();
            }
            return token;

        }
        public static bool VerifyDate(UserInputModel user, DateTime time)
        {
            if (time < user.LoggedTime.AddMinutes(1))
            {
                return true;
            }
            return false;
        }
        public static bool VerifyToken(string token, UserInputModel user)
        {
            if (token == user.Token)
            {
                return true;

            }
            return false;

        }
    }
}
