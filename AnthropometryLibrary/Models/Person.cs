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
        public double MaxVO2{ get; set; }

        //entrada de dados
        public List<MaxLoad> MaxLoadsForOneRepeatTime { get; set; }
        
        //calculado
        public List<MaxLoad> MaxLoads { get; set; }

    }
}
