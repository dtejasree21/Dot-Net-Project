using EmpSkillLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmpSkillService.Controllers
{
    [Authorize]
    public class EmpSkillController : ApiController
    {
        IEmpSkillRepoAsync empSkillRepo = new EmpSkillRepoAsync();
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<EmpSkill> empSkills = await empSkillRepo.GetAllEmpSkillsAsync();
            return Ok(empSkills);
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> GetEmployeeSkillByEid(string eid)
        {
            List<EmpSkill> empSkills = await empSkillRepo.GetEmpSkillsByEmpIdAsync(eid);
            return Ok(empSkills);
        }
        public async Task<IHttpActionResult> GetEmployeeSkillBySkillId(string sid)
        {
            List<EmpSkill> empSkills = await empSkillRepo.GetEmpSkillsBySkillIdAsync(sid);
            return Ok(empSkills);
        }
        public async Task<IHttpActionResult> GetOneEmpSkill(string eid, string sid)
        {
            try
            {
                EmpSkill emp = await empSkillRepo.GetEmpSkillByIdAsync(eid, sid);
                return Ok<EmpSkill>(emp);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPost]

        public async Task<IHttpActionResult> Insert(EmpSkill empSkill)
        {
            await empSkillRepo.InsertEmpSkillAsync(empSkill);
            return Created<EmpSkill>("/api/EmpSkill", empSkill);
        }
        [HttpPut]
        public async Task<IHttpActionResult> Update(string eid, string sid, EmpSkill empSkill)
        {
            await empSkillRepo.UpdateSkillAsync(eid, sid, empSkill);
            return Ok<EmpSkill>(empSkill);
        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string eid, string sid)
        {
            try
            {
                await empSkillRepo.DeleteSkillAsync(eid, sid);
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}
