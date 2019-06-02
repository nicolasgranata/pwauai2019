using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PwaUai2019.Web.Models;

namespace PwaUai2019.Web.Services
{
    public interface ICursadaService
    {
        /// <summary>
        /// Metodo para crear una cursada
        /// </summary>
        /// <param name="cursada"></param>
        /// <returns>
        /// 0 si la cantidad de alumnos supera la capacidad
        /// 1 si la cursada se crea correctamente
        /// </returns>
        int CreateCursada(Cursada cursada);
    }
}
