using Vrelk.Libs.DapperExt.Sql;
using System.Collections.Generic;

namespace Vrelk.Libs.DapperExt.Predicate
{
    public interface IPredicate
    {
        string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters, bool isDml = false);
    }
}
