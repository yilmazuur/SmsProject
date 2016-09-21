using System.Runtime.CompilerServices;
using NHibernate;
using SmsProject.App.Data.DAL;
using SmsProject.App.Model.Map.LogModelMap;

namespace SmsProject.App.Data.Persistence
{
    /// <summary>
    /// ServiceCallMap class in PersistenceSingletonFactory<ServiceCallMap> is to show PersistenceFactory where(which assembly) to look for other entity models.
    /// Any public class from this assembly would be OK.
    /// </summary>
    public sealed class PersistenceFactory
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ISessionFactory Create()
        {
            return PersistenceSingletonFactory<ServiceCallMap>.Instance.SessionFactory;
        }
    }
}
