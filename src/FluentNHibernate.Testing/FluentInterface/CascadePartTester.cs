using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.FluentInterface;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Testing.DomainModel;
using NUnit.Framework;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Testing.FluentInterface
{
    [TestFixture]
    public class CascadePartTester
    {
        [Test]
        public void Cascade_should_be_fluent()
        {
            var oneToManyPart = new OneToManyPart<Artist, Album>(null);
            var attributeStore = new AttributeStore<ICollectionMapping>();
            var cascadePart = new CascadePart<OneToManyPart<Artist, Album>>(oneToManyPart, attributeStore);
            cascadePart.All().ShouldEqual(oneToManyPart);
        }

        [Test]
        public void Can_set_cascade_all()
        {
            var attributeStore = new AttributeStore<ICollectionMapping>();
            var cascadePart = new CascadePart<OneToManyPart<Artist, Album>>(null, attributeStore);
            cascadePart.All();

            attributeStore.Get(x => x.Cascade).ShouldEqual(CollectionCascadeType.All);
        }

        [Test]
        public void Can_set_cascade_save_update()
        {
            var attributeStore = new AttributeStore<ICollectionMapping>();
            var cascadePart = new CascadePart<OneToManyPart<Artist, Album>>(null, attributeStore);
            cascadePart.SaveUpdate();

            attributeStore.Get(x => x.Cascade).ShouldEqual(CollectionCascadeType.SaveUpdate);
        }

        [Test]
        public void Can_set_cascade_delete()
        {
            var attributeStore = new AttributeStore<ICollectionMapping>();
            var cascadePart = new CascadePart<OneToManyPart<Artist, Album>>(null, attributeStore);
            cascadePart.Delete();

            attributeStore.Get(x => x.Cascade).ShouldEqual(CollectionCascadeType.Delete);
        }
        
    }
}
