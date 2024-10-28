using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
        protected BaseSpecification():this(null) { }

        public Expression<Func<T, bool>>? Criteria => criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public bool IsDistinct { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginationEnabled { get; private set; }

        protected void AddOrderBy(Expression<Func<T, Object>> expressionOrderBy)
        { 
            OrderBy = expressionOrderBy;
        }
        protected void AddOrderByDescending(Expression<Func<T, Object>> expressionOrderByDescending)
        { 
            OrderByDescending = expressionOrderByDescending;
        }

        protected void AddIsDistinct()
        { 
            IsDistinct = true;
        }

        protected void AddPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginationEnabled = true;
        }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if (criteria != null)
            { 
                query = query.Where(criteria);
            }
            return query;
        }
    }

    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) :
        BaseSpecification<T>(criteria), ISpecification<T, TResult>
    {
        protected BaseSpecification() : this(null!) { }
        public Expression<Func<T, TResult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        { 
            Select = selectExpression;
        }

    }
}
