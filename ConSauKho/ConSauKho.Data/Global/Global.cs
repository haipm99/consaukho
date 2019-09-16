using ConSauKho.Data.Models.Domains;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConSauKho.Data.Global
{
    public static partial class G
    {
        public static void Configure(IServiceCollection services)
        {
            MapperConfigs.Add(cfg =>
            {

            });

            services.AddScoped<UserDomain>();

            ConfigureAutomapper();
            ConfigureIoC(services);
        }
    }
}
