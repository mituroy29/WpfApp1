using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModelLayer;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SignUpScreen.xaml
    /// </summary>
    public partial class SignUpScreen : Window
    {
        private RegisterUser user = new RegisterUser();
        private bool isValidUsrId;
        private bool isValidPwd;
        private bool isPwdMatched;
        private bool isValidUsrType;
        private string str;
        public SignUpScreen()
        {
            InitializeComponent();
            isValidUsrId = true;
            isPwdMatched = true;
            isValidPwd = true;
            isValidUsrType = true;
            str = string.Empty;

            List<string> lstUserType = new List<string>();
            lstUserType.Add("Student");
            lstUserType.Add("Admin");

            userType.ItemsSource = lstUserType;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            #region
            errMsg1.Text = string.Empty;
            errMsg2.Text = string.Empty;
            errMsg3.Text = string.Empty;
            errMsg4.Text = string.Empty;

            if (usrId.Text.Length == 0)
            {
                errMsg1.Visibility = Visibility.Visible;
                errMsg1.Text = "User ID is a mandatory field!";
                usrId.BorderBrush = Brushes.Red;
                isValidUsrId = false;
            }
            if (usrId.Text.Length != 0 && !Regex.IsMatch(usrId.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                isValidUsrId = false;
                usrId.BorderBrush = Brushes.Red;
                errMsg1.Visibility = Visibility.Visible;
                errMsg1.Text = "Enter valid email id. *example@email.com";
                usrId.Clear();
            }
            if (pwd1.Password.Length == 0)
            {
                isValidPwd = false;
                errMsg2.Visibility = Visibility.Visible;
                errMsg2.Text = str + "Password is a mandatory field!";
                pwd1.BorderBrush = Brushes.Red;
            }
            if (pwd1.Password.Length != 0)
            {
                if (Regex.IsMatch(pwd1.Password, @"[0-9]") == false
                    || Regex.IsMatch(pwd1.Password, @"[~`!@#$%^&*()-+=|\{}':;.,<>/?]") == false)
                {
                    isValidPwd = false;
                    errMsg2.Visibility = Visibility.Visible;
                    errMsg2.Text = "Password should contain atleast one special character and number.";
                    pwd1.BorderBrush = Brushes.Red;
                    pwd1.Clear();
                }
                if (pwd1.Password.Length < 6 || pwd1.Password.Length > 12)
                {
                    isValidPwd = false;
                    errMsg2.Visibility = Visibility.Visible;
                    errMsg2.Text = "Password length should be between 6 and 12.";
                    pwd1.BorderBrush = Brushes.Red;
                    pwd1.Clear();
                }
            }
            if (pwd2.Password.Length == 0)
            {
                isValidPwd = false;
                errMsg3.Visibility = Visibility.Visible;
                errMsg3.Text = str + "Confirm Password is a mandatory field!";
                pwd2.BorderBrush = Brushes.Red;
            }
            if (pwd1.Password.Length != 0 && pwd2.Password.Length != 0 && pwd1.Password != pwd2.Password)
            {
                isPwdMatched = false;
                pwd1.BorderBrush = Brushes.Red;
                pwd2.BorderBrush = Brushes.Red;
                errMsg3.Visibility = Visibility.Visible;
                errMsg3.Text = str + "Passwords mismatch!";
            }
            if (userType.SelectedItem == null)
            {
                errMsg4.Visibility = Visibility.Visible;
                errMsg4.Text = "User Type is a mandatory field!";
            }
            if (isValidUsrId && isValidPwd && isPwdMatched && isValidUsrType)
            {
                if (MessageBox.Show("Account has been registered! \n You can login now.", "Information", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    grdSignup.Visibility = Visibility.Hidden;
                    grdLogin.Visibility = Visibility.Visible;
                }
            }
            #endregion
        }
        private void MinImg_MouseEnter(object sender, MouseEventArgs e)
        {
            minBrdr.BorderThickness = new Thickness(1.0);
            minBrdr.BorderBrush = Brushes.Gray;
            minImg.Height = 18.0;
            minImg.Width = 18.0;
        }

        private void MinImg_MouseLeave(object sender, MouseEventArgs e)
        {
            minBrdr.BorderThickness = new Thickness(0.0);
            minBrdr.BorderBrush = Brushes.Transparent;
            minImg.Height = 20.0;
            minImg.Width = 20.0;
        }

        private void MinImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ClsImg_MouseEnter(object sender, MouseEventArgs e)
        {
            clsBrdr.BorderThickness = new Thickness(1.0);
            clsBrdr.BorderBrush = Brushes.Gray;
            clsImg.Height = 18.0;
            clsImg.Width = 18.0;
        }

        private void ClsImg_MouseLeave(object sender, MouseEventArgs e)
        {
            clsBrdr.BorderThickness = new Thickness(0.0);
            clsBrdr.BorderBrush = Brushes.Transparent;
            clsImg.Height = 20.0;
            clsImg.Width = 20.0;
        }

        private void ClsImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CrtAcntLink_Click(object sender, RoutedEventArgs e)
        {
            grdLogin.Visibility = Visibility.Hidden;
            usrId.Text = string.Empty;
            pwd1.Password = string.Empty;
            pwd2.Password = string.Empty;
            userType.SelectedItem = null;
            grdSignup.Visibility = Visibility.Visible;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            errMsg1.Text = string.Empty;
            errMsg2.Text = string.Empty;
            if (logUsr.Text == null)
            {
                logUsr.BorderBrush = Brushes.Red;
                errMsg1.Text = "UserID is required for login";
            }
            if (logPwd.Password == null)
            {
                logPwd.BorderBrush = Brushes.Red;
                errMsg2.Text = "Password is required for login";
            }
            if (logUsr.Text != null&& logPwd.Password != null)
            {
                if (user.AuthenticateUser(logUsr.Text, logPwd.Password))
                {
                    string strName = logUsr.Text;
                    string strUsrType = user.SelectUserType(strName);
                    //display main content here
                    if (MessageBox.Show("Login successful!!!", "Login status", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        MainScreen main = new MainScreen(strUsrType, logUsr.Text.Substring(0, strName.IndexOf("@")));
                        this.Close();
                        main.Show();
                    }
                }
                else
                {
                    if (MessageBox.Show("Login failed!!! User doesn't exist", "Login status", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                    {
                        logUsr.Text = string.Empty;
                        logPwd.Password = string.Empty;
                        logUsr.BorderBrush = Brushes.Red;
                        logPwd.BorderBrush = Brushes.Red;
                    }
                }
            }
        }
        private void BtnBck_Click(object sender, RoutedEventArgs e)
        {
            grdSignup.Visibility = Visibility.Hidden;
            grdLogin.Visibility = Visibility.Visible;
        }
    }
}
