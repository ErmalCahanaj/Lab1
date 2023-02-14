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
    public class DramaController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public DramaController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select DramaId, DramaName,Imdb,
                            convert(varchar(10),DateOfRelease,120) as DateOfRelease,PhotoFileName
                            from
                            dbo.Drama
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
        public JsonResult Post(Drama Drm)
        {
            string query = @"
                           insert into dbo.Drama
                           (DramaName,Imdb,DateOfRelease,PhotoFileName)
                    values (@DramaName,@Imdb,@DateOfRelease,@PhotoFileName)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DramaName", Drm.DramaName);
                    myCommand.Parameters.AddWithValue("@Imdb", Drm.Imdb);
                    myCommand.Parameters.AddWithValue("@DateOfRelease", Drm.DateOfRelease);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", Drm.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Drama Drm)
        {
            string query = @"
                           update dbo.Drama
                           set DramaName= @DramaName,
                            Imdb=@Imdb,
                            DateOfRelease=@DateOfRelease,
                            PhotoFileName=@PhotoFileName
                            where DramaId=@DramaId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DramaId", Drm.DramaId);
                    myCommand.Parameters.AddWithValue("@DramaName", Drm.DramaName);
                    myCommand.Parameters.AddWithValue("@Imdb", Drm.Imdb);
                    myCommand.Parameters.AddWithValue("@DateOfRelease", Drm.DateOfRelease);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", Drm.PhotoFileName);
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
                           delete from dbo.Drama
                            where DramaId=@DramaId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MovieAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DramaId", id);

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

