using ServiceСenter.Commands;
using ServiceСenter.Model;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ServiceСenter.ViewModel
{
    public class HistoryModelView : DependencyObject
    {
        #region Конструктор
        public HistoryModelView()
        {
        }

        public HistoryModelView(Orders order)
        {
            _order = order;

            HistoryEntries = new CollectionView(SQL.GetHistory().Where(h => h.IdOrder == _order.IdOrder).Select(s => new {Date = s.Date.ToShortDateString() + " "+ s.Date.ToShortTimeString(), s.Description }));
            AddComment = new SimpleCommand((text) =>
            {
                SQL.AddHistory(_order.IdOrder, text.ToString());
                HistoryEntries = new CollectionView(SQL.GetHistory().Where(h => h.IdOrder == _order.IdOrder).Select(s => new { Date = s.Date.ToShortDateString() + " " + s.Date.ToShortTimeString(), s.Description }));
            });
        }
        #endregion

        #region Поля

        private readonly Orders _order;

        public CollectionView HistoryEntries
        {
            get { return (CollectionView)GetValue(HistoryEntriesProperty); }
            set { SetValue(HistoryEntriesProperty, value); }
        }

        public ICommand AddComment { get; set; }

        #endregion
        
        #region Свойства

        public static readonly DependencyProperty HistoryEntriesProperty = DependencyProperty.Register(
            "HistoryEntries",
            typeof(CollectionView),
            typeof(WorkViewModel),
            new UIPropertyMetadata(null));
        
        #endregion
    }
}
