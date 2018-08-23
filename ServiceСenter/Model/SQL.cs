using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SQLite;

namespace ServiceСenter.Model
{
    public static class SQL
    {
        private const string DbName = "ServiceDB.db";

        
        #region Get

        //Устройство
        public static List<DicTypesDevices> GetTypesDevices()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<DicTypesDevices>().ToList();                
            }
        }
        public static string GetTypesDevices(int id)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var d = db.Table<DicTypesDevices>().FirstOrDefault(t => t.IdType == id);
                return d != null ? d.Type:"";
            }
        }
        public static List<DicFabricator> GetFabricator()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<DicFabricator>().ToList(); 
            }
        }
        public static string GetFabricator(int id)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var fab = db.Table<DicFabricator>().FirstOrDefault(f => f.IdFabricator == id);
                return fab==null ? String.Empty : fab.Fabricator;
            }
        }
        public static List<DicModel> GetModel()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<DicModel>().ToList();
            }
        }
        public static string GetModel(int idModel)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var mod = db.Table<DicModel>().FirstOrDefault(m => m.IdModel == idModel);
                return mod==null ? String.Empty : mod.Model;
            }
        }
        public static List<string> GetModelInFabricator(int idFabricator)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var m = new List<string>();
                db.Table<DicModel>().Where(w => w.IdFabricator == idFabricator).ToList().ForEach(m1 => m.Add(m1.Model));
                return m;
            }
        }

        //Состояние
        public static List<DicMalfunction> GetMalfunction()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<DicMalfunction>().ToList();
            }
        }
        public static List<DicAppearance> GetAppearance()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<DicAppearance>().ToList();
            }
        }
        public static List<string> GetNotesOnAdmission()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var f = new List<string>();
                db.Table<DicNotesOnAdmission>().ToList().ForEach(f1 => f.Add(f1.Note));
                return f;
            }
        }
        public static List<string> GetEquipmentInTypeDevice(int id)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var f = new List<string>();
                db.Table<DicEquipment>().Where(e => e.IdTypeDevices == id).ToList().ForEach(f1 => f.Add(f1.Equipment));
                return f;
            }
        }
        public static List<DicEquipment> GetEquipment()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<DicEquipment>().ToList();
            }
        }
        
        //Заказ
       
        public static List<Executors> GetExecutors()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<Executors>().ToList();
            }
        }
        public static string GetExecutors(int id)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var cl = db.Table<Executors>().FirstOrDefault(c => c.IdExecutor == id);
                return (cl != null) ? cl.Name : string.Empty;
            }
        }
        public static List<Orders> GetOrders()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<Orders>().ToList();
            }
        }
        public static Orders GetOrders(int id)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var o = db.Table<Orders>().FirstOrDefault(f => f.IdOrder == id);
                return o ?? new Orders();
            }
        }

        //Клиент
        public static List<Clients> GetClientses()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<Clients>().ToList();
            }
        }
        public static Clients GetClientses(int id)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var cl = db.Table<Clients>().FirstOrDefault(c => c.IdClient == id);
                return cl ?? new Clients();
            }
        }
        
        //Work
        public static List<string> GetDicWork()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var w = new List<string>();
                db.Table<DicWork>().ToList().ForEach(f1 => w.Add(f1.Work));
                return w;
            }
        }
        public static List<DicWork> GetAllDicWork()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<DicWork>().ToList();
            }
        }
        public static List<Works> GetWork()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var w  = db.Table<Works>().ToList();

                return w;
            }
        }
        public static string GetWork(int id)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var w = db.Table<DicWork>().FirstOrDefault(c => c.IdWork == id);
                return (w != null) ? w.Work : string.Empty;
            }
        }
        public static List<History> GetHistory()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<History>().ToList();
            }
        }
        public static List<DicState> GetState()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                return db.Table<DicState>().ToList();
            }
        }
        #endregion
        
        #region GetId
        public static int GetIdFabricator(string fabricator)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var fab = db.Table<DicFabricator>().FirstOrDefault(f => f.Fabricator == fabricator);
                return fab != null ? fab.IdFabricator : 0;
            }
        }
        public static int GetIdTypeDevice(string type)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var dicTypesDevices = db.Table<DicTypesDevices>().FirstOrDefault(t => t.Type == type);
                return dicTypesDevices != null ? dicTypesDevices.IdType : 0;
            }
        }
        public static int GetIdExecutor(string executor)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var e = db.Table<Executors>().FirstOrDefault(t => t.Name == executor);
                return e != null ? e.IdExecutor : 0;
            }
        }
        public static int GetIdModel(string model, int idFabricator)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var dicModel = db.Table<DicModel>().FirstOrDefault(t => t.Model == model && t.IdFabricator == idFabricator);
                return dicModel != null ? dicModel.IdModel : 0;
            }
        }
        public static int GetIdWork(string work)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                var e = db.Table<DicWork>().FirstOrDefault(t => t.Work == work);
                return e != null ? e.IdWork : 0;
            }
        }
        #endregion

        public static int Add(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite, true))
            {
                return db.Insert(table);
            }
        }
        public static int Update(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite, true))
            {
                return db.Update(table);
            }
        }
        public static int Delete(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite, true))
            {
                return db.Delete(table);
            }
        }
        public static int AddHistory(int idOrder, string descripton)
        {
           return Add(new History() { Date = DateTime.Now, Description = descripton, IdOrder = idOrder });
        }

        public static List<T> AddHistory<T>() where T : Table
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                Type type = typeof(T);

                if(type == typeof(Table))
                    MessageBox.Show("Table");
                
                return new List<T>();
            }
        }

    }
}
