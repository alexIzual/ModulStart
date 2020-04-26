using Dapper;
using Npgsql;
using System;

namespace WorkWithDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateTable();

            var empService = new EmployeesService();

            CreateEmployees(empService);

            UpdateEmployee(empService);

            DeleteEmpoyee(empService);

            Console.WriteLine("Press any key to quit...");
            Console.ReadLine();

            DropTable();
        }

        private static void CreateEmployees(EmployeesService empService)
        {
            empService.CreateEmployee(new EmployeeModel
            {
                Id = 1,
                FirstName = "Вася",
                LastName = "Давайпотом",
                Position = "Ведущий инженер-прокрастинатор",
            });

            empService.CreateEmployee(new EmployeeModel
            {
                Id = 2,
                FirstName = "Марио",
                LastName = "Марио",
                Position = "Водопроводчик",
            });
        }

        private static void UpdateEmployee(EmployeesService empService)
        {
            Console.WriteLine("/------Update employee------/");
            Console.WriteLine();

            var vasya = empService.GetEmployee(1);

            Console.WriteLine(vasya.ToString());
            Console.WriteLine();

            vasya.Position = "Главный инженер-прокрастинатор";

            empService.UpdateEmployee(vasya);

            vasya = empService.GetEmployee(1);

            Console.WriteLine(vasya.ToString());
            Console.WriteLine();
        }

        private static void DeleteEmpoyee(EmployeesService empService)
        {
            Console.WriteLine("/------Delete employee------/");
            Console.WriteLine();

            var mario = empService.GetEmployee(2);

            Console.WriteLine(mario.ToString());
            Console.WriteLine();

            empService.DeleteEmployee(2);

            mario = empService.GetEmployee(2);

            Console.WriteLine("Row not found:" + (mario == null));
            Console.WriteLine();
        }

        private static void CreateTable()
        {
            using (var conn = new NpgsqlConnection($"server=localhost;database=postgres;userid=postgres;password=1;Pooling=false"))
            {
                conn.Open();

                conn.Execute(@"CREATE TABLE employees (
                                id int PRIMARY KEY, 
                                firstname text, 
                                lastname text, 
                                position text
                            );");

                Console.WriteLine("CREATE TABLE employees");
                Console.WriteLine();
            }
        }

        private static void DropTable()
        {
            using (var conn = new NpgsqlConnection($"server=localhost;database=postgres;userid=postgres;password=1;Pooling=false"))
            {
                conn.Open();

                conn.Execute(@"DROP TABLE employees;");

                Console.WriteLine("DROP TABLE employees");
                Console.WriteLine();
            }
        }
    }
}
