using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UseDapper.Models;

namespace UseDapper.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {

        private readonly string _connectionString;

        public TeacherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<Teacher> Create(Teacher teacher)
        {
            var sqlQuery = "INSERT INTO Teachers (Name, Surname, Description) VALUES (@Name, @Surname, @Description)";


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    teacher.Name,
                    teacher.Surname,
                    teacher.Description
                });

                return teacher;
            }
        }

        public async Task Delete(int id)
        {
            var sqlQuery = "DELETE FROM Teachers WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new { Id = id });               
            }
        }

        public async Task<IEnumerable<Teacher>> Get()
        {
            var sqlQuery = "SELECT * FROM Teachers";
            

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Teacher>(sqlQuery);
            }
            
        }

        public async Task<Teacher> Get(int id)
        {
            var sqlQuery = "SELECT * FROM Teachers WHERE Id = @TeacherId";


            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Teacher>(sqlQuery, new { TeacherId = id});
            }
        }

        public async Task Update(Teacher teacher)
        {
            var sqlQuery = "UPDATE Teachers SET Name = @Name, Surname = @Surname, Description = @Description WHERE Id = @Id";


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    teacher.Id,
                    teacher.Name,
                    teacher.Surname,
                    teacher.Description
                });

               
            }
        }

    }
}
