using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthropometryLibrary.Enums
{
    public enum GenderEnum {
        [Description("Masculino")]
        Male,
        [Description("Feminino")]
        Female 
    };
    public enum SideEnum { Right, Left };

}
