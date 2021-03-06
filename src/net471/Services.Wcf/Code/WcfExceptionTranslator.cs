﻿namespace Services.Wcf.Code
{
    using Crosscutting.Validators;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ServiceModel;

    public static class WcfExceptionTranslator
    {
        public static FaultException CreateFaultExceptionOrNull(Exception exception)
        {
            if (exception is BrokenRulesException)
            {
                return new FaultException<ValidationError>(
                    new ValidationError { ErrorMessage = exception.Message }, exception.Message);
            }

#if DEBUG
            return new FaultException(exception.ToString());
#else
            return null;
#endif
        }
    }
}
