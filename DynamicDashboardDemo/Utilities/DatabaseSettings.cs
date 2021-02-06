using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Utilities
{
    public class DatabaseSettings
    {
        public string SqlConnectionString { get; set; }
        public string MongoConnectionString { get; set; }
        public string MongoDatabaseName { get; set; }
        public string MongoProcessParamCollectionName { get; set; }
        public string MongoAGIProcessParamCollectionName { get; set; }
    }
}
