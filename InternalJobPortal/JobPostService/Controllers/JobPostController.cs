using JobPostLibrary;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JobPostService.Controllers
{
    [Authorize]
    public class JobPostController : ApiController
    {
        IJobPostRepoAsync jpRepo = new JobPostRepoAsync();
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<JobPost> jobPosts = await jpRepo.GetAllJobPostsAsync();
            return Ok(jobPosts);
        }
        private void PublishToMessageQueue(string integrationEvent, string eventDate)
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventDate);
            channel.BasicPublish(exchange: "jobPost", routingKey: integrationEvent, basicProperties: null, body: body);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetOne(int postId)
        {
            try
            {
                JobPost jobPost = await jpRepo.GetJobByPostIdAsync(postId);
                return Ok<JobPost>(jobPost);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Insert(JobPost jobPost)
        {
            await jpRepo.InsertJobPostAsync(jobPost);
            var integrationEventData = JsonConvert.SerializeObject(new { PostId = jobPost.PostId });
            PublishToMessageQueue("jobPost.add", integrationEventData);
            return Created<JobPost>("api/JobPost/", jobPost);
        }
        [HttpPut]
        public async Task<IHttpActionResult> Update(int postId, JobPost jobPost)
        {
            await jpRepo.UpdateJobPostAsync(postId, jobPost);
            return Ok<JobPost>(jobPost);
        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int postId)
        {
            await jpRepo.DeleteJobPostAsync(postId);
            var integrationEventData = JsonConvert.SerializeObject(new { PostId = postId });
            PublishToMessageQueue("jobPost.delete", integrationEventData);
            return Ok();
        }

    }
}
