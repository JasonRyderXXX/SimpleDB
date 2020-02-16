using SimpleDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDB.MSSql
{
    public class MSSqlBASICSimpleConnnectionString : ISimpleConnectionString
    {
        public string GetConnectionString()
        {
            var credentials = IsTrustedConnection ? $"Trusted_Connection=True;" : $"User Id={this.UserId};Password={this.Password};";
            return $"Server={this.Server};Database={this.Database}; {credentials}";
        }
        public string Server { get; set; } = "localhost";
        public string Database { get; set; } = "test";
        public string UserId { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsTrustedConnection { get; set; } = true;

    }
}
