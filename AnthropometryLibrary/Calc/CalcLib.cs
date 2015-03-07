using AnthropometryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnthropometryLibrary.Enums;

namespace AnthropometryLibrary.Calc
{
    public class CalcLib
    {
        private Person person;

        public Person Calculate(Person person)
        {
            this.person = person;

            BodyCompositionCalcLib bodyCompositionLib = new BodyCompositionCalcLib();
            this.person = bodyCompositionLib.Calculate(this.person);

            WeightsCalcLib weightsCalcLib = new WeightsCalcLib();
            this.person = weightsCalcLib.Calculate(this.person);

            this.WaistHipRelationCalc();

            return this.person;
        }

        private void WaistHipRelationCalc()
        {
            double waist = person.Circumferences.Where(c => c.Type.Equals(TypeCircumferenceEnum.Waist)).First<Circumference>().Value;
            double hip = person.Circumferences.Where(c => c.Type.Equals(TypeCircumferenceEnum.Hip)).First<Circumference>().Value;
            this.person.WaistHipRelation = waist / hip;
        }
    }
}
