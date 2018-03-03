﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Adres
    {
        public string Postcode { get; private set; }

        public Adres(string postcode)
        {
            Postcode = postcode;
        }
        
        // Default constructors are needed for certain ORM frameworks
        private Adres()
        {
        }
    }
}
