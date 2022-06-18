using FungiJournal.Domain.Models;
using Microsoft.EntityFrameworkCore;
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
        DbSet<Entry> GetEntries();
        Task<Entry> GetEntryAsync(int id);
        Task<int> AddEntryAsync(Entry entry);
        Task UpdateEntryAsync(Entry entry);
        Task DeleteEntryAsync(Entry entry);

        //Fungis
        Task<List<Fungi>> GetFungisAsync();
        DbSet<Fungi> GetFungis();
        Task<Fungi> GetFungiAsync(int id);
        Task<int> AddFungiAsync(Fungi fungi);
        Task UpdateFungiAsync(Fungi fungi);
        Task DeleteFungiAsync(Fungi fungi);
    }
}
