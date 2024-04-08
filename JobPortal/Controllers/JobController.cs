using JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers
{
    public class JobController : Controller
    {
        JobPortalDbContext db_context = new JobPortalDbContext();
        // GET: JobController
        public ActionResult Index()
        {
            var job_list = db_context.Jobs.Select(x => new JobViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                Title = x.Title,
                OrganizationId = x.OrganizationId,
                OrganizationName = x.Organization.Name,
            });
            return View(job_list);
        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {
            var job_detail = db_context.Jobs.Include(x => 
            x.Organization).Where(x => x.Id == id).FirstOrDefault();
            var mapped_job = new JobViewModel()
            {
                Id = job_detail.Id, 
                Title = job_detail.Title,
                Description = job_detail.Description,
                OrganizationName = job_detail.Organization.Name,
            };
            return View(mapped_job);
        }

        // GET: JobController/Create
        public ActionResult Create()
        {
            var org_list = db_context.Organizations.Select( x => new OrganizationViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            ViewBag.Organizations = org_list;
            return View();
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobViewModel jvm)
        {
            try
            {
                var entity = new Job()
                {
                    Title = jvm.Title,
                    Description = jvm.Description,
                    OrganizationId = jvm.OrganizationId,
                };
                db_context.Jobs.Add(entity);
                db_context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: JobController/Edit/5
        public ActionResult Edit(int id)
        {
            var job_detail = db_context.Jobs.Include(x =>
            x.Organization).Where(x => x.Id == id).FirstOrDefault();
            var mapped_job = new JobViewModel()
            {
                Id = job_detail.Id,
                Title = job_detail.Title,
                Description = job_detail.Description,
                OrganizationName = job_detail.Organization.Name,
            };
            return View(mapped_job);
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
