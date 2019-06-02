using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PwaUai2019.Web.Models;
using PwaUai2019.Web.Repositories;

namespace PwaUai2019.Web.Services
{
    public class CursadaService : ICursadaService
    {
        private readonly CursadaRepository _cursadaRepository;
        private readonly AulaRepository _aulaRepository;

        public CursadaService(CursadaRepository cursadaRepository, AulaRepository aulaRepository)
        {
            _cursadaRepository = cursadaRepository;
            _aulaRepository = aulaRepository;
        }

        public int Add(Cursada cursada)
        {
            var aula = _aulaRepository.Get(cursada.Aula);
            
            if(aula.Capacidad < cursada.CantidadAlumnos)
            {
                return 0;
            }

            if (ValidateAvailability(cursada, aula.Cursadas.ToList()))
            {
                var count = _cursadaRepository.GetAll().Count();

                cursada.Id = count + 1;

                _cursadaRepository.Add(cursada);

                return 1;
            }

            return 2;
        }

        public void Delete(long id)
        {
            _cursadaRepository.Delete(id);
        }

        public Cursada Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cursada> GetAll()
        {
            return _cursadaRepository.GetAll();
        }

        private bool ValidateAvailability(Cursada cursadaToAdd, List<Cursada> cursadasCurrent)
        {
            return !cursadasCurrent.Any(x => x.Turno == cursadaToAdd.Turno & 
                x.Comision.ToUpper() == cursadaToAdd.Comision.ToUpper() && x.Dia == cursadaToAdd.Dia);
        }


    }
}
