using Npgsql;
using Dapper;

namespace WorkWithDatabase
{
    public class EmployeesService
    {
        public EmployeeModel GetEmployee(int id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                var result = conn.QuerySingleOrDefault<EmployeeModel>("SELECT id, firstname, lastname, position FROM employees WHERE id = @id;",
                    new
                    {
                        id,
                    });

                return result;
            }
        }

        public void CreateEmployee(EmployeeModel employee)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                conn.Execute("INSERT INTO employees (id, firstname, lastname, position) VALUES(@id, @firstname, @lastname, @position);",
                    new
                    {
                        id = employee.Id,
                        firstname = employee.FirstName,
                        lastname = employee.LastName,
                        position = employee.Position,
                    });
            }
        }

        public void UpdateEmployee(EmployeeModel employee)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                conn.Execute("UPDATE employees SET firstname = @firstname, lastname = @lastname, position = @position WHERE id = @id;",
                    new
                    {
                        id = employee.Id,
                        firstname = employee.FirstName,
                        lastname = employee.LastName,
                        position = employee.Position,
                    });
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                conn.Execute("DELETE FROM employees WHERE id = @id;",
                    new
                    {
                        id = employeeId,
                    });
            }
        }

        private NpgsqlConnection CreateConnection()
        {
            var connection = new NpgsqlConnection($"server=localhost;database=postgres;userid=postgres;password=1;Pooling=false");

            return connection;
        }
    }
}
