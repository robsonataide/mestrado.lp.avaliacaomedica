using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthropometryLibrary.Enums
{
    public enum TypeSkinFoldEnum
    {
        [Description("Tríceps")]
        Triceps,
        [Description("Supra-ilíca")]
        SupraIliac,
        [Description("Abdominal")]
        Abdominal,
        [Description("Coxa")]
        Thigh,
        [Description("Subescapular")]
        Subscapular
    }

    public enum MaleTypeSkinFold 
    { 
        [Description("Tríceps")]
        Triceps,
        [Description("Supra-ilíca")]
        SupraIliac,
        [Description("Abdominal")]
        Abdominal 
    }

    public enum FemaleTypeSkinFold 
    {
        [Description("Coxa")]
        Thigh,
        [Description("Supra-ilíca")]
        SupraIliac,
        [Description("Subescapular")]
        Subscapular 
    }
}
