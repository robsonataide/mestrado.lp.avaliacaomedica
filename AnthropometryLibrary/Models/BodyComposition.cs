using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnthropometryLibrary.Models
{
    public class BodyComposition 
    {
        //calculado
        public double BodyDensity { get; set; }
        //calculado
        public double BodyFat { get; set; }

        //calculado
        public double MuscleWeight { get; set; }
        //calculado
        public double FatWeight { get; set; }
        //calculado
        public double BoneWeight { get; set; }
        //calculado
        public double ResidualWeight { get; set; }

        //entrada de dados
        public BoneMeasure BonesMeasure { get; set; }

        //calculado
        public double ConicalIndex { get; set; }

        //calculado (IMC)
        public double BodyMassIndex { get; set; }
    }
}
