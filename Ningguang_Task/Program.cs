using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ningguang.Component;

namespace Ningguang
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // JobHelp.loadJob(new TestJob(),"0/5 * * * * ?","123");
            // JobHelp.loadJob(new TestJob(),"0/5 * * * * ?","456");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}