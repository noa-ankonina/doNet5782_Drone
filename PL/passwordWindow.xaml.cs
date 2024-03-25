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
    /// Interaction logic for passwordWindow.xaml
    /// </summary>
    public partial class passwordWindow : Window
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        public passwordWindow()
        {
            InitializeComponent();
        }

        public passwordWindow(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        //private void ok(object sender, KeyEventArgs e)
        //{
        //    if (pas.Password != "5782")
        //        MessageBox.Show("the password is incorrect");
        //    else
        //    new companyOption(bl).ShowDialog();
        //}

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (pas.Password != "5782")
            {
                MessageBox.Show("the password is incorrect");
                pas.Clear();
            }
            else
            {
                new companyOption(bl).ShowDialog();
                this.Close();
            }

        }
    }
    }

