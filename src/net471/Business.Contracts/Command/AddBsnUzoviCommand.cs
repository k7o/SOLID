﻿using Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Contracts.Command
{
    [Serializable]
    public class AddBsnUzoviCommand : IDataCommand
    {
        [Range(1, 999999999)]
        public int Bsnnummer { get; private set; }

        [Range(0, 9999)]
        public short Uzovi { get; private set; }

        public AddBsnUzoviCommand(int bsnnummer, short uzovi)
        {
            Bsnnummer = bsnnummer;
            Uzovi = uzovi;
        }
    }
}
