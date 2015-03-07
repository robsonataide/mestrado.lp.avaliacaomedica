using AnthropometryLibrary.Enums;
using AnthropometryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthropometryLibrary.Calc
{
    public class WeightsCalcLib
    {
        private Person person;

        public Person Calculate(Person person)
        {
            this.person = person;
            this.ResidualWeightCalc();
            this.FatWeightCalc();
            this.BoneWeightCalc();
            this.MuscleWeightCalc();
            return this.person;
        }

        private void BoneWeightCalc()
        {
            person.BodyComposition.BoneWeight = (Math.Pow(person.Height, 2) * person.BodyComposition.BonesMeasure.BiestiloideRadio * person.BodyComposition.BonesMeasure.BiepicodilianoFemur * 400);
            person.BodyComposition.BoneWeight = 3.02 * (Math.Pow(person.BodyComposition.BoneWeight, 0.712));
        }

        private void FatWeightCalc()
        {
            person.BodyComposition.FatWeight = (person.Weight * (person.BodyComposition.BodyFat/100));
        }

        private void MuscleWeightCalc()
        {
            person.BodyComposition.MuscleWeight = person.Weight - (person.BodyComposition.FatWeight + person.BodyComposition.BoneWeight + person.BodyComposition.ResidualWeight);
        }

        private void ResidualWeightCalc()
        {
            double residualPercent = 0.0;
            if (person.Gender.Equals(GenderEnum.Male))
            {
                residualPercent = 24.1;
            }
            else if (person.Gender.Equals(GenderEnum.Female))
            {
                residualPercent = 20.9;
            }
            person.BodyComposition.ResidualWeight = (person.Weight * residualPercent) / 100;
        }

    }
}
