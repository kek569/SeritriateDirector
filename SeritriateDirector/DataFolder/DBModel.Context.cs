﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Graphics> Graphics { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<Letters> Letters { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<ResponseToTheLetters> ResponseToTheLetters { get; set; }
        public virtual DbSet<ResponseToTheOrders> ResponseToTheOrders { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Secretary> Secretary { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestTwo> TestTwo { get; set; }
        public virtual DbSet<TypeLetters> TypeLetters { get; set; }
        public virtual DbSet<TypeOrders> TypeOrders { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WhoAnswersToWhomLetters> WhoAnswersToWhomLetters { get; set; }
        public virtual DbSet<WhoAnswersToWhomOrders> WhoAnswersToWhomOrders { get; set; }
    }
}
