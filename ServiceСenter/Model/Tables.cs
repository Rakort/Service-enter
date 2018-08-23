using System;
using SQLite;

namespace ServiceСenter.Model
{

    #region Dictionary

    public class Table
    {}
    public class DicAppearance : Table //Внешний вид
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdAppearance { get; set; }

        [MaxLength(255), NotNull]
        public string Appearance { get; set; }
    }
    public class DicEquipment : Table //Комплектация
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdEquipment { get; set; }

        [NotNull]
        public int IdTypeDevices { get; set; }

        [MaxLength(255), NotNull]
        public string Equipment { get; set; }
    }

    public class DicFabricator : Table //Производитель
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdFabricator { get; set; }

        [MaxLength(255), NotNull]
        public string Fabricator { get; set; }
    }

    public class DicMalfunction : Table //Неисправность	
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdMalfunction { get; set; }

        [MaxLength(255), NotNull]
        public string Malfunction { get; set; }
    }

    public class DicModel : Table //Модель
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdModel { get; set; }

        [NotNull]
        public int IdFabricator { get; set; }

        [MaxLength(255), NotNull]
        public string Model { get; set; }
    }

    public class DicNotesOnAdmission : Table //Заметки при приеме
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdNote { get; set; }

        [MaxLength(255), NotNull]
        public string Note { get; set; }
    }

    public class DicTypesDevices : Table //Типы устройств
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdType { get; set; }

        [MaxLength(255), NotNull]
        public string Type { get; set; }
    }

    #endregion

    public class Clients : Table //Клиенты
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdClient { get; set; }

        [NotNull] //Организация
        public bool Entity { get; set; }

        [NotNull] //Поставщик
        public bool Provider { get; set; }

        [MaxLength(100), NotNull]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string Notes { get; set; }
    }

    public class Orders : Table //Заказы
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdOrder { get; set; }

        public string Number { get; set; }
        
        public int IdClient { get; set; }

        [NotNull] 
        public int IdTypeDevice { get; set; }

        [NotNull]
        public int IdFabricator { get; set; }

        [NotNull]
        public int IdModel { get; set; }

        [MaxLength(50)]
        public string SerialNumber { get; set; }

        [MaxLength(255)]
        public string Malfunction { get; set; }

        [MaxLength(255)]
        public string Appearance { get; set; }

        [MaxLength(255)]
        public string Equipment { get; set; }

        [MaxLength(255)]
        public string NotesOnAdmission { get; set; }

        public DateTime AvailabilityDate { get; set; }

        [NotNull]
        public DateTime DateReceipt{ get; set; }

        [MaxLength(50)]
        public int EstimatedPrice { get; set; }

        public int Prepayment { get; set; }

        public int IdExecutor { get; set; }

        
        public int IdState { get; set; }

    }


    public class Executors : Table //Мастера
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdExecutor { get; set; }

        [MaxLength(100), NotNull]
        public string Name { get; set; }
       
    }

    public class DicWork : Table //Словарь работ
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdWork { get; set; }

        [NotNull]
        public string Work { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

    }

    public class Works : Table //Работы
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        [NotNull]
        public int IdWork { get; set; }

        [NotNull]
        public int IdOrder { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }

    }
    public class History : Table //История
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdHistory { get; set; }

        public int IdOrder { get; set; }

        public DateTime Date  { get; set; }

        public string Description { get; set; }
    }

    public class DicState : Table //Статусы
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        public string State { get; set; }
    }


}