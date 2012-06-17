using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject.Syntax;
using Ninject;

namespace Bookit.UI.Mvc4.Infrastructure
{
    public class NinjectResolver : IDependencyResolver
    {
        private readonly StandardKernel _ker;

        public NinjectResolver(StandardKernel ker)
        {
            _ker = ker;
        }

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            return _ker.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _ker.GetAll(serviceType);
        }

        #endregion
    }
}