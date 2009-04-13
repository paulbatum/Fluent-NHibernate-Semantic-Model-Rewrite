using System;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.FluentInterface
{
    public class CollectionCascadePart<TParentPart>
    {
        private readonly TParentPart _parentpart;
        private readonly AttributeStore<ICollectionMapping> _attributes;

        public CollectionCascadePart(TParentPart parentpart, AttributeStore<ICollectionMapping> attributes)
        {
            _parentpart = parentpart;
            _attributes = attributes;
        }

        public TParentPart All()
        {
            _attributes.Set(x => x.Cascade, CollectionCascadeType.All);
            return _parentpart;
        }

        public TParentPart SaveUpdate()
        {
            _attributes.Set(x => x.Cascade, CollectionCascadeType.SaveUpdate);
            return _parentpart;
        }

        public TParentPart Delete()
        {
            _attributes.Set(x => x.Cascade, CollectionCascadeType.Delete);
            return _parentpart;
        }

        public TParentPart AllDeleteOrphan()
        {
            _attributes.Set(x => x.Cascade, CollectionCascadeType.AllDeleteOrphan);
            return _parentpart;
        }

        public TParentPart None()
        {
            _attributes.Set(x => x.Cascade, CollectionCascadeType.None);
            return _parentpart;
        }
    }
}