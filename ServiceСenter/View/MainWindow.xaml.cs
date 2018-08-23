using System.Windows;
using System.Windows.Controls;
using ServiceСenter.Model;
using ServiceСenter.ViewModel;

namespace ServiceСenter.View
{
    public partial class MainWindow : Window
    {
        private UserControl userControl, addedWindow;
        public MainWindow()
        {
            InitializeComponent();
            ShowWin.mainWindow = this;
            
            ShowWin.ShowOrders();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowOrders();
        }

        public void ShowUserControl(UserControl newUserControl)
        {
            MainGrid.Children.Remove(addedWindow);
            StackPanel.Children.Remove(userControl);

            userControl = newUserControl;
            userControl.VerticalAlignment = VerticalAlignment.Stretch;
            userControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            
            StackPanel.Children.Add(userControl);
        }

        public void ShowAddedWindow(UserControl addedWin)
        {
            MainGrid.Children.Remove(addedWindow);
            addedWindow = addedWin;
            MainGrid.Children.Add(addedWin);
            Grid.SetColumn(addedWin, 1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowDictionaries();
        }

        
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ShowWin.ShowInfo();
        }
        


    }
}
