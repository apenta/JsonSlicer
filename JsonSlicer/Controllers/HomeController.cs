using System;
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
            return View(new IndexViewModel());
        }

        [HttpPost]
        public ActionResult FileUpload(IndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.InputFile != null)
                    {
                        using (var reader = new StreamReader(viewModel.InputFile.InputStream))
                        {
                            var contents = reader.ReadToEnd();
                            var inputFile = new InputFile
                            {
                                Data = JsonConvert.DeserializeObject<Dictionary<string, object>>(contents)
                            };

                            var vm = new DetailsViewModel
                            {
                                InputFile = inputFile
                            };

                            TempData["DetailsViewModel"] = vm;
                            return RedirectToAction(nameof(Details));
                        }
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

            return View(nameof(Index), viewModel);
        }

        public ActionResult Details()
        {
            var viewModel = (DetailsViewModel) TempData["DetailsViewModel"];
            return View(viewModel);
        }
    }
}