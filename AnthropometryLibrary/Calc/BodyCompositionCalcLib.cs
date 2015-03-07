using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnthropometryLibrary.Calc;
using AnthropometryLibrary.Models;
using AnthropometryLibrary.Enums;

namespace AnthropometryLibrary
{
    public class BodyCompositionCalcLib
    {

        private Person person;

        public Person Calculate(Person person)
        {
            this.person = person;
            
            this.BodyMassIndexCalc();
            this.BodyDensityCalc();
            this.BodyFatCalc();
            this.ConicalIndexCalc();

            return person;
        }

        //Cálculo do IMC
        private void BodyMassIndexCalc()
        {
            person.BodyComposition.BodyMassIndex = person.Weight / Math.Pow(person.Height, 2);
        }

        //Cálculo usando Guedes
        private void BodyDensityCalc()
        {
            double logSumSkinfFolds = Math.Log10(this.person.SkinFoldSum);
            if (this.person.Gender.Equals(GenderEnum.Male))
            {
                this.person.BodyComposition.BodyDensity = 1.17136 - (0.06706 * logSumSkinfFolds);
            }
            else
            {
                this.person.BodyComposition.BodyDensity = 1.16650 - (0.07063 * logSumSkinfFolds);
            }
        }

        //Cálculo usando Siri
        private void BodyFatCalc()
        {
            this.person.BodyComposition.BodyFat = (495 / this.person.BodyComposition.BodyDensity) - 450;
        }


        private void ConicalIndexCalc()
        {
            double waistValue = this.person.Circumferences.Where(c => c.Type.Equals(TypeCircumferenceEnum.Waist)).First<Circumference>().Value;
            this.person.BodyComposition.ConicalIndex = (waistValue / 0.109) * Math.Sqrt(this.person.Weight / this.person.Height);
        }

    }
}
