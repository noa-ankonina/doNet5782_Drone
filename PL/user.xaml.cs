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

namespace PL
{
    /// <summary>
    /// Interaction logic for user.xaml
    /// </summary>
    public partial class user : Window
    {
        private BlApi.IBL bl;
        public user()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new addCustomerWindoes().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer c = new BO.Customer();
                var cs = bl.viewListCustomer().ToList();
                if (!cs.Exists(x => x.ID == Convert.ToInt32(idTxtBlk.Text)))
                    MessageBox.Show("Error! the customer not exist or deleted");
                else
                {
                    c = bl.findCustomer(Convert.ToInt32(idTxtBlk.Text));
                    new addCustomerWindoes(c, "user").ShowDialog();
                }
                this.Close();
            }

            catch(Exception )
            {
                MessageBox.Show("the id wrong");
                idTxtBlk.Clear();
            }
        }
    }
}
