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
    /// Interaction logic for stattionListView.xaml
    /// </summary>
    public partial class stattionListView : Window
    {
        private BlApi.IBL bl;
       
         private List<BO.StationToList> lst ;
       
        public stattionListView()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            // _myCollection = (ObservableCollection<BO.StationToList>)bl.veiwListStation();
          lst = new List<BO.StationToList>(bl.veiwListStation());
            DataContext = lst;
            clearBtn.Visibility = Visibility.Hidden;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            new stationWindow().ShowDialog();
            lst = new List<BO.StationToList>(bl.veiwListStation());
            DataContext = lst;
            if (clearBtn.Visibility == Visibility.Visible)
            {
                IEnumerable<BO.StationToList> query = bl.veiwListStation().OrderBy(x => x.availableChargeSlots);
                DataContext = query;
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void stationLst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (stationLst.SelectedItem == null)
                MessageBox.Show("Error! choose an item");
            else
            {
                new stationWindow((BO.StationToList)stationLst.SelectedItem).ShowDialog();
                lst = new List<BO.StationToList>(bl.veiwListStation());
                DataContext = lst;
                if (clearBtn.Visibility == Visibility.Visible)
                {
                    IEnumerable<BO.StationToList> query = bl.veiwListStation().OrderBy(x => x.availableChargeSlots);
                    DataContext = query;
                }
            }
        }

        private void sortBtn_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<BO.StationToList> query = bl.veiwListStation().OrderBy(x => x.availableChargeSlots);
            DataContext = query;
            clearBtn.Visibility = Visibility.Visible;
            sortBtn.Visibility = Visibility.Hidden;
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            lst = new List<BO.StationToList>(bl.veiwListStation());
            DataContext = lst;
            clearBtn.Visibility = Visibility.Hidden;
            sortBtn.Visibility = Visibility.Visible;
        }

        private void serchTxtBx_KeyUp(object sender, KeyEventArgs e)
        {

            IEnumerable<BO.StationToList> p = new List<BO.StationToList>();
            p = bl.veiwListStation().Where(x => Convert.ToString(x.ID).StartsWith(serchTxtBx.Text));
            lst = new List<BO.StationToList>(p);
            DataContext = lst;
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
