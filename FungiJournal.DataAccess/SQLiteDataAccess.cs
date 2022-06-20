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
    public class SQLiteDataAccess : IDataAccess, IDisposable
    {
        private readonly CodeFirstDbContext codeFirstDbContext;

        public SQLiteDataAccess(CodeFirstDbContext codeFirstDbContext)
        {
            this.codeFirstDbContext = codeFirstDbContext;
        }

        public Task<List<Entry>> GetEntriesAsync()
        {
            return codeFirstDbContext.Entries!
                .Include(x => x.Fungi)
                .ToListAsync();
        }

        public DbSet<Entry> GetEntries()
        {
            return codeFirstDbContext.Entries!;
        }

        public async Task<Entry> GetEntryAsync(int id)
        {
            var output = await codeFirstDbContext.Entries!.FindAsync(id);
            return output!;
        }

        public Task<int> AddEntryAsync(Entry entry)
        {
            entry.GenerateCreationDate();
            codeFirstDbContext.Entries?.Add(entry);
            return codeFirstDbContext.SaveChangesAsync();
        }

        public Task DeleteEntryAsync(Entry entry)
        {
            codeFirstDbContext.Entries?.Remove(entry);
            return codeFirstDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            codeFirstDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task UpdateEntryAsync(Entry entry)
        {
            codeFirstDbContext.Entry(entry).State = EntityState.Modified;
            try
            {
                entry.UpdateTimestamp();
                return codeFirstDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public Task<List<Fungi>> GetFungisAsync()
        {
            return codeFirstDbContext.Fungis!.ToListAsync();
        }

        public DbSet<Fungi> GetFungis()
        {
            return codeFirstDbContext.Fungis!;
        }

        public async Task<Fungi> GetFungiAsync(int id)
        {
            var output = await codeFirstDbContext.Fungis!.FindAsync(id);
            return output!;
        }

        public Task<int> AddFungiAsync(Fungi fungi)
        {
            codeFirstDbContext.Fungis?.Add(fungi);
            return codeFirstDbContext.SaveChangesAsync();
        }

        public Task DeleteFungiAsync(Fungi fungi)
        {
            codeFirstDbContext.Fungis?.Remove(fungi);
            return codeFirstDbContext.SaveChangesAsync();
        }
        public Task UpdateFungiAsync(Fungi fungi)
        {
            codeFirstDbContext.Entry(fungi).State = EntityState.Modified;
            try
            {
                return codeFirstDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}