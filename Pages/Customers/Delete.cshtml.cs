using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace CRMProject.Pages.Customers
{
    public class Delete : PageModel
    {
        [BindProperty]
        public int CustId { get; set; }

        public void OnGet(int id)
        {
            CustId = id;
        }

        public void OnPost()
        {
            using (var connection =
                new MySqlConnection(
                "Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manager;"))
            {
                connection.Open();

                var command = new MySqlCommand(
                    "DELETE FROM customers WHERE id=@id",
                    connection);

                command.Parameters.AddWithValue("@id", CustId);

                command.ExecuteNonQuery();
            }

            Response.Redirect("/Customers/Index");
        }
    }
}