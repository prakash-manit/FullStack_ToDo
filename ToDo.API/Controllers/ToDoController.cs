using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ToDo.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ToDoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getTasks")]
        public async Task<JsonResult> getTasks()
        {
            string query = "select * from dbo.ToDo";
            SqlDataReader reader;
            DataTable dt;

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("CRUD")))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                 reader = await cmd.ExecuteReaderAsync();
                dt = new DataTable();
                dt.Load(reader);
            }

            return new JsonResult(dt);
        }

        [HttpPost("addTask")]
        public async Task<JsonResult> addTask([FromForm] string task)
        {
            string query = "insert into dbo.ToDo values(@task)";
            SqlDataReader reader;

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("CRUD")))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@task", task);
                reader = await cmd.ExecuteReaderAsync();               
            }

            return new JsonResult("Added successfully!!");
        }

        [HttpPost("deleteTask")]
        public async Task<JsonResult> deleteTask([FromForm] string id)
        {
            string query = "delete from dbo.ToDo where id = @id";
            SqlDataReader reader;

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("CRUD")))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                reader = await cmd.ExecuteReaderAsync();
            }

            return new JsonResult("Deleted successfully!!");
        }
    }
}
