﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using OW.Experts.Domain.Infrastructure;
using OW.Experts.Domain.Infrastructure.Fetching;
using OW.Experts.Domain.Infrastructure.Query;
using OW.Experts.Domain.Infrastructure.Repository;
using OW.Experts.Domain.NHibernate;

namespace OW.Experts.IntergrationTests
{
    public class DropCreateTestFixture
    {
        protected ILinqProvider LinqProvider { get; private set; }

        protected IUnitOfWorkFactory UnitOfWorkFactory { get; private set; }

        private ISessionFactory SessionFactory { get; set; }

        [OneTimeSetUp]
        public void RegisterDependencies()
        {
            FetchableQueryableFactory.Current = new NHFetchableQueryableFactory();
        }

        public void DropCreate()
        {
            SessionFactory = Fluently.Configure().Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(
                            @"Data Source=(localdb)\ProjectsV13;Initial Catalog=TestOW;Integrated Security=True;")
                        .ShowSql)
                .CurrentSessionContext("thread_static")
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHUnitOfWork>())
                .ExposeConfiguration(
                    cfg =>
                    {
                        // logging in output window of test
                        cfg.SetInterceptor(new SqlStatementInterceptor());

                        var schema = new SchemaExport(cfg);
                        schema.Drop(false, true);
                        schema.Create(false, true);
                    })
                .BuildSessionFactory();

            UnitOfWorkFactory = new NHUnitOfWorkFactory(SessionFactory);
            LinqProvider = new NHLinqProvider(SessionFactory);
        }

        protected IRepository<T> GetRepository<T>()
            where T : DomainObject
        {
            return new NHRepository<T>(SessionFactory);
        }
    }
}