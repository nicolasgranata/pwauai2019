using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PwaUai2019.Web.Models;

namespace PwaUai2019.Web.Services
{
    public interface ICursadaService
    {
        void Add(Cursada cursada);

        void Delete(long id);

        IEnumerable<Cursada>GetAll();

        Cursada Get(long id);

        void AssignAula();

        IEnumerable<Cursada> GetAllWithoutAula();

    }
}
