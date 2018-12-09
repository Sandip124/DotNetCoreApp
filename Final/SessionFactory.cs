/*
 * Created By: Niroj Dahal
 * On: 1/9/2018
 * to manage nHibernate sessions
 * Modification log:
 * Modiied On:
 * Modified By:
 * Modification Reason:
 */
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;

namespace Final.Session_Factory
{
    public class SessionFactory
    {
        private static ISessionFactory iSessionFactory;
        protected static ISession session;
        private static object syncRoot = new Object();

        private static ISessionFactory GetSessionFactory()
        {
            if (iSessionFactory == null)
            {
                lock (syncRoot)
                {
                    if (iSessionFactory == null)
                    {
                        buildSessionFactory();
                    }
                }
            }
            return iSessionFactory;
        }

        private static void buildSessionFactory()
        {
            Configuration configuration = new Configuration().Configure("hibernate.cfg.xml");
            iSessionFactory = configuration.BuildSessionFactory();
          // new SchemaExport(configuration).Execute(true, true, false);
        }

        public static ISession OpenSession
        {
            get
            {
                iSessionFactory = GetSessionFactory();
                if (!CurrentSessionContext.HasBind(iSessionFactory))
                {
                    session = iSessionFactory.OpenSession();
                    CurrentSessionContext.Bind(session);
                }
                
                return session;
            }
        }

        public static void disposeSession()
        {
            ISession currentSession = CurrentSessionContext.Unbind(iSessionFactory);
            if (currentSession != null)
            {
                currentSession.Flush();
                currentSession.Dispose();
            }
        }
        public static void rollbackTransaction()
        {
            ISession currentSession = CurrentSessionContext.Unbind(iSessionFactory);
            if (currentSession != null)
            {
                currentSession.Dispose();
            }
        }
    }
}
