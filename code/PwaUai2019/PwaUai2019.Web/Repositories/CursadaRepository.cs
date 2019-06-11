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
                .OptionalMatch("(aula:Aula)")
                .Where((Aula aula) => aula.Id == entity.AulaId)
                .Create("(cursada:Cursada {newCursada})")
                .WithParam("newCursada", entity)
                .ExecuteWithoutResults();
        }

        public void UpdateRelationship(Cursada entity)
        {
            _graphClient.Cypher
                .Match("(aula:Aula),(cursada: Cursada)")
                .Where((Aula aula) => aula.Id == entity.AulaId)
                .AndWhere((Cursada cursada) => cursada.Id == entity.Id)
                .CreateUnique("(aula)-[:CURSADA]->(cursada)")
                .ExecuteWithoutResults();

            Update(entity);
        }

        public void DeleteRelationship(Cursada entity)
        {
            _graphClient.Cypher
                .Match("(cursada: Cursada)")
                .Where((Cursada cursada) => cursada.Id == entity.Id)
                .OptionalMatch("(cursada:Cursada)<-[r]-()")
                .Delete("r")
                .ExecuteWithoutResults();
        }

        public void Delete(long id)
        {
            _graphClient.Cypher
                .Match("(cursada:Cursada)")
                .Where((Cursada cursada) => cursada.Id == id)
                .OptionalMatch("(cursada:Cursada)<-[r]-()")
                .Where((Cursada cursada) => cursada.Id == id)
                .Delete("r, cursada")
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

        public int MaxID()
        {
            var queryResults = _graphClient.Cypher
                .Match("(cursada:Cursada)")
                .Return(cursada => cursada.As<Cursada>())
                .OrderByDescending("cursada.Id")
                .Results.FirstOrDefault();

            return int.Parse(queryResults.Id.ToString());
        }

        public Cursada Get(long id)
        {
            return _graphClient.Cypher
                .Match("(cursada:Cursada)")
                .Where((Cursada cursada) => cursada.Id == id)
                .Return(cursada => cursada.As<Cursada>())
                .Results.FirstOrDefault();
        }

        public void Update(Cursada entity)
        {
            _graphClient.Cypher
                .Match("(cursada:Cursada)")
                .Where((Cursada cursada) => cursada.Id == entity.Id)
                .Set("cursada = {updateCursada}")
                .WithParam("updateCursada", entity)
                .ExecuteWithoutResults();
        }

    }
}
