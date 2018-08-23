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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServiceСenter.Model;
using ServiceСenter.ViewModel;

namespace ServiceСenter.View
{
    /// <summary>
    /// Логика взаимодействия для PrintAct.xaml
    /// </summary>
    public partial class PrintAct : UserControl
    {
        public PrintAct(Orders order)
        {
            InitializeComponent();
            SpOrder.DataContext = new NewOrderViewModel(order);
            SpWork.DataContext = new WorkViewModel(order);
        }
    }
}
