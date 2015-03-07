using AnthropometryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnthropometryLibrary.Enums;

namespace AnthropometryLibrary.Calc
{
    public class RMCalc
    {
        private Person person;

        public Person Calculate(Person person)
        {
            this.AllOneMaxLoadCalc();
            return this.person;
        }

        private void AllOneMaxLoadCalc()
        {
            Array indexTypeMaxLoad = Enum.GetValues(typeof(TypeRMEnum));
            foreach(int index in indexTypeMaxLoad){
                this.OneMaxLoadCalc((TypeRMEnum)index);
            }
        }

        private void OneMaxLoadCalc(TypeRMEnum type){

            MaxLoad maxload = this.person.MaxLoadsForOneRepeatTime.Where(rm => rm.TypeRM.Equals(type)).First<MaxLoad>();
            if (type.Equals(TypeRMEnum.Supino))
            {
                maxload.Value = (maxload.Weight) / (1.0278 - (maxload.RepeatAmount * 0.0278));
            }
            else if (type.Equals(TypeRMEnum.RoscaDireta))
            {
                maxload.Value = (maxload.Weight) / (1.0278 - (maxload.RepeatAmount * 0.0278));
            }
            else if (type.Equals(TypeRMEnum.PuxadaFrontal))
            {
                maxload.Value = (maxload.Weight) / (1.0278 - (maxload.RepeatAmount * 0.0278));
            }
            else if (type.Equals(TypeRMEnum.ExtensaoJoelho))
            {
                maxload.Value = (maxload.Weight) / (1.0278 - (maxload.RepeatAmount * 0.0278));
            }

            this.person.MaxLoadsForOneRepeatTime.Where(rm => rm.TypeRM.Equals(type)).First<MaxLoad>().Value = maxload.Value;

        }
    }
}
