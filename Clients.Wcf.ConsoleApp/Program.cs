namespace Clients.Wcf.ConsoleApp
{
    using Clients.Wcf.ConsoleApp.Controllers;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Bootstrapper.Bootstrap();

            var whitelistController = Bootstrapper.GetInstance<CommandExampleController>();

            whitelistController.AddAdres("1111AA");
            whitelistController.AddAdres("2000AA");

            whitelistController.AddBsnUzovi("121212", 32);
            whitelistController.AddBsnUzovi("44321212", 77);

            var whitelistQueryController = Bootstrapper.GetInstance<QueryExampleController>();
            whitelistQueryController.IsAdresInWhitelist("111AA");
            

            Console.ReadLine();
        }
    }
}
