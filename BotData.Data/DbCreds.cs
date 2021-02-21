using System;

namespace BotData.Data
{
    public class DbCreds
    {
        public string Url { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public string ConnectionString => $"Host={Url};Username={Username};Password={Password};Database={Database};Port={Port}";

        public DbCreds()
        {
            Url = Environment.GetEnvironmentVariable("DB_URL");
            Port = Environment.GetEnvironmentVariable("DB_PORT");
            Username = Environment.GetEnvironmentVariable("DB_USER");
            Password = Environment.GetEnvironmentVariable("DB_PASS");
            Database = Environment.GetEnvironmentVariable("DB_NAME");
        }

        public static DbCreds GetCreds()
        {
            var dbCreds = new DbCreds();

            if (string.IsNullOrEmpty(dbCreds.Url) || string.IsNullOrEmpty(dbCreds.Username) || string.IsNullOrEmpty(dbCreds.Password) || string.IsNullOrEmpty(dbCreds.Database))
                throw new ArgumentException("Missing postgres db environment variables");

            return dbCreds;
        }
    }
}