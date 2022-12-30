using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillLibrary
{
    public class SkillRepoAsync : ISkillRepoAsync
    {
        SkillDBEntities ent = new SkillDBEntities();
        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            List<Skill> skills = await ent.Skills.ToListAsync();
            return skills;
        }

        public async Task<Skill> GetSkillByIdAsync(string skillId)
        {
            try
            {
                Skill skill = await (from s in ent.Skills where s.SkillId == skillId select s).FirstAsync();
                return skill;
            }
            catch (Exception)
            {
                throw new Exception("No such Skill Id");
            }
        }
    }
}
