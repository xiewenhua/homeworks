﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaleAppMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HomeworksDBEntities : DbContext
    {
        public HomeworksDBEntities()
            : base("name=HomeworksDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<batch> batches { get; set; }
        public virtual DbSet<batch_good> batch_goods { get; set; }
        public virtual DbSet<good> goods { get; set; }
        public virtual DbSet<ordergood> ordergoods { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<paytype> paytypes { get; set; }
        public virtual DbSet<repo_good> repo_goods { get; set; }
        public virtual DbSet<repository> repositories { get; set; }
        public virtual DbSet<saleprice> saleprices { get; set; }
        public virtual DbSet<snapshot> snapshots { get; set; }
        public virtual DbSet<storegood> storegoods { get; set; }
        public virtual DbSet<store> stores { get; set; }
        public virtual DbSet<vendor> vendors { get; set; }
    }
}
