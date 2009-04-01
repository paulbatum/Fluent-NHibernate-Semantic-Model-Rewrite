using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Output;
using NHibernate.Cfg.MappingSchema;
using NUnit.Framework;
using FluentNHibernate.MappingModel;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class HbmSetWriterTester
    {
        private RhinoAutoMocker<HbmSetWriter> _mocker;
        private HbmSetWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _mocker = new RhinoAutoMocker<HbmSetWriter>();
            _writer = _mocker.ClassUnderTest;
        }

        [Test]
        public void Should_produce_valid_hbm()
        {
            var set = new SetMapping { Name = "set1", Contents = new OneToManyMapping(), Key = new KeyMapping() };

            _mocker.Get<IHbmWriter<ICollectionContentsMapping>>()
                .Expect(x => x.Write(set.Contents)).Return(new HbmOneToMany { @class = "class1" });
            _mocker.Get<IHbmWriter<KeyMapping>>()
                .Expect(x => x.Write(set.Key)).Return(new HbmKey());

            _writer.ShouldGenerateValidOutput(set);
        }

        [Test]
        public void Should_write_the_attributes()
        {

            var testHelper = new HbmTestHelper<SetMapping>();
            testHelper.Check(x => x.Name, "set1").MapsToAttribute("name");
            testHelper.Check(x => x.OrderBy, "column1").MapsToAttribute("order-by");
            testHelper.Check(x => x.IsInverse, true).MapsToAttribute("inverse");
            testHelper.Check(x => x.IsLazy, true).MapsToAttribute("lazy");

            testHelper.VerifyAll(_writer);
        }

        [Test]
        public void Should_write_the_specified_access_type()
        {
            var set = new SetMapping();
            set.MemberAccess = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.CamelCase);

            _writer.VerifyXml(set)
                .HasAttribute("access", "field.camelcase");
        }

        [Test]
        public void Should_not_write_the_default_access_type()
        {
            var set = new SetMapping();

            _writer.VerifyXml(set)
                .DoesntHaveAttribute("access");
        }
    }
}
