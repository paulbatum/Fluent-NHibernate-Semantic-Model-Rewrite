using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Output;
using NUnit.Framework;
using StructureMap.AutoMocking;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    public abstract class HbmCollectionMappingCascadeBehaviorBase<T> where T : CollectionMappingBase
    {
        protected IHbmWriter<T> _writer;
        protected T _mapping;

        [Test]
        public void Default_cascade_type_is_none()
        {
            _mapping.Cascade.ShouldEqual(CollectionCascadeType.None);
        }

        [Test]
        public void Should_not_write_default_cascade_type()
        {
            _writer.VerifyXml(_mapping)
                .DoesntHaveAttribute("cascade");
        }

        [Test]
        public void Should_write_the_cascade_type_when_set()
        {
            _mapping.Cascade = CollectionCascadeType.SaveUpdate;

            _writer.VerifyXml(_mapping)
                .HasAttribute("cascade", "save-update");
        }
    }

    [TestFixture]
    public class HbmBagWriterCascadeBehavior : HbmCollectionMappingCascadeBehaviorBase<BagMapping>
    {        
        [SetUp]
        public void SetUp()
        {
            _writer = new HbmBagWriter(null, null);
            _mapping = new BagMapping();
        }
    }

    [TestFixture]
    public class HbmSetWriterCascadeBehavior : HbmCollectionMappingCascadeBehaviorBase<SetMapping>
    {
        [SetUp]
        public void SetUp()
        {
            _writer = new HbmSetWriter(null, null);
            _mapping = new SetMapping();
        }
    }

    [TestFixture]
    public class HbmListWriterCascadeBehavior : HbmCollectionMappingCascadeBehaviorBase<ListMapping>
    {
        [SetUp]
        public void SetUp()
        {
            _writer = new HbmListWriter(null, null, null);
            _mapping = new ListMapping();
        }
    }
}
