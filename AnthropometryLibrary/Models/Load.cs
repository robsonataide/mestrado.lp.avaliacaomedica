﻿using AnthropometryLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnthropometryLibrary.Models
{
    public class Load 
    {
        //entrada de dados e calculado
        public TypeRMEnum TypeRM{ get; set; }
        //entrada de dados e calculado
        public int RepeatAmount { get; set; }
        //calculado
        public double MaxLoad { get; set; }

        //entrada de dados
        public double SubMaxLoad { get; set; }
    }
}
