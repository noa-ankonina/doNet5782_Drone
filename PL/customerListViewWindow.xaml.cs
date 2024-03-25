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
using System.Data;
using System.Collections.ObjectModel;

using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for customerListView.xaml
    /// </summary>
    /// 
    public partial class customerListView : Window
    {
        private BlApi.IBL bl;

        private IEnumerable<BO.CustomerToList> lst;
      //new ObservableCollection<BO.Customer>();
        
       
        public customerListView()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            List<BO.CustomerToList> lst1 = new List<BO.CustomerToList>(bl.viewListCustomer());
            lst = lst1;
            DataContext = lst;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            new addCustomerWindoes().ShowDialog();
            lst = new List<BO.CustomerToList>(bl.viewListCustomer());
            DataContext = lst;
        }

        private void Image_TouchEnter(object sender, TouchEventArgs e)
        {

        }

        private void customerLstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (customerLstView.SelectedItem == null)
                MessageBox.Show("Error! choose an item");
            else
            {
                CustomerToList c = new CustomerToList();
                c = (CustomerToList)customerLstView.SelectedItem;
                new addCustomerWindoes(c, "maneger").ShowDialog();
                lst = new List<BO.CustomerToList>(bl.viewListCustomer());
                DataContext = lst;
            }
        }

        private void serchTxtBx_KeyUp(object sender, KeyEventArgs e)
        {
            IEnumerable<BO.CustomerToList> p = new List<BO.CustomerToList>();
            p = bl.viewListCustomer().Where(x => Convert.ToString(x.ID).StartsWith(serchTxtBx.Text));
            lst = new List<BO.CustomerToList>(p);
            DataContext = lst;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void checkInputdigit(KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.Back ||
e.Key == Key.Delete || e.Key == Key.CapsLock || e.Key == Key.LeftShift
|| e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl
|| e.Key == Key.LeftAlt || e.Key == Key.RightAlt || e.Key == Key.LWin ||
e.Key == Key.RWin || e.Key == Key.System || e.Key == Key.Left || e.Key == Key.Up
|| e.Key == Key.Down || e.Key == Key.Right) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return;
            e.Handled = true;
            MessageBox.Show("The input must be only digits");
        }

        private void serchTxtBx_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputdigit(e);
        }
    }
}
