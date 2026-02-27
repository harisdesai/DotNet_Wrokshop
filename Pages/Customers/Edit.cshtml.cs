using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRMProject.Pages.Customers
{
    public class Edit : PageModel
    {
        [BindProperty]
        public int CustId { get; set; }

        [BindProperty]
        public string CustName { get; set; }

        [BindProperty]
        public string CustEmail { get; set; }

        [BindProperty]
        public string CustPhone { get; set; }

        public string ErrorMessage = "";

        public void OnGet(int id)
        {
            try
            {
                using (var connection =
                    new MySqlConnection(
                    "Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manager;"))
                {
                    connection.Open();

                    var command = new MySqlCommand(
                        "SELECT * FROM customers WHERE id=@CustId",
                        connection);

                    command.Parameters.AddWithValue("@CustId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CustId = reader.GetInt32(0);
                            CustName = reader.GetString(1);
                            CustEmail = reader.GetString(2);
                            CustPhone = reader.GetString(3);
                        }
                        else
                        {
                            ErrorMessage = "Customer not found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            try
            {
                using (var connection =
                    new MySqlConnection(
                    "Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manager;"))
                {
                    connection.Open();

                    var command = new MySqlCommand(
                        "UPDATE customers SET name=@name, email=@email, phone=@phone WHERE id=@id",
                        connection);

                    command.Parameters.AddWithValue("@id", CustId);
                    command.Parameters.AddWithValue("@name", CustName);
                    command.Parameters.AddWithValue("@email", CustEmail);
                    command.Parameters.AddWithValue("@phone", CustPhone);

                    command.ExecuteNonQuery();
                }

                Response.Redirect("/Customers/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}