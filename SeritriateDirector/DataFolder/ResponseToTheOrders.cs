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
    
    public partial class ResponseToTheOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResponseToTheOrders()
        {
            this.WhoAnswersToWhomOrders = new HashSet<WhoAnswersToWhomOrders>();
        }
    
        public int IdResponseToTheOrders { get; set; }
        public int IdOrders { get; set; }
    
        public virtual Orders Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WhoAnswersToWhomOrders> WhoAnswersToWhomOrders { get; set; }
    }
}
