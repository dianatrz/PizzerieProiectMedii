using PizzeriaModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

namespace ProgramAngajati
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        PizzeriaEntitiesModel ctx = new PizzeriaEntitiesModel();
        CollectionViewSource angajatiVSource;
        CollectionViewSource orarVSource;
        CollectionViewSource angajatiProgramsVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //using System.Data.Entity;
            angajatiVSource =
           ((System.Windows.Data.CollectionViewSource)(this.FindResource("angajatiViewSource")));
            angajatiVSource.Source = ctx.Angajatis.Local;
            ctx.Angajatis.Load();
            orarVSource =
           ((System.Windows.Data.CollectionViewSource)(this.FindResource("orarViewSource")));
            orarVSource.Source = ctx.Orars.Local;
            //ctx.Orars.Load();
            angajatiProgramsVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("angajatiProgramsViewSource")));

            //angajatiProgramsVSource.Source = ctx.Programs.Local;
            ctx.Programs.Load();
            ctx.Orars.Load();
            cmbAngajati.ItemsSource = ctx.Angajatis.Local;
            cmbAngajati.SelectedValuePath = "AngajatiId";
            cmbOrar.ItemsSource = ctx.Orars.Local;
            cmbOrar.SelectedValuePath = "OrarId";
            BindDataGrid();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            SetValidationBinding();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            SetValidationBinding();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }
        private void btnNextAng_Click(object sender, RoutedEventArgs e)
        {
            angajatiVSource.View.MoveCurrentToNext();
        }
        private void btnPrevAng_Click(object sender, RoutedEventArgs e)
        {
            angajatiVSource.View.MoveCurrentToPrevious();
        }
        private void btnNextOrar_Click(object sender, RoutedEventArgs e)
        {
            orarVSource.View.MoveCurrentToNext();
        }
        private void btnPrevOrar_Click(object sender, RoutedEventArgs e)
        {
            orarVSource.View.MoveCurrentToPrevious();
        }

        private void SaveAngajati()
        {
            Angajati angajati = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Angajati entity
                    angajati = new Angajati()
                    {
                        Prenume = prenumeTextBox.Text.Trim(),
                        Nume = numeTextBox.Text.Trim(),
                        Post = postTextBox.Text.Trim(),
                        DataAngajare = dataAngajareDatePicker.SelectedDate,
                        DataNastere = dataNastereDatePicker.SelectedDate
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Angajatis.Add(angajati);
                    angajatiVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    angajati = (Angajati)angajatiDataGrid.SelectedItem;
                    angajati.Prenume = prenumeTextBox.Text.Trim();
                    angajati.Nume = numeTextBox.Text.Trim();
                    angajati.Post = postTextBox.Text.Trim();
                    angajati.DataNastere = dataNastereDatePicker.SelectedDate;
                    angajati.DataAngajare = dataAngajareDatePicker.SelectedDate;
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    angajati = (Angajati)angajatiDataGrid.SelectedItem;
                    ctx.Angajatis.Remove(angajati);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                angajatiVSource.View.Refresh();
            }

        }
        private void SaveOrar()
        {
            Orar orar = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Orar entity
                    orar = new Orar()
                    {
                        Tura = turaTextBox.Text.Trim(),
                        ZiuaSaptamanii = ziuaSaptamaniiTextBox.Text.Trim(),
                        DataIntreaga = dataIntreagaDatePicker.SelectedDate
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Orars.Add(orar);
                    orarVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    orar = (Orar)orarDataGrid.SelectedItem;
                    orar.Tura = turaTextBox.Text.Trim();
                    orar.ZiuaSaptamanii = ziuaSaptamaniiTextBox.Text.Trim();
                    orar.DataIntreaga = dataIntreagaDatePicker.SelectedDate;
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    orar = (Orar)orarDataGrid.SelectedItem;
                    ctx.Orars.Remove(orar);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                orarVSource.View.Refresh();
            }

        }
        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlPizzeria.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Angajati":
                    SaveAngajati();
                    break;
                case "Orar":
                    SaveOrar();
                    break;
                case "Program":
                    SaveProgram();
                    break;
            }
            ReInitialize();
        }
        private void BindDataGrid()
        {
            var queryProgram = from prog in ctx.Programs
                             join ang in ctx.Angajatis on prog.AngajatiId equals
                             ang.AngajatiId join orr in ctx.Orars on prog.OrarId
                             equals orr.OrarId select new
                             {
                                 prog.ProgramId,
                                 prog.OrarId,
                                 prog.AngajatiId,
                                 ang.Nume,
                                 ang.Prenume,
                                 orr.DataIntreaga,
                                 orr.Tura
                             };
            angajatiProgramsVSource.Source = queryProgram.ToList();
        }
        private void SaveProgram()
        {
            Program program = null;
            if (action == ActionState.New)
            {
                try
                {
                    Angajati angajati = (Angajati)cmbAngajati.SelectedItem;
                    Orar orar = (Orar)cmbOrar.SelectedItem;
                    //instantiem Program entity
                    program = new Program()
                    {

                        AngajatiId = angajati.AngajatiId,
                        OrarId = orar.OrarId
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Programs.Add(program);
                    //salvam modificarile
                    ctx.SaveChanges();
                    BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                dynamic selectedProgram = programsDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedProgram.ProgramId;
                    var editedProgram = ctx.Programs.FirstOrDefault(s => s.ProgramId == curr_id);
                    if (editedProgram != null)
                    {
                        editedProgram.AngajatiId = Int32.Parse(cmbAngajati.SelectedValue.ToString());
                        editedProgram.OrarId = Convert.ToInt32(cmbOrar.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                angajatiProgramsVSource.View.MoveCurrentTo(selectedProgram);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedProgram = programsDataGrid.SelectedItem;
                    int curr_id = selectedProgram.ProgramId;
                    var deletedProgram = ctx.Programs.FirstOrDefault(s => s.ProgramId == curr_id);
                    if (deletedProgram != null)
                    {
                        ctx.Programs.Remove(deletedProgram);
                        ctx.SaveChanges();
                        MessageBox.Show("Program Deleted Successfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void SetValidationBinding()
        {
            Binding prenumeValidationBinding = new Binding();
            prenumeValidationBinding.Source = angajatiVSource;
            prenumeValidationBinding.Path = new PropertyPath("Prenume");
            prenumeValidationBinding.NotifyOnValidationError = true;
            prenumeValidationBinding.Mode = BindingMode.TwoWay;
            prenumeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string required
            prenumeValidationBinding.ValidationRules.Add(new StringNotEmpty());
            prenumeValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            prenumeTextBox.SetBinding(TextBox.TextProperty, prenumeValidationBinding);
            Binding numeValidationBinding = new Binding();
            numeValidationBinding.Source = angajatiVSource;
            numeValidationBinding.Path = new PropertyPath("Nume");
            numeValidationBinding.NotifyOnValidationError = true;
            numeValidationBinding.Mode = BindingMode.TwoWay;
            numeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string min length validator
            numeValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            numeValidationBinding.ValidationRules.Add(new StringNotEmpty());
            numeTextBox.SetBinding(TextBox.TextProperty, numeValidationBinding); //setare binding nou
        }
        private void angajatiDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
