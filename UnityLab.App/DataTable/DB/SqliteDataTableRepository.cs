using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLab.DataTable
{
    public class SqliteDataTableRepository<T> : IDataTableRepository<T> where T : BaseEntity, new()
    {
        private IDbConnectionFactory dbConnection;

        public SqliteDataTableRepository(IDbConnectionFactory _dbConnection)
        {

            this.dbConnection = _dbConnection;
            using (var db = dbConnection.Open())
            {
                db.CreateTableIfNotExists<T>();
            }
        }

        public SqliteDataTableRepository()
        {
            using (var db = dbConnection.Open())
            {
                db.CreateTableIfNotExists<T>();
            }
        }
        
        public List<T> Load()
        {
            using (var db = dbConnection.Open())
            {
                return db.Select<T>();
            }
        }

        public int Update(params T[] t)
        {
            using (var db = dbConnection.Open())
            {
                return db.Update<T>(t);
            }
        }
        public void Insert(params T[] t)
        {
            using (var db = dbConnection.Open())
            {
                db.Insert<T>(t);
            }
        }

        public void Delete(params T[] t)
        {
            using (var db = dbConnection.Open())
            {
                db.Delete<T>(t);
            }
        }

        //public void Init() {
        //    IDbConnectionFactory dbfactory = new OrmLiteConnectionFactory("sqlite.db", SqliteDialect.Instance);
        //}


    }
}
