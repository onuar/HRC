namespace HRC.Test.DumbObjects
{
    public class HrcTestObject : IHrcTestObject
    {
        private readonly IHrcInjectorTestObject _injectorTestObject;

        public HrcTestObject()
            : this(new HrcInjectorTestObject2())
        { }

        public HrcTestObject(IHrcInjectorTestObject injectorTestObject)
        {
            _injectorTestObject = injectorTestObject;
        }

        public string ShoutName()
        {
            return _injectorTestObject.Name + "!!!";
        }

        public string ShoutSomething()
        {
            return "SOMETHING!!!";
        }
    }
}