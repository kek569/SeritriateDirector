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
    
    public partial class TypeOrders
    {
        public int IdTypeOrders { get; set; }
        public string NameTypeOrders { get; set; }
    
        public virtual Orders Orders { get; set; }
    }
}
