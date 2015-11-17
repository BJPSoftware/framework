using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate;

namespace BJP.Framework.Repository
{
    public class FluentNhibernateHelper
    {
        public static readonly ISessionFactory SessionFactory;

        public FluentNhibernateHelper()
        {
            var configuration = CreateConfiguration();
        }

        private static Configuration CreateConfiguration()
        {
            Configuration cfg = FluentNHibernate.Cfg.Fluently.Configure()
                        .Database( FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008 )
                        .BuildConfiguration();
            throw new NotImplementedException();
        }
    }
}
