using System.Windows;
/// ===============================
///  AUTHOR 
///  Mykyta Shvets
/// ===============================
namespace Multiform
{
    public partial class EditEmployeeWindow : Window
    {
        VM vm;
        public EditEmployeeWindow(VM vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            vm.UpdateInfo();
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
