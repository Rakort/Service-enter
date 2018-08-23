using System;
using System.Windows;
using System.Windows.Controls;
using ServiceСenter.Commands;
using ServiceСenter.ViewModel;

namespace ServiceСenter.View
{
    public partial class NewOrderView : UserControl
    {
        public NewOrderView()
        {
            InitializeComponent();
            BtnAddOrder.Click += BtnAddOrderOnClick;
        }

        private void BtnAddOrderOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            (BtnAddOrder.Command as SimpleCommand).IsExecute = !string.IsNullOrEmpty(CbTypeDevice.Text) && !string.IsNullOrEmpty(CbFabricator.Text) && !string.IsNullOrEmpty(CbModel.Text);

            LblTypeDevice.Visibility = string.IsNullOrEmpty(CbTypeDevice.Text) ? Visibility.Visible : Visibility.Collapsed;
            LblFabricator.Visibility = string.IsNullOrEmpty(CbFabricator.Text) ? Visibility.Visible : Visibility.Collapsed;
            LblModel.Visibility = string.IsNullOrEmpty(CbModel.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void cbMalfunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dc = (this.DataContext as NewOrderViewModel);
            if (dc.Malfunction != String.Empty)
                dc.Malfunction += ", ";
            dc.Malfunction += cbMalfunction.SelectedValue;
        }

        private void cbEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dc = (this.DataContext as NewOrderViewModel);
            if (dc.Equipment != String.Empty)
                dc.Equipment += ", ";
            dc.Equipment += cbEquipment.SelectedValue;
        }

        private void cbAppearance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dc = (this.DataContext as NewOrderViewModel);
            if (dc.Appearance != String.Empty)
                dc.Appearance += ", ";
            dc.Appearance += cbAppearance.SelectedValue;
        }
        
    }
}
