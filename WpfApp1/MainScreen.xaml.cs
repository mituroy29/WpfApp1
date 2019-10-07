using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        string strUserType = string.Empty;
        AddStudent objStud = new AddStudent();
        StudentResultDetails objStudMrks = new StudentResultDetails();
        System.Data.DataTable dt = new System.Data.DataTable();
        public MainScreen()
        {
            InitializeComponent();
            //WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        public MainScreen(string usrType, string usrName)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Usr.Text = usrName;
            strUserType = usrType;
            if (usrType == "Student")
            {
                //menuStud.Text = "Personal Details";
                menuStud.Visibility = Visibility.Hidden;
                stStud.Visibility = Visibility.Hidden;
                mnStudPrsnl.Visibility = Visibility.Hidden;
                mnStudMrks.Visibility = Visibility.Hidden;
                mnStudRprtCrd.Visibility = Visibility.Hidden;
                mnStud.Items.Remove(stMenu);

                usrLogo.Source = new BitmapImage(new Uri("/Images/edu.png", UriKind.Relative));

                //stMenu.Items.RemoveAt(1);
                //stMenu.Items.RemoveAt(0);
                rtMenu.Items.RemoveAt(0);
            }
            if (usrType == "Admin")
            {
                usrLogo.Source = new BitmapImage(new Uri("/Images/admin.png", UriKind.Relative));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void MnStudPrsnl_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Source = new Uri("StudentPersonalDetailsPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void MnStudMrks_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Source = new Uri("StudentMarkDetailsPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void MnStudRprtCrd_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Source = new Uri("StudentDetailsPage.xaml", UriKind.RelativeOrAbsolute);
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
            this.DragMove();
        }
        private void LnkLogout_Click(object sender, RoutedEventArgs e)
        {
            SignUpScreen su = new SignUpScreen();
            su.Show();
            this.Close();
        }

        private void RtMenu_Click(object sender, RoutedEventArgs e)
        {
            if (strUserType == "Student")
            {
                frmMain.Source = new Uri("StudentDetailsPage.xaml", UriKind.RelativeOrAbsolute);
            }
        }
    }
}