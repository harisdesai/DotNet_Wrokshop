using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
namespace CRMProject.Pages.Customers
{
    public class Create : PageModel
    {
        public string ErrorMessage { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "Enter the name")]
        public string name { get; set; } = "";

        [BindProperty,
        Required(ErrorMessage = "Enter the email"),
        EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; } = "";

        [BindProperty,
        Required(ErrorMessage = "Enter the phone number")]
        public string phone { get; set; } = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Form is Invalid");
                return;
            }

            try
            {
                using (var connection = new MySqlConnection(
                    "Server=localhost;Port=3306;Database=dkte;Uid=root;Pwd=manager;"
                ))
                {
                    connection.Open();

                    var command = new MySqlCommand(
                        "INSERT INTO Customers (name, email, phone) VALUES (@CustName, @CustEmail, @CustPhone)",
                        connection
                    );

                    command.Parameters.AddWithValue("@CustName", name);
                    command.Parameters.AddWithValue("@CustEmail", email);
                    command.Parameters.AddWithValue("@CustPhone", phone);

                    int i = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding customer." + $"Error: {ex.Message}");
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Customers/Index");
        }
    }
}