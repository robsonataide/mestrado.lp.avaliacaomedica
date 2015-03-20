using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnthropometryLibrary;
using AnthropometryLibrary.Enums;
using System.Collections.Generic;
using AnthropometryLibrary.Models;
using AnthropometryLibrary.Calc;

namespace UnitTests
{
    [TestClass]
    public class CalcTests
    {

        private Person person;

        private void createPerson()
        {
            
            this.person  = new Person { Gender = GenderEnum.Male, Height = 1.8, Weight = 86.9, Age = 45, TimeTest2400 = 14.98 };

            this.person.SkinFolds = new List<SkinFold>();
            this.person.SkinFolds.Add(new SkinFold { TypeSkinFold = TypeSkinFoldEnum.Triceps, Value = 18.33 });
            this.person.SkinFolds.Add(new SkinFold { TypeSkinFold = TypeSkinFoldEnum.SupraIliac, Value = 28.33 });
            this.person.SkinFolds.Add(new SkinFold { TypeSkinFold = TypeSkinFoldEnum.Abdominal, Value = 24.67 });
            this.person.SkinFolds.Add(new SkinFold { TypeSkinFold = TypeSkinFoldEnum.Thigh, Value = 26.33 });
            
            this.person.Circumferences = new List<Circumference>();
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Thorax, Value = 99.5 });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Arm, Value = 31.5, Side = SideEnum.Right });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Arm, Value = 31.0, Side = SideEnum.Left });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Forearm, Value = 29.5, Side = SideEnum.Right });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Forearm, Value = 28.5, Side = SideEnum.Left });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Waist, Value = 88.5 });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Abdomen, Value = 97.0 });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Hip, Value = 105.0 });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Thigh, Value = 63.5, Side = SideEnum.Right });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Thigh, Value = 63.0, Side = SideEnum.Left });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Calf, Value = 39.5, Side = SideEnum.Right });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Calf, Value = 39.5, Side = SideEnum.Left });

            this.person.BodyComposition = new BodyComposition { BonesMeasure = new BoneMeasure { BiestiloideRadio = 0.061, BiepicodilianoFemur = 0.114 } };

            this.person.MaxLoadsForOneRepeatTime = new List<Load>();
            this.person.MaxLoadsForOneRepeatTime.Add(new Load { TypeRM = TypeRMEnum.Supino, SubMaxLoad = 100, RepeatAmount = 20 });
            this.person.MaxLoadsForOneRepeatTime.Add(new Load { TypeRM = TypeRMEnum.RoscaDireta, SubMaxLoad = 100, RepeatAmount = 20 });
            this.person.MaxLoadsForOneRepeatTime.Add(new Load { TypeRM = TypeRMEnum.PuxadaFrontal, SubMaxLoad = 100, RepeatAmount = 20 });
            this.person.MaxLoadsForOneRepeatTime.Add(new Load { TypeRM = TypeRMEnum.ExtensaoJoelho, SubMaxLoad = 100, RepeatAmount = 20 });

        }
        
        
        [TestMethod]
        public void CalcTest()
        {
            this.createPerson();
            bool testSuccess = false;
            
            CalcLib calcLib = new CalcLib();
            
            try
            {
                this.person = calcLib.Calculate(person);
                testSuccess = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                testSuccess = false;
            }
            
            Console.WriteLine("IMC : " + this.person.BodyComposition.BodyMassIndex);
            Console.WriteLine("Densidade corporal : " + this.person.BodyComposition.BodyDensity);
            Console.WriteLine("Percentual de gordura : " + this.person.BodyComposition.BodyFat);
            Console.WriteLine("Peso gordo : " + this.person.BodyComposition.FatWeight);
            Console.WriteLine("Peso dos ossos : " + this.person.BodyComposition.BoneWeight);
            Console.WriteLine("Peso residual : " + this.person.BodyComposition.ResidualWeight);
            Console.WriteLine("Peso muscular : " + this.person.BodyComposition.MuscleWeight);
            Console.WriteLine("Peso da massa magra : " + this.person.BodySlimMass);
            Console.WriteLine("Relação Cintura/Quadril : " + this.person.WaistHipRelation);
            Console.WriteLine("Índice de conicidade : " + this.person.BodyComposition.ConicalIndex);

            Console.WriteLine("VO2 Máximo : " + this.person.MaxVO2);
            Console.WriteLine("VO2 Repouso : " + this.person.RestVO2);
            Console.WriteLine("MET : " + this.person.MET);

            if (this.person.MaxLoadsForOneRepeatTime != null && this.person.MaxLoadsForOneRepeatTime.Count > 0)
            {
                Console.WriteLine("Carga máxima para uma repetição:");
                foreach (var maxLoad in this.person.MaxLoadsForOneRepeatTime)
                {
                    Console.WriteLine("\t"+maxLoad.TypeRM.ToString() + " = " + maxLoad.MaxLoad);
                }
            }

            Assert.IsTrue(testSuccess);

        }
        
    }
}
