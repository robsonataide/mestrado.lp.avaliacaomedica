using AnthropometryLibrary.Calc;
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
            this.FillComboCircunferencia();
            TxtAltura.KeyUp += UtilUI.MaskNumber;
            TxtIdade.KeyUp += UtilUI.MaskInt;
            TxtPeso.KeyUp += UtilUI.MaskNumber;
            
            TxtBMPRepouso.KeyUp += UtilUI.MaskNumber;
            TxtDiastolica.KeyUp += UtilUI.MaskNumber;
            TxtSistolica.KeyUp += UtilUI.MaskNumber;
            TxtTempo2400.KeyUp += UtilUI.MaskNumber;
            
            TxtValorDobra.KeyUp += UtilUI.MaskNumber;
            TxtValorCirc.KeyUp += UtilUI.MaskNumber;
            
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

        private void CbxSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var genderSelected = EnumHelper.GetValueFromDescription<GenderEnum>(CbxSexo.SelectedValue.ToString());
            this.FillComboDobra(genderSelected);
            LvDobras.Items.Clear();
            TabMeasures.IsEnabled = true;
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
                            this.person.SkinFolds.Where<SkinFold>(sf => sf.TypeSkinFold.Equals(skinfoldSelected)).FirstOrDefault<SkinFold>().Value = valor;
                        }
                    }

                    if (!hasModification)
                    {
                        SkinFold skinfold = new SkinFold { TypeSkinFold = skinfoldSelected, Value = valor };
                        LvDobras.Items.Add(skinfold);
                        this.person.SkinFolds.Add(skinfold);
                    }
                    else
                    {
                        LvDobras.Items.Refresh();
                    }
                    TxtValorDobra.Text = String.Empty;

                }
                catch (Exception)
                {
                    MessageBox.Show("Informe um valor válido.");
                }
                
            }
        }

        private void FillPerson()
        {
            try
            {
                this.person.Name = TxtNome.Text;
                this.person.Age = Convert.ToInt32(TxtIdade.Text);
                this.person.Gender = EnumHelper.GetValueFromDescription<GenderEnum>(CbxSexo.SelectedValue.ToString());
                this.person.Weight = Convert.ToDouble(TxtPeso.Text);
                this.person.Height = Convert.ToDouble(TxtAltura.Text);
                //TODO: colocar dados de pressão no obj pessoa
                this.person.RestVO2 = Convert.ToDouble(TxtBMPRepouso.Text);
                this.person.TimeTest2400 = Convert.ToInt64(TxtTempo2400.Text);

            }
            catch (Exception ex)
            {
                TabControl.SelectedIndex = 0;
                TabMeasures.IsEnabled = false;
                TabDiagnose.IsEnabled = false;
                MessageBox.Show("Pro favor, preencha os dados corretamente.");
            }
        }

        private void FillComboCircunferencia()
        {
            CbxTipoCirc.Items.Clear();

            foreach (var value in Enum.GetValues(typeof(TypeCircumferenceEnum)))
            {
                var memInfo = typeof(TypeCircumferenceEnum).GetMember(value.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
                    false);
                var description = ((DescriptionAttribute)attributes[0]).Description;
                CbxTipoCirc.Items.Add(description);
            }
        }

        private void TxtTempo2400_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(TxtTempo2400.Text)))
            {
                this.person.TimeTest2400 = Convert.ToDouble(TxtTempo2400.Text);
                this.TxtVo2.Text = Math.Round(this.person.MaxVO2, 2).ToString() + " l/m";
                this.TxtVo2.ToolTip = "Cálculo utilizando teste de cooper (480/"+TxtTempo2400.Text+") + 3,5 ml/min.";
                PnlVo2Max.Visibility = Visibility.Visible;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.FillPerson();
        }

        private void BtnAdicionarCirc_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TxtValorCirc.Text) && CbxTipoCirc.SelectedValue != null && !(String.IsNullOrEmpty(CbxTipoCirc.SelectedValue.ToString())))
            {
                try
                {
                    var circSelected = EnumHelper.GetValueFromDescription<TypeCircumferenceEnum>(CbxTipoCirc.SelectedValue.ToString());
                    double valor = Convert.ToDouble(TxtValorCirc.Text);

                    if (!RdbDireito.IsChecked.Value && !RdbEsquerdo.IsChecked.Value)
                    {
                        MessageBox.Show("Selecione o lado.");
                        throw new Exception("Selecione o lado.");
                    }

                    bool hasModification = false;
                    foreach (Circumference circumference in LvCircunferencias.Items.Cast<Circumference>())
                    {
                        if (circumference.Type.Equals(circSelected))
                        {
                            circumference.Value = valor;
                            hasModification = true;
                        }
                    }

                    if (!hasModification)
                    {
                        LvCircunferencias.Items.Add(new Circumference{ Type= circSelected, Value = valor });
                    }
                    else
                    {
                        LvCircunferencias.Items.Refresh();
                    }
                    TxtValorCirc.Text = String.Empty;

                }
                catch (Exception)
                {
                    MessageBox.Show("Informe um valor válido.");
                }

                
            }
        }

    }
}
