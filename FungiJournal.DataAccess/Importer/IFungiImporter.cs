using FungiJournal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiJournal.DataAccess.Importer
{
    public interface IFungiImporter
    {
        public void ImportFungis(string path);
        public void ImportFungi(Fungi fungi);
    }
}
