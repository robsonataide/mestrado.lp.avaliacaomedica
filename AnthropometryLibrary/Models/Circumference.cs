using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnthropometryLibrary.Enums;

namespace AnthropometryLibrary.Models
{
    public class Circumference
    {
        //entrada de dados
        public TypeCircumferenceEnum Type { get; set; }
        //entrada de dados
        public SideEnum Side { get; set; }
        //entrada de dados
        public double Value { get; set; }
    }
}
