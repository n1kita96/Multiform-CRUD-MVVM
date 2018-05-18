using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
/// ===============================
///  AUTHOR 
///  Mykyta Shvets
/// ===============================
namespace Multiform
{
    public class VM : INotifyPropertyChanged
    {

        DB db;

        ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        public VM()
        {
            db = DB.GetInstance();
            Employees = new ObservableCollection<Employee>(db.Get());
        }

        Employee currentEmpoyee;
        public Employee CurrentEmployee
        {
            get { return currentEmpoyee; }
            set { currentEmpoyee = value; OnPropertyChanged(); }
        }

        public void DeleteEmployee(Employee employee)
        {
            employee.IsDeleted = true;
        }

        public void SaveAll()
        {
            db.Save(Employees.ToList());
        }

        public void Refresh()
        {
            Employees.Clear();
            foreach (Employee employee in db.Get())
            {
                Employees.Add(employee);
            }
        }

        public void UpdateInfo()
        {
            foreach (Employee employee in Employees)
            {
                if (CurrentEmployee.Id == employee.Id)
                {
                    employee.IsChanged = true;
                    employee.Name = currentEmpoyee.Name;
                    employee.Position = currentEmpoyee.Position;
                    employee.HourlyPayRate = currentEmpoyee.HourlyPayRate;
                }
            }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
