using Introduction.Common;
using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System.Text;

namespace Introduction.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string connectionString = "Host=localhost:5432; Username=postgres; Password=postgres; Database=postgres";

        public async Task<bool> PostAsync(User user)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO \"User\" VALUES(@Id, @FirstName, @LastName, @DateOfBirth, @Sex);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@Sex", user.Sex);

                connection.Open();

                var numberOfCommits = await command.ExecuteNonQueryAsync();

                connection.Close();
                if (numberOfCommits == 0)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = "DELETE FROM \"User\" WHERE \"Id\" = @Id;";
                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                var numberOfCommits = await command.ExecuteNonQueryAsync();

                connection.Close();
                if (numberOfCommits == 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PutAsync(Guid id, User user)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                var commandText = $"UPDATE \"User\" SET ";
                stringBuilder.Append(commandText);
                using var command = new NpgsqlCommand(commandText, connection);
                if (user.FirstName != null)
                {
                    stringBuilder.Append("\"FirstName\"=@firstName, ");
                    command.Parameters.AddWithValue("@firstName", user.FirstName);
                }
                if (user.LastName != null)
                {
                    stringBuilder.Append("\"LastName\"=@lastName, ");
                    command.Parameters.AddWithValue("@lastName", user.LastName);
                }
                if (user.DateOfBirth != null)
                {
                    stringBuilder.Append("\"DateOfBirth\"=@dateOfBirth, ");
                    command.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
                }
                if (user.Sex != null)
                {
                    stringBuilder.Append("\"Sex\"=@sex, ");
                    command.Parameters.AddWithValue("@sex", user.Sex);
                }

                if (stringBuilder.ToString().EndsWith(", "))
                {
                    stringBuilder.Length -= 2;
                }

                stringBuilder.Append(" WHERE\"Id\"=@id;");
                command.Parameters.AddWithValue("@id", id);

                command.CommandText = stringBuilder.ToString();
                command.Connection = connection;

                connection.Open();

                var numberOfCommits = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (numberOfCommits == 0)
                    return false;
                return true;
            }
            catch 
            {
                return false;
            }
        } 

        public async Task<User> GetAsync(Guid id)
        {
            try
            {
                var user = new User();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"User\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    await reader.ReadAsync();

                    user.Id = Guid.Parse(reader[0].ToString());
                    user.FirstName = reader[1].ToString();
                    user.LastName = reader[2].ToString();
                    var temp = Convert.ToDateTime(reader[3]);
                    user.DateOfBirth = DateOnly.FromDateTime(temp);
                    user.Sex = Convert.ToChar(reader[4]);
                }

                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<List<FacebookPost>> GetUserAndPostsAsync(Guid id)
        {
            try
            {
                List<FacebookPost> posts = new List<FacebookPost>();
                
                var user = new User();
                user.Posts = new List<FacebookPost>();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"User\" INNER JOIN \"FacebookPost\" ON \"User\".\"Id\" = \"FacebookPost\".\"UserId\" WHERE \"User\".\"Id\" = @id";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        await reader.ReadAsync();

                        user.Id = Guid.Parse(reader[0].ToString());
                        user.FirstName = reader[1].ToString();
                        user.LastName = reader[2].ToString();
                        var temp = Convert.ToDateTime(reader[3]);
                        user.DateOfBirth = DateOnly.FromDateTime(temp);
                        user.Sex = Convert.ToChar(reader[4]);

                        FacebookPost facebookPost = new FacebookPost();
                        facebookPost.Id = Guid.Parse(reader[5].ToString());
                        facebookPost.Caption = reader[6].ToString();
                        facebookPost.UserId = Guid.Parse(reader[7].ToString());
                        posts.Add(facebookPost);
                    }
                    user.Posts = posts;
                }

                if (user == null || posts == null)
                {
                    return null;
                }
                return posts;
            }
            catch 
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                List<User> users = new List<User>();
                StringBuilder stringBuilder = new StringBuilder();
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                var commandText = $"SELECT * FROM \"User\" WHERE 1=1 ";
                stringBuilder.Append(commandText);
                using var command = new NpgsqlCommand(commandText, connection);
                command.Connection = connection;

                connection.Open();

                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new()
                        {
                            Id = Guid.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            DateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(reader[3])),
                            Sex = Convert.ToChar(reader[4]),
                        };
                        users.Add(user);
                    }
                }
                return users;
            }
            catch (NpgsqlException)
            {
                return null;
            }
        }
    }
}
