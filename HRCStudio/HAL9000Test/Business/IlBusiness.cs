using System;
using HRC.Library.DatabaseObject.DatabaseSchema.Operations;

namespace HAL9000Test
{
    class IlBusiness : IIlBusiness
    {
        #region IIlBusiness Members

        public void Insert(Entities.Il il)
        {
            DbObjectOperationManager.Instance.Insert(il);
        }

        public void Update(Entities.Il il)
        {
            DbObjectOperationManager.Instance.Update(il);
        }

        public void Delete(Entities.Il il)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IIlBusiness Members


        public void DoSomething(Entities.Il il)
        {
            Insert(il);

            Random r = new Random();
            il.Ad = "changed #" + r.Next(10, 100); ;

            HRC.Library.ContextFoundation.ProxyHelper<IlBusiness, IIlBusiness>.Instance.AddOrGet().Update(il);
        }

        #endregion
    }
}
