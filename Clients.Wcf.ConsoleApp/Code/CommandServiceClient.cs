﻿namespace Clients.Wcf.ConsoleApp.Code
{
    using Clients.Wcf.ConsoleApp.Wcf;
    using System.Diagnostics;
    using System.ServiceModel;

    // This service reference is hand-coded. This allows us to use our custom KnownCommandTypesAttribute, 
    // which allows providing WCF with known types at runtime. This prevents us to have to update the client 
    // reference each time a new command is added to the system.
    [KnownCommandTypes]
    [ServiceContract(
        Namespace = "https://github.com/k7o/SOLID/commandservice/v1.0",
        ConfigurationName = "CommandServices.CommandService")]
    public interface CommandService
    {
        [OperationContract(
            Action = "https://github.com/k7o/SOLID/commandservice/v1.0/CommandService/Execute",
            ReplyAction = "https://github.com/k7o/SOLID/commandservice/v1.0/CommandService/ExecuteResponse")]
        object Execute(object command);
    }

    public class CommandServiceClient : ClientBase<CommandService>, CommandService
    {
        [DebuggerStepThrough]
        public object Execute(object command) => this.Channel.Execute(command);
    }
}