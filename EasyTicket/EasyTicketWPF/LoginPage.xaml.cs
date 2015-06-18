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
using EasyTicketLogic;

namespace EasyTicketWPFClient
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private Register logicController;

        public User CurrentUser { get; set; }

        public LoginPage(Register controller)
        {
            InitializeComponent();
            logicController = controller;
        }

        private void HyperlinkClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            var register = new RegisterWindow(logicController);
            register.ShowDialog();
        }

        async private void LogInButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                logInButton.IsEnabled = false;
                if (await logicController.LoginUserAsync(login.Text, password.Password))
                {
                    CurrentUser = logicController.GetCurrentUser();
                    MessageBox.Show("Successfully logon!");
                    this.Close();
                }
                else
                    MessageBox.Show("Invalid login and password!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed access to database!");
            }
            logInButton.IsEnabled = true;
        }
    }
}
