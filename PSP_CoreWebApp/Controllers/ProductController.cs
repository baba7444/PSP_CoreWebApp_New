﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSP_CoreWebApp.Models;
using PSP_CoreWebApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core_WebApp.Controllers
{
    /// <summary>
    /// DI the Repository Classes
    /// Controller: Base class for MVC Controllers that has following
    /// IActionFilter, IFilterMetadata, IAsyncActionFilter,  the Action FIlter Interfaces
    /// IDisposable, tyhe Memeory management Interface 
    /// All Action method of Controller class are HttpGet By Default 
    /// 
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IRepository<Product, int> repository;
        private readonly IRepository<Category, int> catRepository;
        public ProductController(IRepository<Product, int> repository, IRepository<Category, int> catRepository)
        {
            this.repository = repository;
            this.catRepository = catRepository;
        }
        public async Task<IActionResult> Index()
        {
            var res = await repository.GetAsync();
            return View(res); // return the Index View
        }

        public async Task<IActionResult> Create()
        {
            // define a ViewBag that will pass the Category List to Create View
            // so that it will be rendered in DropDown aka <select>
            // use the 'SelectList' class that will carry the data
            // Note: The Key of ViewBag must match with the Property from Model class bind to View
            // so that it can be used for Form Post
            // ViewBag compiled as ViewDataDiectionary at Runtime
            // ViewBag will be expired after the method completes it execution
            // IMP**, if a View Accept/uses a ViewBag then all action methods
            // returning the same view must pass ViewBag to View else View will crash
            var res = new Product();
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAsync(),
                "CategoryRowId", "CategoryName");

            return View(res); // return the create view
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product Product)
        {
            // check for the validation
            if (ModelState.IsValid)
            {
                var res = await repository.CreateAsync(Product);
                return RedirectToAction("Index"); // return to the Index View
            }
            else
            {
                ViewBag.CategoryRowId = new SelectList(await catRepository.GetAsync(),
              "CategoryRowId", "CategoryName");
                return View(Product); // stey on create view with validation error messages

            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            return View(res); // return view with data to be edited
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product Product)
        {
            // check for the validation
            if (ModelState.IsValid)
            {
                var res = await repository.UpdateAsync(id, Product);
                return RedirectToAction("Index"); // return to the Index View
            }
            return View(Product); // stey on edit view with validation error messages
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.DeleteAsync(id);
            if (res) // delete successful
            {
                return RedirectToAction("Index"); // return to the Index View
            }
            return RedirectToAction("Index"); // return to the Index View
        }
    }
}