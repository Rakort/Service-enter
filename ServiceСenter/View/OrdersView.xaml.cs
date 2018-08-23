using System;
using System.Windows;
using System.Windows.Controls;
using ServiceСenter.ViewModel;

namespace ServiceСenter.View
{
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
            BtnCansel.Click += (s, e) => BtnFilter.Command.Execute(null);
        }

        private void CBDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SpDate == null) return;
            SpDate.Visibility = Visibility.Collapsed;
            TbDate.Visibility = Visibility.Visible;

            var d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime d1 = new DateTime(), d2 = new DateTime();
            switch (CBDate.SelectedIndex)
            {
                case 0:
                    TbDate.Text = "За все время"; 
                    d1 = new DateTime();
                    d2 = new DateTime(9999,12,31);
                    break;
                case 1:
                    d1 = d2 = d;
                    break;
                case 2:
                    d1 = d2 = d.AddDays(-1);
                    break;
                case 3:
                    d1 = d.AddDays(((((int) d.DayOfWeek) - 1)*(-1)));
                    d2 = d.AddDays(7 - (int) d.DayOfWeek);
                    break;
                case 4:
                    d1 = new DateTime(d.Year,d.Month,1);
                    d2 = (d.Month != 12)?new DateTime(d.Year, d.Month+1, 1).AddDays(-1) : new DateTime(d.Year, 12, 31);
                    break;
                case 5:
                    d1 = d.AddDays(((((int) d.DayOfWeek) - 1)*(-1))-7);
                    d2 = d.AddDays(7 - (int) d.DayOfWeek-7);
                    break;
                case 6:
                    d1 = new DateTime(d.Year, d.Month-1, 1);
                    d2 = new DateTime(d.Year, d.Month, 1).AddDays(-1);
                    break;
                case 7:
                    if (d1 == new DateTime())
                        d1 = d2 = d;
                    SpDate.Visibility = Visibility.Visible;
                    TbDate.Visibility = Visibility.Collapsed;
                    break;
            }

            if (d1 != new DateTime())
            {
                d2 = d2.AddDays(1).AddMilliseconds(-1);
                TbDate.Text = (d1.Date == d2.Date)
                    ? d1.ToShortDateString()
                    : String.Format("{0} - {1}", d1.ToShortDateString(), d2.ToShortDateString());
                DtStart.SelectedDate = d1;
                DtEnd.SelectedDate = d2;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CbClient.SelectedIndex = 0;
            CbExecutor.SelectedIndex = 0;
            CbFabricator.SelectedIndex = 0;
            CbModel.SelectedIndex = 0;
            CbState.SelectedIndex = 0;
            CbType.SelectedIndex = 0;
            CBDate.SelectedIndex = 0;
        }
        

        
    }
}
