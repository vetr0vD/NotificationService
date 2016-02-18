using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public sealed class MyExtensions
    {
        public static string GetPropertiesName<T>(Expression<Func<T>> expression)
        {
            return ((MemberExpression)expression.Body).Member.Name;
        }
    }
}
