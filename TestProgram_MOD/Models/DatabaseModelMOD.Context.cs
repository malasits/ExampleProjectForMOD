﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestProgram_MOD.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbForMODEntity : DbContext
    {
        public DbForMODEntity()
            : base("name=DbForMODEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<PeopleID> PeopleID { get; set; }
        public virtual DbSet<V_GetAllDataFromDatabase> V_GetAllDataFromDatabase { get; set; }
    
        public virtual int P_DeleteData(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("P_DeleteData", iDParameter);
        }
    
        public virtual ObjectResult<P_GetPersonByID_Result> P_GetPersonByID(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<P_GetPersonByID_Result>("P_GetPersonByID", iDParameter);
        }
    
        public virtual int P_InsertData(string firstName, string lastName, Nullable<System.DateTime> bornDate)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var bornDateParameter = bornDate.HasValue ?
                new ObjectParameter("BornDate", bornDate) :
                new ObjectParameter("BornDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("P_InsertData", firstNameParameter, lastNameParameter, bornDateParameter);
        }
    
        public virtual int P_UpdateData(Nullable<int> iD, string firstName, string lastName, Nullable<System.DateTime> bornDate)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var bornDateParameter = bornDate.HasValue ?
                new ObjectParameter("BornDate", bornDate) :
                new ObjectParameter("BornDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("P_UpdateData", iDParameter, firstNameParameter, lastNameParameter, bornDateParameter);
        }
    }
}
