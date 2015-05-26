using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace appPDU
{
    public class Settings
    {
        public string Database { get; set; }
        public string MongoConnection { get; set; }
    }
}
