using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostLibrary
{
    public class JobPostRepoAsync : IJobPostRepoAsync
    {
        JobPostDBEntities ent = new JobPostDBEntities();

        public JobPostRepoAsync()
        {
            ent.Configuration.ProxyCreationEnabled = false;
        }
        public async Task<List<Job>> GetAllJobIdsAsync()
        {
            List<Job> jobs = await (from j in ent.Jobs select j).ToListAsync();
            return jobs;
        }
        public async Task DeleteJobPostAsync(int postId)
        {
            JobPost jobPost = await GetJobByPostIdAsync(postId);
            ent.JobPosts.Remove(jobPost);
            await ent.SaveChangesAsync();
        }

        public async Task<List<JobPost>> GetAllJobPostsAsync()
        {
            List<JobPost> jobPosts = await ent.JobPosts.ToListAsync();
            return jobPosts;
        }

        public async Task<JobPost> GetJobByPostIdAsync(int postId)
        {
            try
            {
                JobPost jobPost = await (from x in ent.JobPosts where x.PostId == postId select x).FirstAsync();
                return jobPost;
            }
            catch (Exception)
            {
                throw new Exception("No such Post with PostId");
            }
        }

        public async Task InsertJobPostAsync(JobPost jobPost)
        {
            ent.JobPosts.Add(jobPost);
            await ent.SaveChangesAsync();
        }

        public async Task UpdateJobPostAsync(int postId, JobPost jobPost)
        {
            JobPost jb = await GetJobByPostIdAsync(postId);
            jb.DOP = jobPost.DOP;
            jb.LastDate = jobPost.LastDate;
            jb.Vacancies = jobPost.Vacancies;
            await ent.SaveChangesAsync();
        }
    }
}
