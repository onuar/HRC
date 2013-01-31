using System;
using HRC.Library.EntityLibrary.EntityBase;
using HRC.Foundation.ConvertionLibrary;

namespace Entities
{
    public class Il : BaseEntity
    {
        public Il()
            :
            base("Il")
        {

        }
        public int Id
        {
            get
            {
                return GetValue<int>("Id");
            }
            set
            {
                SetValue("Id", value);

            }
        }
        public string Ad
        {
            get
            {
                return GetValue<string>("Ad");
            }
            set
            {
                SetValue("Ad", value);

            }
        }
        public Boolean Aktif
        {
            get
            {
                return GetValue<Boolean>("Aktif");
            }
            set
            {
                SetValue("Aktif", value);

            }
        }
    }
}