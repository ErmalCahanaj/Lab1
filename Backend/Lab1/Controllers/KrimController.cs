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
    public class KrimController : ControllerBase
    {

  
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public KrimController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select KrimId, KrimName,Imdb,
                            convert(varchar(10),DateOfRelease,120) as DateOfRelease,PhotoFileName
                            from
                            dbo.Krim
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
        public JsonResult Post(Krim krm)
        {
            string query = @"
                           insert into dbo.Krim
                           (KrimName,Imdb,DateOfRelease,PhotoFileName)
                    values (@KrimName,@Imdb,@DateOfRelease,@PhotoFileName)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@KrimName", krm.KrimName);
                    myCommand.Parameters.AddWithValue("@Imdb", krm.Imdb);
                    myCommand.Parameters.AddWithValue("@DateOfRelease", krm.DateOfRelease);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", krm.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Krim krm)
        {
            string query = @"
                           update dbo.Krim
                           set KrimName= @KrimName,
                            Imdb=@Imdb,
                            DateOfRelease=@DateOfRelease,
                            PhotoFileName=@PhotoFileName
                            where KrimId=@KrimId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@KrimId", krm.KrimId);
                    myCommand.Parameters.AddWithValue("@KrimName", krm.KrimName);
                    myCommand.Parameters.AddWithValue("@Imdb", krm.Imdb);
                    myCommand.Parameters.AddWithValue("@DateOfRelease", krm.DateOfRelease);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", krm.PhotoFileName);
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
                           delete from dbo.Krim
                            where KrimId=@KrimId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@KrimId", id);

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

