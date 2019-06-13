using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PsHelloAzure.Models;
using PsHelloAzure.Services;

namespace PsHelloAzure.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseStore courseStore;

        public CoursesController(CourseStore courseStore)
        {
            this.courseStore = courseStore;
        }

        public IActionResult Index()
        {
            var model = courseStore.GetAllCourses();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert()
        {
            var data = new SampleData().GetCourses();
            await courseStore.InsertCourses(data);
            return RedirectToAction(nameof(Index));
        }
    }
}