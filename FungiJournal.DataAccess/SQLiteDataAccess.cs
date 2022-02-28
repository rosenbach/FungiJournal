using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FungiJournal.Domain.Models;
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

        public List<Entry> LoadEntries()
        {
            var output = codeFirstDbContext.Entries?.ToList();

            return output?.ToList() ?? new List<Entry>();
        }

        public Entry GetEntry(int id)
        {
            var output = codeFirstDbContext.Entries?.Find(id);

            return output;
        }

        public void AddEntry(Entry entry)
        {
            codeFirstDbContext.Entries?.Add(entry);
            codeFirstDbContext.SaveChanges();
        }

        public void DeleteEntry(int id)
        {
            codeFirstDbContext.Entries?.Remove(GetEntry(id));
            codeFirstDbContext.SaveChanges();
        }

        public void Dispose()
        {
            codeFirstDbContext.Dispose();
        }
    }
}