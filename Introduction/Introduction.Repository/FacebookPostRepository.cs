using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System.Collections.Generic;
using System.Text;

namespace Introduction.Repository
{
    public class FacebookPostRepository : IFacebookPostRepository
    {
        private const string connectionString = "Host=localhost:5432; Username=postgres; Password=postgres; Database=postgres";

        public async Task<bool> PostAsync(FacebookPost facebookPost)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO \"FacebookPost\" VALUES(@id, @caption, @userId, @postedAt);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@caption", facebookPost.Caption);
                command.Parameters.AddWithValue("@userId", facebookPost.UserId);
                command.Parameters.AddWithValue("@postedAt", facebookPost.PostedAt);

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
                string commandText = "DELETE FROM \"FacebookPost\" WHERE \"Id\" = @Id;";
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

        public async Task<bool> PutAsync(Guid id, FacebookPost facebookPost)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                var commandText = $"UPDATE \"FacebookPost\" SET ";
                stringBuilder.Append(commandText);
                using var command = new NpgsqlCommand(commandText, connection);

                if (facebookPost.Caption != null)
                {
                    stringBuilder.Append("\"Caption\"=@caption, ");
                    command.Parameters.AddWithValue("@caption", facebookPost.Caption);
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

        public async Task<FacebookPost> GetByIdAsync(Guid id)
        {
            try
            {
                FacebookPost facebookPost = new FacebookPost();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"FacebookPost\" WHERE \"FacebookPost\".\"Id\" = @id";
                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    await reader.ReadAsync();

                    facebookPost.Id = Guid.Parse(reader[0].ToString());
                    facebookPost.Caption = reader[1].ToString();
                    facebookPost.UserId = Guid.Parse(reader[2].ToString());
                }

                if (facebookPost == null)
                {
                    return null;
                }
                return facebookPost;
            }
            catch
            {
                return null;
            }
        }
    }
}
