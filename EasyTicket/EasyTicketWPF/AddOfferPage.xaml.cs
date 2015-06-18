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
    /// Interaction logic for AddOfferPage.xaml
    /// </summary>
    public partial class AddOfferPage : Window
    {
        private Register logicController;

        public AddOfferPage(Register controller)
        {
            InitializeComponent();
            logicController = controller;
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {            
            try
            {
                DateTime? departureDate = depDate.SelectedDate;
                DateTime? arrivalDate = arrDate.SelectedDate;
                Address departureAddress = new Address(depAddress.Text);
                Address arrivalAddress = new Address(arrAddress.Text);
                decimal ticketPrice = decimal.Parse(price.Text);
                Ticket ticket = new Ticket(ticketPrice, departureAddress, departureDate, arrivalAddress, arrivalDate);
                try
                {
                    LinkedList<Ticket> list = new LinkedList<Ticket>();
                    list.AddLast(ticket);
                    Offer offer = new Offer(list, 1);
                    logicController.AddOffer(offer);
                    MessageBox.Show("Offer was successfully added!");
                    this.Close();
                }
                catch (ValidationException ex)
                {
                    MessageBox.Show("All fields must be filled, according to given schema!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to access database!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("All fields must be filled, according to given schema!");
            }
        }
    }
}
