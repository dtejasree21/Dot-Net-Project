using EmpPostLibrary;
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
    public class EmpPostApiRepo
    {
        static HttpClient webApi;
        string token;
        public async Task GetToken(string userName, string role,string key)
        {
            webApi = new HttpClient();
            webApi.BaseAddress = new Uri("http://localhost:61992/api/EmpPost/");
            token = await webApi.GetStringAsync("http://localhost:52347/api/Auth?userName=" + userName + "&role=" + role + "&key=" + key);
            webApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task DeleteEmpPostAsync(int postId,string empId)
        {
            await webApi.DeleteAsync("?postId="+postId+"&empId="+empId);
        }
        /*public async Task<List<Employee>> GetAllEmpIdsAsync()
        {
            HttpResponseMessage response = await webApi.GetAsync("");
            string str = await response.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(str);
            return employees;
        }
*/

        public async Task<List<EmpPost>> GeAllEmpPostAsync()
        {
            HttpResponseMessage response = await webApi.GetAsync("");
            string str = await response.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            List<EmpPost> empPosts = JsonConvert.DeserializeObject<List<EmpPost>>(str);
            return empPosts;
        }

        public async Task<EmpPost> GetEmpPostByIdAsync(int postId,string empId)
        {
            HttpResponseMessage response = await webApi.GetAsync("?postId="+postId+"&empId="+empId);
            string str = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                EmpPost empPost = JsonConvert.DeserializeObject<EmpPost>(str);
                return empPost;
            }
            else
            {
                throw new Exception("No such post id");
            }
        }

        public async Task InsertEmpPostAsync(EmpPost empPost)
        {
            var json = JsonConvert.SerializeObject(empPost);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await webApi.PostAsync("", data);

        }

        public async Task UpdateEmpPostAsync(int postId,string empId, EmpPost empPost)
        {
            var json = JsonConvert.SerializeObject(empPost);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await webApi.PutAsync("?postId="+postId+"&empId="+empId, data);
        }
    }
}