using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LubanSample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage(string setfolder)
        {
            try
            {
                if (Request.Files.Count == 1)
                {
                    //把每个文件转换成HttpPostedFileBase
                    HttpPostedFileBase hpf = Request.Files[0] as HttpPostedFileBase;
                    string folder = DateTime.Now.ToString("yyyyMMdd");
                    if (!string.IsNullOrEmpty(setfolder))
                        folder = setfolder;
                    string fileName = DateTime.Now.ToString("yyyyMMddhhmmssff") + Path.GetExtension(hpf.FileName);
                    var result = ImageUtils.UploadFiles(hpf, folder, fileName);

                    return Request.CreateResponse(result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "一次只能上传一个文件");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}