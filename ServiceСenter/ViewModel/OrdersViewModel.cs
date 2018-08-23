using ServiceСenter.Commands;
using ServiceСenter.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ServiceСenter.ViewModel
{
    public class OrdersViewModel : DependencyObject
    {
        
        public CollectionView OrdersEntries { get; set; }
        public ICommand EditOrder { get; set; }
        public ICommand NewOrder { get; set; }

        public OrdersViewModel()
        {
            var fab = SQL.GetFabricator();
            var mod = SQL.GetModel();

            var cli = SQL.GetClientses();
            var states = SQL.GetState();
            
            OrdersEntries = new CollectionView(SQL.GetOrders().Select(o =>
            {
                var client = cli.FirstOrDefault(c => c.IdClient == o.IdClient);
                return new Order()
                {
                    Id = o.IdOrder,
                    State = states.First(f=>f.Id == o.IdState).State,
                    Number = o.Number ?? "",
                    NameClient = client != null? client.Name: "",
                    PhoneClient = client != null ? client.Phone : "",
                    Model =
                        String.Format("{0} {1}",
                            fab.First(f => f.IdFabricator == o.IdFabricator).Fabricator,
                            mod.First(m => m.IdModel == o.IdModel).Model),
                    DateReceipt = o.DateReceipt.Date,
                    AvailabilityDate = o.AvailabilityDate.Date,
                    NameExecutor = SQL.GetExecutors(o.IdExecutor),
                    EditOrder = new SimpleCommand((_) =>  ShowWin.ShowWork(o))
                };
            }));

            EditOrder = new SimpleCommand(() =>
            {
                if (OrdersEntries.CurrentItem != null)
                {
                    var o = SQL.GetOrders().FirstOrDefault(f=>f.IdOrder == (OrdersEntries.CurrentItem as Order).Id);
                    if (o.IdState == 1)
                    {
                        o.IdState = 2;
                        SQL.Update(o);
                    }
                    ShowWin.ShowWork(o);
                }

            });
            NewOrder = new SimpleCommand(() => ShowWin.ShowNewOrder(null));
            LoadFilterEntries();
        }

        #region Filter
        public ICommand ApplyFilter { get; set; }

        public CollectionView StateEntries { get; set; }
        public CollectionView TypeDeviceEntries { get; set; }
        public CollectionView ModelEntries { get; set; }
        public CollectionView FabricatorEntries { get; set; }
        public CollectionView ExecutorEntries { get; set; }
        public CollectionView ClientNameEntries { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        
        private void LoadFilterEntries()
        {
            var client = new List<string>() { "Любой" };
            client.AddRange(SQL.GetClientses().Select(c => c.Name + " " + c.Phone));
            ClientNameEntries = new CollectionView(client);
            
            var states = new List<string>(){"Любой"};
            states.AddRange(SQL.GetState().Select(s=>s.State));
            StateEntries = new CollectionView(states);

            var type = new List<string>() { "Любой" };
            type.AddRange(SQL.GetTypesDevices().Select(s => s.Type));
            TypeDeviceEntries = new CollectionView(type);

            var model = new List<string>() { "Любой" };
            model.AddRange(SQL.GetModel().Select(s => s.Model));
            ModelEntries = new CollectionView(model);

            var fabricator = new List<string>() { "Любой" };
            fabricator.AddRange(SQL.GetFabricator().Select(s => s.Fabricator));
            FabricatorEntries = new CollectionView(fabricator);

            var executor = new List<string>() { "Любой", "Без исполнителя" };
            executor.AddRange(SQL.GetExecutors().Select(s=>s.Name));
            ExecutorEntries = new CollectionView(executor);

            ApplyFilter = new SimpleCommand(() =>
            {
                OrdersEntries.Filter = null;
                OrdersEntries.Filter = OrderFilter;
            });
        }

        private bool OrderFilter(object obj)
        {
            return false;
            var order = (obj as Order);
            if (order == null)
                return false;
            var orders = SQL.GetOrders().FirstOrDefault(f => f.IdOrder == order.Id);

            if (StateEntries.CurrentItem.ToString()!="Любой")
                if(order.State!=StateEntries.CurrentItem.ToString()) return false;
            
            if (TypeDeviceEntries.CurrentItem.ToString() != "Любой")
                if(SQL.GetTypesDevices(orders.IdTypeDevice) != TypeDeviceEntries.CurrentItem.ToString()) return false;
            
            if (ModelEntries.CurrentItem.ToString() != "Любой")
                if(SQL.GetModel(orders.IdModel) != ModelEntries.CurrentItem.ToString()) return false;
            
            if (FabricatorEntries.CurrentItem.ToString() != "Любой")
                if(SQL.GetFabricator(orders.IdFabricator) != FabricatorEntries.CurrentItem.ToString()) return false;
            
            if (ExecutorEntries.CurrentItem.ToString() != "Любой")
                if (ExecutorEntries.CurrentItem.ToString() == "Без исполнителя")
                {
                    if (SQL.GetExecutors(orders.IdExecutor) != String.Empty) return false;
                }
                else if (SQL.GetExecutors(orders.IdExecutor) != ExecutorEntries.CurrentItem.ToString()) return false;

            if (ClientNameEntries.CurrentItem.ToString() != "Любой")
            {
                var c =
                    SQL.GetClientses(orders.IdClient);
                if (c.Name + " " + c.Phone != ClientNameEntries.CurrentItem.ToString())
                    return false;
            }

            if (StartDate != new DateTime())
                if(order.DateReceipt < StartDate && order.DateReceipt > EndDate) return false;

            return true;
        
        }
        
        #endregion

    }

    public class Order
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string Number { get; set; }

        public string NameClient { get; set; }

        public string PhoneClient { get; set; }

        public string Model { get; set; }

        public DateTime AvailabilityDate { get; set; }

        public DateTime DateReceipt { get; set; }

        public string NameExecutor { get; set; }

        public ICommand EditOrder { get; set; }

    }
}
