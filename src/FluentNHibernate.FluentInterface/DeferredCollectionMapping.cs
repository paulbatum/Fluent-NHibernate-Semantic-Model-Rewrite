using System;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.FluentInterface
{
    internal class DeferredCollectionMapping<TParentPart> : IDeferredCollectionMapping
    {
        private readonly MemberInfo _info;
        private readonly AttributeStore<ICollectionMapping> _attributes;
        private readonly TParentPart _parentPart;

        private Func<ICollectionMapping> _collectionBuilder;

        public DeferredCollectionMapping(MemberInfo info, TParentPart parentPart)
        {
            _info = info;
            _parentPart = parentPart;
            _attributes = new AttributeStore<ICollectionMapping>();
            AsBag();   
        }

        public CollectionCascadePart<TParentPart> Cascade
        {
            get { return new CollectionCascadePart<TParentPart>(_parentPart, _attributes); }
        }

        public void AsBag()
        {
            _collectionBuilder = () => new BagMapping();
        }

        public void AsSet()
        {
            _collectionBuilder = () => new SetMapping();
        }

        public void IsInverse()
        {
            _attributes.Set(x => x.IsInverse, true);
        }

        public ICollectionMapping ResolveCollectionMapping()
        {
            var collection = _collectionBuilder();       
            _attributes.CopyTo(collection.Attributes);

            collection.BindToMember(_info);
            collection.Key = new KeyMapping();

            return collection;
        }
    }
}