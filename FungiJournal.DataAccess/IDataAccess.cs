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
        Task<List<Entry>> GetEntriesAsync();
        Task<Entry> GetEntryAsync(int id);
        Task<int> AddEntryAsync(Entry entry);
        Task UpdateEntryAsync(int id, Entry entry);
        Task DeleteEntryAsync(Entry entry);
    }
}
