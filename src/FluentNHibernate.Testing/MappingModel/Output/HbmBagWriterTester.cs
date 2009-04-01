using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using NUnit.Framework;
using FluentNHibernate.MappingModel.Output;
using NHibernate.Cfg.MappingSchema;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class HbmBagWriterTester
    {
        private RhinoAutoMocker<HbmBagWriter> _mocker;
        private HbmBagWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _mocker = new RhinoAutoMocker<HbmBagWriter>();
            _writer = _mocker.ClassUnderTest;
        }

        [Test]
        public void Should_produce_valid_hbm()
        {
            var bag = new BagMapping { Name = "bag1", Contents = new OneToManyMapping(), Key = new KeyMapping() };

            _mocker.Get<IHbmWriter<ICollectionContentsMapping>>()
                .Expect(x => x.Write(bag.Contents)).Return(new HbmOneToMany { @class = "class1" });

            _mocker.Get<IHbmWriter<KeyMapping>>()
                .Expect(x => x.Write(bag.Key)).Return(new HbmKey());

            _writer.ShouldGenerateValidOutput(bag);
        }

        [Test]
        public void Should_write_the_attributes()
        {
            var testHelper = new HbmTestHelper<BagMapping>();
            testHelper.Check(x => x.Name, "bag1").MapsToAttribute("name");
            testHelper.Check(x => x.OrderBy, "column1").MapsToAttribute("order-by");
            testHelper.Check(x => x.IsInverse, true).MapsToAttribute("inverse");
            testHelper.Check(x => x.IsLazy, true).MapsToAttribute("lazy");                

            testHelper.VerifyAll(_writer);
        }

        [Test]
        public void Should_write_the_contents()
        {
            var bag = new BagMapping {Contents = new OneToManyMapping()};

            _mocker.Get<IHbmWriter<ICollectionContentsMapping>>()
                .Expect(x => x.Write(bag.Contents))
                .Return(new HbmOneToMany());

            _writer.VerifyXml(bag)
                .Element("one-to-many").Exists();
        }

        [Test]
        public void Should_write_the_key()
        {
            var bag = new BagMapping { Key = new KeyMapping()};

            _mocker.Get<IHbmWriter<KeyMapping>>()
                .Expect(x => x.Write(bag.Key))
                .Return(new HbmKey());

            _writer.VerifyXml(bag)
                .Element("key").Exists();
        }

        [Test]
        public void Should_write_the_specified_access_type()
        {
            var bag = new BagMapping();
            bag.MemberAccess = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.CamelCase);

            _writer.VerifyXml(bag)
                .HasAttribute("access", "field.camelcase");
        }

        [Test]
        public void Should_not_write_the_default_access_type()
        {
            var bag = new BagMapping();

            _writer.VerifyXml(bag)
                .DoesntHaveAttribute("access");
        }



    }
}