using System.Windows;
/// ===============================
///  AUTHOR 
///  Mykyta Shvets
/// ===============================
namespace Multiform
{
    public partial class AddEmployeeWindow : Window
    {
        VM vm;
        public AddEmployeeWindow(VM vm)
        {
            vm.CurrentEmployee = new Employee();
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            vm.Employees.Add(vm.CurrentEmployee);
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
