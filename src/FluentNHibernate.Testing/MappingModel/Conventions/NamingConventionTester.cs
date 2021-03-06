using System.Linq;
using FluentNHibernate.MappingModel.Conventions;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Reflection;
using NUnit.Framework;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Testing.MappingModel.Conventions
{
    [TestFixture]
    public class NamingConventionTester
    {
        private NamingConvention _namingConvention;

        [SetUp]
        public void SetUp()
        {            
            _namingConvention = new NamingConvention();
        }

        [Test]
        public void Should_apply_to_unnamed_classes_with_type_specified()
        {
            var classMapping = new ClassMapping();
            classMapping.Type = typeof (Album);
            _namingConvention.ProcessClass(classMapping);

            classMapping.Name.ShouldEqual(classMapping.Type.AssemblyQualifiedName);
        }

        [Test]
        public void Should_not_apply_to_named_classes()
        {
            var classMapping = new ClassMapping();
            classMapping.Name = "class1";
            classMapping.Type = typeof(Album);
            _namingConvention.ProcessClass(classMapping);

            classMapping.Name.ShouldEqual("class1");
        }

        [Test, ExpectedException(typeof(ConventionException))]
        public void Should_throw_exception_if_class_has_no_name_and_no_type_specified()
        {
            var classMapping = new ClassMapping();
            _namingConvention.ProcessClass(classMapping);
        }

        [Test]
        public void Should_apply_to_property_mapping()
        {
            var propertyInfo = ReflectionHelper.GetMember((Album a) => a.Title);
            var propertyMapping = new PropertyMapping();
            propertyMapping.BindToMember(propertyInfo);

            _namingConvention.ProcessProperty(propertyMapping);

            propertyMapping.Name.ShouldEqual(propertyMapping.MappedMember.Name);
        }

        [Test]
        public void Should_apply_to_collection_mapping()
        {
            var bagMapping = new BagMapping();
            bagMapping.BindToMember(ReflectionHelper.GetMember((Album a) => a.Tracks));

            _namingConvention.ProcessBag(bagMapping);

            bagMapping.Name.ShouldEqual(bagMapping.MappedMember.Name);
        }

        [Test]
        public void Should_apply_to_id_mapping()
        {
            var idMapping = new IdMapping();
            idMapping.BindToMember(ReflectionHelper.GetMember((Album a) => a.ID));

            _namingConvention.ProcessId(idMapping);

            idMapping.Name.ShouldEqual(idMapping.MappedMember.Name);
        }

        [Test]
        public void Should_apply_to_column_mapping()
        {
            var propertyInfo = ReflectionHelper.GetMember((Album a) => a.ID);
            var columnMapping = new ColumnMapping() { MappedMember = propertyInfo };

            _namingConvention.ProcessColumn(columnMapping);

            columnMapping.Name.ShouldEqual(columnMapping.MappedMember.Name);
        }

        [Test]
        public void Should_apply_to_many_to_one_mapping()
        {
            var manyToOneMapping = new ManyToOneMapping();
            manyToOneMapping.BindToMember(ReflectionHelper.GetMember((Album a) => a.Artist));
            
            _namingConvention.ProcessManyToOne(manyToOneMapping);

            manyToOneMapping.Name.ShouldEqual(manyToOneMapping.MappedMember.Name);
        }

        [Test]
        public void Should_apply_to_joined_subclass_mapping()
        {
            var joinedSubclassMapping = new JoinedSubclassMapping();
            joinedSubclassMapping.Type = typeof(Album);
            _namingConvention.ProcessJoinedSubclass(joinedSubclassMapping);

            joinedSubclassMapping.Name.ShouldEqual(joinedSubclassMapping.Type.AssemblyQualifiedName);
        }

        [Test]
        public void Should_apply_to_subclass_mapping()
        {
            var subclassMapping = new SubclassMapping();
            subclassMapping.Type = typeof(Album);
            _namingConvention.ProcessSubclass(subclassMapping);

            subclassMapping.Name.ShouldEqual(subclassMapping.Type.AssemblyQualifiedName);  
        }

        [Test]
        public void Should_apply_to_one_to_many_mapping()
        {
            var oneToManyMapping = new OneToManyMapping();
            oneToManyMapping.ChildType = typeof (Album);
            _namingConvention.ProcessOneToMany(oneToManyMapping);

            oneToManyMapping.ClassName.ShouldEqual(oneToManyMapping.ChildType.AssemblyQualifiedName);
        }

        [Test]
        public void Should_apply_to_many_to_many_mapping()
        {
            var manyToManyMapping = new ManyToManyMapping();
            manyToManyMapping.ChildType = typeof (Album);
            _namingConvention.ProcessManyToMany(manyToManyMapping);

            manyToManyMapping.ClassName.ShouldEqual(manyToManyMapping.ChildType.AssemblyQualifiedName);
        }

        [Test]
        public void Should_apply_to_components()
        {
            var propertyInfo = ReflectionHelper.GetMember((SalaryEmployee e) => e.Salary);
            var componentMapping = new ComponentMapping();
            componentMapping.BindToMember(propertyInfo);

            _namingConvention.ProcessComponent(componentMapping);

            componentMapping.PropertyName.ShouldEqual(propertyInfo.Name);
        }
        
    }
}