using System.Linq;
using FluentNHibernate.FluentInterface;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Reflection;
using NUnit.Framework;
using FluentNHibernate.Testing.DomainModel;

namespace FluentNHibernate.Testing.FluentInterface
{
    [TestFixture]
    public class SubclassPartTester
    {
        [Test]
        public void Can_map_property()
        {
            var mapping = new SubclassMapping();
            var subclassPart = new SubclassPart<SalaryEmployee>(mapping);

            subclassPart.Map(x => x.Salary);

            var salaryProperty = mapping.Properties.FirstOrDefault();
            salaryProperty.ShouldNotBeNull();
            salaryProperty.MappedMember.ShouldEqual(ReflectionHelper.GetMember<SalaryEmployee>(x => x.Salary));
        }
    }
}