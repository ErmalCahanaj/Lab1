using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Lab1.Models;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HindiController : ControllerBase
    { 

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public HindiController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select HindiId, HindiName,Imdb,
                            convert(varchar(10),DateOfRelease,120) as DateOfRelease,PhotoFileName
                            from
                            dbo.Hindi
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Hindi hin)
        {
            string query = @"
                           insert into dbo.Hindi
                           (HindiName,Imdb,DateOfRelease,PhotoFileName)
                    values (@HindiName,@Imdb,@DateOfRelease,@PhotoFileName)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@HindiName", hin.HindiName);
                    myCommand.Parameters.AddWithValue("@Imdb", hin.Imdb);
                    myCommand.Parameters.AddWithValue("@DateOfRelease", hin.DateOfRelease);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", hin.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Hindi hin)
        {
            string query = @"
                           update dbo.Hindi
                           set HindiName= @HindiName,
                            Imdb=@Imdb,
                            DateOfRelease=@DateOfRelease,
                            PhotoFileName=@PhotoFileName
                            where HindiId=@HindiId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@HindiId", hin.HindiId);
                    myCommand.Parameters.AddWithValue("@HindiName", hin.HindiName);
                    myCommand.Parameters.AddWithValue("@Imdb", hin.Imdb);
                    myCommand.Parameters.AddWithValue("@DateOfRelease", hin.DateOfRelease);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", hin.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Hindi
                            where HindiId=@HindiId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@HindiId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

    }
}


