using Autofac;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityLab.DataTable;

namespace UnityLab.App
{
    public class GameEntry
    {
        private static GameEntry ins = new GameEntry();
        public static GameEntry Instance { get { return ins; } }
        public IContainer container;

        public IContainer Register() {
            var builder = new Autofac.ContainerBuilder();
            builder.Register<IDbConnectionFactory>(c =>
    new OrmLiteConnectionFactory("Data Source=sqllite.db;Version=3;UseUTF16Encoding=True;", SqliteDialect.Provider)).AsImplementedInterfaces()
    .SingleInstance()
    .PropertiesAutowired( PropertyWiringOptions.AllowCircularDependencies); //InMemory Sqlite DB
            builder.RegisterGeneric(typeof(SqliteDataTableRepository<>)).As(typeof(IDataTableRepository<>))
                .SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterGeneric(typeof(DataJsonRepository<>)).As(typeof(IDataJsonRepository<>))
                .SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            return container = builder.Build();
        }
        public T GetService<T>() {
            return container.Resolve<T>();
        }

    }
}
