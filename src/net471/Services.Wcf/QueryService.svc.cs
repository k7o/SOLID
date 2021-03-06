﻿namespace Services.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.ServiceModel;
    using Code;

    [ServiceContract(Namespace = "https://github.com/k7o/SOLID/queryservice/v1.0")]
    [ServiceKnownType(nameof(GetKnownTypes))]
    public class QueryService
    {
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider) =>
            Bootstrapper.QueryAndResultTypes;

        [OperationContract]
        [FaultContract(typeof(ValidationError))]
        public object Execute(dynamic query) => ExecuteQuery(query);

        internal static object ExecuteQuery(dynamic query)
        {
            Type queryType = query.GetType();

            try
            {
                using (Bootstrapper.BeginLifetimeScope())
                {
                    dynamic queryHandler = Bootstrapper.GetQueryHandler(query.GetType());
                    return queryHandler.Handle(query);
                }
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
