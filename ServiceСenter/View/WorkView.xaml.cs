using System;
using ServiceСenter.Model;
using ServiceСenter.ViewModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ServiceСenter.View
{
    /// <summary>
    /// Логика взаимодействия для WorkView.xaml
    /// </summary>
    public partial class WorkView : UserControl
    {
        private Orders _order;
        public WorkView(Orders orders)
        {
            InitializeComponent();
            _order = orders;
            MainGrid.DataContext = new NewOrderViewModel(orders);
            SpWork.DataContext = new WorkViewModel(orders);
            SpTotalDate.DataContext = SpWork.DataContext;
            SpHistory.DataContext = new HistoryModelView(orders);

            RoutedEventHandler a = (s, e) =>
            {
                var t = new Thread(() =>
                {
                    Thread.Sleep(10);
                    Dispatcher.Invoke(new Action(ChangeState));
                });
                t.Start();
            };
            ChangeState();
            BtnSog.Click += a; BtnSog.Click += (s, e) => SQL.AddHistory(orders.IdOrder, "Заказ отправлен на согласование");
            BtnOj.Click += a; BtnOj.Click += (s, e) => SQL.AddHistory(orders.IdOrder, "Заказ переведен в статус Ожидания");
            BtnGot.Click += a; BtnGot.Click += (s, e) => SQL.AddHistory(orders.IdOrder, "Заказ готов для выдачи");
            BtnDor.Click += a; BtnDor.Click += (s, e) => SQL.AddHistory(orders.IdOrder, "Возврат в статус В работе");
            BtnOjOk.Click += a; BtnOjOk.Click += (s, e) => SQL.AddHistory(orders.IdOrder, "Запчасти поступили");
            BtnSogNo.Click += a; BtnSogNo.Click += (s, e) => SQL.AddHistory(orders.IdOrder, "Отказ от ремонта");
            BtnSogOk.Click += a; BtnSogOk.Click += (s, e) => SQL.AddHistory(orders.IdOrder, "Ремонт согласован");
            BtnVidan.Click += a; BtnVidan.Click += (s, e) => SQL.AddHistory(orders.IdOrder, "Заказ выдан");
        }

        private void ChangeState()
        {
            switch (LblState.Content.ToString())
            {
                case "В работе":
                    SpTotal.Visibility = Visibility.Collapsed;
                    BtnSog.Visibility = Visibility.Visible;
                    BtnOj.Visibility = Visibility.Visible;
                    BtnGot.Visibility = Visibility.Visible;
                    BtnDor.Visibility = Visibility.Collapsed;

                    BtnOjOk.Visibility = Visibility.Collapsed;
                    BtnSogNo.Visibility = Visibility.Collapsed;
                    BtnSogOk.Visibility = Visibility.Collapsed;
                       
                    break;
                case "На согласовании":
                    SpTotal.Visibility = Visibility.Collapsed;
                    BtnSog.Visibility = Visibility.Collapsed;
                    BtnOj.Visibility = Visibility.Collapsed;
                    BtnGot.Visibility = Visibility.Collapsed;
                    BtnDor.Visibility = Visibility.Collapsed;
                    BtnSogNo.Visibility = Visibility.Visible;
                    BtnSogOk.Visibility = Visibility.Visible;
                    break;
                case "Ждет запчасть":
                    SpTotal.Visibility = Visibility.Collapsed;
                    BtnSog.Visibility = Visibility.Collapsed;
                    BtnOj.Visibility = Visibility.Collapsed;
                    BtnGot.Visibility = Visibility.Collapsed;
                    BtnDor.Visibility = Visibility.Collapsed;
                    BtnOjOk.Visibility = Visibility.Visible;
                    break;
                case "Готов":
                    SpTotal.Visibility = Visibility.Visible;
                    BtnSog.Visibility = Visibility.Collapsed;
                    BtnOj.Visibility = Visibility.Collapsed;
                    BtnGot.Visibility = Visibility.Collapsed;
                    BtnDor.Visibility = Visibility.Visible;
                    BtnOjOk.Visibility = Visibility.Collapsed;
                    BtnSogNo.Visibility = Visibility.Collapsed;
                    BtnSogOk.Visibility = Visibility.Collapsed;
                    break;
                case "Выдан":
                    SpTotal.Visibility = Visibility.Collapsed;
                    BtnSog.Visibility = Visibility.Collapsed;
                    BtnOj.Visibility = Visibility.Collapsed;
                    BtnGot.Visibility = Visibility.Collapsed;
                    BtnDor.Visibility = Visibility.Visible;
                    BtnOjOk.Visibility = Visibility.Collapsed;
                    BtnSogNo.Visibility = Visibility.Collapsed;
                    BtnSogOk.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintDialog testPrint = new PrintDialog();
            if (testPrint.ShowDialog() == true)
            {
                testPrint.PrintVisual(new PrintAct(_order), "Акт выполненных работ");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(new PrintAdmission() { DataContext = new NewOrderViewModel(_order) }, "Квитанция о приеме");
            }
        }
    }
}
