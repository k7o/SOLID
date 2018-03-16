namespace Clients.Wcf.ConsoleApp.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class KnownCommandTypesAttribute : KnownTypesAttribute
    {
        public KnownCommandTypesAttribute() : base(new KnownTypesDataContractResolver(CommandTypes))
        {
        }

        private static IEnumerable<Type> CommandTypes =>
            from type in typeof(Business.Implementation.Command.AddAdresCommand).Assembly.GetExportedTypes()
            where type.Name.EndsWith("Command")
            select type;
    }
}