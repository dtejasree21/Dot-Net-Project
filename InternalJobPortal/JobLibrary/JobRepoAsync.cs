using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLibrary
{
    public class JobRepoAsync:IJobRepoAsync
    {
        JobDBEntities ent = new JobDBEntities();

        public async Task<List<Job>> GetAllJobsAsync()
        {
            List<Job> jobs = await ent.Jobs.ToListAsync();
            return jobs;
        }

        public async Task<Job> GetJobByIdAsync(string jobId)
        {
            try
            {
                Job job = await(from j in ent.Jobs where j.JobId == jobId select j).FirstAsync();
                return job;
            }catch(Exception)
            {
                throw new Exception("No such job Id");
            }
        }
    }
}
