using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FungiJournal.Domain.Models;

namespace FungiJournal.DataAccess
{
    public class SQLiteDataAccess
    {

        public static List<Entry> LoadEntries()
        {
            //TODO put into a .config file and call via ConfigurationManager
            using (IDbConnection cnn = new SQLiteConnection("Data Source=..\\FungiJournal.DataAccess\\FungiJournalDb.db;Version=3;UseUTF16Encoding=True;"))
            {
                var output = cnn.Query<Entry>("select * from entries", new DynamicParameters());
                return output.ToList();
            };
        }

        public static void AddEntry(Entry entry)
        {
            //TODO put into a .config file and call via ConfigurationManager
            using (IDbConnection cnn = new SQLiteConnection("Data Source=..\\FungiJournal.DataAccess\\FungiJournalDb.db;Version=3;UseUTF16Encoding=True;"))
            {
                cnn.Execute("insert into entries (Description) values (@Description)", entry);
            };

        }



    }
}
