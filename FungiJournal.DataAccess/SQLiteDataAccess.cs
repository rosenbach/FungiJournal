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

        public async Task<List<Entry>> GetEntries()
        {
            var output = await codeFirstDbContext.Entries.ToListAsync();
            return output;
        }

        public async Task<Entry> GetEntry(int id)
        {
            var output = await codeFirstDbContext.Entries.FindAsync(id);

            return output;
        }

        public async void AddEntry(Entry entry)
        {
            codeFirstDbContext.Entries?.Add(entry);
            await codeFirstDbContext.SaveChangesAsync();
        }

        public async void DeleteEntry(int id)
        {
            codeFirstDbContext.Entries?.Remove(await GetEntry(id));
            await codeFirstDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            codeFirstDbContext.Dispose();
        }
    }
}