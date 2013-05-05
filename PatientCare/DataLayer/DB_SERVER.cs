using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DataLayer.EntityModel;

namespace DataLayer
{
    public class DB_SERVER : DbContext
    {
       
        public DB_SERVER()
            : base("name=db_connection")
        {
        }
        public DbSet<tUser> User { get; set; }
        public DbSet<tDoctor> Doctor { get; set; }
        public DbSet<tPatient> Patient { get; set; }
        public DbSet<tAddress> Address { get; set; }
    }
}
