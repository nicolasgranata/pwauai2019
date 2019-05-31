using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public IEnumerable<Aula> GetAll()
        {
            return _graphClient.Cypher
                 .Match("(aula:Aula)")
                 .Return(aula => aula.As<Aula>())
                 .Results;
        }
    }
}
