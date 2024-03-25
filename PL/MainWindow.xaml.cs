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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // BlApi.BL bl;
        BlApi.IBL bl = BlApi.BlFactory.GetBl();
        public MainWindow()
        {
            InitializeComponent();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // new Try().ShowDialog();
            new passwordWindow(bl).ShowDialog();
           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new addCustomerWindoes().ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void customerBtn_Click(object sender, RoutedEventArgs e)
        {
            new user().ShowDialog();
        }
    }
}
