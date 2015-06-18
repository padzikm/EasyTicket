using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EasyTicketLogicTest")]
namespace EasyTicketLogic
{
    
    internal class UserManagement
    {        
        internal User CurrentUser { get; private set; }

        internal UserManagement()
        {            
        }

        async internal Task<bool> RegisterUserAsync(User user)
        {
            bool loginExists;

            //Task<User> t = EasyTicketContext.Instance.GetUserByLoginAsync(user.Login);
            User resultUser = await Task<User>.Run(async () => { return await EasyTicketContext.Instance.GetUserByLoginAsync(user.Login); });

            loginExists = resultUser != null;

            if (loginExists)
                return false;

            await Task.Run(async () => { await EasyTicketContext.Instance.AddNewUserAsync(user); });
            
            return true;            
        }

        async internal Task<bool> LoginUserAsync(string login, string password)
        {
            User user;
            
            user = await Task<User>.Run(async () => { return await EasyTicketContext.Instance.GetUserByLoginAsync(login); });

            if (user == null || !CheckUserPassword(password, user.Password))
                return false;

            CurrentUser = user;
            return true;
        }

        private bool CheckUserPassword(string password, string passwordHash)
        {
            return string.Equals(passwordHash, Encode.Hash(password));
        }




    }
}
