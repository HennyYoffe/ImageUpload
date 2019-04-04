using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;
using Lesson55_Upload_April1.Models;

namespace Lesson55_Upload_April1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase image, string password)
        {
            string ext = Path.GetExtension(image.FileName);
            string fileName = $"{Guid.NewGuid()}{ext}";
            string fullPath = $"{Server.MapPath("/UploadedImages")}\\{fileName}";
            image.SaveAs(fullPath);
            var mgr = new ImageManager(Properties.Settings.Default.ConStr);
            int id= mgr.SaveImage(new Image
            {
                FileName = fileName,
                Password = password
            });
            
            return View(mgr.GetImage(id));
        }
        public ActionResult Image(int id, string password)
        {
            var mgr = new ImageManager(Properties.Settings.Default.ConStr);
            Image image = mgr.GetImage(id);
            ImageViewModel vm = new ImageViewModel();
            vm.Password = (string)Session["password"];
            if(image.Password == vm.Password)
            {
                vm.Image = image;
                vm.Session = true;
                vm.Image.Views = mgr.UpdateAndGetViews(image.Id);
            }
            else
            {
                if(image.Password == password)
                {
                    vm.Image = image;
                    Session["password"] = image.Password;
                    vm.Session = true;
                    vm.Image.Views = mgr.UpdateAndGetViews(image.Id);
                }
                else if(password != null)
                {
                    vm.Session = false;
                }
                else
                {
                    vm.First = true;
                    vm.Image = image;
                }
            }
            return View(vm);
        }

       
    }
}