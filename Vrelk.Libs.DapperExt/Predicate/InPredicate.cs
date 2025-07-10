﻿using Vrelk.Libs.DapperExt.Sql;
using System.Collections;
using System.Collections.Generic;

namespace Vrelk.Libs.DapperExt.Predicate
{
    public interface IInPredicate : IBasePredicate
    {
        ICollection Collection { get; }
        bool Not { get; set; }
    }

    public class InPredicate<T> : BasePredicate, IInPredicate
    {
        public ICollection Collection { get; }
        public bool Not { get; set; }

        public InPredicate(ICollection collection, string propertyName, bool isNot = false)
        {
            PropertyName = propertyName;
            Collection = collection;
            Not = isNot;
        }

        public override string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters, bool isDml = false)
        {
            var columnName = GetColumnName(typeof(T), sqlGenerator, PropertyName);

            var @params = new List<string>();

            foreach (var item in Collection)
            {
                var p = ReflectionHelper.GetParameter(typeof(T), sqlGenerator, PropertyName, item);
                @params.Add(parameters.SetParameterName(p, sqlGenerator.Configuration.Dialect.ParameterPrefix));
            }

            var commaDelimited = string.Join(", ", @params);

            return $"({columnName.Trim()} {GetIsNotStatement(Not)}IN ({commaDelimited}))";
        }

        private static string GetIsNotStatement(bool not)
        {
            return not ? "NOT " : string.Empty;
        }
    }
}
