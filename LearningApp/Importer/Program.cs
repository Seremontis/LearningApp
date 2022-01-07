using System;

namespace Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            WordsDataImporter excel = new WordsDataImporter();
            excel.ImportData();
            Console.WriteLine("Hello World!");
        }
    }
}
