﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TryOut_Dekstop1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MotorcycleRepairEntities : DbContext
    {
        public MotorcycleRepairEntities()
            : base("name=MotorcycleRepairEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DetailProduct> DetailProduct { get; set; }
        public virtual DbSet<DetailService> DetailService { get; set; }
        public virtual DbSet<Mechanics> Mechanics { get; set; }
        public virtual DbSet<MotorcycleServices> MotorcycleServices { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<TransactionService> TransactionService { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}