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
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.ResponseToTheOrders = new HashSet<ResponseToTheOrders>();
            this.WhoAnswersToWhomOrders1 = new HashSet<WhoAnswersToWhomOrders>();
        }
    
        public int IdOrders { get; set; }
        public int IdTypeOrders { get; set; }
        public string WhereOrders { get; set; }
        public int IdStaff { get; set; }
        public string SubjectOrders { get; set; }
        public System.DateTime DateReceivingOrDeparturesOrders { get; set; }
        public string TextOrders { get; set; }
        public string FromWhomOrders { get; set; }
        public string ToWhomOrders { get; set; }
        public Nullable<int> IdWhoAnswersToWhomOrders { get; set; }
    
        public virtual Staff Staff { get; set; }
        public virtual TypeOrders TypeOrders { get; set; }
        public virtual WhoAnswersToWhomOrders WhoAnswersToWhomOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponseToTheOrders> ResponseToTheOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WhoAnswersToWhomOrders> WhoAnswersToWhomOrders1 { get; set; }
    }
}
