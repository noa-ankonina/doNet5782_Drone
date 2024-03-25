using System;
using System.Windows;
using System.Windows.Input;

namespace PL
{


    /// <summary>
    /// Interaction logic for stationWindow.xaml
    /// </summary>
    public partial class stationWindow : Window
    {
        private BlApi.IBL bl;
        BO.Station s;
        public stationWindow()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            s = new BO.Station();
            s.location = new BO.Location();
            DataContext = s;
            lstLbl.Visibility = Visibility.Hidden;
            droneslst.Visibility = Visibility.Hidden;
            updateBtn.Visibility = Visibility.Hidden;
            // updateGrid.Visibility = Visibility.Hidden;
        }


        public stationWindow(BO.StationToList st)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            s = new BO.Station();
            s = bl.findStation(st.ID);
            //s.location = new BO.Location();
            DataContext = s;
            droneslst.ItemsSource = s.dronesInChargeList;
            idTxt.IsReadOnly = true;
            longitudtTxt.IsReadOnly = true;
            latitudeTxt.IsReadOnly = true;
            addBtn.Visibility = Visibility.Hidden;
            closeBtn.Content = "close";
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addStation(s);
                MessageBox.Show("the station successfully added");
                s = new BO.Station();
                s.location = new BO.Location();
                DataContext = s;
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void up(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateStation(s.ID, s.name, s.chargeSlots);
                MessageBox.Show("the station successfully updated");
                // s = new BO.Station();
                s = bl.findStation(s.ID);
                //s.location = new BO.Location();
                DataContext = s;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void droneslst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (droneslst.SelectedItem == null)
                MessageBox.Show("Error! choose an item");
            else
            {
                new droneView((BO.DroneInCharge)droneslst.SelectedItem).ShowDialog();
            }
        }


        private void checkInputdigit(KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.Back ||
 e.Key == Key.Delete || e.Key == Key.CapsLock || e.Key == Key.LeftShift
 || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl
 || e.Key == Key.LeftAlt || e.Key == Key.RightAlt || e.Key == Key.LWin || 
 e.Key == Key.RWin || e.Key == Key.System || e.Key == Key.Left || e.Key == Key.Up 
 ||e.Key == Key.Down || e.Key == Key.Right) return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return;
            e.Handled = true;
            MessageBox.Show("The input must be only digits");
        }

        private void checkInputdigitForFraction(KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.Back ||
 e.Key == Key.Delete || e.Key == Key.CapsLock || e.Key == Key.LeftShift
 || e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl
 || e.Key == Key.LeftAlt || e.Key == Key.RightAlt || e.Key == Key.LWin ||
 e.Key == Key.RWin || e.Key == Key.System || e.Key == Key.Left || e.Key == Key.Up
 || e.Key == Key.Down || e.Key == Key.Right||e.Key==Key.OemPeriod) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return;
            e.Handled = true;
            MessageBox.Show("The input must be only digits");
        }

        private void checkInputLetters(KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.Back ||
e.Key == Key.Delete || e.Key == Key.CapsLock || e.Key == Key.LeftShift
|| e.Key == Key.RightShift || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl
|| e.Key == Key.LeftAlt || e.Key == Key.RightAlt || e.Key == Key.LWin ||
e.Key == Key.RWin || e.Key == Key.System || e.Key == Key.Left || e.Key == Key.Up
|| e.Key == Key.Down || e.Key == Key.Right) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsLetter(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return;
            e.Handled = true;
            MessageBox.Show("The input must be only letters");
        }

        private void idTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputdigit(e);
        }

        private void nameTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputLetters(e);
        }

        private void longitudtTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputdigitForFraction(e);
        }

        private void latitudeTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputdigitForFraction(e);
        }

        private void slotsTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            checkInputdigit(e);
        }
    }
}
