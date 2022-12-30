using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using MVCApp.Models;
using EmpSkillLibrary;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class EmpSkillController : Controller
    {
        public static IEmpSkillRepoAsync esRepo = new EmpSkillRepoAsync();
        public EmpSkillApiRepo empSkillRepo = new EmpSkillApiRepo();
        // GET: EmpSkill
        public async Task<ActionResult> Index()
        {
            List<EmpSkill> empSkills = await empSkillRepo.GetAllEmpSkillsAsync();
            return View(empSkills);
        }

        // GET: EmpSkill/Details/5
        public async Task<ActionResult> Details(string empId, string skillId)
        {
            EmpSkill empSkill = await empSkillRepo.GetEmpSkillByIdAsync(empId, skillId);
            return View(empSkill);
        }

        // GET: EmpSkill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmpSkill/Create
        [HttpPost]
        public async Task<ActionResult> Create(EmpSkill empSkill)
        {
            try
            {
                // TODO: Add insert logic here
                await empSkillRepo.InsertEmpSkillAsync(empSkill);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpSkill/Edit/5
        public ActionResult Edit(string empId, string skillId)
        {
            return View();
        }

        // POST: EmpSkill/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string empId, string skillId, EmpSkill empSkill)
        {
            try
            {
                // TODO: Add update logic here
                await empSkillRepo.UpdateSkillAsync(empId, skillId, empSkill);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpSkill/Delete/5
        public async Task<ActionResult> Delete(string empId, string skillId)
        {
            EmpSkill empSkill=await empSkillRepo.GetEmpSkillByIdAsync(empId, skillId);
            return View(empSkill);
        }

        // POST: EmpSkill/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string empId, string skillId, EmpSkill empSkill)
        {
            try
            {
                // TODO: Add delete logic here
                await empSkillRepo.DeleteEmpSkillAsync(empId, skillId);
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
            
            List<Employee> employees = await esRepo.GetAllEmpIdsAsync();
            List<SelectListItem> enos = new List<SelectListItem>();
            foreach (Employee emp in employees)
            {
                enos.Add(new SelectListItem { Text = emp.EmpId, Value = emp.EmpId });
            }
            return enos;
        }

        public static async Task<List<SelectListItem>> GetAllSkillIds()
        {
            //IFightRepoAsync flightRepo = new FlightRepoAsync();

            List<Skill> skills = await esRepo.GetAllSkillIdsAsync();
            List<SelectListItem> enos = new List<SelectListItem>();
            foreach (Skill skill in skills)
            {
                enos.Add(new SelectListItem { Text = skill.SkillId, Value = skill.SkillId });
            }
            return enos;
        }
    }
}
