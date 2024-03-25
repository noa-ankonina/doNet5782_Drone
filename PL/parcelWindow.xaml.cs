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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for parcelWindow.xaml
    /// </summary>
    public partial class parcelWindow : Window
    {
        private BlApi.IBL bl;
        BO.Parcel p;
        public parcelWindow(string permission="", int id=0)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            p = new BO.Parcel();
           
            senderTxt.ItemsSource = bl.viewListCustomer().Select(x => x.ID);
            targetCmb.ItemsSource = bl.viewListCustomer().Select(x => x.ID);
            priorityCmb.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            WeightCmb.ItemsSource = Enum.GetValues(typeof(BO.WeightCategorie));

            p.sender = new BO.CustomerInParcel();
            p.target = new BO.CustomerInParcel();
            p.drone = new DroneInParcel();
            
            DataContext = p;
            idLbl.Visibility = Visibility.Hidden;
            idTxt.Visibility = Visibility.Hidden;
            sender.Visibility = Visibility.Hidden;
            targetTxt.Visibility = Visibility.Hidden;
            priorityTxt.Visibility = Visibility.Hidden;
            WeightTxt.Visibility = Visibility.Hidden;
            droneLbl.Visibility = Visibility.Hidden;
            droneTxt.Visibility = Visibility.Hidden;
            reqLbl.Visibility = Visibility.Hidden;
            requeTxt.Visibility = Visibility.Hidden;
            scheLbl.Visibility = Visibility.Hidden;
            scheTxt.Visibility = Visibility.Hidden;
            pickLbl.Visibility = Visibility.Hidden;
            pickTxt.Visibility = Visibility.Hidden;
            deliLbl.Visibility = Visibility.Hidden;
            deliTxt.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;

            if (permission == "user")
            {
                
                senderTxt.ItemsSource = bl.viewListCustomer().Where(x => x.ID == id).Select(x => x.ID) ;
                
            }

        }

        public parcelWindow(ParcelToList pr)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
            p = bl.findParcel(pr.ID);

            DataContext = p;
            idLbl.Visibility = Visibility.Visible;
            idTxt.Visibility = Visibility.Visible;
            idTxt.IsReadOnly = true;
            sender.Visibility = Visibility.Visible;
            sender.IsReadOnly = true;
            targetTxt.Visibility = Visibility.Visible;
            targetTxt.IsReadOnly = true;
            priorityTxt.Visibility = Visibility.Visible;
            priorityTxt.IsReadOnly = true;
            WeightTxt.Visibility = Visibility.Visible;
            WeightTxt.IsReadOnly = true;
            droneLbl.Visibility = Visibility.Visible;
            droneTxt.Visibility = Visibility.Visible;
            droneTxt.IsReadOnly = true;
            reqLbl.Visibility = Visibility.Visible;
            requeTxt.Visibility = Visibility.Visible;
            requeTxt.IsReadOnly = true;
            scheLbl.Visibility = Visibility.Visible;
            scheTxt.Visibility = Visibility.Visible;
            scheTxt.IsReadOnly = true;
            pickLbl.Visibility = Visibility.Visible;
            pickTxt.Visibility = Visibility.Visible;
            pickTxt.IsReadOnly = true;
            deliLbl.Visibility = Visibility.Visible;
            deliTxt.Visibility = Visibility.Visible;
            deliTxt.IsReadOnly = true;
            addBtn.Visibility = Visibility.Hidden;
            closeBtn.Visibility = Visibility.Hidden;
            senderTxt.Visibility = Visibility.Hidden;
            priorityCmb.Visibility = Visibility.Hidden;
            targetCmb.Visibility = Visibility.Hidden;
            WeightCmb.Visibility = Visibility.Hidden;
            senderLbl.Visibility = Visibility.Hidden;
            targetLbl.Visibility = Visibility.Hidden;
            showSenderBtn.Visibility = Visibility.Visible;
            showTargetBtn.Visibility = Visibility.Visible;
            if(pr.status==ParcelStatus.Match||pr.status==ParcelStatus.PickedUp)
                showDroneBtn.Visibility = Visibility.Visible;
           
        }


        public parcelWindow(int id)
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
           
            p = bl.findParcel(id);

            DataContext = p;
          
            idLbl.Visibility = Visibility.Visible;
            idTxt.Visibility = Visibility.Visible;
            idTxt.IsReadOnly = true;
            sender.Visibility = Visibility.Visible;
            sender.IsReadOnly = true;
            targetTxt.Visibility = Visibility.Visible;
            targetTxt.IsReadOnly = true;
            priorityTxt.Visibility = Visibility.Visible;
            priorityTxt.IsReadOnly = true;
            WeightTxt.Visibility = Visibility.Visible;
            WeightTxt.IsReadOnly = true;
            droneLbl.Visibility = Visibility.Visible;
            droneTxt.Visibility = Visibility.Visible;
            droneTxt.IsReadOnly = true;
            reqLbl.Visibility = Visibility.Visible;
            requeTxt.Visibility = Visibility.Visible;
            requeTxt.IsReadOnly = true;
            scheLbl.Visibility = Visibility.Visible;
            scheTxt.Visibility = Visibility.Visible;
            scheTxt.IsReadOnly = true;
            pickLbl.Visibility = Visibility.Visible;
            pickTxt.Visibility = Visibility.Visible;
            pickTxt.IsReadOnly = true;
            deliLbl.Visibility = Visibility.Visible;
            deliTxt.Visibility = Visibility.Visible;
            deliTxt.IsReadOnly = true;
            addBtn.Visibility = Visibility.Hidden;
            closeBtn.Visibility = Visibility.Hidden;
            senderTxt.Visibility = Visibility.Hidden;
            priorityCmb.Visibility = Visibility.Hidden;
            targetCmb.Visibility = Visibility.Hidden;
            WeightCmb.Visibility = Visibility.Hidden;
            senderLbl.Visibility = Visibility.Hidden;
            targetLbl.Visibility = Visibility.Hidden;
            showSenderBtn.Visibility = Visibility.Hidden;
            showTargetBtn.Visibility = Visibility.Hidden;
           
                showDroneBtn.Visibility = Visibility.Hidden;
          
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void droneslst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void up(object sender, RoutedEventArgs e)
        {

        }

        private void addBtn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(senderTxt.SelectedItem) == Convert.ToInt32(targetCmb.SelectedItem))
                    MessageBox.Show("Error! the sender and the target can't be same");
                else
                {
                    int x = bl.addParcel(Convert.ToInt32(senderTxt.SelectedItem), Convert.ToInt32(targetCmb.SelectedItem), (int)p.weight, (int)p.priority);
                    MessageBox.Show("the parcel successfully added,the ID of the parcel is:" + x);
                    p.sender = new BO.CustomerInParcel();
                    p.target = new BO.CustomerInParcel();
                    DataContext = p;
                    Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void closeBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.deleteParcel(p.ID);
                MessageBox.Show("The parcel was delete");
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void showDroneBtn_Click(object sender, RoutedEventArgs e)
        {
            new droneView(p.drone.ID).ShowDialog();
        }

        private void showSenderBtn_Click(object sender, RoutedEventArgs e)
        {
            new addCustomerWindoes(p.sender.ID).ShowDialog();
        }

        private void showTargetBtn_Click(object sender, RoutedEventArgs e)
        {
            new addCustomerWindoes(p.target.ID).ShowDialog();

        }
    }
}
