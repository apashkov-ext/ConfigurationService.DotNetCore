using ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning;
using ConfigurationManagementSystem.Framework.Exceptions;
using Xunit;

namespace ConfigurationManagementSystem.Framework.Tests
{
    public class TypeImplementationProviderTests
    {
        [Fact]
        public void GetImplementation_OnlyBaseClass_ReturnsTheSame()
        {
            var baseType = typeof(BaseClass);
            var typeProv = new TestTypeProvider(new[] { baseType });
            var implProv = new TypeImplementationProvider(typeProv);

            var implementation = implProv.GetImplementation(baseType);

            Assert.Equal(baseType, implementation);
        }

        [Fact]
        public void GetImplementation_OnlyBaseAbstractClass_ThrowsException()
        {
            var baseType = typeof(BaseAbstractClass);
            var typeProv = new TestTypeProvider(new[] { baseType });
            var implProv = new TypeImplementationProvider(typeProv);

            Assert.Throws<ImplementationNotFoundException>(() => implProv.GetImplementation(baseType));
        }

        [Fact]
        public void GetImplementation_BaseAbstractClassWithImpl_ReturnsImpl()
        {
            var baseType = typeof(BaseAbstractClass);
            var typeProv = new TestTypeProvider(new[] { baseType, typeof(ClassImplementsAbstract) });
            var implProv = new TypeImplementationProvider(typeProv);

            var implementation = implProv.GetImplementation(baseType);

            Assert.Equal(typeof(ClassImplementsAbstract), implementation);
        }

        [Fact]
        public void GetImplementation_BaseClassWithAmbiguousImplementation_ThrowsException()
        {
            var baseType = typeof(BaseClassWithAmbigImpl);
            var typeProv = new TestTypeProvider(new[] {
            baseType,
            typeof(ClassImplementedBaseClassWithAmbigImpl),
            typeof(ClassImplementedBaseClassWithAmbigImpl2)
        });
            var implProv = new TypeImplementationProvider(typeProv);

            Assert.Throws<AmbiguousTypeImplementationException>(() => implProv.GetImplementation(baseType));
        }

        [Fact]
        public void GetImplementation_BaseClassWithAmbiguousImplementation2_ThrowsException()
        {
            var baseType = typeof(BaseClassWithAmbigImpl2);
            var typeProv = new TestTypeProvider(new[] {
            baseType,
            typeof(AnotherClassImplementedBaseClassWithAmbigImpl),
            typeof(AnotherClassImplementedBaseClassWithAmbigImpl2),
            typeof(AnotherClassImplementedBaseClassWithAmbigImpl3)
        });
            var implProv = new TypeImplementationProvider(typeProv);

            Assert.Throws<AmbiguousTypeImplementationException>(() => implProv.GetImplementation(baseType));
        }

        [Fact]
        public void GetImplementation_BaseClassWithHierarchy_ReturnsTopHierarchyType()
        {
            var baseType = typeof(BaseClass);
            var typeProv = new TestTypeProvider(new[] {
            baseType,
            typeof(DerivedClass),
            typeof(DerivedClass2),
            typeof(DerivedClass3)
        });
            var implProv = new TypeImplementationProvider(typeProv);

            var implementation = implProv.GetImplementation(baseType);

            Assert.Equal(typeof(DerivedClass3), implementation);
        }

        [Fact]
        public void GetImplementation_OnlyInterface_ThrowsException()
        {
            var baseType = typeof(IBaseType);
            var typeProv = new TestTypeProvider(new[] { baseType });
            var implProv = new TypeImplementationProvider(typeProv);

            Assert.Throws<ImplementationNotFoundException>(() => implProv.GetImplementation(baseType));
        }

        [Fact]
        public void GetImplementation_InterfaceWithDerivedAbstractClass_ThrowsException()
        {
            var baseType = typeof(IBaseType3);
            var typeProv = new TestTypeProvider(new[] { baseType, typeof(AbstractClassImplementsIBaseType3) });
            var implProv = new TypeImplementationProvider(typeProv);

            Assert.Throws<ImplementationNotFoundException>(() => implProv.GetImplementation(baseType));
        }

        [Fact]
        public void GetImplementation_InterfaceWithOneImpl_ReturnsImpl()
        {
            var baseType = typeof(IBaseType2);
            var typeProv = new TestTypeProvider(new[] { baseType, typeof(ClassImplementsIBaseType2) });
            var implProv = new TypeImplementationProvider(typeProv);

            var implementation = implProv.GetImplementation(baseType);

            Assert.Equal(typeof(ClassImplementsIBaseType2), implementation);
        }

        [Fact]
        public void GetImplementation_InterfaceWithHierarchyImpl_ReturnsTopHierarchyImpl()
        {
            var baseType = typeof(IBaseTypeWithHierarchy);
            var typeProv = new TestTypeProvider(new[]
            {
            baseType,
            typeof(ClassImplementsIBaseTypeWithHierarchy),
            typeof(ClassImplementsIBaseTypeWithHierarchy2)
        });
            var implProv = new TypeImplementationProvider(typeProv);

            var implementation = implProv.GetImplementation(baseType);

            Assert.Equal(typeof(ClassImplementsIBaseTypeWithHierarchy2), implementation);
        }

        [Fact]
        public void GetImplementation_InterfaceWithDerivedInterface_ThrowsException()
        {
            var baseType = typeof(IBaseType4);
            var typeProv = new TestTypeProvider(new[] { baseType, typeof(InterfaceImplementsIBaseType4) });
            var implProv = new TypeImplementationProvider(typeProv);

            Assert.Throws<ImplementationNotFoundException>(() => implProv.GetImplementation(baseType));
        }

        [Fact]
        public void GetImplementation_InterfaceWithAmbiguousImplementation_ThrowsException()
        {
            var baseType = typeof(IInterfaceWithAmbigImpl);
            var typeProv = new TestTypeProvider(new[] {
            baseType,
            typeof(ClassImplementsIInterfaceWithAmbigImpl),
            typeof(ClassImplementsIInterfaceWithAmbigImpl2)
        });
            var implProv = new TypeImplementationProvider(typeProv);

            Assert.Throws<AmbiguousTypeImplementationException>(() => implProv.GetImplementation(baseType));
        }

        [Fact]
        public void GetImplementation_InterfaceWithAmbiguousImplementation2_ThrowsException()
        {
            var baseType = typeof(AnotherIInterfaceWithAmbigImpl);
            var typeProv = new TestTypeProvider(new[] {
            baseType,
            typeof(ClassImplementsAnotherIInterfaceWithAmbigImpl),
            typeof(ClassImplementsAnotherIInterfaceWithAmbigImpl2),
            typeof(ClassImplementsAnotherIInterfaceWithAmbigImpl3)
        });
            var implProv = new TypeImplementationProvider(typeProv);

            Assert.Throws<AmbiguousTypeImplementationException>(() => implProv.GetImplementation(baseType));
        }
    }

    /////////////////////////
    //// Types ////////////////
    /////////////////////////////

    public abstract class BaseAbstractClass { }
    public class ClassImplementsAbstract : BaseAbstractClass { }

    //

    public class BaseClass { }
    public class DerivedClass : BaseClass { }
    public class DerivedClass2 : DerivedClass { }
    public class DerivedClass3 : DerivedClass2 { }

    //

    public class BaseClassWithAmbigImpl { }
    public class ClassImplementedBaseClassWithAmbigImpl : BaseClassWithAmbigImpl { }
    public class ClassImplementedBaseClassWithAmbigImpl2 : BaseClassWithAmbigImpl { }

    //

    public class BaseClassWithAmbigImpl2 { }
    public class AnotherClassImplementedBaseClassWithAmbigImpl : BaseClassWithAmbigImpl2 { }
    public class AnotherClassImplementedBaseClassWithAmbigImpl2 : AnotherClassImplementedBaseClassWithAmbigImpl { }
    public class AnotherClassImplementedBaseClassWithAmbigImpl3 : AnotherClassImplementedBaseClassWithAmbigImpl { }

    //

    public interface IBaseType { }

    //

    public interface IBaseType2 { }
    public class ClassImplementsIBaseType2 : IBaseType2 { }

    //

    public interface IBaseType3 { }
    public abstract class AbstractClassImplementsIBaseType3 : IBaseType3 { }

    //

    public interface IBaseType4 { }
    public interface InterfaceImplementsIBaseType4 : IBaseType4 { }

    //

    public interface IBaseTypeWithHierarchy { }
    public class ClassImplementsIBaseTypeWithHierarchy : IBaseTypeWithHierarchy { }
    public class ClassImplementsIBaseTypeWithHierarchy2 : ClassImplementsIBaseTypeWithHierarchy { }

    //

    public interface IInterfaceWithAmbigImpl { }
    public class ClassImplementsIInterfaceWithAmbigImpl : IInterfaceWithAmbigImpl { }
    public class ClassImplementsIInterfaceWithAmbigImpl2 : IInterfaceWithAmbigImpl { }

    //

    public interface AnotherIInterfaceWithAmbigImpl { }
    public class ClassImplementsAnotherIInterfaceWithAmbigImpl : AnotherIInterfaceWithAmbigImpl { }
    public class ClassImplementsAnotherIInterfaceWithAmbigImpl2 : ClassImplementsAnotherIInterfaceWithAmbigImpl { }
    public class ClassImplementsAnotherIInterfaceWithAmbigImpl3 : ClassImplementsAnotherIInterfaceWithAmbigImpl { }
}
