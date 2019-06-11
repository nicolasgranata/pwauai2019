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
        public int MaxID()
        {
            var queryResults = _graphClient.Cypher
                .Match("(aula:Aula)")
                .Return(aula => aula.As<Aula>())
                .OrderByDescending("aula.Id")
                .Results.FirstOrDefault();

            return int.Parse(queryResults.Id.ToString());
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
            var queryResults = _graphClient.Cypher
                .Match("(aula:Aula)")
                .OptionalMatch("(aula:Aula)-[CURSADA]-(cursada:Cursada)")
                 .Return((aula, cursada) => new
                 {
                     Cursadas = cursada.CollectAs<Cursada>(),
                     Aula = aula.CollectAs<Aula>()
                 })
                .Results.FirstOrDefault();

            List<Aula> results = new List<Aula>();

            foreach (var res in queryResults.Aula)
            {
                res.Cursadas = queryResults.Cursadas.Where(x => x.AulaId == res.Id);

                if (!results.Any(x => x.Id == res.Id))
                {
                    results.Add(res);
                }
             }
            
            return results;
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
            result.Cursadas = queryResults.Cursadas.ToList();

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
