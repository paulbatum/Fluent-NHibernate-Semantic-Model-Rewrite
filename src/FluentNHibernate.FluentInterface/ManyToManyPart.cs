using System;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.FluentInterface
{
    public class ManyToManyPart<PARENT, CHILD> : IDeferredCollectionMapping
    {
        private DeferredCollectionMapping<ManyToManyPart<PARENT, CHILD>> _internalCollection;

        public ManyToManyPart(MemberInfo info)
        {
            _internalCollection = new DeferredCollectionMapping<ManyToManyPart<PARENT, CHILD>>(info, this);
        }

        public CollectionCascadePart<ManyToManyPart<PARENT, CHILD>> Cascade
        {
            get { return _internalCollection.Cascade; }
        }

        public ManyToManyPart<PARENT, CHILD> AsBag()
        {
            _internalCollection.AsBag();
            return this;
        }

        public ManyToManyPart<PARENT, CHILD> AsSet()
        {
            _internalCollection.AsSet();
            return this;
        }

        public ManyToManyPart<PARENT, CHILD> IsInverse()
        {
            _internalCollection.IsInverse();
            return this;
        }

        ICollectionMapping IDeferredCollectionMapping.ResolveCollectionMapping()
        {
            var collection = _internalCollection.ResolveCollectionMapping();
            collection.Contents = new ManyToManyMapping { ChildType = typeof(CHILD) };
            return collection;
        }
    }
}