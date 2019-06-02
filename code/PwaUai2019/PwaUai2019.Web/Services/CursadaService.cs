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
            if (cursada.AulaId != 0)
            {
                return Validate(cursada);
            }

            return AddCursada(cursada);         
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

        private int AddCursada(Cursada cursada)
        {
            var count = _cursadaRepository.GetAll().Count();

            cursada.Id = count + 1;

            _cursadaRepository.Add(cursada);

            return 1;
        }

        private int Validate(Cursada cursada)
        {
            if (!ValidateCapacity(cursada))
            {
                return 0;
            }

            return ValidateAvailability(cursada, _cursadaRepository.GetAll().ToList());
        }

        private int ValidateAvailability(Cursada cursadaToAdd, List<Cursada> cursadasCurrent)
        {
            var availability = !cursadasCurrent.Any(x => x.Turno == cursadaToAdd.Turno &
                x.Comision.ToUpper() == cursadaToAdd.Comision.ToUpper() && x.Dia == cursadaToAdd.Dia);

            if (availability)
            {
                return AddCursada(cursadaToAdd);
            }
            else
            {
                return 2;
            }
        }

        private bool ValidateCapacity(Cursada cursadaToAdd)
        {
            Aula aula = _aulaRepository.Get(cursadaToAdd.AulaId);

            if (aula.Capacidad < cursadaToAdd.CantidadAlumnos)
            {
                return false;
            }

            return true;
        }
    }
}
