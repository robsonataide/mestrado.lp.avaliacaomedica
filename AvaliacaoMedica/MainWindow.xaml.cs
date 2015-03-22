using AnthropometryLibrary.Enums;
using AnthropometryLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            this.FillComboSexo();
            
        }


        private void FillComboSexo(){
            foreach (var value in Enum.GetValues(typeof(GenderEnum)))
            {
                var type = typeof(GenderEnum);
                var memInfo = type.GetMember(value.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
                    false);
                var description = ((DescriptionAttribute)attributes[0]).Description;
                CbxSexo.Items.Add(description);
            }
        }

        private void FillComboDobra(GenderEnum gender)
        {
            CbxTipoDobra.Items.Clear();

            Type type = (GenderEnum.Male.Equals(gender)) ? typeof(MaleTypeSkinFold) : typeof(FemaleTypeSkinFold);
            foreach (var value in Enum.GetValues(type))
            {
                var memInfo = type.GetMember(value.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
                    false);
                var description = ((DescriptionAttribute)attributes[0]).Description;
                CbxTipoDobra.Items.Add(description);
            }
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

        private void CbxSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var genderSelected = EnumHelper.GetValueFromDescription<GenderEnum>(CbxSexo.SelectedValue.ToString());
            this.FillComboDobra(genderSelected);
        }

        private void BtnAdicionarDobra_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TxtValorDobra.Text) && CbxTipoDobra.SelectedValue!=null)
            {
                try
                {
                    var skinfoldSelected = EnumHelper.GetValueFromDescription<TypeSkinFoldEnum>(CbxTipoDobra.SelectedValue.ToString());
                    double valor = Convert.ToDouble(TxtValorDobra.Text);
                    bool hasModification = false;
                    foreach (SkinFold skinfold in LvDobras.Items.Cast<SkinFold>())
                    {
                        if (skinfold.TypeSkinFold.Equals(skinfoldSelected))
                        {
                            skinfold.Value = valor;
                            hasModification = true;
                        }
                    }

                    if (!hasModification)
                    {
                        LvDobras.Items.Add(new SkinFold { TypeSkinFold = skinfoldSelected, Value = valor });
                    }
                    else
                    {
                        LvDobras.Items.Refresh();
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Informe um valor válido.");
                }
                
            }
        }

    }
}
