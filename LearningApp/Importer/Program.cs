using System;

namespace Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelOperations excel = new ExcelOperations();
            excel.ImportData();
            Console.WriteLine("Hello World!");
        }
    }
}
