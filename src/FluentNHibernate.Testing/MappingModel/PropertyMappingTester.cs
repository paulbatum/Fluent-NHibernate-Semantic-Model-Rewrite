using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel
{
    [TestFixture]
    public class PropertyMappingTester
    {
        private PropertyMapping _propertyMapping;

        [SetUp]
        public void SetUp()
        {
            _propertyMapping = new PropertyMapping();    
        }

        [Test]
        public void Should_have_default_member_access_of_property()
        {
            _propertyMapping.MemberAccess.AccessStrategy.ShouldEqual(AccessStrategy.Property);
            _propertyMapping.MemberAccess.NamingStrategy.ShouldEqual(NamingStrategy.None);
        }

        [Test]
        public void Binding_to_member_should_set_the_mapped_member()
        {
            MemberInfo member = ReflectionHelper.GetMember<TestEntity>(x => x.StringProperty);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MappedMember.ShouldEqual(member);
        }

        [Test]
        public void Binding_to_member_should_set_member_access()
        {
            MemberInfo member = ReflectionHelper.GetMember<TestEntity>(x => x.StringField);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.Field, NamingStrategy.None));
        }
        
        private class TestEntity
        {
            public string StringProperty { get; set; }
            public string StringField;
        }
    }
}