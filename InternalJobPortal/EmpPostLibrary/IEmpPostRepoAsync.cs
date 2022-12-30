using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpPostLibrary
{
    public interface IEmpPostRepoAsync
    {
        Task<List<EmpPost>> GetAllEmpPostAsync();
        Task<EmpPost> GetEmpPostByIdAsync(int postId, string empId);
        Task InsertEmpPostAsync(EmpPost empPost);
        Task UpdateEmpPostAsync(int postId, string empId, EmpPost empPost);
        Task DeleteEmpPostAsync(int postId, string empId);
        Task Add(int postId);
        Task Delete(int postId);
        Task<List<Employee>> GetAllEmpIdsAsync();
        Task<List<int>> GetAllPostIdsAsync();
    }
}
