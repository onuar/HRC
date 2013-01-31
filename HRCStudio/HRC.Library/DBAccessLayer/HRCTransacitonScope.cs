using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRC.Library.DBAccessLayer
{
    public class HRCTransacitonScope : IDisposable
    {
        DbManager _db;

        public HRCTransacitonScope(DbManager db)
        {
            _db = db;
            _db.BeginTransaction();
        }

        bool _completed;
        public void Complete()
        {
            _db.CompleteTransaction();
            _completed = true;
        }

        public void Dispose()
        {
            if (!_completed)
                _db.AbortTransaction();
        }
    }
}
