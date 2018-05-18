using System.Windows;
/// ===============================
///  AUTHOR 
///  Mykyta Shvets
/// ===============================
namespace Multiform
{
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            object item = GridEmployees.CurrentCell.Item;
            vm.CurrentEmployee = (Employee)item;
            EditEmployeeWindow edit = new EditEmployeeWindow(vm);
            edit.ShowDialog();
            GridEmployees.Items.Refresh();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployee = new AddEmployeeWindow(vm);
            addEmployee.ShowDialog();
            GridEmployees.Items.Refresh();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            object item = GridEmployees.CurrentCell.Item;
            vm.DeleteEmployee((Employee)item);
            GridEmployees.Items.Refresh();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveAll();
            vm.Refresh();
            MessageBox.Show("Saved!");
            GridEmployees.Items.Refresh();
        }

        private void BtnRollback_Click(object sender, RoutedEventArgs e)
        {
            vm.Refresh();
            MessageBox.Show("Rollback!");
            GridEmployees.Items.Refresh();
        }
    }
}
