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
using System.Collections;
using BO;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for droneListView.xaml
    /// </summary>
    public partial class droneListView : Window
    {

        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        public IEnumerable<DroneToList> myCollection = new List<DroneToList>();
        public droneListView()
        {
            InitializeComponent();
            statusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            weightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategorie));
            myCollection = bl.getAllDrones();
            DronesListView.DataContext = myCollection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new addDroneWindow().ShowDialog();
            myCollection = bl.getAllDrones();
            DronesListView.ItemsSource = myCollection;
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mouse(object sender, MouseButtonEventArgs e)
        {
            if (DronesListView.SelectedItem == null)
                MessageBox.Show("Error! choose an item");
            else
            {
                DroneToList dr = new DroneToList();
                dr = (DroneToList)DronesListView.SelectedItem;
                new droneView(dr,this).ShowDialog();
                myCollection = bl.getAllDrones();
                DronesListView.DataContext = myCollection;
            }
        }

        private void filtering(object sender, SelectionChangedEventArgs e)
        {

            if (statusSelector.SelectedItem != null)
                DronesListView.ItemsSource = bl.droneFilterStatus((DroneStatus)statusSelector.SelectedItem);
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = bl.getAllDrones();
            weightSelector.Text = "";
            statusSelector.Text = "";
        }

        private void DronesListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void weightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (weightSelector.SelectedItem != null)
                DronesListView.ItemsSource = bl.droneFilterWheight((WeightCategorie)weightSelector.SelectedItem);
        }

        private void groupingBtn_Click(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = bl.getAllDrones();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("status ");
            view.GroupDescriptions.Add(groupDescription);

        }

        public  void Worker_ProgressChanged2(object sender, ProgressChangedEventArgs e)
        {
            myCollection = bl.getAllDrones();
         
            DronesListView.ItemsSource = myCollection;
        }

    }
}

