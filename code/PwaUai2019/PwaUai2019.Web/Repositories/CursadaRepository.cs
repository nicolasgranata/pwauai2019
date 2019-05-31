using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;
using PwaUai2019.Web.Models;

namespace PwaUai2019.Web.Repositories
{
    public class CursadaRepository
    {
        private readonly IGraphClient _graphClient;

        public CursadaRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public void Add(Cursada entity)
        {
            _graphClient.Cypher
                .Create("(cursada:Cursada {newCursada})")
                .WithParam("newCursada", entity)
                .ExecuteWithoutResults();
        }

        public IEnumerable<Cursada> GetAll()
        {
            return _graphClient.Cypher
                 .Match("(cursada:Cursada)")
                 .Return(aula => aula.As<Cursada>())
                 .Results;
        }
    }
}
