using ElevenNote.Service;
using EverNote.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class CatagoryController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CatagoryService(userId);
            var model = service.GetCatagory();

            return View(model);
        }

        //Add method here VVVV
        //GET
        public ActionResult Create()
        {
            return View();
        }

        //Add code here vvvv
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatagoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCatagoryService();

            if (service.CreateCatagory(model))
            {
                TempData["SaveResult"] = "Your Catagory was created.";

                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateCatagoryService();
            var model = svc.GetCatagoryById(id);

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var service = CreateCatagoryService();
            var detail = service.GetCatagoryById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.CatagoryId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CatagoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CatagoryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCatagoryService();

            if (service.UpdateCatagory(model))
            {
                TempData["SaveResult"] = "Your Catagory was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Catagory could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCatagoryService();
            var model = svc.GetCatagoryById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCatagoryService();

            service.DeleteCatagory(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private CatagoryService CreateCatagoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CatagoryService(userId);
            return service;
        }


    }
}