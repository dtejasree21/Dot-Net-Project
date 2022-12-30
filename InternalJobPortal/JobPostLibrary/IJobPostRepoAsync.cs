using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostLibrary
{
    public interface IJobPostRepoAsync
    {
        Task<List<JobPost>> GetAllJobPostsAsync();
        Task<JobPost> GetJobByPostIdAsync(int postId);

        Task InsertJobPostAsync(JobPost jobPost);
        Task UpdateJobPostAsync(int postId, JobPost jobPost);
        Task DeleteJobPostAsync(int postId);
        Task<List<Job>> GetAllJobIdsAsync();

    }
}
