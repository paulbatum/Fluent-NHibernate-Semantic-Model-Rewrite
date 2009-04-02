using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Reflection;
using FluentNHibernate.Testing.DomainModel;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel
{
    [TestFixture]
    public class BindingPropertyMappingToMemberTester
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
            MemberInfo member = ReflectionHelper.GetMember<EntityWithPublicGettersAndPrivateFields>(x => x.StringProperty);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MappedMember.ShouldEqual(member);
        }

        [Test]
        public void Binding_to_field_should_set_access_strategy_to_field()
        {
            MemberInfo member = ReflectionHelper.GetMember<EntityWithPublicGettersAndPrivateFields>(x => x.StringField);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.Field, NamingStrategy.None));
        }

        [Test]
        public void Will_find_camelcase_underscore_field_when_binding_to_property_with_no_setter()
        {
            MemberInfo member = ReflectionHelper.GetMember<EntityWithPublicGettersAndPrivateFields>(x => x.FieldIsCamelcaseUnderscore);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.NoSetter, NamingStrategy.CamelCaseUnderscore));
        }

        [Test]
        public void Will_find_camelcase_field_when_binding_to_property_with_no_setter()
        {
            MemberInfo member = ReflectionHelper.GetMember<EntityWithPublicGettersAndPrivateFields>(x => x.FieldIsCamelCase);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.NoSetter, NamingStrategy.CamelCase));
        }

        [Test]
        public void Will_find_lowercase_field_when_binding_to_property_with_no_setter()
        {
            MemberInfo member = ReflectionHelper.GetMember<EntityWithPublicGettersAndPrivateFields>(x => x.FieldIsLowerCase);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.NoSetter, NamingStrategy.LowerCase));
        }

        [Test]
        public void Will_find_lowercase_underscore_field_when_binding_to_property_with_no_setter()
        {
            MemberInfo member = ReflectionHelper.GetMember<EntityWithPublicGettersAndPrivateFields>(x => x.FieldIsLowerCaseUnderscore);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.NoSetter, NamingStrategy.LowerCaseUnderscore));
        }

        [Test]
        public void Will_find_pascalcase_underscore_field_when_binding_to_property_with_no_setter()
        {
            MemberInfo member = ReflectionHelper.GetMember<EntityWithPublicGettersAndPrivateFields>(x => x.FieldIsPascalCaseUnderscore);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.NoSetter, NamingStrategy.PascalCaseUnderscore));
        }

        [Test]
        public void Will_find_pascalcase_m_underscore_field_when_binding_to_property_with_no_setter()
        {
            MemberInfo member = ReflectionHelper.GetMember<EntityWithPublicGettersAndPrivateFields>(x => x.FieldIsPascalCaseMUnderscore);
            _propertyMapping.BindToMember(member);
            _propertyMapping.MemberAccess.ShouldEqual(MemberAccess.Create(AccessStrategy.NoSetter, NamingStrategy.PascalCase_M_Underscore));
        }

       
    }
}