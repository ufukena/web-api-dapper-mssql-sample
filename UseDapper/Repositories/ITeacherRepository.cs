using System.Collections.Generic;
using System.Threading.Tasks;
using UseDapper.Models;

namespace UseDapper.Repositories
{
    public interface ITeacherRepository
    {

        public Task<IEnumerable<Teacher>> Get();

        public Task<Teacher> Get(int id);

        public Task<Teacher> Create(Teacher teacher);

        public Task Update(Teacher teacher);

        public Task Delete(int id);

    }
}
