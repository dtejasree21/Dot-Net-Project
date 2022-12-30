using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using MVCApp.Models;
using JobPostLibrary;
using System.Threading.Tasks;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class JobPostController : Controller
    {
        public static IJobPostRepoAsync jpRepo = new JobPostRepoAsync();
        public JobPostApiRepo jobPostRepo = new JobPostApiRepo();

        // GET: JobPost
        public async Task<ActionResult> Index()
        {
            List<JobPost> JobPosts = await jobPostRepo.GetAllJobPostsAsync();
            return View(JobPosts);
        }

        // GET: JobPost/Details/5
        public async Task<ActionResult> Details(int postId)
        {
            JobPost JobPost = await jobPostRepo.GetJobByPostIdAsync(postId);
            return View(JobPost);
        }

        // GET: JobPost/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobPost/Create
        [HttpPost]
        public async Task<ActionResult> Create(JobPost jobPost)
        {
            try
            {
                // TODO: Add insert logic here
                await jobPostRepo.InsertJobPostAsync(jobPost);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobPost/Edit/5
        public ActionResult Edit(int postId)
        {
            return View();
        }

        // POST: JobPost/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int postId, JobPost jobPost)
        {
            try
            {
                // TODO: Add update logic here
                await jobPostRepo.UpdateJobPostAsync(postId, jobPost);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobPost/Delete/5
        public async Task<ActionResult> Delete(int postId)
        {
            JobPost jobPost = await jobPostRepo.GetJobByPostIdAsync(postId);
            return View(jobPost);
        }

        // POST: JobPost/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int postId, JobPost jobPost)
        {
            try
            {
                // TODO: Add delete logic here
                await jobPostRepo.DeleteJobPostAsync(postId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public static async Task<List<SelectListItem>> GetAllJobIds()
        {
            //IFightRepoAsync flightRepo = new FlightRepoAsync();

            List<Job> jobs = await jpRepo.GetAllJobIdsAsync();
            List<SelectListItem> jnos = new List<SelectListItem>();
            foreach (Job job in jobs)
            {
                jnos.Add(new SelectListItem { Text = job.JobId, Value = job.JobId });
            }
            return jnos;
        }


    }
}
