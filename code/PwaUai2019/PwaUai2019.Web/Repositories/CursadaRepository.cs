using System.Collections.Generic;
using System.Linq;
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
                .Match("(aula:Aula)")
                .Where((Aula aula) => aula.Id == entity.Aula)
                .Create("(aula)-[:CURSADA]->(cursada:Cursada {newCursada})")
                .WithParam("newCursada", entity)
                .ExecuteWithoutResults();
        }

        public void Delete(long id)
        {
            _graphClient.Cypher
                .Match("(cursada:Cursada)")
                .Where((Cursada cursada) => cursada.Id == id)
                .Delete("cursada")
                .ExecuteWithoutResults();
        }

        public IEnumerable<Cursada> GetAll()
        {
            return _graphClient.Cypher
                 .Match("(cursada:Cursada)")
                 .Return(cursada => cursada.As<Cursada>())
                 .Results;
        }

        public Cursada Get(long id)
        {
            return _graphClient.Cypher
                .Match("(cursada:Cursada)")
                .Where((Cursada cursada) => cursada.Id == id)
                .Return(cursada => cursada.As<Cursada>())
                .Results.FirstOrDefault();
        }
    }
}
