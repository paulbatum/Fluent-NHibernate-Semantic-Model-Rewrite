using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel
{
    [TestFixture]
    public class IdMappingTester
    {
        private IdMapping _idMapping;

        [SetUp]
        public void SetUp()
        {
            _idMapping = new IdMapping();
        }

        [Test]
        public void Should_have_default_member_access_of_property()
        {
            _idMapping.MemberAccess.AccessStrategy.ShouldEqual(AccessStrategy.Property);
            _idMapping.MemberAccess.NamingStrategy.ShouldEqual(NamingStrategy.None);
        }

        [Test]
        public void Binding_to_member_should_set_the_mapped_member()
        {
            MemberInfo member = ReflectionHelper.GetMember<TestEntity>(x => x.StringProperty);
            _idMapping.BindToMember(member);
            _idMapping.MappedMember.ShouldEqual(member);
        }

        [Test]
        public void Binding_to_member_should_set_member_access()
        {
            MemberInfo member = ReflectionHelper.GetMember<TestEntity>(x => x.StringField);
            _idMapping.BindToMember(member);
            _idMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.Field, NamingStrategy.None));
        }

        private class TestEntity
        {
            public string StringProperty { get; set; }
            public string StringField;
        }
    }
}