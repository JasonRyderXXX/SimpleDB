using SimpleDB.Interfaces;
using SimpleDB.MSSql;
using SimpleDB.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace TestSimpleDBApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISimpleDB db = new MSSqlSimpleDB(new MSSqlBASICSimpleConnnectionString());
            var test22 = db
                .GetDataReader("test3", new Dictionary<string, object>())
                .ReadAs<testmodel>()
                .ToList();

                

                
            Console.WriteLine(test22.Count());
            Console.ReadLine();
        }
    }
}
