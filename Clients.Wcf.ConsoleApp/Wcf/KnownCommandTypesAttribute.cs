namespace Clients.Wcf.ConsoleApp.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public sealed class KnownCommandTypesAttribute : KnownTypesAttribute
    {
        public KnownCommandTypesAttribute() : base(new KnownTypesDataContractResolver(CommandTypes))
        {
        }

        private static IEnumerable<Type> CommandTypes =>
            from type in typeof(Business.Contracts.Command.AddAdresCommand).Assembly.GetExportedTypes()
            where type.Name.EndsWith("Command", true, CultureInfo.InvariantCulture)
            select type;
    }
}
