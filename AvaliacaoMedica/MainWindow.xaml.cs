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
            this.FillComboExercicio();
            TxtAltura.KeyUp += UtilUI.MaskNumber;
            TxtIdade.KeyUp += UtilUI.MaskInt;
            TxtPeso.KeyUp += UtilUI.MaskNumber;
            
            TxtBMPRepouso.KeyUp += UtilUI.MaskNumber;
            TxtDiastolica.KeyUp += UtilUI.MaskNumber;
            TxtSistolica.KeyUp += UtilUI.MaskNumber;
            TxtTempo2400.KeyUp += UtilUI.MaskNumber;
            TxtValorCirc.KeyUp += UtilUI.MaskNumber;
            
            TxtValorDobra.KeyUp += UtilUI.MaskNumber;
            TxtValorCirc.KeyUp += UtilUI.MaskNumber;

            TxtRepeticoes.KeyUp += UtilUI.MaskInt;
            TxtSubmaxima.KeyUp += UtilUI.MaskNumber;

            TxtBiestiloideRadio.KeyUp += UtilUI.MaskNumber;
            TxtBiepicodilianoFemur.KeyUp += UtilUI.MaskNumber;
            
        }

        private void FillComboExercicio()
        {
            foreach (var value in Enum.GetValues(typeof(TypeRMEnum)))
            {
                CbxTipoRM.Items.Add(value.ToString());
            }
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
                        if (this.person.SkinFolds == null)
                        {
                            this.person.SkinFolds = new List<SkinFold>();
                        }
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
                this.person.RestVO2 = Convert.ToDouble(TxtBMPRepouso.Text);
                this.person.TimeTest2400 = Convert.ToInt64(TxtTempo2400.Text);
                BoneMeasure bm = new BoneMeasure { BiestiloideRadio = Convert.ToDouble(TxtBiestiloideRadio.Text), BiepicodilianoFemur = Convert.ToDouble(TxtBiepicodilianoFemur.Text) };
                this.person.BodyComposition = new BodyComposition { BonesMeasure = bm };

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
            if (CbxTipoCirc.Items.IsEmpty)
            {
            foreach (var value in Enum.GetValues(typeof(TypeCircumferenceEnum)))
            {
                var memInfo = typeof(TypeCircumferenceEnum).GetMember(value.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
                    false);
                var description = ((DescriptionAttribute)attributes[0]).Description;
                CbxTipoCirc.Items.Add(description);
            }
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
            var item = sender as TabControl;
            if (item.SelectedIndex == 1)
            {
                this.FillPerson();
            }
        }

        private void BtnAdicionarCirc_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TxtValorCirc.Text) && CbxTipoCirc.SelectedValue != null && !(String.IsNullOrEmpty(CbxTipoCirc.SelectedValue.ToString())))
            {
                try
                {
                    var circSelected = EnumHelper.GetValueFromDescription<TypeCircumferenceEnum>(CbxTipoCirc.SelectedValue.ToString());
                    var memInfo = typeof(TypeCircumferenceEnum).GetMember(CbxTipoCirc.SelectedValue.ToString());

                    SideEnum side = SideEnum.Right;
                    if (RdbEsquerdo.IsChecked.Value)
                    {
                        side = SideEnum.Left;
                    }

                    double valor = Convert.ToDouble(TxtValorCirc.Text);
                    bool hasModification = false;
                    foreach (Circumference circumference in LvCircunferencias.Items.Cast<Circumference>())
                    {
                        if (circumference.Type.Equals(circSelected) && circumference.Side.Equals(side))
                        {
                            circumference.Value = valor;
                            this.person.Circumferences.Where<Circumference>(sml => sml.Type.Equals(circSelected)).FirstOrDefault<Circumference>().Value = valor;
                            hasModification = true;
                        }
                    }

                    if (!hasModification)
                    {
                        Circumference circToAdd = new Circumference { Type = circSelected, Value = valor };
                        if (PnlLado.IsVisible)
                        {
                            circToAdd.Side = side;
                        }
                        if (this.person.Circumferences == null)
                        {
                            this.person.Circumferences = new List<Circumference>();
                        }

                        this.person.Circumferences.Add(circToAdd);
                        LvCircunferencias.Items.Add(circToAdd);
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

        private void CbxTipoCirc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CbxTipoCirc.SelectedIndex >= 0)
            {
                TypeCircumferenceEnum typeCircumSelected = EnumHelper.GetValueFromDescription<TypeCircumferenceEnum>(CbxTipoCirc.SelectedValue.ToString());
                var memInfo = typeof(TypeCircumferenceEnum).GetMember(typeCircumSelected.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(HasSideAttribute), false);
                HasSideAttribute hasSide = attributes[0] as HasSideAttribute;
                if (!hasSide.HasSide)
                {
                    PnlLado.Visibility = Visibility.Hidden;
                }
                else
                {
                    PnlLado.Visibility = Visibility.Visible;
                }

            }

        }

        private void BtnAdicionarRM_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TxtRepeticoes.Text) && !String.IsNullOrEmpty(TxtSubmaxima.Text) && CbxTipoRM.SelectedValue != null)
            {
                try
                {
                    var typeRMselected = TypeRMEnum.ExtensaoJoelho;

                    foreach(TypeRMEnum typeRM in Enum.GetValues(typeof(TypeRMEnum)))
                    {
                        if(typeRM.ToString().Equals(CbxTipoRM.SelectedValue.ToString())){
                            typeRMselected = typeRM;
                        }
                    }
                    
                    Int32 repeticoes = Convert.ToInt32(TxtRepeticoes.Text);
                    double submaxima = Convert.ToDouble(TxtSubmaxima.Text);
                    bool hasModification = false;
                    foreach (Load load in LvRMs.Items.Cast<Load>())
                    {
                        if (load.TypeRM.Equals(typeRMselected))
                        {
                            load.SubMaxLoad = submaxima;
                            load.RepeatAmount = repeticoes;
                            hasModification = true;
                            this.person.MaxLoads.Where<Load>(sml => sml.TypeRM.Equals(typeRMselected)).FirstOrDefault<Load>().SubMaxLoad = submaxima;
                            this.person.MaxLoads.Where<Load>(sml => sml.TypeRM.Equals(typeRMselected)).FirstOrDefault<Load>().RepeatAmount = repeticoes;
                        }
                    }

                    if (!hasModification)
                    {
                        Load load = new Load{ TypeRM = typeRMselected, RepeatAmount = repeticoes, SubMaxLoad = submaxima};
                        if (this.person.MaxLoads == null)
                        {
                            this.person.MaxLoads = new List<Load>();
                        }
                        this.person.MaxLoads.Add(load);
                        LvRMs.Items.Add(load);
                    }
                    else
                    {
                        LvRMs.Items.Refresh();
                    }
                    TxtRepeticoes.Text = String.Empty;
                    TxtSubmaxima.Text = String.Empty;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Informe valores válidos.");
                }

            }
        }

        private void BtnDiagnostico_Click(object sender, RoutedEventArgs e)
        {
            TabDiagnose.Visibility = Visibility.Visible;
            //this.createPerson();
            this.person = (new CalcLib()).Calculate(this.person);

            lvResult.Items.Add("IMC : " + this.person.BodyComposition.BodyMassIndex);
            lvResult.Items.Add("Densidade corporal : " + this.person.BodyComposition.BodyDensity);
            lvResult.Items.Add("Percentual de gordura : " + this.person.BodyComposition.BodyFat);
            lvResult.Items.Add("Peso gordo : " + this.person.BodyComposition.FatWeight);
            lvResult.Items.Add("Peso dos ossos : " + this.person.BodyComposition.BoneWeight);
            lvResult.Items.Add("Peso residual : " + this.person.BodyComposition.ResidualWeight);
            lvResult.Items.Add("Peso muscular : " + this.person.BodyComposition.MuscleWeight);
            lvResult.Items.Add("Peso da massa magra : " + this.person.BodySlimMass);
            lvResult.Items.Add("Relação Cintura/Quadril : " + this.person.WaistHipRelation);
            lvResult.Items.Add("Índice de conicidade : " + this.person.BodyComposition.ConicalIndex);

            lvResult.Items.Add("VO2 Repouso : " + this.person.RestVO2);
            lvResult.Items.Add("VO2 Máximo : " + this.person.MaxVO2);
            lvResult.Items.Add("MET : " + this.person.MET);

            if (this.person.MaxLoadsForOneRepeatTime != null && this.person.MaxLoadsForOneRepeatTime.Count > 0)
            {
                lvResult.Items.Add("Carga máxima para uma repetição:");
                foreach (var maxLoad in this.person.MaxLoadsForOneRepeatTime)
                {
                    lvResult.Items.Add("\t" + maxLoad.TypeRM.ToString() + " = " + maxLoad.MaxLoad);
                }
            }
            TabControl.SelectedItem = TabDiagnose;
        }

        private void createPerson()
        {

            this.person = new Person { Gender = GenderEnum.Male, Height = 1.8, Weight = 86.9, Age = 45, TimeTest2400 = 14.98, RestVO2 = 80 };

            this.person.SkinFolds = new List<SkinFold>();
            this.person.SkinFolds.Add(new SkinFold { TypeSkinFold = TypeSkinFoldEnum.Triceps, Value = 18.33 });
            this.person.SkinFolds.Add(new SkinFold { TypeSkinFold = TypeSkinFoldEnum.SupraIliac, Value = 28.33 });
            this.person.SkinFolds.Add(new SkinFold { TypeSkinFold = TypeSkinFoldEnum.Abdominal, Value = 24.67 });
            this.person.SkinFolds.Add(new SkinFold { TypeSkinFold = TypeSkinFoldEnum.Thigh, Value = 26.33 });

            this.person.Circumferences = new List<Circumference>();
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Thorax, Value = 99.5 });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Arm, Value = 31.5, Side = SideEnum.Right });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Arm, Value = 31.0, Side = SideEnum.Left });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Forearm, Value = 29.5, Side = SideEnum.Right });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Forearm, Value = 28.5, Side = SideEnum.Left });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Waist, Value = 88.5 });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Abdomen, Value = 97.0 });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Hip, Value = 105.0 });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Thigh, Value = 63.5, Side = SideEnum.Right });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Thigh, Value = 63.0, Side = SideEnum.Left });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Calf, Value = 39.5, Side = SideEnum.Right });
            this.person.Circumferences.Add(new Circumference { Type = TypeCircumferenceEnum.Calf, Value = 39.5, Side = SideEnum.Left });

            this.person.BodyComposition = new BodyComposition { BonesMeasure = new BoneMeasure { BiestiloideRadio = 0.061, BiepicodilianoFemur = 0.114 } };

            this.person.MaxLoadsForOneRepeatTime = new List<Load>();
            this.person.MaxLoadsForOneRepeatTime.Add(new Load { TypeRM = TypeRMEnum.Supino, SubMaxLoad = 100, RepeatAmount = 20 });
            this.person.MaxLoadsForOneRepeatTime.Add(new Load { TypeRM = TypeRMEnum.RoscaDireta, SubMaxLoad = 100, RepeatAmount = 20 });
            this.person.MaxLoadsForOneRepeatTime.Add(new Load { TypeRM = TypeRMEnum.PuxadaFrontal, SubMaxLoad = 100, RepeatAmount = 20 });
            this.person.MaxLoadsForOneRepeatTime.Add(new Load { TypeRM = TypeRMEnum.ExtensaoJoelho, SubMaxLoad = 100, RepeatAmount = 20 });

        }

    }
}
