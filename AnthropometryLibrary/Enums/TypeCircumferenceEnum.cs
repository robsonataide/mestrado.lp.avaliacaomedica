using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthropometryLibrary.Enums
{
    public enum TypeCircumferenceEnum
    {
        [Description("Tórax"), HasSide(false)]
        Thorax,
        [Description("Braço"), HasSide(true)]
        Arm,
        [Description("Antebraço"), HasSide(true)]
        Forearm,
        [Description("Cintura"), HasSide(false)]
        Waist,
        [Description("Abdomên"), HasSide(false)]
        Abdomen,
        [Description("Quadril"), HasSide(false)]
        Hip,
        [Description("Coxa"), HasSide(true)]
        Thigh,
        [Description("Panturrilha"), HasSide(true)]
        Calf
    }

    public class HasSideAttribute : Attribute
    {
        public bool HasSide{get; set;}
        
        public HasSideAttribute(bool hasSide)
        {
            this.HasSide = hasSide;
        }

        
    }
}
