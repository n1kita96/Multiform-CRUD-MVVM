using System;
/// ===============================
///  AUTHOR 
///  Mykyta Shvets
/// ===============================
namespace Multiform
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int HourlyPayRate { get; set; }
        public Boolean IsChanged { get; set; }
        public Boolean IsDeleted { get; set; }
        
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Position: {Position}, Hourly pay rate {HourlyPayRate}, Changed: {IsChanged}, Deleted: {IsDeleted}";
        }
    }
}
