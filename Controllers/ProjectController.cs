using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using e_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace e_Project.Controllers
{
    public class ProjectController : Controller
    {
        private readonly DB _context;
        public ProjectController(DB context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            _context.Add(project);
            _context.SaveChanges();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        public IActionResult ConfirmDelete(int Id)
        {
            var project = _context.projects.Where(p => p.Id == Id).First();
            ViewBag.returnURL = Request.Headers["Referer"].ToString();
            return View(project);
        }
        public IActionResult Delete(int Id)
        {
            var project = _context.projects.Where(p => p.Id == Id).First();
            _context.projects.Remove(project);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit(int Id)
        {
            var project = _context.projects.Where(p => p.Id == Id).First();
            ViewBag.returnURL = Request.Headers["Referer"].ToString();
            return View(project);
        }
        [HttpPost]
        public IActionResult Edit(Project project)
        {
            _context.projects.Where(p => p.Id == project.Id).First().ProgrammingLanguage = project.ProgrammingLanguage;
            _context.projects.Where(p => p.Id == project.Id).First().Status = project.Status;
            _context.projects.Where(p => p.Id == project.Id).First().Title = project.Title;
            _context.projects.Where(p => p.Id == project.Id).First().Description = project.Description;
            _context.projects.Where(p => p.Id == project.Id).First().Author = project.Author;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Search(string subString)
        {
            var data = _context.projects.Where(p => p.Title.Contains(subString) || p.Description.Contains(subString)
            || p.Author.Contains(subString) || p.ProgrammingLanguage.Contains(subString));

            var projectsContainingString = data.ToList();
            List<int> numberOfTimesContained = new List<int>();
            foreach(var p in projectsContainingString)
            {
                int count = 0, n = 0;
                
                while ((n = p.Title.IndexOf(subString, n, StringComparison.InvariantCulture)) != -1)
                {
                    n += subString.Length;
                    count++;
                }
                n = 0;
                while ((n = p.Description.IndexOf(subString, n, StringComparison.InvariantCulture)) != -1)
                {
                    n += subString.Length;
                    count++;
                }
                n = 0;
                while ((n = p.Author.IndexOf(subString, n, StringComparison.InvariantCulture)) != -1)
                {
                    n += subString.Length;
                    count++;
                }
                n = 0;
                while ((n = p.ProgrammingLanguage.IndexOf(subString, n, StringComparison.InvariantCulture)) != -1)
                {
                    n += subString.Length;
                    count++;
                }

                numberOfTimesContained.Add(count);
            }

            for(int i = 0; i < projectsContainingString.Count; i++)
            {
                for(int j = 0; j < projectsContainingString.Count; j++)
                {
                    if( numberOfTimesContained[i] > numberOfTimesContained[j])
                    {
                        var temp = projectsContainingString[j];
                        projectsContainingString[j] = projectsContainingString[i];
                        projectsContainingString[i] = temp;

                        var tempNumber = numberOfTimesContained[j];
                        numberOfTimesContained[j] = numberOfTimesContained[i];
                        numberOfTimesContained[i] = tempNumber;
                    }
                }
            }
            ViewBag.message = subString;
            return View(projectsContainingString);
        }
    }
}
