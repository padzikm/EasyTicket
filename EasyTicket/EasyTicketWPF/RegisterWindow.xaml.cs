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
using System.Data.Entity.Validation;
using EasyTicketLogic;
using System.Threading;
using System.Globalization;
using System.Collections.ObjectModel;

namespace EasyTicketWPFClient
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private Register logicController;
        private User user = new User();

        public RegisterWindow(Register controller)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            logicController = controller;
        }

        async public void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    user = new User(name.Text, lastname.Text, login.Text, password.Password, email.Text, (bool)newsCheckBox.IsChecked);
                    signUpButton.IsEnabled = false;
                    if (await logicController.RegisterUserAsync(user))
                    {
                        MessageBox.Show("Successfully registered!");
                        this.Close();
                    }
                    else
                        MessageBox.Show("Login already exists in database!");

                }
                catch (ValidationException ex)
                {
                    FillErrors(ex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (ValidationException ex)
            {
                FillErrors(ex);
            }
            signUpButton.IsEnabled = true;
        }

        private void FillErrors(ValidationException ex)
        {
            StringBuilder message = new StringBuilder();
            message.Append(ex.ValidationResults["User"].ContainsKey("Name") ? ex.ValidationResults["User"]["Name"] + "\n": "");
            message.Append(ex.ValidationResults["User"].ContainsKey("Lastname") ? ex.ValidationResults["User"]["Lastname"] + "\n" : "");
            message.Append(ex.ValidationResults["User"].ContainsKey("Login") ? ex.ValidationResults["User"]["Login"] + "\n" : "");
            message.Append(ex.ValidationResults["User"].ContainsKey("Password") ? ex.ValidationResults["User"]["Password"] + "\n" : "");
            message.Append(ex.ValidationResults["User"].ContainsKey("Email") ? ex.ValidationResults["User"]["Email"] + "\n" : "");
            message.Append(ex.ValidationResults["User"].ContainsKey("Date") ? ex.ValidationResults["User"]["Date"] + "\n" : "");

            MessageBox.Show(message.ToString());
        }

        private Dictionary<string, Dictionary<string, string>> CreateException(string property, string message)
        {
            var dic = new Dictionary<string, Dictionary<string, string>>();
            var dic2 = new Dictionary<string, string>();
            dic2.Add(property, message);
            dic.Add("User", dic2);
            return dic;
        }
    }

}
