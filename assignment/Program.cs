using assignment.Servers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace assignment
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IntegrityConstraints integrityConstraints = new IntegrityConstraints();
			Timer myTimer = new Timer(integrityConstraints.examinationOnGoodTime, null, 4000, 4000);
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
