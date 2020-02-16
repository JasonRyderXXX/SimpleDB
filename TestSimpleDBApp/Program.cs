using SimpleDB.Interfaces;
using SimpleDB.MSSql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSimpleDBApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISimpleDB db = new MSSqlSimpleDB(new MSSqlBASICSimpleConnnectionString());

            var test3 = db.GetScaler("test3", new Dictionary<string, object>());
            Console.WriteLine(test3);
            var test = db.GetDataSet("test", new Dictionary<string, object>());
            Console.WriteLine(test.Tables.Count);
            var test1 = db.GetDataReaders("test", new Dictionary<string, object>());
            Console.WriteLine(test1.Count());
            var test2 = db.GetDataReaders("test2", new Dictionary<string, object>());
            Console.WriteLine(test2.Count());
            var test21 = db.GetDataSet("test2", new Dictionary<string, object>());
            Console.WriteLine(test21.Tables.Count);
            var test22 = db.GetDataReader("test2", new Dictionary<string, object>());
            Console.WriteLine(test22.Read());
            Console.ReadLine();
        }
    }
}
