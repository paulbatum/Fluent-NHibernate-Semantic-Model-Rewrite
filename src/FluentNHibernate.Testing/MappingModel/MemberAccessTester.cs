using System;
using System.Reflection;
using FluentNHibernate.Reflection;
using NUnit.Framework;
using FluentNHibernate.MappingModel;
using NHibernate.Properties;

namespace FluentNHibernate.Testing.MappingModel
{
    [TestFixture]
    public class MemberAccessTester
    {
        [Test]
        public void Default_member_access_is_property_with_no_naming_strategy()
        {
            var access = MemberAccess.CreateDefault();
            access.AccessStrategy.ShouldEqual(AccessStrategy.Property);
            access.NamingStrategy.ShouldEqual(NamingStrategy.None);
        }

        [Test]
        public void Can_specify_field_access()
        {
            var access = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.None);
            access.AccessStrategy.ShouldEqual(AccessStrategy.Field);
            access.NamingStrategy.ShouldEqual(NamingStrategy.None);
        }

        [Test]
        public void ToString_will_provide_nhibernate_compatible_format()
        {
            var access = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.CamelCase);
            access.ToString().ShouldEqual("field.camelcase");
        }

        [Test]
        public void ToString_only_includes_access_strategy_when_naming_strategy_is_none()
        {
            var access = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.None);
            access.ToString().ShouldEqual("field");
        }

        [Test]
        public void Can_specify_custom_strategy()
        {
            var access = MemberAccess.CreateCustom<TestCustomAccessor>();
            access.ToString().ShouldEqual(typeof(TestCustomAccessor).AssemblyQualifiedName);
        }

        [Test]
        public void Member_access_instances_with_same_strategies_should_be_equal()
        {
            var memberAccess1 = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.PascalCaseUnderscore);
            var memberAccess2 = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.PascalCaseUnderscore);

            memberAccess1.ShouldEqual(memberAccess2);
        }

        private class TestCustomAccessor : IPropertyAccessor
        {
            public IGetter GetGetter(Type theClass, string propertyName)
            {
                throw new System.NotImplementedException();
            }

            public ISetter GetSetter(Type theClass, string propertyName)
            {
                throw new System.NotImplementedException();
            }

            public bool CanAccessTroughReflectionOptimizer
            {
                get { throw new System.NotImplementedException(); }
            }
        }

    }


}