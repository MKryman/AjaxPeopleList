using System.Data.SqlClient;

namespace Homework_04_24.Data
{
    public class PeopleRepository
    {
        private string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People";
            connection.Open();
            List<Person> people = new();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }

            return people;
        }

        public void AddPerson(Person p)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO People(FirstName, LastName, Age) " +
                "VALUES(@fName, @lName, @age)";
            command.Parameters.AddWithValue("@fName", p.FirstName);
            command.Parameters.AddWithValue("@lName", p.LastName);
            command.Parameters.AddWithValue("@age", p.Age);
            connection.Open();

            command.ExecuteNonQuery();
        }

        public Person GetPersonById(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();

            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return new Person
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"]
            };
        }

        public void UpdatePerson(Person p)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE People SET FirstName = @fName, LastName = @lName, Age = @age WHERE Id = @id";
            
            command.Parameters.AddWithValue("@fName", p.FirstName);
            command.Parameters.AddWithValue("@lName", p.LastName);
            command.Parameters.AddWithValue("@age", p.Age);
            command.Parameters.AddWithValue("@id", p.Id);
            connection.Open();

            command.ExecuteNonQuery();
        }

        public void DeletePerson(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM People WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();

            command.ExecuteNonQuery();
        }
       
    }
}