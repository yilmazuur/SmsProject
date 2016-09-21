using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsProject.App.Data.DAL
{
    /// <summary>
    /// dynamic type will not produce any error on compile time. that provides flexibility for query results.
    /// Joined queries can return custom results. 
    /// Instead of casting every query result to a class/instance, couple of instances can be populated by single query in operation layer.
    /// </summary>
    public static class NHibernateExtensions
    {
        public static IList<dynamic> DynamicList(this IQuery query)
        {
            return query.SetResultTransformer(NHibernateTransformers.ExpandoObject)
                        .List<dynamic>();
        }
    }

    #region NHibernateExtension Elements

    /// <summary>
    /// Singleton Transformer
    /// </summary>
    public class NHibernateTransformers
    {
        private static IResultTransformer _expandoObject;

        public static IResultTransformer ExpandoObject
        {
            get 
            {
                if (_expandoObject == null)
                {
                    _expandoObject = new ExpandoObjectResultSetTransformer();
                }
                return _expandoObject;
            }
        }
    }

    internal class ExpandoObjectResultSetTransformer : IResultTransformer
    {
        public IList TransformList(IList collection)
        {
            return collection;
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            var expando = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)expando;
            for (int i = 0; i < tuple.Length; i++)
            {
                string alias = aliases[i];
                if (alias != null)
                {
                    dictionary[alias] = tuple[i];
                }
            }
            return expando;
        }
    }

    #endregion

  
}
