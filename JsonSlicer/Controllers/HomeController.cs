using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JsonSlicer.Models;
using JsonSlicer.ViewModels;
using JsonSlicer.ViewModels.Home;
using Newtonsoft.Json;
using IndexViewModel = JsonSlicer.ViewModels.Home.IndexViewModel;

namespace JsonSlicer.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new IndexViewModel
            {
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.InputFile != null)
                    {
                        using (var reader = new StreamReader(viewModel.InputFile.InputStream))
                        {
                            viewModel.Contents = reader.ReadToEnd();
                        }
                    }

                    var inputFile = new InputFile
                    {
                        Data = JsonConvert.DeserializeObject<Dictionary<string, object>>(viewModel.Contents)
                    };

                    viewModel.File = inputFile;

                    return View(viewModel);
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

            return View(viewModel);
        }

        [HttpPost]

        public FileResult Export(IndexViewModel viewModel)
        {
            var inputFile = new InputFile
            {
                Data = JsonConvert.DeserializeObject<Dictionary<string, object>>(viewModel.Contents)
            };

            var outputFile = new InputFile
            {
                Data = new Dictionary<string, object>()
            };

            foreach (var key in viewModel.SelectedKeys)
            {
                var value = inputFile.Data[key];
                outputFile.Data[key] = value;
            }

            var json = JsonConvert.SerializeObject(outputFile.Data, Formatting.Indented);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(bytes);

            return File(stream, "application/json", string.Format("{0}.json", "JsonSlicerOutput"));
            
            
        }
    }
}