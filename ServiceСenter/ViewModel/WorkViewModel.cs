using ServiceСenter.Commands;
using ServiceСenter.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ServiceСenter.ViewModel
{
    public class WorkViewModel : DependencyObject
    {
        

        #region Конструктор

        public WorkViewModel(Orders orders)
        {
            _orders = orders;

            Delete = new SimpleCommand(id =>
            {
                SQL.Delete(SQL.GetWork().First(w => w.Id == (int) id));
                SQL.AddHistory(_orders.IdOrder, String.Format("Удаление работы: {0}", WorkName));
                Load();
            });
            Edit = new SimpleCommand(id =>
            {
                var a = ShowWin.AddedWork(SQL.GetWork().First(w => w.Id == (int)id));
                a.OnClose += Load;
            });

            AddWork = new SimpleCommand(() =>
            {
                if (!SQL.GetDicWork().Contains(WorkName))
                    SQL.Add(new DicWork() {Work = WorkName, Price = Price});
                SQL.Add(new Works()
                {
                    IdOrder = _orders.IdOrder,
                    IdWork = SQL.GetIdWork(WorkName),
                    Count = Count == 0 ? 1 : Count,
                    Price = Price
                });
                SQL.AddHistory(_orders.IdOrder, String.Format("Добавлена работа: {0}", WorkName));
                Load();
            });

            Load();

            DicWorkEntries.CurrentChanged +=
                (u, e) =>
                {
                    if (DicWorkEntries.CurrentItem != null)
                        Price =
                            SQL.GetAllDicWork()
                                .First(w => w.IdWork == SQL.GetIdWork(DicWorkEntries.CurrentItem.ToString()))
                                .Price;
                };

         }

        public WorkViewModel()
        {
            
        }

        #endregion

        #region Поля

        private readonly Orders _orders;

        public ICommand AddWork { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }

        public int Count { get; set; }
        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        public int TotalRepairs
        {
            get { return (int)GetValue(TotalRepairsProperty); }
            set { SetValue(TotalRepairsProperty, value); }
        }
        public string WorkName { get; set; }
        
        public int Price
        {
            get { return (int)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public CollectionView DicWorkEntries 
        {
            get { return (CollectionView)GetValue(DicWorkEntriesProperty); }
            set { SetValue(DicWorkEntriesProperty, value); }
        }

        public CollectionView WorkViewEntries
        {
            get { return (CollectionView)GetValue(WorkViewEntriesProperty); }
            set { SetValue(WorkViewEntriesProperty, value); }
        }
        
        #endregion

        #region Свойства

        public static readonly DependencyProperty TotalProperty = DependencyProperty.Register(
            "Total",
            typeof(int),
            typeof(WorkViewModel),
            new UIPropertyMetadata(0));
        public static readonly DependencyProperty TotalRepairsProperty = DependencyProperty.Register(
            "TotalRepairs",
            typeof(int),
            typeof(WorkViewModel),
            new UIPropertyMetadata(0));

        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register(
            "Price",
            typeof(int),
            typeof(WorkViewModel),
            new UIPropertyMetadata(0));

        public static readonly DependencyProperty WorkViewEntriesProperty = DependencyProperty.Register(
            "WorkViewEntries",
            typeof(CollectionView),
            typeof(WorkViewModel),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty DicWorkEntriesProperty = DependencyProperty.Register(
            "DicWorkEntries",
            typeof(CollectionView),
            typeof(WorkViewModel),
            new UIPropertyMetadata(null));

        #endregion

        #region Методы

        private void Load()
        {
            DicWorkEntries = new CollectionView(SQL.GetDicWork()); 

            WorkViewEntries = new CollectionView(SQL.GetWork().Where(s=>s.IdOrder==_orders.IdOrder).Select( w => (new 
            {
                                w.Id,
                                Work = SQL.GetWork(w.IdWork),
                                w.Price,
                                w.Count,
                                Summ = w.Price*w.Count,
                                Delete = (ICommand) Delete,
                                Edit = (ICommand) Edit
                            }))); 

            TotalRepairs = 0;
            SQL.GetWork().Where(w => w.IdOrder == _orders.IdOrder).ToList().ForEach(o => TotalRepairs += o.Count*o.Price);
            Total = TotalRepairs - _orders.Prepayment;

            Count = 1;
        }

        #endregion

    }
}
