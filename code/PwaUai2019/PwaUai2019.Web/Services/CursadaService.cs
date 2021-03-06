﻿using System;
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

        public void Add(Cursada cursada)
        {
            var count = _cursadaRepository.MaxID();

            cursada.Id = count + 1;

            _cursadaRepository.Add(cursada);
        }

        public void Delete(long id)
        {
            _cursadaRepository.Delete(id);
        }

        public Cursada Get(long id)
        {
            return _cursadaRepository.Get(id);
        }

        public IEnumerable<Cursada> GetAll()
        {
            return _cursadaRepository.GetAll();
        }

        public void AssignAula()
        {
            var cursadas = _cursadaRepository.GetAll();

            //Limpio todas las aulas asignadas
            foreach (var c in cursadas)
            {
                _cursadaRepository.DeleteRelationship(c);
                c.AulaId = 0;
                c.Aula = null;
                Update(c);
            }

            cursadas = cursadas.OrderByDescending(x => x.CantidadAlumnos);
            //Se asignan las Aulas
            foreach (var c in cursadas)
            {
                var aula = GetAvailableAula(c);

                if (aula != null)
                {
                    c.AulaId = aula.Id;
                    _cursadaRepository.UpdateRelationship(c);
                }
            }
        }

        private Aula GetAvailableAula(Cursada cursadaToAdd)
        {
           var aulas = _aulaRepository.GetAll();

            var aulasTemp = aulas.Where(x => x.Capacidad >= cursadaToAdd.CantidadAlumnos).OrderBy(x => x.Capacidad).ToList();

            Aula aula = null;
            //Se verifica que aula esta disponible, si tienen o no asignada otra cursada para ese turno.
            foreach (var au in aulasTemp)
            {
                if (au.Cursadas.Any())
                {
                    bool Ocupada = false;
                    foreach (var cu in au.Cursadas.ToList())
                    {
                        if (cu.Turno == cursadaToAdd.Turno && cu.Dia == cursadaToAdd.Dia) Ocupada = true;
                    }
                    if (!Ocupada)
                    {
                        aula = au;
                        break;
                    }
                }
                else
                {
                    aula = au;
                    break;
                }
            }
            return aula;
        }

        public IEnumerable<Cursada> GetAllWithoutAula()
        {
            return _cursadaRepository.GetAll().Where(x => x.AulaId == 0);
        }

        public void Update(Cursada cursada)
        {
            _cursadaRepository.Update(cursada);
        }
    }
}
