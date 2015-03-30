using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvaliacaoMedica
{
    static class UtilUI
    {
        public static void MaskNumber(object sender, KeyEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            String strText = txtBox.Text;
            double iValue = -1;

            bool convert = Double.TryParse(strText, out iValue);
            if (!convert)
            {
                txtBox.Text = Regex.Replace(strText, "[^0-9.]", "");
            }
            txtBox.Select(txtBox.Text.Length, 0);
        }

        public static void MaskInt(object sender, KeyEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            String strText = txtBox.Text;
            int iValue = -1;

            bool convert = Int32.TryParse(strText, out iValue);
            if (!convert)
            {
                txtBox.Text = Regex.Replace(strText, "[^0-9]", "");
            }
            txtBox.Select(txtBox.Text.Length, 0);
        }
    }
}
