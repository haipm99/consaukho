using System;
using TNT.Core.Template.DataService;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new SimpleGenerator(
                    "ConSauKho.Data","localhost","ConSauKhoDev","sa","huy123456","Models","ConSauKhoContext","../../../../ConSauKho.Data"
                );

            gen.Regen(args);
        }
    }
}
