using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSkillLibrary
{
    public interface IJobSkillRepoAsync
    {
        Task<List<JobSkill>> GetAllJobSkillsAsync();
        Task<JobSkill> GetJobSkillByIdsAsync(string skillId, string jobId);
    }
}
