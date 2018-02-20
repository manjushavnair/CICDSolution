using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApp.Models
{
    public class UserModels
    {
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        public string Address { get; set; }
       
       
        public string Email { get; set; }
       
        public DateTime DOB { get; set; }
       
        public decimal Salary { get; set; }
    }
    public class Users
    {
        public Users()
        {
            _userList.Add(new UserModels
            {
                FirstName = "Anu",
                LastName = "Kumar",
                Address = "America",
                Email = "test@test.com",
                DOB = DateTime.ParseExact("22/06/1976", "dd/MM/yyyy", null),
                Salary = 40000

            });
            _userList.Add(new UserModels
            {
                FirstName = "Sinu",
                LastName = "mol",
                Address = "Indore MP",
                Email = "America",
                DOB = DateTime.ParseExact("07/11/2011", "dd/MM/yyyy", null),
                Salary = 7000

            });
            _userList.Add(new UserModels
            {
                FirstName = "Jinu",
                LastName = "mon",
                Address = "America",
                Email = "test2@test.com",
                DOB = DateTime.ParseExact("22/08/2013", "dd/MM/yyyy", null),
                Salary = 5000

            });
        }

        public List<UserModels> _userList = new List<UserModels>();

        public void CreateUser(UserModels userModel)
        {
            _userList.Add(userModel);
        }

        public void UpdateUser(UserModels userModel)
        {
            foreach (UserModels usrlst in _userList)
            {
                if (usrlst.Email == userModel.Email)
                {
                    _userList.Remove(usrlst);
                    _userList.Add(userModel);
                    break;
                }
            }
        }
        public  UserModels GetUser(string Email) {
            UserModels usrMdl = null;

            foreach (UserModels um in _userList)
                if (um.Email == Email)
                    usrMdl = um;

            return usrMdl;
        }

       
    }
}
