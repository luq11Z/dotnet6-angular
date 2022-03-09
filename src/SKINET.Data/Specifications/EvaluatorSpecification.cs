using Microsoft.EntityFrameworkCore;
using SKINET.Business.Interfaces;
using SKINET.Business.Models;

namespace SKINET.Data.Specifications
{
    /* This class will take the querys and add the aggragates and criterias based on the entity.*/
    public class EvaluatorSpecification<TEntity> where TEntity : Entity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria); //search by whatever criteria desired
            }

            if (specification.OrderBy != null)
            {
               query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.isPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include)); //add the aggregates (includes) desired

            return query;
        }
    }
}
