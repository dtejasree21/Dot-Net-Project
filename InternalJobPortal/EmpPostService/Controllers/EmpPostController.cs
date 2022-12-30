using EmpPostLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmpPostService.Controllers
{
    [Authorize]
    public class EmpPostController : ApiController
    {
        public static IEmpPostRepoAsync empPostRepoAsync = new EmpPostRepoAsync();
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<EmpPost> empPosts = await empPostRepoAsync.GetAllEmpPostAsync();
            return Ok<List<EmpPost>>(empPosts);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetOne(int postId,string empId)
        {
            try
            {
                EmpPost empPost = await empPostRepoAsync.GetEmpPostByIdAsync(postId, empId);
                return Ok<EmpPost>(empPost);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Insert(EmpPost empPost)
        {
            await empPostRepoAsync.InsertEmpPostAsync(empPost);
            return Created<EmpPost>("api/EmpPost/", empPost);
        }
        [HttpPut]
        public async Task<IHttpActionResult> Update(int postId, string empId,EmpPost empPost)
        {
            await empPostRepoAsync.UpdateEmpPostAsync(postId, empId, empPost);
            return Ok<EmpPost>(empPost);
        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int postId,string empId)
        {
            await empPostRepoAsync.DeleteEmpPostAsync(postId, empId);
            return Ok();
        }
    }
}
