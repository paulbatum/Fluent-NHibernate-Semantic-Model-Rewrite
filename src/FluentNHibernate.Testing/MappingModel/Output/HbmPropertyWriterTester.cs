using System.Reflection;
using NUnit.Framework;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Output;
using NHibernate.Cfg.MappingSchema;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class HbmPropertyWriterTester
    {
        private HbmPropertyWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _writer = new HbmPropertyWriter();
        }

        [Test]
        public void Should_produce_valid_hbm()
        {
            var property = new PropertyMapping {Name = "Property1"};
            _writer.ShouldGenerateValidOutput(property);
        }

        [Test]
        public void Should_write_the_attributes()
        {
            var testHelper = new HbmTestHelper<PropertyMapping>();
            testHelper.Check(x => x.Name, "test").MapsToAttribute("name");
            testHelper.Check(x => x.Length, 50).MapsToAttribute("length");
            testHelper.Check(x => x.IsNotNullable, true).MapsToAttribute("not-null");
            testHelper.Check(x => x.ColumnName, "thecolumn").MapsToAttribute("column");
				testHelper.Check(x => x.Unique, true).MapsToAttribute("unique");
            testHelper.VerifyAll(_writer);
        }


        [Test]
        public void Should_write_the_specified_access_type()
        {
            var property = new PropertyMapping();
            property.MemberAccess = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.CamelCase);

            _writer.VerifyXml(property)
                .HasAttribute("access", "field.camelcase");
        }

        [Test]
        public void Should_not_write_the_default_access_type()
        {
            var property = new PropertyMapping();

            _writer.VerifyXml(property)
                .DoesntHaveAttribute("access");
        }

        
    }
}
