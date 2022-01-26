namespace WorkWithDatabase
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public override string ToString()
        {
            return $"Id:{Id} FirstName:{FirstName} LastName:{LastName} Position:{Position}";
        }

    }
}