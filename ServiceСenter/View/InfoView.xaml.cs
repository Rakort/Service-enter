using System.Windows;
using System.Windows.Controls;

namespace ServiceСenter.View
{
    public partial class InfoView : UserControl
    {
        public InfoView()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        
    }
}
