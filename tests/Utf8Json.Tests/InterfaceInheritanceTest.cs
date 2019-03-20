using Moq;
using Xunit;
using Utf8Json.Resolvers;

namespace Utf8Json.Tests
{
    public class InterfaceInheritanceTest
    {
        public interface IExampleInherited
        {
            int ExampleInherited { get; set; }
        }
        public interface IExampleInheritor : IExampleInherited
        {
            int ExampleInheritor { get; set; }
        }

        public class ExampleInheritance : IExampleInheritor
        {
            public int ExampleInheritor { get; set; }
            public int ExampleInherited { get; set; }
        }

        public class ExampleExplicitInheritance : IExampleInheritor
        {
            int IExampleInheritor.ExampleInheritor { get; set; }
            int IExampleInherited.ExampleInherited { get; set; }
        }
        
        [Fact]
        public void InheritedInterfaceNonGenericTest()
        {
            // already passed previously

            var ei = new ExampleInheritance();

            JsonSerializer.NonGeneric.ToJsonString(ei, StandardResolver.Default)
                .Is("{\"ExampleInheritor\":0,\"ExampleInherited\":0}");
        }

        [Fact]
        public void InheritedInterfaceNonGenericTest2()
        {

            var eiMock = new Mock<IExampleInheritor>();

            JsonSerializer.NonGeneric.ToJsonString(typeof(IExampleInheritor), eiMock.Object, StandardResolver.Default)
                .Is("{\"ExampleInheritor\":0,\"ExampleInherited\":0}");
        }

        [Fact]
        public void InheritedInterfaceNonGenericTest3()
        {

            var eiMock = new Mock<ExampleInheritance>();

            JsonSerializer.NonGeneric.ToJsonString(typeof(IExampleInheritor), eiMock.Object, StandardResolver.Default)
                .Is("{\"ExampleInheritor\":0,\"ExampleInherited\":0}");
        }

        [Fact]
        public void InheritedInterfaceNonGenericTest4()
        {
            // already passed previously

            var eiMock = new Mock<ExampleInheritance>();

            JsonSerializer.NonGeneric.ToJsonString(typeof(ExampleInheritance), eiMock.Object, StandardResolver.Default)
                .Is("{\"ExampleInheritor\":0,\"ExampleInherited\":0}");
        }

        [Fact]
        public void InheritedInterfaceNonGenericTest4s()
        {
            // already passed previously
            
            var o = (ExampleInheritance) 
                JsonSerializer.NonGeneric.Deserialize(typeof(ExampleInheritance), "{\"ExampleInheritor\":0,\"ExampleInherited\":0}", StandardResolver.Default);
            
            o.ExampleInheritor.Is(0);
            o.ExampleInherited.Is(0);
        }

        [Fact]
        public void InheritedInterfaceNonGenericTest5()
        {
            // already passed previously

            var eiMock = new Mock<ExampleExplicitInheritance>();

            JsonSerializer.NonGeneric.ToJsonString(typeof(ExampleExplicitInheritance), eiMock.Object, StandardResolver.Default)
                .Is("{}");
        }
    }
}
