using AnthropometryLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthropometryLibrary.Models
{
    public class Person
    {
        //entrada de dado
        public String Name { get; set; }
        //entrada de dado
        public int Age { get; set; }
        //entrada de dado
        public GenderEnum Gender { get; set; }

        //entrada de dado
        public double Weight { get; set; }
        //entrada de dado
        public double Height { get; set; }

        //entrada de dado
        public List<SkinFold> SkinFolds { get; set; }

        //calculado
        public double SkinFoldSum
        {
            get
            {
                Type genderSkinFold = null;
                if (this.Gender.Equals(GenderEnum.Male))
                {
                    genderSkinFold = typeof(MaleTypeSkinFold);
                }
                else
                {
                    genderSkinFold = typeof(FemaleTypeSkinFold);
                }
                double sum = 0.0;
                sum = SkinFolds.Where(sf => Enum.GetValues(genderSkinFold).Cast<TypeSkinFoldEnum>().Contains(sf.TypeSkinFold)).Sum(sf => sf.Value);
                return sum;
            }
        }

        //entrada de dados e calculado
        public BodyComposition BodyComposition { get; set; }

        //entrada de dados
        public List<Circumference> Circumferences { get; set; }

        //calculado
        public double WaistHipRelation { get; set; }
        
        //calculado
        public double MaxVO2
        {
            get{
                if (this.TimeTest2400 != null) { 
                    return (480 / this.TimeTest2400) +3.5;
                }
                else
                {
                    return 0;
                }
            }
        }

        //entrada de dados
        public List<Load> MaxLoadsForOneRepeatTime { get; set; }
        
        //calculado
        public List<Load> MaxLoads { get; set; }

        //calculado
        public double BodySlimMass
        {
            get{
                return this.Weight - this.BodyComposition.FatWeight;
            }
        }

        //entrada de dados
        public double TimeTest2400 { get; set; }

        //calculado
        public double MET { get; set; }

        //entrada de dados
        public double RestVO2 { get; set; }
    }
}
