using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace WebApplicationSqlite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            using (var connection = new SqliteConnection("Data Source=database.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select name from person";
                
                //command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        _logger.LogInformation($"Hello, {name}!");
                        //Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
        }
    }
}