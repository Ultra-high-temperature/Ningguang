using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Ningguang.DataAccess;

namespace Ningguang.Component
{
    public class TaskDbContextFactory
    :IDesignTimeDbContextFactory<NingTaskContext>
        // :DbContextFactory<NingTaskContext>
    {
        private static string connection = null;

        public static void InitConnectionString()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var resp = httpClient.GetAsync("https://localhost:5001/api/TaskConfigure/connectionString").Result;
            string str = resp.Content.ReadAsStringAsync().Result;
            string[] strings = str.Split("\"");
            connection = strings[1];
        }

        public NingTaskContext CreateDbContext(string[] args)
        {
            if (connection == null)
            {
                InitConnectionString();
            }
            var optionsBuilder = new DbContextOptionsBuilder<NingTaskContext>();
            ServerVersion serverVersion = ServerVersion.AutoDetect(connection);
            optionsBuilder.UseMySql(connection,serverVersion);
            return new NingTaskContext(optionsBuilder.Options);
        }
        
    }
}