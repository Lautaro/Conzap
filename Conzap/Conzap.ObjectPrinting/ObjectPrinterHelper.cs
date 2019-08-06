using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.ObjectPrinting
{
     class Helper
    {
        static internal PropertyInfo GetPropertyFromExpression<T>(Expression<Func<T, object>> GetPropertyLambda)
        {
            MemberExpression Exp = null;

            //this line is necessary, because sometimes the expression comes in as Convert(originalexpression)
            if (GetPropertyLambda.Body is UnaryExpression)
            {
                var UnExp = (UnaryExpression)GetPropertyLambda.Body;
                if (UnExp.Operand is MemberExpression)
                {
                    Exp = (MemberExpression)UnExp.Operand;
                }
                else
                    throw new ArgumentException();
            }
            else if (GetPropertyLambda.Body is MemberExpression)
            {
                Exp = (MemberExpression)GetPropertyLambda.Body;
            }
            else
            {
                throw new ArgumentException();
            }

            return (PropertyInfo)Exp.Member;
        }

        public Type GetListType(PropertyInfo pi)
        {
            Type type = pi.PropertyType;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                Type itemType = type.GetGenericArguments()[0]; // use this...
                return itemType;
            }

            return null;
        }
    }
}
