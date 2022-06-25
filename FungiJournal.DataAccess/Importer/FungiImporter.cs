using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FungiJournal.Domain.Models;

namespace FungiJournal.DataAccess.Importer
{
    public class FungiImporter
    {

        public static List<Fungi> Read(string path)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using StreamReader sr = new(@path, Encoding.UTF8);
                List<Fungi> ImportedFungis = new();
                String? line;


                string sep = "\t";

                // Read and display lines from the file until the end of
                // the file is reached.
                bool firstLine = true;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!firstLine)
                    {
                        var values = line.Split(sep.ToCharArray());

                        ImportedFungis.Add(new Fungi
                        {
                            CommonName = values[0],
                            LatinName = values[1],
                            Occurrence = values[2],
                            Season = values[3],
                            FoodValue = int.Parse(values[4])
                        });
                    }
                    else
                    {
                        firstLine = false;
                    }
                }
                return ImportedFungis;
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return new List<Fungi>();
            }
        }
    }
}
