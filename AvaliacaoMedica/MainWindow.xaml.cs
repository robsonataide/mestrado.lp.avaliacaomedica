using AnthropometryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AvaliacaoMedica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person person;
        public MainWindow()
        {
            InitializeComponent();
            //this.TxtAltura.KeyUp += MaskNumber;
            this.person = new Person();
            
        }

        private void MaskNumber(object sender, KeyEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            String strText = txtBox.Text;
            int iValue = -1;

            bool convert = Int32.TryParse(strText, out iValue);
            if (!convert)
            {
                txtBox.Text = Regex.Replace(strText, "[^0-9.]", "");
            }
            txtBox.Select(txtBox.Text.Length, 0);
        }

    }
}
