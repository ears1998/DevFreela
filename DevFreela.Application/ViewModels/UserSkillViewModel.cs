namespace DevFreela.Application.ViewModels
{
    public class UserSkillViewModel
    {
        public UserSkillViewModel(int userId, int skillId)
        {
            UserId = userId;
            SkillId = skillId;
        }

        public int UserId { get; private set; }
        public int SkillId { get; private set; }
    }
}
