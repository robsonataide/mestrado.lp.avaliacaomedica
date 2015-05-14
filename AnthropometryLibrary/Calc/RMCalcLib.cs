using AnthropometryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnthropometryLibrary.Enums;

namespace AnthropometryLibrary.Calc
{
    public class RMCalcLib
    {
        private Person person;

        public Person Calculate(Person person)
        {
            this.person = person;
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
            if (this.person.MaxLoadsForOneRepeatTime != null) { 
                Load maxload = this.person.MaxLoadsForOneRepeatTime.Where(rm => rm.TypeRM.Equals(type)).First<Load>();
                
                if (type.Equals(TypeRMEnum.ExtensaoJoelho))
                {
                    //Brzycki
                    maxload.MaxLoad = (maxload.SubMaxLoad) / (1.0278 - (maxload.RepeatAmount * 0.0278));
                }
                else
                {
                   //Adams
                    maxload.MaxLoad = (maxload.SubMaxLoad) / (100 - (2 * maxload.RepeatAmount)) * 100;
                }
                this.person.MaxLoadsForOneRepeatTime.Where(rm => rm.TypeRM.Equals(type)).First<Load>().MaxLoad = maxload.MaxLoad;
            }
        }
    }
}
