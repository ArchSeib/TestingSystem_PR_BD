//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestingSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users_Tests
    {
        public long ID_User_Test { get; set; }
        public long ID_User { get; set; }
        public long ID_Test { get; set; }
        public long ID_Result_Test { get; set; }
        public int Time_Spent { get; set; }
        public System.DateTime Data_Passing_Test { get; set; }
        public int Number_Points { get; set; }
    
        public virtual Results Results { get; set; }
        public virtual Tests Tests { get; set; }
        public virtual Users Users { get; set; }
    }
}
