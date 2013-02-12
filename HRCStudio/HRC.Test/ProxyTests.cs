using HRC.Library.ContextFoundation;
using HRC.Test.DumbObjects;
using NUnit.Framework;

namespace HRC.Test
{
    [TestFixture]
    public class ProxyTests
    {
        [Test]
        public void Can_Generate_Proxy()
        {
            var ihrcTestObject = ProxyHelper<IHrcTestObject, HrcTestObject>.Instance.AddOrGet();
            var result = ihrcTestObject.ShoutSomething();
            Assert.AreEqual(result, "SOMETHING!!!");
        }
    }
}
