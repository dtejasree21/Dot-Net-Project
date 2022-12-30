using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSkillLibrary
{
    public class JobSkillRepoAsync : IJobSkillRepoAsync
    {
        JobSkillDBEntities ent = new JobSkillDBEntities();
        public JobSkillRepoAsync()
        {
            ent.Configuration.ProxyCreationEnabled = false;
            ent.Configuration.LazyLoadingEnabled = true;
        }
        public async Task<List<JobSkill>> GetAllJobSkillsAsync()
        {
            List<JobSkill> jobSkills = await ent.JobSkills.ToListAsync();
            return jobSkills;
        }

        public async Task<JobSkill> GetJobSkillByIdsAsync(string skillId, string jobId)
        {
            try
            {
                JobSkill jobSkill = await (from js in ent.JobSkills where (js.SkillId == skillId & js.JobId == jobId) select js).FirstAsync();
                return jobSkill;
            }
            catch (Exception)
            {
                throw new Exception("No such job skill");
            }
        }
    }
}
