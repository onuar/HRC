namespace HRC.Library.DatabaseObject.EntityLibrary.EntityBase
{
    public class EntityColumn
    {
        public object CurrentValue { get; set; }
        public object OldValue { get; set; }
        public bool IsChanged { get; set; }
    }
}