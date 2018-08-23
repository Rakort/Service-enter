using ServiceСenter.Commands;
using ServiceСenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ServiceСenter.ViewModel
{
    public class NewOrderViewModel : DependencyObject
    {
        private readonly Orders _orders;

        public ICommand AddOrder { get; set; }
        public ICommand DeleteOrder { get; set; }
        public ICommand EditOrder { get; set; }
        public ICommand SetState { get; set; }
        public ICommand OpenOrders { get; set; }
        public ICommand Back { get; set; }
        
        #region Конструктор
        public NewOrderViewModel()
        {
            _orders = null;
        }

        public NewOrderViewModel(Orders orders = null)
        {
            _orders = orders;
            AddOrder = new SimpleCommand(Create);
            EditOrder = new SimpleCommand(() => ShowWin.ShowNewOrder(_orders));
            Back = new SimpleCommand(() =>
            {
                if(orders == null)
                    ShowWin.ShowOrders();
                else
                    ShowWin.ShowWork(orders);
            });
            SetState = new SimpleCommand((state) =>
            {
                _orders.IdState = Int32.Parse(state.ToString());
                SQL.Update(_orders);
                State = _orders == null ? "" : SQL.GetState().First(w => w.Id == _orders.IdState).State; 
            });
            OpenOrders = new SimpleCommand((_) =>  ShowWin.ShowOrders());
            if (orders != null)
            {
                Number = orders.Number;
                Malfunction = orders.Malfunction;
                Appearance = orders.Appearance;
                Equipment = orders.Equipment;
                NotesOnAdmission = orders.NotesOnAdmission;
                SerialNumber = orders.SerialNumber;
                Fabricator = SQL.GetFabricator(orders.IdFabricator);
                Model = SQL.GetModel(orders.IdModel);
                AvailabilityDate = orders.AvailabilityDate;
                EstimatedPrice = orders.EstimatedPrice;
                Prepayment = orders.Prepayment;
                TypesDevices = SQL.GetTypesDevices(orders.IdTypeDevice);
                Name = SQL.GetClientses(orders.IdClient).Name;
                Phone = SQL.GetClientses(orders.IdClient).Phone;
                Email = SQL.GetClientses(orders.IdClient).Email;
                Address = SQL.GetClientses(orders.IdClient).Address;
                DateReceipt = orders.DateReceipt;
            }
            else
            {
                var firstOrDefault = SQL.GetOrders().OrderByDescending(o => o.Number).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var n = firstOrDefault.Number;
                    int res;
                    if (Int32.TryParse(n, out res))
                        Number = (res + 1).ToString();
                }
            }
            ClientNameEntries = new CollectionView(SQL.GetClientses().Select(c => c.Name + " " + c.Phone));
            var e = new List<string> {""};
            e.AddRange(SQL.GetExecutors().Select(s=>s.Name));
            ExecutorsEntries = new CollectionView(e);
            State =  _orders == null ? "" : SQL.GetState().First(w => w.Id == _orders.IdState).State; 
            DeleteOrder = new SimpleCommand(() =>
            {
                var res = MessageBox.Show("Вы действительно хотите удалить заказ","Удалить заказ", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    SQL.GetHistory().Where(w=>w.IdOrder == _orders.IdOrder).ToList().ForEach(f =>SQL.Delete(f));
                    SQL.GetWork().Where(w => w.IdOrder == _orders.IdOrder).ToList().ForEach(f => SQL.Delete(f));
                    SQL.Delete(_orders);
                    ShowWin.ShowOrders();
                }
            });
        }

        #endregion

        #region CollectionView
        //Клиент
        public CollectionView ClientNameEntries
        {
            get { return (CollectionView)GetValue(ClientNameEntriesProperty); }
            set { SetValue(ClientNameEntriesProperty, value); }
        }

        //Устройство
        public CollectionView TypesDevicesEntries
        {
            get { return new CollectionView(SQL.GetTypesDevices().Select(s=>s.Type)); }
        }
        public CollectionView FabricatorEntries
        {
            get { return new CollectionView(SQL.GetFabricator().Select(f => f.Fabricator)); }
        }
        public CollectionView ModelsEntries
        {
            get { return (CollectionView)GetValue(ModelsEntriesProperty); }
            set { SetValue(ModelsEntriesProperty, value); }
        }

        //Состояние
        public CollectionView MalfunctionEntries
        {
            get { return new CollectionView(SQL.GetMalfunction().Select(s=>s.Malfunction)); }
        }
        public CollectionView AppearanceEntries
        {
            get { return new CollectionView(SQL.GetAppearance().Select(s=>s.Appearance)); }
        }
        public CollectionView NotesOnAdmissionEntries
        {
            get { return new CollectionView(SQL.GetNotesOnAdmission()); }
        }

        //Заказ
        public CollectionView ExecutorsEntries { get; private set; }
        public CollectionView EquipmentEntries
        {
            get { return (CollectionView)GetValue(EquipmentEntriesProperty); }
            set { SetValue(EquipmentEntriesProperty, value); }
        }
        
        #endregion

        #region Поля

        #region Устройство

        public string Number { get; set; }
        public string TypesDevices
        {
            get { return (string)GetValue(TypesDevicesProperty); }
            set { SetValue(TypesDevicesProperty, value); }
        }
        public string Fabricator
        {
            get { return (string)GetValue(FabricatorProperty); }
            set { SetValue(FabricatorProperty, value); }
        }
        public string Model { get; set; }
        public string SerialNumber { get; set; }

        #endregion

        #region Состояние
        public string Malfunction
        {
            get { return (string)GetValue(MalfunctionProperty); }
            set { SetValue(MalfunctionProperty, value); }
        }
        public string Appearance
        {
            get { return (string)GetValue(AppearanceProperty); }
            set { SetValue(AppearanceProperty, value); }
        }
        public string Equipment
        {
            get { return (string)GetValue(EquipmentProperty); }
            set { SetValue(EquipmentProperty, value); }
        }
        public string NotesOnAdmission
        {
            get { return (string)GetValue(NotesOnAdmissionProperty); }
            set { SetValue(NotesOnAdmissionProperty, value); }
        }
        #endregion

        #region Заказ

        public DateTime DateReceipt { get; set; }

        public DateTime AvailabilityDate
        {
            get { return (DateTime)GetValue(AvailabilityDateProperty); }
            set { SetValue(AvailabilityDateProperty, value); }
        }
        public int EstimatedPrice
        {
            get { return (int)GetValue(EstimatedPriceProperty); }
            set { SetValue(EstimatedPriceProperty, value); }
        }
        public int Prepayment
        {
            get { return (int)GetValue(PrepaymentProperty); }
            set { SetValue(PrepaymentProperty, value); }
        }
        public string Executors
        {
            get { return (string)GetValue(ExecutorsProperty); }
            set { SetValue(ExecutorsProperty, value); }
        }

        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        #endregion

        #region Клиент

        public string ClientName
        {
            get { return (string)GetValue(ClientNameProperty); }
            set { SetValue(ClientNameProperty, value); }
        }
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }
        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        #endregion

        #endregion
        
        #region Свойства
        #region Устройство

        public static readonly DependencyProperty ModelsEntriesProperty = DependencyProperty.Register(
            "ModelsEntries",
            typeof(CollectionView),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty TypesDevicesProperty = DependencyProperty.Register(
            "TypesDevices",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata("", TypesDevices_Changed));

        public static readonly DependencyProperty FabricatorProperty = DependencyProperty.Register(
            "Fabricator",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata("", Fabricator_Changed));
        

        #endregion

        #region Состояние

        public static readonly DependencyProperty EquipmentEntriesProperty = DependencyProperty.Register(
            "EquipmentEntries",
            typeof(CollectionView),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty MalfunctionProperty = DependencyProperty.Register(
            "Malfunction",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        public static readonly DependencyProperty AppearanceProperty = DependencyProperty.Register(
            "Appearance",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        public static readonly DependencyProperty EquipmentProperty = DependencyProperty.Register(
            "Equipment",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        public static readonly DependencyProperty NotesOnAdmissionProperty = DependencyProperty.Register(
            "NotesOnAdmission",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        #endregion

        #region Заказ

        public static readonly DependencyProperty AvailabilityDateProperty = DependencyProperty.Register(
            "AvailabilityDate",
            typeof(DateTime),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(DateTime.Now));
        public static readonly DependencyProperty EstimatedPriceProperty = DependencyProperty.Register(
            "EstimatedPrice",
            typeof(int),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(0));
        public static readonly DependencyProperty PrepaymentProperty = DependencyProperty.Register(
            "Prepayment",
            typeof(int),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(0));
        public static readonly DependencyProperty ExecutorsProperty = DependencyProperty.Register(
            "Executors",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        #endregion

        #region Клиент

        public static readonly DependencyProperty ClientNameEntriesProperty = DependencyProperty.Register(
            "ClientNameEntries",
            typeof(CollectionView),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(null));

        public static readonly DependencyProperty ClientNameProperty = DependencyProperty.Register(
            "ClientName",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata("", ClientName_Changed));

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register(
            "Name",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        public static readonly DependencyProperty PhoneProperty = DependencyProperty.Register(
            "Phone",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        public static readonly DependencyProperty EmailProperty = DependencyProperty.Register(
            "Email",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));
        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register(
            "Address",
            typeof(string),
            typeof(NewOrderViewModel),
            new UIPropertyMetadata(""));

        
        #endregion
        #endregion

        #region Методы

        private static void Fabricator_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as NewOrderViewModel;
            if (current != null)
            {
                current.ModelsEntries = new CollectionView(SQL.GetModelInFabricator(SQL.GetIdFabricator(current.Fabricator)));
            }
        }
        private static void TypesDevices_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as NewOrderViewModel;
            if (current != null)
            {
                current.EquipmentEntries = new CollectionView(SQL.GetEquipmentInTypeDevice(SQL.GetIdTypeDevice(current.TypesDevices)));
            }
        }
        private static void ClientName_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as NewOrderViewModel;
            if (current != null)
            {
                current.ClientNameEntries.Filter = null;
                current.ClientNameEntries.Filter = current.FilterClient;

                var cl = SQL.GetClientses().FirstOrDefault(c => c.Name + " " + c.Phone == current.ClientName);
                if (cl != null)
                {
                    current.Name = cl.Name;
                    current.Phone = cl.Phone;
                    current.Address = cl.Address;
                    current.Email = cl.Email;
                }
            }

        }
        private bool FilterClient(object obj)
        {
            var current = (obj as string);
            if (current == null)
                return false;

            return current.Contains(ClientName);
        }
        public void Create()
        {
            //Если клиента нет в базе он добавляется
            if (SQL.GetClientses().Find(t => t.Name == Name && t.Phone == Phone) == null)
            {
                SQL.Add(new Clients() {Name = Name, Address = Address, Phone = Phone, Email = Email});
            }
            
            //Если типа устройства нет в базе он добавляется
            if (TypesDevices != String.Empty)
                if (!SQL.GetTypesDevices().Select(s => s.Type).Contains(TypesDevices))
                {
                    SQL.Add(new DicTypesDevices() {Type = TypesDevices});
                }
            if (Fabricator != String.Empty)
                if (!SQL.GetFabricator().Select(f => f.Fabricator).Contains(Fabricator))
                {
                    SQL.Add(new DicFabricator() {Fabricator = Fabricator});
                }

            if (Model != String.Empty)
                if (!SQL.GetModelInFabricator(SQL.GetIdFabricator(Fabricator)).Contains(Model))
                {
                    SQL.Add(new DicModel() {Model = Model, IdFabricator = SQL.GetIdFabricator(Fabricator)});
                }
            
            var o = new Orders()
            {
                IdClient = SQL.GetClientses().First(t => t.Name == Name && t.Phone == Phone).IdClient,
                Number = Number,
                IdTypeDevice = SQL.GetIdTypeDevice(TypesDevices),
                IdFabricator = SQL.GetIdFabricator(Fabricator),
                IdModel = SQL.GetIdModel(Model, SQL.GetIdFabricator(Fabricator)),
                SerialNumber = SerialNumber,
                Appearance = Appearance,
                Equipment = Equipment,
                Malfunction = Malfunction,
                NotesOnAdmission = NotesOnAdmission,
                AvailabilityDate = AvailabilityDate,
                DateReceipt = DateTime.Now,
                EstimatedPrice = EstimatedPrice,
                Prepayment = Prepayment,
                IdExecutor = SQL.GetIdExecutor(Executors)
            };
            if (_orders == null)
            {
                o.IdState = 1;
                SQL.Add(o);
                SQL.AddHistory(SQL.GetOrders().OrderByDescending(o1 => o1.IdOrder).First().IdOrder, "Заказ принят");
                ShowWin.ShowOrders();
            }
            else
            {
                o.IdState = _orders.IdState;
                o.IdOrder = _orders.IdOrder;
                SQL.AddHistory(o.IdOrder, "Редактирование заказа");
                SQL.Update(o);
                ShowWin.ShowWork(o);
            }
        }
        
        #endregion
    }
}
