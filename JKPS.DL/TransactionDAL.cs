using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace JKPS.DL
{
    public class TransactionDAL : System.Data.Common.DbTransaction//IDbTransaction
    {
        public IDbConnection Connection { get { return null; } }
        public override IsolationLevel IsolationLevel { get { return IsolationLevel.ReadUncommitted; } }
        public override void Commit() { }
        protected override System.Data.Common.DbConnection DbConnection
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
        public override void Rollback() { }

        //public void Dispose() { }
    }
}
