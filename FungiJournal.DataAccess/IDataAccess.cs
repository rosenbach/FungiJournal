using FungiJournal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiJournal.DataAccess
{
    public interface IDataAccess
    {
        Task<List<Entry>> GetEntries();
        Task<Entry> GetEntry(int id);
        void AddEntry(Entry entry);
        void DeleteEntry(int id);
    }
}
