﻿namespace Clients.Wcf.ConsoleApp.Code
{
    using Contracts;
    using System;
    using System.Diagnostics;

    public sealed class WcfServiceCommandHandlerProxy<TCommand> : ICommandStrategyHandler<TCommand> where TCommand : ICommand
    {
        [DebuggerStepThrough]
        public void Handle(TCommand command)
        {
            var service = new CommandServiceClient();

            try
            {
                service.Execute(command);
            }
            finally
            {
                try
                {
                    ((IDisposable)service).Dispose();
                }
                catch
                {
                    // Against good practice and the Framework Design Guidelines, WCF can throw an
                    // exception during a call to Dispose, which can result in loss of the original exception.
                    // See: https://marcgravell.blogspot.com/2008/11/dontdontuse-using.html
                    // See: https://msdn.microsoft.com/en-us/library/aa355056.aspx
                }
            }
        }
    }
}
