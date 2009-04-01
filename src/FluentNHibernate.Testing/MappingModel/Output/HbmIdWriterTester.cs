using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Output;
using NUnit.Framework;
using FluentNHibernate.MappingModel.Identity;
using NHibernate.Cfg.MappingSchema;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class HbmIdWriterTester
    {
        private RhinoAutoMocker<HbmIdWriter> _mocker;
        private HbmIdWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _mocker = new RhinoAutoMocker<HbmIdWriter>();
            _writer = _mocker.ClassUnderTest;
        }

        [Test]
        public void Should_produce_valid_hbm()
        {
            var id = new IdMapping { Generator = new IdGeneratorMapping()};
            _mocker.Get<IHbmWriter<IdGeneratorMapping>>()
                .Expect(x => x.Write(id.Generator)).Return(new HbmGenerator { @class = "native"});

            _writer.ShouldGenerateValidOutput(id);
        }

        [Test]
        public void Should_write_the_attributes()
        {
            var testHelper = new HbmTestHelper<IdMapping>();
            testHelper.Check(x => x.Name, "id1").MapsToAttribute("name");

            testHelper.VerifyAll(_writer);
        }

        [Test]
        public void Should_write_the_generator()
        {
            var idMapping = new IdMapping {Generator = new IdGeneratorMapping()};

            _mocker.Get<IHbmWriter<IdGeneratorMapping>>()
                .Expect(x => x.Write(idMapping.Generator)).Return(new HbmGenerator());

            _writer.VerifyXml(idMapping)
                .Element("generator").Exists();
        }

        [Test]
        public void Should_write_the_columns()
        {
            var idMapping = new IdMapping(new ColumnMapping());

            _mocker.Get<IHbmWriter<ColumnMapping>>()
                .Expect(x => x.Write(idMapping.Columns.First())).Return(new HbmColumn());

            _writer.VerifyXml(idMapping)
                .Element("column").Exists();
        }

        [Test]
        public void Should_write_the_specified_access_type()
        {
            var idMapping = new IdMapping();
            idMapping.MemberAccess = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.CamelCase);

            _writer.VerifyXml(idMapping)
                .HasAttribute("access", "field.camelcase");
        }

        [Test]
        public void Should_not_write_the_default_access_type()
        {
            var idMapping = new IdMapping();

            _writer.VerifyXml(idMapping)
                .DoesntHaveAttribute("access");
        }

    }
}
