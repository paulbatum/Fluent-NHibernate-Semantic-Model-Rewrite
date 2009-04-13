using System.Reflection;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.FluentInterface
{
    public class OneToManyPart<PARENT, CHILD> : IDeferredCollectionMapping
    {
        private DeferredCollectionMapping<OneToManyPart<PARENT, CHILD>> _internalCollection;

        public OneToManyPart(MemberInfo info)
        {
            _internalCollection = new DeferredCollectionMapping<OneToManyPart<PARENT, CHILD>>(info, this);
        }

        public CollectionCascadePart<OneToManyPart<PARENT, CHILD>> Cascade
        {
            get { return _internalCollection.Cascade; }
        }

        public OneToManyPart<PARENT, CHILD> AsBag()
        {
            _internalCollection.AsBag();
            return this;
        }

        public OneToManyPart<PARENT, CHILD> AsSet()
        {
            _internalCollection.AsSet();
            return this;
        }

        public OneToManyPart<PARENT, CHILD> IsInverse()
        {
            _internalCollection.IsInverse();
            return this;
        }

        ICollectionMapping IDeferredCollectionMapping.ResolveCollectionMapping()
        {
            var collection = _internalCollection.ResolveCollectionMapping();
            collection.Contents = new OneToManyMapping { ChildType = typeof(CHILD) };
            return collection;
        }
    }
}