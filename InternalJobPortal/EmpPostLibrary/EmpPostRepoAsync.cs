using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpPostLibrary
{
    public class EmpPostRepoAsync:IEmpPostRepoAsync
    {
        EmpPostDBEntities ent = new EmpPostDBEntities();
        public EmpPostRepoAsync()
        {
            ent.Configuration.ProxyCreationEnabled = false;
        }

        public async Task Add(int postId)
        {
            ent.JobPosts.Add(new JobPost() { PostId = postId });
            await ent.SaveChangesAsync();
        }

        public async Task Delete(int postId)
        {
            JobPost jobPost = await (from jp in ent.JobPosts where postId == jp.PostId select jp).FirstAsync();
            ent.JobPosts.Remove(jobPost);
            await ent.SaveChangesAsync();
        }

        public async Task DeleteEmpPostAsync(int postId, string empId)
        {
            try
            {
                EmpPost empPost = await (from ep in ent.EmpPosts where (ep.PostId == postId & ep.EmpId == empId) select ep).FirstAsync();
                ent.EmpPosts.Remove(empPost);
                await ent.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("No Such EmpId for this postId");
            }
        }

        public async Task<List<EmpPost>> GetAllEmpPostAsync()
        {
            List<EmpPost> empPosts = await ent.EmpPosts.ToListAsync();
            return empPosts;
        }

        public async Task<EmpPost> GetEmpPostByIdAsync(int postId, string empId)
        {
            try
            {
                EmpPost empPost = await (from ep in ent.EmpPosts where (ep.PostId == postId & ep.EmpId == empId) select ep).FirstAsync();
                return empPost;
            }
            catch (Exception)
            {
                throw new Exception("No such postId exists with the empId");
            }
        }

        public async Task InsertEmpPostAsync(EmpPost empPost)
        {
            ent.EmpPosts.Add(empPost);
            await ent.SaveChangesAsync();
        }

        public async Task UpdateEmpPostAsync(int postId, string empId, EmpPost empPost)
        {
            EmpPost ep = await GetEmpPostByIdAsync(postId, empId);
            ep.ApplicationStatus = empPost.ApplicationStatus;
            await ent.SaveChangesAsync();
        }
        public async Task<List<Employee>> GetAllEmpIdsAsync()
        {
            List<Employee> employees = await (from e in ent.Employees select e).ToListAsync();
            return employees;
        }



        public async Task<List<int>> GetAllPostIdsAsync()
        {
            List<int> postIds = await (from p in ent.JobPosts select p.PostId).ToListAsync();
            return postIds;
        }

    }
}
