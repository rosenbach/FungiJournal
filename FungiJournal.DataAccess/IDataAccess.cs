﻿using FungiJournal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiJournal.DataAccess
{
    public interface IDataAccess
    {
        void AddEntry(Entry entry);
        Entry GetEntry(int id);
        List<Entry> LoadEntries();
        void DeleteEntry(int id);
    }
}
