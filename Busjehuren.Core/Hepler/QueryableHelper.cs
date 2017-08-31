using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Busjehuren.Core.Hepler
{
  public static class QueryableHelper
    {
      public static Expression<Func<TItem, bool>> PropertyEquals<TItem, TValue>(PropertyInfo property, TValue value)
      {
          var param = Expression.Parameter(typeof(TItem));
          var body = Expression.Equal(Expression.Property(param, property),
              Expression.Constant(value));

          return Expression.Lambda<Func<TItem, bool>>(body, param);
      }

      public static IQueryable<TModel> OrderBy<TModel>(this IQueryable<TModel> q, string name)
      {
          Type entityType = typeof(TModel);
          PropertyInfo p = entityType.GetProperty(name);
          MethodInfo m = typeof(QueryableHelper).GetMethod("OrderByProperty").MakeGenericMethod(entityType, p.PropertyType);
          return (IQueryable<TModel>)m.Invoke(null, new object[] { q, p });
      }

      public static IQueryable<TModel> OrderByDescending<TModel>(this IQueryable<TModel> q, string name)
      {
          Type entityType = typeof(TModel);
          PropertyInfo p = entityType.GetProperty(name);
          MethodInfo m = typeof(QueryableHelper).GetMethod("OrderByPropertyDescending").MakeGenericMethod(entityType, p.PropertyType);
          return (IQueryable<TModel>)m.Invoke(null, new object[] { q, p });
      }

      public static IQueryable<TModel> OrderByPropertyDescending<TModel, TRet>(IQueryable<TModel> q, PropertyInfo p)
      {
          ParameterExpression pe = Expression.Parameter(typeof(TModel));
          Expression se = Expression.Convert(Expression.Property(pe, p), typeof(object));
          return q.OrderByDescending(Expression.Lambda<Func<TModel, TRet>>(se, pe));
      }

      public static IQueryable<TModel> OrderByProperty<TModel, TRet>(IQueryable<TModel> q, PropertyInfo p)
      {
          ParameterExpression pe = Expression.Parameter(typeof(TModel));
          Expression se = Expression.Convert(Expression.Property(pe, p), typeof(object));
          return q.OrderBy(Expression.Lambda<Func<TModel, TRet>>(se, pe));
      }
    }
}
