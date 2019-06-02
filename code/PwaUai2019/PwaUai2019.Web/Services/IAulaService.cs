using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PwaUai2019.Web.Models;

namespace PwaUai2019.Web.Services
{
    public interface IAulaService
    {
        void Add(Aula aula);

        void Delete(long id);

        IEnumerable<Aula> GetAll();

        Aula Get(long id);

        void Update(Aula aula);
    }
}
