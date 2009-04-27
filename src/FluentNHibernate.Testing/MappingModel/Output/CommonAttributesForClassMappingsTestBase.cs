using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Output;
using FluentNHibernate.Testing.DomainModel;
using NUnit.Framework;
using StructureMap.AutoMocking;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    public class CommonAttributesForClassMappingsTestBase<TMapping, TWriter> 
        where TMapping: ClassMappingBase, new() 
        where TWriter : class, IHbmWriter<TMapping>
    {
        protected RhinoAutoMocker<TWriter> _mocker = new RhinoAutoMocker<TWriter>();
        protected IHbmWriter<TMapping> _writer;

        [SetUp]
        public void SetUp()
        {
            _writer = _mocker.ClassUnderTest;
        }

        [Test]
        public void Should_write_common_attributes()
        {
            var testHelper = new HbmTestHelper<TMapping>();
            testHelper.Check(x => x.Proxy, typeof(Album)).MapsToAttribute("proxy", typeof(Album).AssemblyQualifiedName);
            testHelper.Check(x => x.Lazy, true).MapsToAttribute("lazy");
            testHelper.Check(x => x.DynamicUpdate, true).MapsToAttribute("dynamic-update");
            testHelper.Check(x => x.DynamicInsert, true).MapsToAttribute("dynamic-insert");
            testHelper.Check(x => x.SelectBeforeUpdate, true).MapsToAttribute("select-before-update");
            testHelper.Check(x => x.Abstract, true).MapsToAttribute("abstract");

            testHelper.VerifyAll(_writer);
        }
    }

    [TestFixture]
    public class ClassMappingCommonAttributesTester 
        : CommonAttributesForClassMappingsTestBase<ClassMapping, HbmClassWriter>
    {

    }

    [TestFixture]
    public class JoinedSubclassMappingCommonAttributesTester 
        : CommonAttributesForClassMappingsTestBase<JoinedSubclassMapping, HbmJoinedSubclassWriter>
    {

    }

    [TestFixture]
    public class SubclassMappingCommonAttributesTester 
        : CommonAttributesForClassMappingsTestBase<SubclassMapping, HbmSubclassWriter>
    {

    }


}
