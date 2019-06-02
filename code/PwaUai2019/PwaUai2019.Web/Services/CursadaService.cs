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

        public int CreateCursada(Cursada cursada)
        {
            var aula = _aulaRepository.Get(cursada.Aula);
            
            if(aula.Capacidad < cursada.CantidadAlumnos)
            {
                return 0;
            }

            var count = _cursadaRepository.GetAll().Count();

            cursada.Id = count + 1;

            _cursadaRepository.Add(cursada);

            return 1;
        }
    }
}
