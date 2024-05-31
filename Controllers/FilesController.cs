using FileStorage.Core.DTO;
using FileStorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileStorage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController(FilesService filesService) : Controller
    {
        [Authorize]
        [HttpPost("upload")]
        public async Task<ActionResult> Upload(IFormFile formFile)
        {
            if (formFile is null) return BadRequest("No file provided.");

            string fileName = filesService.GetFileName(formFile.FileName);

            if (string.IsNullOrEmpty(fileName)) return BadRequest("File name is not valid.");

            string userEmail = User.Claims.First(claim => claim.Type.Equals(ClaimTypes.Email)).Value;

            long insertionID = await filesService.Insert(formFile, fileName, userEmail);

            return (insertionID > 0) ?
                Ok(insertionID) :
                StatusCode(StatusCodes.Status500InternalServerError);
        }


        [HttpGet("get")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> Get(int id)
        {
            if (id < 0) return BadRequest("Id must be a positive integer");

            FileDTO? file;

            bool isAdmin = User.IsInRole("Admin");
            if (isAdmin)
            {
                file = await filesService.Get(id);
            }
            else
            {
                string userEmail = User.Claims.First(claim => claim.Type.Equals(ClaimTypes.Email)).Value;
                file = await filesService.Get(id, userEmail);
            }

            if (file is null) return NotFound();

            Stream stream = new MemoryStream(file.FileBytes);

            return File(stream, "application/octet-stream", file.FileName);
        }
    }
}