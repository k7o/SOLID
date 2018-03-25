using System;

namespace Clients.WebApi.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client("http://localhost:51964/");
            client.ApiCommandAddAdresPostAsync(new AddAdresCommand { Postcode = "2323" });
            var result = client.ApiQueryAdresGetAsync("1", "2323");

            Console.WriteLine("Hello World!");
        }
    }
}
