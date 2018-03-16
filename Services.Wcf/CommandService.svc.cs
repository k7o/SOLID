namespace Services.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.ServiceModel;
    using Code;

    [ServiceContract(Namespace = "https://github.com/k7o/SOLID/commandservice/v1.0")]
    [ServiceKnownType(nameof(GetKnownTypes))]
    public class CommandService
    {
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider) =>
            Bootstrapper.CommandTypes;

        [OperationContract]
        [FaultContract(typeof(ValidationError))]
        public void Execute(dynamic command)
        {
            try
            {
                dynamic commandHandler = Bootstrapper.GetCommandHandler(command.GetType());

                commandHandler.Handle(command);
            }
            catch (Exception ex)
            {
                Bootstrapper.Log(ex);

                var faultException = WcfExceptionTranslator.CreateFaultExceptionOrNull(ex);

                if (faultException != null)
                {
                    throw faultException;
                }

                throw;
            }
        }
    }
}
