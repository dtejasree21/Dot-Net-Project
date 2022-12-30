using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EmpPostLibrary;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class EmpPostController : Controller
    {
        public static IEmpPostRepoAsync eslRepo = new EmpPostRepoAsync();
        public EmpPostApiRepo esRepo = new EmpPostApiRepo();

        // GET: EmpPost
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string roleName = "Employee";//User.Claims.ToArray()[4].Value;
            string key = "my_secret_key_12345";
            await esRepo.GetToken(userName, roleName,key);
            List<EmpPost> EmpPosts = await esRepo.GeAllEmpPostAsync();
            return View(EmpPosts);
        }

        // GET: EmpPost/Details/5
        public async Task<ActionResult> Details(int postId, string empId)
        {
            EmpPost empPost = await esRepo.GetEmpPostByIdAsync(postId, empId);
            return View(empPost);
        }

        // GET: EmpPost/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmpPost/Create
        [HttpPost]
        public async Task<ActionResult> Create(EmpPost empPost)
        {
            try
            {
                // TODO: Add insert logic here
                //empPost.PostId = 1;
                empPost.ApplyDate = DateTime.Now;
                empPost.ApplicationStatus = "Reviewing";
                await esRepo.InsertEmpPostAsync(empPost);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpPost/Edit/5
        public ActionResult Edit(string empId, string skillId)
        {
            return View();
        }

        // POST: EmpPost/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int postId, string empId, EmpPost EmpPost)
        {
            try
            {
                // TODO: Add update logic here
                await esRepo.UpdateEmpPostAsync(postId, empId, EmpPost);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpPost/Delete/5
        public async Task<ActionResult> Delete(int postId, string empId)
        {
            EmpPost empPost = await esRepo.GetEmpPostByIdAsync(postId, empId);
            return View(empPost);
        }

        // POST: EmpPost/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int postId, string empId, EmpPost EmpPost)
        {
            try
            {
                // TODO: Add delete logic here
                await esRepo.DeleteEmpPostAsync(postId, empId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public static async Task<List<SelectListItem>> GetAllEmpIds()
        {
            //IFightRepoAsync flightRepo = new FlightRepoAsync();

            List<Employee> employees = await eslRepo.GetAllEmpIdsAsync();
            List<SelectListItem> enos = new List<SelectListItem>();
            foreach (Employee emp in employees)
            {
                enos.Add(new SelectListItem { Text = emp.EmpId, Value = emp.EmpId });
            }
            return enos;
        }

        public static async Task<List<SelectListItem>> GetAllPostIds()
        {
            //IFightRepoAsync flightRepo = new FlightRepoAsync();

            List<int> postIds = await eslRepo.GetAllPostIdsAsync();
            List<SelectListItem> pids = new List<SelectListItem>();
            foreach (int pid in postIds)
            {
                pids.Add(new SelectListItem { Text = pid.ToString(), Value = pid.ToString() });
            }
            return pids;
        }

        public static List<SelectListItem> GetApplicationStatus()
        {
            List<SelectListItem> status = new List<SelectListItem>();
            status.Add(new SelectListItem { Text = "Reviewing", Value = "Reviewing" });
            status.Add(new SelectListItem { Text = "Accepted", Value = "Accepted" });
            status.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });
            return status;
        }
    }
}
