using FluentNHibernate.FluentInterface;
using NUnit.Framework;
using FluentNHibernate.Testing.DomainModel;

namespace FluentNHibernate.Testing.FluentInterface
{
    [TestFixture]
    public class ManyToManyPartTester
    {
        [Test]
        public void Can_get_cascade_part()
        {
            var manyToManyPart = new ManyToManyPart<Album, Tag>(null);
            manyToManyPart.Cascade.ShouldNotBeNull();
        }
    }
}