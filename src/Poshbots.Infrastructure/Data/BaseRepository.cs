using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poshbots.Infrastructure.Data
{
    public abstract class BaseRepository
    {
        protected IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public IDbTransaction BeginTransaction()
        {
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }
            return db.BeginTransaction();
        }

        public void CommitTransaction(IDbTransaction transaction)
        {
            transaction.Commit();
        }

        public void RollbackTransaction(IDbTransaction transaction)
        {
            transaction.Rollback();
        }
    }
}
