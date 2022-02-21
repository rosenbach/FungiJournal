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
    public class SQLiteDataAccess : IDataAccess
    {
        private readonly DataAccessConfiguration dataAccessConfiguration;

        public IDbConnection Connection => new SQLiteConnection(dataAccessConfiguration.ConnectionString);

        public SQLiteDataAccess(IOptions<DataAccessConfiguration> dataAccessConfiguration)
        {
            this.dataAccessConfiguration = dataAccessConfiguration.Value;
        }

        public List<Entry> LoadEntries()
        {
            using IDbConnection cnn = Connection;
            var output = cnn.Query<Entry>("select * from entries", new DynamicParameters());
            return output.ToList();
        }

        public void AddEntry(Entry entry)
        {
            using IDbConnection cnn = Connection;
            cnn.Execute("insert into entries (Description) values (@Description)", entry);
        }
    }
}