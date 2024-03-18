using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using TestWebApi.Models;


namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IConfiguration _configuration;  
        
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

            /* public string Get()
             {
                    string connectionString = _configuration.GetConnectionString("MyDB");
                    using(SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TestWebApi", connection);
                        DataTable dataTable = new DataTable();
                        da.Fill(dataTable);
                        if(dataTable.Rows.Count > 0)
                        {
                            return JsonConvert.SerializeObject(dataTable);
                        }
                        else
                        {
                            return "No data found";
                        }
                    }
              }*/

        public IActionResult Get()
        {
            List<TestWebApiTable> data = new List<TestWebApiTable>();
            string connectionString = _configuration.GetConnectionString("MyDB");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT Id, Name, Email, RollNo FROM TestWebApi";
                using (SqlCommand command = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TestWebApiTable model = new TestWebApiTable();
                            model.Id = reader.GetInt32(0);
                            model.Name = reader.GetString(1);
                            model.Email = reader.GetString(2);
                            model.RollNo = reader.GetInt32(3);
                            data.Add(model);

                        }
                    }
                }
            }
            if(data.Count > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No data Found");
            }
        }
    }
}

