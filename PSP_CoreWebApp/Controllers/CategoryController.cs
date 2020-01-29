using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSP_CoreWebApp.Models;
using PSP_CoreWebApp.Services;

namespace PSP_CoreWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category, int> repository;

        public CategoryController(IRepository<Category ,int> repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var res = await repository.GetAsync();
            return View(res); // return the Index View
        }

        public IActionResult Create()
        {
            return View(new Category()); // return the create view
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            // check for the validation
            if (ModelState.IsValid)
            {
                var res = await repository.CreateAsync(category);
                return RedirectToAction("Index"); // return to the Index View
            }
            return View(category); // stey on create view with validation error messages
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            return View(res); // return view with data to be edited
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            // check for the validation
            if (ModelState.IsValid)
            {
                var res = await repository.UpdateAsync(id, category);
                return RedirectToAction("Index"); // return to the Index View
            }
            return View(category); // stey on edit view with validation error messages
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
