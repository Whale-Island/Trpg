using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace WhaleIsland.Trpg.GM.Common.Reflect
{
    /// <summary>
    ///
    /// </summary>
    internal static class FastActivator
    {
        private static Dictionary<Type, Func<object[], object>> factoryCache = new Dictionary<Type, Func<object[], object>>();

        /// <summary>
        /// Creates an instance of the specified type using a generated factory to avoid using Reflection.
        /// </summary>
        /// <param Name="type">The type to be created.</param>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns>The newly created instance.</returns>
        public static object Create(Type type, params object[] args)
        {
            Func<object[], object> f;

            if (!factoryCache.TryGetValue(type, out f))
                lock (factoryCache)
                    if (!factoryCache.TryGetValue(type, out f))
                    {
                        Type[] typeArray = args.Select(obj => obj.GetType()).ToArray();
                        factoryCache[type] = f = BuildDeletgateObj(type, typeArray);
                    }

            return f(args);
        }

        private static Func<object[], object> BuildDeletgateObj(Type type, Type[] typeList)
        {
            ConstructorInfo constructor = type.GetConstructor(typeList);
            ParameterExpression paramExp = Expression.Parameter(typeof(object[]), "args_");
            Expression[] expList = GetExpressionArray(typeList, paramExp);
            NewExpression newExp = Expression.New(constructor, expList);
            Expression<Func<object[], object>> expObj = Expression.Lambda<Func<object[], object>>(newExp, paramExp);
            return expObj.Compile();
        }

        private static Expression[] GetExpressionArray(Type[] typeList, ParameterExpression paramExp)
        {
            List<Expression> expList = new List<Expression>();
            for (int i = 0; i < typeList.Length; i++)
            {
                var paramObj = Expression.ArrayIndex(paramExp, Expression.Constant(i));
                var expObj = Expression.Convert(paramObj, typeList[i]);
                expList.Add(expObj);
            }

            return expList.ToArray();
        }
    }
}
