using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ViewModelLayer
{
    public class RegisterUser
    {
        private User obj = new User();
        public ICommand UserRegisterCommand { get; set; }

        public RegisterUser()
        {
            UserRegisterCommand = new MyCommandBase(Execute, CanExecute);
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }
        private void Execute(object parameter)
        {
            PasswordBox pwd = (PasswordBox)parameter;
            obj.Password = pwd.Password;
            if (obj.Password != null && obj.UserId != null && obj.UserType != null &&
                obj.Password.Length != 0 && obj.UserId.Length != 0 && obj.UserType.Length != 0)
            {
                obj.InsertUser();
            }
        }

        public bool AuthenticateUser(string UserId, string Password)
        {
            return obj.SelectUser(UserId, Password);
        }
        public string SelectUserType(string UserId)
        {
            return obj.SelectUserType(UserId);
        }

        public string UserId
        {
            get
            {
                return obj.UserId;
            }
            set
            {
                obj.UserId = value;
            }
        }
        public string Password
        {
            get
            {
                return obj.Password;
            }
            set
            {
                obj.Password = value;
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return obj.ConfirmPassword;
            }
            set
            {
                obj.ConfirmPassword = value;
            }
        }
        public string UserType
        {
            get
            {
                return obj.UserType;
            }
            set
            {
                obj.UserType = value;
            }
        }
        public string PasswordMatch
        {
            get
            {
                if (obj.Password != null && obj.ConfirmPassword != null)
                {
                    if (obj.Password == obj.ConfirmPassword)
                    {
                        return "LightGreen";
                    }
                    else
                    {
                        return "LightCoral";
                    }
                }
                else
                {
                    return " ";
                }
            }
            //set
            //{
            //    obj.PasswordMatch = value;
            //}
        }
        public string UserIdValid
        {
            get
            {
                if (obj.UserId != null)
                {
                    if (obj.UserId.Length == 0)
                    {
                        return "User ID is a mandatory field!";
                    }
                    else if (!Regex.IsMatch(obj.UserId, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                    {
                        return "Please enter valid email id!";
                    }
                    else
                    {
                        return " ";
                    }
                }
                else
                {
                    return " ";
                }
            }
            //set
            //{
            //    obj.UserIdValid = value;
            //}
        }
        public string PwdValid
        {
            get
            {
                if (obj.Password != null && obj.ConfirmPassword != null)
                {
                    if (!Regex.IsMatch(obj.Password, @"[0-9]+")
                        && !Regex.IsMatch(obj.Password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]")
                        && obj.Password.Length < 6 && obj.Password.Length > 12)
                    {
                        return "Password length should be between 6 and 12. The password should contain atleast one special character and number.";
                    }
                    if (obj.Password.Length == 0)
                    {
                        return "Password is a mandatory field!";
                    }
                    if (obj.ConfirmPassword.Length == 0)
                    {
                        return "Confirm Password is a mandatory field!";
                    }
                }
                return " ";
            }
            //set
            //{
            //    obj.PwdValid = value;
            //}
        }
    }
}
