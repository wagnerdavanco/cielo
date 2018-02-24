using Cielo.Domain;
using System;

namespace Cielo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CieloClient cieloClient = new CieloClient("XXX", "XXX");

            var cieloSale = new CieloSale("123", "XXXXX", "XXXXXXXXXXX", "XXXXXXX", 01, 01, "XXX", EnumBrand.Master, 1, 1, "XXXXXXXXX");

            cieloClient.CreateSale(cieloSale).Wait();

            System.Console.WriteLine("Hello World!");
        }
    }
}
