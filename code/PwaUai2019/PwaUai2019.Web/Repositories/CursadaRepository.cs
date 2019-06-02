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
                .Where((Aula aula) => aula.Id == entity.AulaId)
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
           var queryResults = _graphClient.Cypher
                 .Match("(cursada:Cursada)")
                 .OptionalMatch("(cursada:Cursada)-[CURSADA]-(aula:Aula)")
                 .Return((cursada, aula) => new
                  {
                      Cursadas = cursada.CollectAs<Cursada>(),
                      Aula = aula.CollectAs<Aula>()
                 })
                .Results.FirstOrDefault();

            List<Cursada> results = new List<Cursada>();

            foreach(var res in queryResults.Cursadas)
            {
                res.Aula = queryResults.Aula.Where(x => x.Id == res.AulaId).FirstOrDefault();

                results.Add(res);
            }
            
            return results;
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
