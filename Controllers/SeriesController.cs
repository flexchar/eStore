    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace eStore.Controllers
{
    [Authorize]
    public class SeriesController : Controller
    {
        private readonly SqlConnection sql;

        private readonly string table = "Series";

        public SeriesController(IConfiguration configuration)
        {
            // Prepare SQL Connection for using Parameterized queries
            sql = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Drone Series";

            List<Series> models = new List<Series>();

            string query = $"select * from {table}";

            SqlCommand command = new SqlCommand(query, sql);

            try
            {
                sql.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    models.Add(new Series
                    {
                        name = reader["name"].ToString(),
                        description = reader["description"].ToString()
                    });
                }

                sql.Close();
            }
            catch (Exception ex)
            {
                sql.Close();
                throw ex;
            }

            return View(models);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Create new Series";
            return View();
        }

        [HttpPost]
        public ActionResult Create(Series model)
        {
            string query = $"INSERT INTO {table} (name, description) VALUES (@Name, @Desc)";

            SqlCommand command = new SqlCommand(query, sql);

            command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar);
            command.Parameters["@Name"].Value = model.name;

            command.Parameters.Add("@Desc", System.Data.SqlDbType.VarChar);
            command.Parameters["@Desc"].Value = model.description;

            try
            {
                sql.Open();
                command.ExecuteNonQuery();
                sql.Close();

            }
            catch (Exception ex)
            {
                sql.Close();
                throw ex;
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Update()
        {
            ViewBag.Title = "Edit series";
            return View();
        }

        [HttpPost]
        public ActionResult Update(Series model)
        {
            ViewBag.Title = "Edit series";

            string query = $"UPDATE {table} SET description = @Desc WHERE name = @Name";

            SqlCommand command = new SqlCommand(query, sql);

            command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar);
            command.Parameters["@Name"].Value = model.name;

            command.Parameters.Add("@Desc", System.Data.SqlDbType.VarChar);
            command.Parameters["@Desc"].Value = model.description;

            try
            {
                sql.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    sql.Close();
                }
                else
                {
                    sql.Close();
                    ViewBag.message = "Such entry not found";
                    return View();
                }

            }
            catch (Exception ex)
            {
                sql.Close();
                throw ex;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete()
        {
            ViewBag.Title = "Delete series";
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Series model)
        {

            ViewBag.Title = "Delete series";

            string query = $"DELETE FROM {table} WHERE name = @Name";

            SqlCommand command = new SqlCommand(query, sql);

            command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar);
            command.Parameters["@Name"].Value = model.name;

            try
            {
                sql.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    sql.Close();
                    ViewBag.message = "Such entry not found";
                    return View();
                }
                else
                {
                    sql.Close();
                }

            }
            catch (Exception ex)
            {
                sql.Close();
                throw ex;
            }

            return RedirectToAction("Index");
        }
    }
}