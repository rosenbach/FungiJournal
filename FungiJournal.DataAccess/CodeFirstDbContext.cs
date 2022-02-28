using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FungiJournal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FungiJournal.DataAccess
{
    public class CodeFirstDbContext : DbContext
    {
        private readonly DataAccessConfiguration dataAccessConfiguration;

        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options,
            IOptions<DataAccessConfiguration> dataAccessConfiguration)
            : base(options)
        {
            this.dataAccessConfiguration = dataAccessConfiguration.Value;
        }
        public DbSet<Fungi>? Fungis => this.Set<Fungi>();
        public DbSet<Entry>? Entries => this.Set<Entry>();

        public List<Entry> LoadEntries()
        {
            return Entries?.ToList() ?? new List<Entry>();
        }


        #region Fluent API configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.Fungi);

        }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (dataAccessConfiguration.UseInMemoryDatabase)
            {
                optionsBuilder.UseInMemoryDatabase("Test");
            }
            else
            {
                optionsBuilder.UseSqlite(dataAccessConfiguration.ConnectionString);
            }

        }

    }
}
