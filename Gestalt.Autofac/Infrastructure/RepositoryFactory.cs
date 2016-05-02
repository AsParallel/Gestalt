using Autofac;
using Gestalt.Abstractions;
using Gestalt.Abstractions.Infrastructure;
using System;

namespace Gestalt.Autofac.Infrastructure
{
    /// <summary>
    /// This class is responsible for providing repository resolution from resolved types
    /// </summary>
    public class AutofacRepositoryFactory : IRepositoryFactory
    {
        private IContainer container;
        public AutofacRepositoryFactory(IContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// This requires you to be aware of the value of T at design time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IGestaltConfigurationRepository<T> GetRepository<T>() where T: IGestaltConfigurationSchema
        {
            return container.Resolve<IGestaltConfigurationRepository<T>>();
        }

        ///// <summary>
        ///// This will require a cast on the other side
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public virtual object GetRepository(Type t)
        //{
        //    return container.Resolve(typeof(IGestaltConfigurationRepository<>).MakeGenericType(t));
        //}
    }
}
