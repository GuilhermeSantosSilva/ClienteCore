using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;

namespace ClienteCore.DbHelper
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }
    }

    public class DataSettingsManager
    {
        private const string _dataSettingsFilePath = "appsettings.json";
        public virtual DataSettings LoadSettings()
        {
            var text = File.ReadAllText(_dataSettingsFilePath);
            if (string.IsNullOrEmpty(text))
                return new DataSettings();

            //get data settings from the JSON file  
            DataSettings settings = JsonConvert.DeserializeObject<DataSettings>(text);
            return settings;
        }

    }

    public class DataSettings
    {
        [JsonProperty("ConnectionString")]
        public string DataConnectionString { get; set; }
    }
}