using FungiJournal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiJournal.DataAccess
{
    public class CodeFirstAccess : DbContext
    {
        internal class MyContext : DbContext
        {
            public DbSet<Fungi> Fungis { get; set; } = default!;
            public DbSet<Entry> Entries { get; set; } = default!;


            #region Fluent API configuration
            /*
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Entry>()
                    .Property(e => e.EntryId)
                    .IsRequired();
                modelBuilder.Entity<Fungi>()
                    .Property(f => f.FungiId)
                    .IsRequired();
            }
            */
            #endregion
        }
    }
}
