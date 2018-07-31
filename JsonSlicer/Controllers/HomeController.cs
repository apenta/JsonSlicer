using JsonSlicer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JsonSlicer.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Server.MapPath("~/UploadedFiles") +
                                               Path.GetFileName(file.FileName);
                        file.SaveAs(path);
                        var webClient = new WebClient();
                        var json = webClient.DownloadString(path);
                        var keyvaluepairs = JsonConvert.DeserializeObject<KeyValuePair>(json);
                        
                    }

                    ViewBag.Message = "File uploaded successfully";
                }


                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }

      
    }
}