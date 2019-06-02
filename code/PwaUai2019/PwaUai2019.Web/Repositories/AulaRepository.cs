using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using PwaUai2019.Web.Models;

namespace PwaUai2019.Web.Repositories
{
    public class AulaRepository
    {
        private readonly IGraphClient _graphClient;

        public AulaRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public void Add(Aula entity)
        {
            _graphClient.Cypher
                .Create("(aula:Aula {newAula})")
                .WithParam("newAula", entity)
                .ExecuteWithoutResults();
        }

        public void Delete(long id)
        {
            _graphClient.Cypher
                .Match("(aula:Aula)")
                .Where((Aula aula) => aula.Id == id)
                .Delete("aula")
                .ExecuteWithoutResults();
        }

        public IEnumerable<Aula> GetAll()
        {
            return _graphClient.Cypher
                .Match("(aula:Aula)")
                .Return(aula => aula.As<Aula>())
                .Results;
        }

        public Aula Get(long id)
        {
            var queryResults = _graphClient.Cypher
                .OptionalMatch("(aula:Aula)")
                .Where((Aula aula) => aula.Id == id)
                .OptionalMatch("(aula:Aula)-[CURSADA]-(cursada:Cursada)")
                .Where((Aula aula) => aula.Id == id)
                .Return((aula, cursada) => new
                {
                    Aula = aula.As<Aula>(),
                    Cursadas = cursada.CollectAs<Cursada>()
                })
                .Results.FirstOrDefault();

            var result = queryResults.Aula;
            result.Cursadas = queryResults.Cursadas;

            return result;
        }

        public void Update(Aula entity)
        {
            _graphClient.Cypher
                .Match("(aula:Aula)")
                .Where((Aula aula) => aula.Id == entity.Id)
                .Set("aula = {updateAula}")
                .WithParam("updateAula", entity)
                .ExecuteWithoutResults();
        }
    }
}
