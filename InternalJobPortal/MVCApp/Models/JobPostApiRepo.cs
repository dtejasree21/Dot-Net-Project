using JobPostLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVCApp.Models
{
    public class JobPostApiRepo
    {
        static HttpClient webApi;
        string token;
        public async Task GetToken(string userName, string role, string key)
        {
            webApi = new HttpClient();
            webApi.BaseAddress = new Uri("http://localhost:56702/api/JobPost/");
            token = await webApi.GetStringAsync("http://localhost:52347/api/Auth?userName=" + userName + "&role=" + role + "&key=" + key);
            webApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task DeleteJobPostAsync(int postId)
        {
            await webApi.DeleteAsync("?postId=" + postId);
        }
        public async Task<List<JobPost>> GetAllJobPostsAsync()
        {
            HttpResponseMessage response = await webApi.GetAsync("");
            string str = await response.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            List<JobPost> jobPost = JsonConvert.DeserializeObject<List<JobPost>>(str);
            return jobPost;
        }

        public async Task<JobPost> GetJobByPostIdAsync(int postId)
        {
            HttpResponseMessage response = await webApi.GetAsync("?postId=" + postId );
            string str = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                JobPost jobPost = JsonConvert.DeserializeObject<JobPost>(str);
                return jobPost;
            }
            else
            {
                throw new Exception("No such post id");
            }
        }

        public async Task InsertJobPostAsync(JobPost jobPost)
        {
            var json = JsonConvert.SerializeObject(jobPost);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await webApi.PostAsync("", data);

        }

        public async Task UpdateJobPostAsync(int postId,JobPost jobPost)
        {
            var json = JsonConvert.SerializeObject(jobPost);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await webApi.PutAsync("?postId=" + postId, data);
        }
    }
}