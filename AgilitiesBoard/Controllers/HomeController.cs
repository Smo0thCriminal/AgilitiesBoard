using AgilitiesBoard.Models;
using AgilitiesBoard.Services;
using AgilitiesBoard.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilitiesBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBoardService _service;

        public HomeController(IBoardService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var data = _service.GetBoardData("data");

            if (data != null)
            {
                ViewBag.MsgCount = data.Count();
                return View(data.OrderByDescending(x => x.Date));
            }
            return View();
        }

        [HttpPost]
        public ActionResult BoardUpdate(string searchString)
        {
            var data = _service.GetBoardData("data");

            if (data == null)
            {
                throw new HttpException(404, "Board is empty");
            }
            if (string.IsNullOrEmpty(searchString))
            {
                if (data != null)
                {
                    var resultData = data.OrderByDescending(x => x.Date);
                    ViewBag.MsgCount = resultData.Count();
                    return PartialView("_BoardPage", resultData);
                }
            }
            var result = _service.FindByNameOrMessage("data", searchString);
            if (result.Count != 0)
                ViewBag.MsgCount = result.Count();

            return PartialView("_BoardPage", result.OrderByDescending(x => x.Date));
        }

        [HttpPost]
        public ActionResult BoardPost(BoardModel model)
        {
            var data = new List<BoardModel>();
            if (_service.GetBoardData("data") == null)
            {
                data.Add(model);
                _service.SaveChanges("data", data);
            }
            else
            {
                foreach (var item in _service.GetBoardData("data"))
                {
                    data.Add(item);
                }
                model.MessageNumber = data.Max(x => x.MessageNumber) + 1;
                data.Add(model);
                _service.SaveChanges("data", data);
            }
            
            ViewBag.MsgCount = data.Count();
            return PartialView("_BoardPage", data.OrderByDescending(x => x.Date).ToList());
        }

        public ActionResult Upload(byte[] bytes)
        {
            bool success = false;
            string message = String.Empty;
            string imageUrl = String.Empty;
            
            string fileName = Request["HTTP_X_FILE_NAME"]; 
            string fileSize = Request["HTTP_X_FILE_SIZE"];
            try
            {
                byte[] buffer = new byte[Request.InputStream.Length];
                int offset = 0;
                int cnt = 0;
                while ((cnt = Request.InputStream.Read(buffer, offset, 10)) > 0)
                {
                    offset += cnt;
                }
                
                var path = AppDomain.CurrentDomain.BaseDirectory + "uploads";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                }

                success = true;
                message = "Success...";
                imageUrl = Path.Combine("\\uploads", fileName);
            }
            catch (Exception)
            {
                success = false;
                message = "Error...";
            }

            var jsonData = new
            {
                success = success,
                message = message,
                imageUrl = imageUrl
            };

            return Json(jsonData);
        }
    }
}