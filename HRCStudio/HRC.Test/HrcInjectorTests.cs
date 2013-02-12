using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRC.Foundation.DependencyInjection;
using NUnit.Framework;

namespace HRC.Test
{
    [TestFixture]
    public class HrcInjectorTests
    {
        [Test]
        public void Can_Single_Register_For_More_Than_One_Type()
        {
            var injector = new HrcInjector();
            const string instance = "HRC Testing";
            injector.Bind(instance);

            var result = injector.Resolve<string>();
            Assert.AreEqual(result, instance);

            const string otherInstance = "HRC Testing...";
            injector.Bind(otherInstance);

            var otherResult = injector.Resolve<string>();
            Assert.AreEqual(otherResult, otherInstance);
            Assert.AreNotEqual(otherResult, instance);
        }

        [Test]
        public void Can_Resolve_By_Type()
        {
            var injector = new HrcInjector();
            injector.Bind<int>(666);

            var result = injector.Resolve(typeof(int));
            Assert.AreEqual(result, 666);
        }

        [Test]
        public void Can_Register_Concrete_Object_For_Interface()
        {
            var injector = new HrcInjector();
            injector.Bind<IHrcInjectorTestObject, HrcInjectorTestObject2>();

            var result = injector.Resolve<IHrcInjectorTestObject>();
            Assert.AreEqual(typeof(HrcInjectorTestObject2), result.GetType());
            Assert.IsInstanceOf<IHrcInjectorTestObject>(result);
        }

        [Test]
        public void Can_Do_Constructor_Injection()
        {
            var injector = new HrcInjector();
            injector.Bind<IHrcTestObject, HrcTestObject>();

            var result = injector.Resolve(typeof(IHrcTestObject));//injector.Resolve<IHrcTestObject>();
            Assert.IsAssignableFrom<IHrcTestObject>(result);
        }
    }

    public interface IHrcInjectorTestObject
    {
        string Name { get; set; }
    }

    public class HrcInjectorTestObject : IHrcInjectorTestObject
    {
        public string Name { get; set; }
    }

    public class HrcInjectorTestObject2 : IHrcInjectorTestObject
    {
        public string Name { get; set; }
    }

    public interface IHrcTestObject
    {
    }

    public class HrcTestObject : IHrcTestObject
    {
        private readonly IHrcInjectorTestObject _injectorTestObject;

        public HrcTestObject(IHrcInjectorTestObject injectorTestObject)
        {
            _injectorTestObject = injectorTestObject;
        }

        public string ShoutName()
        {
            return _injectorTestObject.Name;
        }
    }
}