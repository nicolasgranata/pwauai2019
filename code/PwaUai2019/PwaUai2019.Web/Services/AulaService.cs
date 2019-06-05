using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PwaUai2019.Web.Models;
using PwaUai2019.Web.Repositories;

namespace PwaUai2019.Web.Services
{
    public class AulaService : IAulaService
    {
        private readonly CursadaRepository _cursadaRepository;
        private readonly AulaRepository _aulaRepository;

        public AulaService(CursadaRepository cursadaRepository, AulaRepository aulaRepository)
        {
            _cursadaRepository = cursadaRepository;
            _aulaRepository = aulaRepository;
        }

        public void Add(Aula aula)
        {
            var count = _aulaRepository.GetAll().Count();

            aula.Id = count + 1;

            _aulaRepository.Add(aula);
        }

        public bool Delete(long id)
        {
            if (_aulaRepository.Get(id).Cursadas.Any())
            {
                return false;
            }

            _aulaRepository.Delete(id);

            return true;
        }

        public Aula Get(long id)
        {
            return _aulaRepository.Get(id);
        }

        public IEnumerable<Aula> GetAll()
        {
            return _aulaRepository.GetAll();
        }

        public void Update(Aula aula)
        {
            _aulaRepository.Update(aula);
        }
    }
}
