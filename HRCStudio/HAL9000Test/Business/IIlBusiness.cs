using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HRC.Library.ContextFoundation.Aspects.BusinessAspects;

namespace HAL9000Test
{
    public interface IIlBusiness
    {
        void Insert(Entities.Il il);
        [ChangedEntityValueLog]
        void Update(Entities.Il il);
        void Delete(Entities.Il il);
        void DoSomething(Entities.Il il);
    }
}
