using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvaliacaoMedica.Util
{
    public class NumericTextBox : TextBox
    {

        public NumericTextBox() {
            DefaultStyleKey = typeof(NumericTextBox);
        }
        
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            String strText = this.Text;
            int iValue = -1;

            bool convert = Int32.TryParse(strText, out iValue);
            if (!convert)
            {
                this.Text = Regex.Replace(strText, "[^0-9.]", "");
            }
            this.Select(this.Text.Length, 0);
        }


    }
}
