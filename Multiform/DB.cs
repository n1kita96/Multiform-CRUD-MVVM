using System.Collections.Generic;
using System.Data.SqlClient;
/// ===============================
///  AUTHOR 
///  Mykyta Shvets
/// ===============================
namespace Multiform
{
    class DB
    {
        const string CONNECTION_STRING = @"Server=.\SQLEXPRESS; Database=Personnel; Trusted_Connection=True; ";
        SqlConnection conn;
        static DB db;

        private DB()
        {
            conn = new SqlConnection(CONNECTION_STRING);
            conn.Open();
        }

        public static DB GetInstance()
        {
            if (db == null)
                db = new DB();
            return db;
        }

        public void Save(List<Employee> employees)
        {
            List<Employee> employeesToDelete = new List<Employee>();

            foreach (Employee employee in employees)
            {
                if (employee.IsDeleted)
                {
                    Delete(employee);
                    employeesToDelete.Add(employee);
                }
                else if (employee.IsChanged)
                {
                    Update(employee);
                }
                else if (employee.Id == 0)
                {
                    Add(employee);
                }
            }

            foreach (Employee employee in employeesToDelete)
            {
                employees.Remove(employee);
            }
        }

        public void Add(Employee employee)
        {
            string cmdString = "INSERT INTO employee " +
                                    "(name, position, hourlyPayRate)" +
                                    "VALUES" +
                                    "(@NAME, @POSITION, @HOURLY_PAY_RATE)";

            SqlCommand cmd = new SqlCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@NAME", employee.Name);
            cmd.Parameters.AddWithValue("@POSITION", employee.Position);
            cmd.Parameters.AddWithValue("@HOURLY_PAY_RATE", employee.HourlyPayRate);

            cmd.ExecuteNonQuery();
        }

        public void Delete(Employee employee)
        {
            string cmdString = "DELETE FROM employee " +
                               "WHERE id = @ID";

            SqlCommand cmd = new SqlCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@ID", employee.Id);

            cmd.ExecuteNonQuery();
        }

        public void Update(Employee employee)
        {
            string cmdString = "UPDATE employee SET name = @NAME, " +
                                                 "position = @POSITION, " +
                                                 "hourlyPayRate = @HOURLY_PAY_RATE " +
                               "WHERE id = @ID";

            SqlCommand cmd = new SqlCommand(cmdString, conn);

            cmd.Parameters.AddWithValue("@ID", employee.Id);
            cmd.Parameters.AddWithValue("@NAME", employee.Name);
            cmd.Parameters.AddWithValue("@POSITION", employee.Position);
            cmd.Parameters.AddWithValue("@HOURLY_PAY_RATE", employee.HourlyPayRate);

            cmd.ExecuteNonQuery();
        }

        public List<Employee> Get()
        {
            List<Employee> employees = new List<Employee>();

            string cmdString = "SELECT id, name, position, hourlyPayRate FROM employee";
            SqlCommand cmd = new SqlCommand(cmdString, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
                employees.Add(
               new Employee()
               {
                   Id = rd.GetInt32(rd.GetOrdinal("Id")),
                   Name = rd.GetString(rd.GetOrdinal("Name")),
                   Position = rd.GetString(rd.GetOrdinal("Position")),
                   HourlyPayRate = rd.GetInt32(rd.GetOrdinal("HourlyPayRate"))
               });
            rd.Close();

            return employees;
        }
    }

}
