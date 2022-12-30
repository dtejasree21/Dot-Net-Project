using EmpSkillLibrary;
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
    public class EmpSkillApiRepo
    {
        static HttpClient webApi;
        string token;
        public async Task GetToken(string userName, string role, string key)
        {
            webApi = new HttpClient();
            webApi.BaseAddress = new Uri("http://localhost:63191/api/EmpSkill/");
            token = await webApi.GetStringAsync("http://localhost:52347/api/Auth?userName=" + userName + "&role=" + role + "&key=" + key);
            webApi.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public async Task DeleteEmpSkillAsync( string empId,string skillId)
        {
            await webApi.DeleteAsync("?empId=" + empId + "&skillId=" + skillId);
        }
        public async Task<List<EmpSkill>> GetAllEmpSkillsAsync()
        {
            HttpResponseMessage response = await webApi.GetAsync("");
            string str = await response.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            List<EmpSkill> empSkills = JsonConvert.DeserializeObject<List<EmpSkill>>(str);
            return empSkills;
        }

        public async Task<EmpSkill> GetEmpSkillByIdAsync(string empId,string skillId)
        {
            HttpResponseMessage response = await webApi.GetAsync("?empId=" + empId + "&skillId=" + skillId);
            string str = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                EmpSkill empSkill = JsonConvert.DeserializeObject<EmpSkill>(str);
                return empSkill;
            }
            else
            {
                throw new Exception("No such post id");
            }
        }

        public async Task InsertEmpSkillAsync(EmpSkill empSkill)
        {
            var json = JsonConvert.SerializeObject(empSkill);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await webApi.PostAsync("", data);

        }

        public async Task UpdateSkillAsync(string empId, string skillId,EmpSkill empSkill)
        {
            var json = JsonConvert.SerializeObject(empSkill);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await webApi.PutAsync("?empId=" + empId + "&skillId=" + skillId, data);
        }


    }
}