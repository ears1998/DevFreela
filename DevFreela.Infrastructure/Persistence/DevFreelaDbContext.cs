using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {

        public DevFreelaDbContext()
        {
            Projects = new List<Project>()
            {
                new Project("Meu projeto ASPNET Core 1", "Minha descrição de projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core 2", "Minha descrição de projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core 3", "Minha descrição de projeto 3", 1, 1, 30000)
            };

            Users = new List<User>()
            {
                new User("Eduardo Sampaio", "eduardo.sampaio@hotmail.com", new DateTime(1998,04,16)),
                new User("Rogerio Sampaio", "rogerio.sampaio@hotmail.com", new DateTime(1998,05,16)),
                new User("Paulo Sampaio", "paulo.sampaio@hotmail.com", new DateTime(1998,06,16))
            };

            Skills = new List<Skill>()
            {
                new Skill(".NET CORE"),
                new Skill("C#"),
                new Skill("SQL")
            };

            ProjectComments = new List<ProjectComment>()
            {
                new ProjectComment("Comentario projeto 1", 1, 1),
                new ProjectComment("Comentario projeto 2", 2, 2),
                new ProjectComment("Comentario projeto 3", 3, 3)
            };


        }

        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }

        public List<ProjectComment> ProjectComments { get; set; }
    }
}
