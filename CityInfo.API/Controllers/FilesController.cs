using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _filesExtentionContentTypeProvider; 
        public FilesController(FileExtensionContentTypeProvider filesExtentionContentTypeProvider)
        {
            _filesExtentionContentTypeProvider = filesExtentionContentTypeProvider ?? throw new System.ArgumentException(nameof(filesExtentionContentTypeProvider)); 
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            var pathToFile = "LichKing.png";

            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            if(!_filesExtentionContentTypeProvider.TryGetContentType(pathToFile, out var contentType)) 
            {
                contentType = "application/octet-stream"; 
            }   

            var bytes = System.IO.File.ReadAllBytes(pathToFile);    

            return File(bytes, contentType, Path.GetFileName(pathToFile));

        }
    }
}
