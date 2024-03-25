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
    /// Interaction logic for companyOption.xaml
    /// </summary>
    public partial class companyOption : Window
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();


       

        public companyOption(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }


        public companyOption()
        {
            InitializeComponent();
        }

        private void drone_Click(object sender, RoutedEventArgs e)
        {
            new droneListView().Show();
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            new customerListView().ShowDialog();
        }

        private void station_Click(object sender, RoutedEventArgs e)
        {
            new stattionListView().ShowDialog();
        }

        private void station_Click_1(object sender, RoutedEventArgs e)
        {
            new stattionListView().ShowDialog();

        }

        private void parcel_Click(object sender, RoutedEventArgs e)
        {
            new parcelsListWindow().ShowDialog();
        }
    }
}
