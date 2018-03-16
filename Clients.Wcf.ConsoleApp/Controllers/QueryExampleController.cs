namespace Clients.Wcf.ConsoleApp.Controllers
{
    using Contracts;
    using System;
    using System.Linq;

    public class QueryExampleController
    {
        private readonly IQueryProcessor queryProcessor;

        public QueryExampleController(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public void ShowAdressen(int pageIndex, int pageSize)
        {
            /*
            var orders = this.queryProcessor.Execute(new GetUnshippedOrdersForCurrentCustomerQuery
            {
                Paging = new PageInfo { PageIndex = pageIndex, PageSize = pageSize }
            });

            Console.WriteLine();
            Console.WriteLine("Query returned {0} orders: ", orders.Items.Length);

            foreach (var order in orders.Items)
            {
                Console.WriteLine("OrderId: {0}, Amount: {1}, ShipDate: {2:d}",
                    order.Id, order.TotalAmount, order.CreationDate);
            }

            Console.WriteLine("Total: " + orders.Items.Sum(order => order.TotalAmount));
            */
        }
    }
}
