using Beehive.Models.DbRecords;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beehive.Controllers
{
    public class FileController(ApplicationContext ac) : Controller
    {
        readonly ApplicationContext db = ac;

        [HttpPost]
        [Authorize]
        public IActionResult PostFile(FileInfo fileInfo, byte[] data)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "files");
            if (GlobalVals.ImageExtentions.Contains(fileInfo.Extension))
                path = Path.Combine(path, "images");
            else if (GlobalVals.VideoExtentions.Contains(fileInfo.Extension))
                path = Path.Combine(path, "videos");
            else if (GlobalVals.AudioExtentions.Contains(fileInfo.Extension))
                path = Path.Combine(path, "audio");
            else path = Path.Combine(path, "other");
            path = Path.Combine(path, fileInfo.Name, Convert.ToString(data.Sum(e => e), 16));
            var n = new FileInfo(path);
            if (n.Exists) return Ok(n);
            n.Create();
            using var s = new BinaryWriter(n.OpenWrite());
            s.Write(data);
            var rec = new FileRecord()
            {
                Location = path,
                UseCount = 0
            };
            db.Add(rec);
            db.SaveChanges();
            return Created(path, rec);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetFile(string path)
        {
            var info = new FileInfo(path);
            if (!info.Exists) return NotFound();
            var f = info.OpenRead();
            using var s = new BinaryReader(f);
            var res = s.ReadBytes((int)f.Length);
            return Ok(res);
        }
    }
}
