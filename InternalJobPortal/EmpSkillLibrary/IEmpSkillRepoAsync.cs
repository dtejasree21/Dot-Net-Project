using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpSkillLibrary
{
    public interface IEmpSkillRepoAsync
    {
        Task<List<EmpSkill>> GetAllEmpSkillsAsync();
        Task<EmpSkill> GetEmpSkillByIdAsync(string empId, string skillId);
        Task InsertEmpSkillAsync(EmpSkill empSkill);
        Task UpdateSkillAsync(string empId, string skillId,  EmpSkill empSkill);
        Task DeleteSkillAsync(string empId, string skillId);
        Task<List<EmpSkill>> GetEmpSkillsByEmpIdAsync(string empId);
        Task<List<EmpSkill>> GetEmpSkillsBySkillIdAsync(string skillId);
        Task<List<Employee>> GetAllEmpIdsAsync();
        Task<List<Skill>> GetAllSkillIdsAsync();
    }
}
