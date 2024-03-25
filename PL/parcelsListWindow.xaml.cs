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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for parcelsListWindow.xaml
    /// </summary>
    public partial class parcelsListWindow : Window
    {
        private BlApi.IBL bl;
        private IEnumerable<BO.ParcelToList> lst;
        public parcelsListWindow()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            lst = new List<BO.ParcelToList>(bl.getAllParcels());
            DataContext = lst;
            statusCmb.ItemsSource= Enum.GetValues(typeof(BO.ParcelStatus));
        }

        private void statusCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterByStatus.IsChecked == true)
            {
                IEnumerable<BO.ParcelToList> p = new List<BO.ParcelToList>();
                p = bl.getAllParcels().Where(x => x.status == (BO.ParcelStatus)statusCmb.SelectedItem);
                lst = new List<BO.ParcelToList>(p);
                parcelistView.ItemsSource = p;
            }
            else
            {
                lst = new List<BO.ParcelToList>(bl.getAllParcels());
                DataContext = lst;
            }
        }

        private void serchTxtBx_KeyUp(object sender, KeyEventArgs e)
        {
            IEnumerable<BO.ParcelToList> p = new List<BO.ParcelToList>();
            p = bl.getAllParcels().Where(x =>Convert.ToString(x.ID).StartsWith(serchTxtBx.Text));
            lst = new List<BO.ParcelToList>(p);
            DataContext = lst;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            new parcelWindow().ShowDialog();
            lst = new List<BO.ParcelToList>();
            parcelistView.ItemsSource = bl.getAllParcels();
        }

        private void parcelistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (parcelistView.SelectedItem == null)
                MessageBox.Show("Error! choose an item");
            else
            {
                new parcelWindow((BO.ParcelToList)parcelistView.SelectedItem).ShowDialog();
                lst = new List<BO.ParcelToList>(bl.getAllParcels());
                DataContext = lst;
            }

        }

        private void dateCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterByDates.IsChecked == true)
            {
                IEnumerable<BO.ParcelToList> p = new List<BO.ParcelToList>();
                p = bl.getAllParcels();
                if (dateCmb.SelectedIndex == 0)
                    p = bl.getAllParcels().Where(x => bl.findParcel(x.ID).requested > DateTime.Now.AddDays(-1));
                if (dateCmb.SelectedIndex == 1)
                    p = bl.getAllParcels().Where(x => bl.findParcel(x.ID).requested > DateTime.Now.AddDays(-7));
                if (dateCmb.SelectedIndex == 2)
                    p = bl.getAllParcels().Where(x => bl.findParcel(x.ID).requested > DateTime.Now.AddMonths(-1));
                if (dateCmb.SelectedIndex == 3)
                    p = bl.getAllParcels().Where(x => bl.findParcel(x.ID).requested > DateTime.Now.AddYears(-1));
                parcelistView.ItemsSource = p;
            }
            else
                parcelistView.ItemsSource = bl.getAllParcels();
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            parcelistView.ItemsSource = bl.getAllParcels();
        }

        private void normalView_Checked(object sender, RoutedEventArgs e)
        {
            parcelistView.ItemsSource = bl.getAllParcels();   
        }

        private void groupingViewC_Checked(object sender, RoutedEventArgs e)
        {
            parcelistView.ItemsSource = bl.getAllParcels();
           
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(parcelistView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("targetName");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void groupingViewS_Checked(object sender, RoutedEventArgs e)
        {
            parcelistView.ItemsSource = bl.getAllParcels();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(parcelistView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("senderName");
            view.GroupDescriptions.Add(groupDescription);
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


