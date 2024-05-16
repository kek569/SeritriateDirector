//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeritriateDirector.DataFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            this.Graphics = new HashSet<Graphics>();
            this.Letters = new HashSet<Letters>();
            this.Orders = new HashSet<Orders>();
            this.Staff1 = new HashSet<Staff>();
        }
    
        public int IdStaff { get; set; }
        public string FirstNameStaff { get; set; }
        public string SurNameStaff { get; set; }
        public string MiddleName { get; set; }
        public string NumberPhoneStaff { get; set; }
        public System.DateTime DateOfBirthStaff { get; set; }
        public int SeriesPassport { get; set; }
        public int IdGender { get; set; }
        public int IdJobTitle { get; set; }
        public int IdUser { get; set; }
        public Nullable<int> IdSecretary { get; set; }
        public string AdressStaff { get; set; }
        public string FullName { get; set; }
        public byte[] PhotoStaff { get; set; }
        public int NumberPassport { get; set; }
    
        public virtual Gender Gender { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Graphics> Graphics { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Letters> Letters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Staff> Staff1 { get; set; }
        public virtual Staff Staff2 { get; set; }
        public virtual User User { get; set; }
    }
}
