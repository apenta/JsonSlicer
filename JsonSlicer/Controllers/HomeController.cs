using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        [ValidateAntiForgeryToken]
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
    }
}