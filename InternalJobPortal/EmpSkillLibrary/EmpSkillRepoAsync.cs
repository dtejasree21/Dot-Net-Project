using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpSkillLibrary
{
    public class EmpSkillRepoAsync : IEmpSkillRepoAsync
    {
        EmpSkillDBEntities ent = new EmpSkillDBEntities();
        public EmpSkillRepoAsync()
        {
            ent.Configuration.ProxyCreationEnabled = false;
            ent.Configuration.LazyLoadingEnabled = true;
        }
        public async Task DeleteSkillAsync(string empId, string skillId)
        {
            try
            {
                EmpSkill empSkill = await (from es in ent.EmpSkills where (es.EmpId == empId & es.SkillId == skillId) select es).FirstAsync();
                ent.EmpSkills.Remove(empSkill);
                await ent.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("No such Employee Skill");
            }

        }

        public async Task<List<EmpSkill>> GetAllEmpSkillsAsync()
        {
            List<EmpSkill> empSkills = await ent.EmpSkills.ToListAsync();
            return empSkills;
        }

        public async Task<EmpSkill> GetEmpSkillByIdAsync(string empId, string skillId)
        {
            try
            {
                EmpSkill empSkill =await (from es in ent.EmpSkills where (es.EmpId == empId & es.SkillId == skillId) select es).FirstAsync();
                return empSkill;
            }
            catch (Exception)
            {
                throw new Exception("No such Employee Skill");
            }
        }

        public async Task<List<EmpSkill>> GetEmpSkillsByEmpIdAsync(string empId)
        {
            try
            {
                List<EmpSkill> empSkills = await (from es in ent.EmpSkills where (es.EmpId == empId) select es).ToListAsync();
                return empSkills;

            }
            catch (Exception)
            {
                throw new Exception("No such Employee");
            }
        }

        public async Task<List<EmpSkill>> GetEmpSkillsBySkillIdAsync(string skillId)
        {
            try
            {
                List<EmpSkill> empSkills = await (from es in ent.EmpSkills where (es.SkillId == skillId) select es).ToListAsync();
                return empSkills;

            }
            catch (Exception)
            {
                throw new Exception("No such Skill");
            }
        }

        public async Task InsertEmpSkillAsync(EmpSkill empSkill)
        {
            ent.EmpSkills.Add(empSkill);
            await ent.SaveChangesAsync();
        }

        public async Task UpdateSkillAsync(string empId, string skillId, EmpSkill empSkill)
        {
            try
            {
                EmpSkill empSkillCur= await (from es in ent.EmpSkills where (es.EmpId == empId & es.SkillId == skillId) select es).FirstAsync();
                empSkillCur.SkillExperience = empSkill.SkillExperience;
                await ent.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("No such Employee Skill");
            }
        }
        public async Task<List<Employee>> GetAllEmpIdsAsync()
        {
            List<Employee> employees = await (from e in ent.Employees select e).ToListAsync();
            return employees;
        }



        public async Task<List<Skill>> GetAllSkillIdsAsync()
        {
            List<Skill> skills = await (from s in ent.Skills select s).ToListAsync();
            return skills;
        }
    }
}
