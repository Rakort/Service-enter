using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ServiceСenter.Commands;

namespace ServiceСenter.View
{
    public partial class AddedView : UserControl
    {
        public IEnumerable<Control> Controls;
        public ICommand Submit;
        public delegate void DelegateVoid();
        public event DelegateVoid OnClose;

        public AddedView(IEnumerable<Control> controls, ICommand submit)
        {
            InitializeComponent();
            Controls = controls;
            Submit = submit;
            Init();
        }

        public AddedView()
        {
            InitializeComponent();
        }

        public void Init()
        {
            foreach (var control in Controls)
            {
                StackPanel.Children.Add(control);
            }
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition(){Width = new GridLength(1,GridUnitType.Star)});
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            var btnOk = new Button() { Content = "Сохранить", Command = Submit, Margin = new Thickness(10), Padding = new Thickness(10,2,10,2)};
            var btnClose = new Button() { Content = "Отмена", Command = new SimpleCommand(Close), Margin = new Thickness(10), Padding = new Thickness(10, 2, 10, 2) };
            
            Grid.SetColumn(btnClose, 0);
            Grid.SetColumn(btnOk, 1);
            grid.Children.Add(btnOk);
            grid.Children.Add(btnClose);
            StackPanel.Children.Add(grid);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Close()
        {
            this.Visibility = Visibility.Collapsed;
            if(OnClose != null)
                OnClose();
        }
    }
}
