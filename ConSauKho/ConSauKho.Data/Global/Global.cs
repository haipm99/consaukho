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

            ConfigureAutomapper();
            ConfigureIoC(services);
        }
    }
}
