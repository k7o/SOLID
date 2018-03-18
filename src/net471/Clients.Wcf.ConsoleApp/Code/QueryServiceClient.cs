namespace Clients.Wcf.ConsoleApp.Code
{
    using Clients.Wcf.ConsoleApp.Wcf;
    using System.Diagnostics;
    using System.ServiceModel;

    // This service reference is hand-coded. This allows us to use the KnownQueryAndResultTypesAttribute, 
    // which allows providing WCF with known types at runtime. This prevents us to have to update the client 
    // reference each time a new command is added to the system.
    [KnownQueryAndResultTypes]
    [ServiceContract(
        Namespace = "https://github.com/k7o/SOLID/queryservice/v1.0",
        ConfigurationName = "QueryServices.QueryService")]
    public interface QueryService
    {
        [OperationContract(
            Action = "https://github.com/k7o/SOLID/queryservice/v1.0/QueryService/Execute",
            ReplyAction = "https://github.com/k7o/SOLID/queryservice/v1.0/QueryService/ExecuteResponse")]
        object Execute(object query);
    }

    public class QueryServiceClient : ClientBase<QueryService>, QueryService
    {
        [DebuggerStepThrough]
        public object Execute(object query) => this.Channel.Execute(query);
    }
}
