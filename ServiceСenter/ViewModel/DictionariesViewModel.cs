using ServiceСenter.Commands;
using ServiceСenter.Model;
using ServiceСenter.View;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ServiceСenter.ViewModel
{
    public class DictionariesViewModel : DependencyObject
    {

        public CollectionView OrdersEntries { get; set; }

        #region Типы устройств
        public ICommand AddTypeDevices { get; set; }
        public ICommand DeleteTypeDevices { get; set; }

        public CollectionView TypesDevicesEntries 
        {
            get { return (CollectionView)GetValue(TypesDevicesProperty); }
            set { SetValue(TypesDevicesProperty, value); }
        }
        
        public static readonly DependencyProperty TypesDevicesProperty = DependencyProperty.Register(
            "TypesDevices",
            typeof(CollectionView),
            typeof(DictionariesViewModel),
            new UIPropertyMetadata(null, (o, args) => { }));

        #endregion

        #region Производители
        public ICommand AddFabricator { get; set; }
        public ICommand DeleteFabricator { get; set; }
        public CollectionView FabricatorEntries
        {
            get { return (CollectionView)GetValue(FabricatorProperty); }
            set { SetValue(FabricatorProperty, value); }
        }
        
        public static readonly DependencyProperty FabricatorProperty = DependencyProperty.Register(
            "Fabricator",
            typeof(CollectionView),
            typeof(DictionariesViewModel),
            new UIPropertyMetadata(null));
        #endregion

        #region Модель
        public ICommand AddModel { get; set; }
        public ICommand DeleteModel { get; set; }
        public CollectionView ModelEntries
        {
            get { return (CollectionView)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model",
            typeof(CollectionView),
            typeof(DictionariesViewModel),
            new UIPropertyMetadata(null));
        #endregion

        #region Работы
        public ICommand AddWork { get; set; }
        public ICommand DeleteWork { get; set; }
        public CollectionView WorkEntries
        {
            get { return (CollectionView)GetValue(WorkProperty); }
            set { SetValue(WorkProperty, value); }
        }

        public static readonly DependencyProperty WorkProperty = DependencyProperty.Register(
            "Work",
            typeof(CollectionView),
            typeof(DictionariesViewModel),
            new UIPropertyMetadata(null));
        #endregion

        #region Неисправность
        public ICommand AddMalfunction { get; set; }
        public ICommand DeleteMalfunction { get; set; }
        public CollectionView MalfunctionEntries
        {
            get { return (CollectionView)GetValue(MalfunctionProperty); }
            set { SetValue(MalfunctionProperty, value); }
        }

        public static readonly DependencyProperty MalfunctionProperty = DependencyProperty.Register(
            "Malfunction",
            typeof(CollectionView),
            typeof(DictionariesViewModel),
            new UIPropertyMetadata(null));
        #endregion

        #region Состояние
        public ICommand AddAppearance { get; set; }
        public ICommand DeleteAppearance { get; set; }
        public CollectionView AppearanceEntries
        {
            get { return (CollectionView)GetValue(AppearanceProperty); }
            set { SetValue(AppearanceProperty, value); }
        }

        public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
            "Appearance",
            typeof(CollectionView),
            typeof(DictionariesViewModel),
            new UIPropertyMetadata(null));
        #endregion

        #region Комплектация
        public ICommand AddEquipment { get; set; }
        public ICommand DeleteEquipment { get; set; }
        public CollectionView EquipmentEntries
        {
            get { return (CollectionView)GetValue(EquipmentProperty); }
            set { SetValue(EquipmentProperty, value); }
        }

        public static readonly DependencyProperty EquipmentProperty = DependencyProperty.Register(
            "Equipment",
            typeof(CollectionView),
            typeof(DictionariesViewModel),
            new UIPropertyMetadata(null));
        #endregion
        
        #region Исполнитель
        public ICommand AddExecutors { get; set; }
        public ICommand DeleteExecutors { get; set; }
        public CollectionView ExecutorsEntries
        {
            get { return (CollectionView)GetValue(ExecutorsProperty); }
            set { SetValue(ExecutorsProperty, value); }
        }

        public static readonly DependencyProperty ExecutorsProperty = DependencyProperty.Register(
            "Executors",
            typeof(CollectionView),
            typeof(DictionariesViewModel),
            new UIPropertyMetadata(null));
        #endregion
        public DictionariesViewModel()
        {
            var fab = SQL.GetFabricator(); fab.Add(new DicFabricator() { Fabricator = "", IdFabricator = 0 }); fab.Add(new DicFabricator() { Fabricator = "" });
            var mod = SQL.GetModel();

            var cli = SQL.GetClientses();
            var states = SQL.GetState();
            OrdersEntries = new CollectionView(SQL.GetOrders().Select(o =>
            {
                var client = cli.FirstOrDefault(c => c.IdClient == o.IdClient);
                return new Order()
                {
                    Id = o.IdOrder,
                    State = states.First(f => f.Id == o.IdState).State,
                    Number = o.Number ?? "",
                    NameClient = client != null ? client.Name : "",
                    PhoneClient = client != null ? client.Phone : "",
                    Model =
                        String.Format("{0} {1}",
                            fab.First(f => f.IdFabricator == o.IdFabricator).Fabricator,
                            mod.First(m => m.IdModel == o.IdModel).Model),
                    DateReceipt = o.DateReceipt.Date,
                    AvailabilityDate = o.AvailabilityDate.Date,
                    NameExecutor = SQL.GetExecutors(o.IdExecutor),
                    EditOrder = new SimpleCommand((_) => ShowWin.ShowWork(o))
                };
            }));

            #region Тип устройств

            TypesDevicesEntries = new CollectionView(SQL.GetTypesDevices());
            
            AddTypeDevices = new SimpleCommand((edit) =>
            {
                AddedView a = bool.Parse(edit.ToString()) ? ShowWin.AddedTypeDevice(TypesDevicesEntries.CurrentItem as DicTypesDevices) : ShowWin.AddedTypeDevice(null);
                a.OnClose += () => { TypesDevicesEntries = new CollectionView(SQL.GetTypesDevices()); TypesDevicesEntries.Refresh(); }; 
            });

            DeleteTypeDevices = new SimpleCommand((_) =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить тип устройств","Удалить тип устройств", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.Delete(TypesDevicesEntries.CurrentItem as DicTypesDevices);
                    TypesDevicesEntries = new CollectionView(SQL.GetTypesDevices());
                }
            });

            #endregion

            #region Производители

            FabricatorEntries = new CollectionView(SQL.GetFabricator());

            AddFabricator = new SimpleCommand((edit) =>
            {
                AddedView a = bool.Parse(edit.ToString()) ? ShowWin.AddedFabricator(FabricatorEntries.CurrentItem as DicFabricator) : ShowWin.AddedFabricator(null);
                a.OnClose += () => FabricatorEntries = new CollectionView(SQL.GetFabricator());
            });

            DeleteFabricator = new SimpleCommand((_) =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить производителя","Удалить производителя", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.Delete(TypesDevicesEntries.CurrentItem as DicFabricator);
                    FabricatorEntries = new CollectionView(SQL.GetFabricator());
                }
            });

            #endregion

            #region Модель

            ModelEntries = new CollectionView(SQL.GetModel().OrderBy(o=>o.IdFabricator));

            AddModel = new SimpleCommand((edit) =>
            {
                var a = bool.Parse(edit.ToString()) ? ShowWin.AddedModel(ModelEntries.CurrentItem as DicModel) : ShowWin.AddedModel(null);
                a.OnClose += () => ModelEntries = new CollectionView(SQL.GetModel().OrderBy(o => o.IdFabricator));
            });

            DeleteModel = new SimpleCommand((_) =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить модель","Удалить модель", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.Delete(ModelEntries.CurrentItem as DicModel);
                    ModelEntries = new CollectionView(SQL.GetModel().OrderBy(o => o.IdFabricator));
                }
            });

            #endregion

            #region Работы

            WorkEntries = new CollectionView(SQL.GetAllDicWork());

            AddWork = new SimpleCommand((edit) =>
            {
                var a = bool.Parse(edit.ToString()) ? ShowWin.AddedDicWork(WorkEntries.CurrentItem as DicWork) : ShowWin.AddedDicWork(null);
                a.OnClose += () => WorkEntries = new CollectionView(SQL.GetAllDicWork());
            });

            DeleteWork = new SimpleCommand((_) =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить работу","Удалить работу", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.Delete(WorkEntries.CurrentItem as DicWork);
                    WorkEntries = new CollectionView(SQL.GetAllDicWork());
                }
            });

            #endregion

            #region Неисправность

            MalfunctionEntries = new CollectionView(SQL.GetMalfunction());

            AddMalfunction = new SimpleCommand((edit) =>
            {
                var a = bool.Parse(edit.ToString()) ? ShowWin.AddedMalfunction(MalfunctionEntries.CurrentItem as DicMalfunction) : ShowWin.AddedMalfunction(null);
                a.OnClose += () => MalfunctionEntries = new CollectionView(SQL.GetMalfunction());
            });

            DeleteMalfunction = new SimpleCommand((_) =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить неисправность", "Удалить неисправность", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.Delete(MalfunctionEntries.CurrentItem as DicMalfunction);
                    MalfunctionEntries = new CollectionView(SQL.GetMalfunction());
                }
            });

            #endregion

            #region Состояние

            AppearanceEntries = new CollectionView(SQL.GetAppearance());

            AddAppearance = new SimpleCommand((edit) =>
            {
                var a = bool.Parse(edit.ToString()) ? ShowWin.AddedAppearance(AppearanceEntries.CurrentItem as DicAppearance) : ShowWin.AddedAppearance(null);
                a.OnClose += () => AppearanceEntries = new CollectionView(SQL.GetAppearance());
            });

            DeleteAppearance = new SimpleCommand((_) =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить состояние", "Удалить состояние", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.Delete(AppearanceEntries.CurrentItem as DicAppearance);
                    AppearanceEntries = new CollectionView(SQL.GetAppearance());
                }
            });
            #endregion

            #region Комплектация

            EquipmentEntries = new CollectionView(SQL.GetEquipment());

            AddEquipment = new SimpleCommand((edit) =>
            {
                var a = bool.Parse(edit.ToString()) ? ShowWin.AddedEquipment(EquipmentEntries.CurrentItem as DicEquipment) : ShowWin.AddedEquipment(null);
                a.OnClose += () => EquipmentEntries = new CollectionView(SQL.GetEquipment());
            });
            DeleteEquipment = new SimpleCommand((_) =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить комплектацию", "Удалить комплектацию", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.Delete(EquipmentEntries.CurrentItem as DicEquipment);
                    EquipmentEntries = new CollectionView(SQL.GetEquipment());
                }
            });
            #endregion
            
            #region Исполнитель

            ExecutorsEntries = new CollectionView(SQL.GetExecutors());

            AddExecutors = new SimpleCommand((edit) =>
            {
                var a = bool.Parse(edit.ToString()) ? ShowWin.AddedExecutors(ExecutorsEntries.CurrentItem as Executors) : ShowWin.AddedExecutors(null);
                a.OnClose += () => ExecutorsEntries = new CollectionView(SQL.GetExecutors());
            });
            DeleteExecutors = new SimpleCommand((_) =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить исполнителя", "Удалить исполнителя", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.Delete(ExecutorsEntries.CurrentItem as Executors);
                    ExecutorsEntries = new CollectionView(SQL.GetExecutors());
                }
            });
            #endregion
        }
    }
}
