using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillLibrary
{
    public interface ISkillRepoAsync
    {
        Task<List<Skill>> GetAllSkillsAsync();
        Task<Skill> GetSkillByIdAsync(string skillId);
    }
}
