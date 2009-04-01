using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Output;
using NUnit.Framework;
using NHibernate.Cfg.MappingSchema;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class HbmManyToOneWriterTester
    {
        private HbmManyToOneWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _writer = new HbmManyToOneWriter();
        }

        [Test]
        public void Should_produce_valid_hbm()
        {
            var manyToOne = new ManyToOneMapping { Name = "manyToOne" };
            _writer.ShouldGenerateValidOutput(manyToOne);
        }

        [Test]
        public void Should_write_the_attributes()
        {
            var testHelper = new HbmTestHelper<ManyToOneMapping>();
            testHelper.Check(x => x.Name, "manyToOne").MapsToAttribute("name");
            testHelper.Check(x => x.IsNotNullable, true).MapsToAttribute("not-null");

            testHelper.VerifyAll(_writer);
        }

        [Test]
        public void Should_write_the_specified_access_type()
        {
            var manyToOne = new ManyToOneMapping();
            manyToOne.MemberAccess = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.CamelCase);

            _writer.VerifyXml(manyToOne)
                .HasAttribute("access", "field.camelcase");
        }

        [Test]
        public void Should_not_write_the_default_access_type()
        {
            var manyToOne = new ManyToOneMapping();

            _writer.VerifyXml(manyToOne)
                .DoesntHaveAttribute("access");
        }

    }
}
