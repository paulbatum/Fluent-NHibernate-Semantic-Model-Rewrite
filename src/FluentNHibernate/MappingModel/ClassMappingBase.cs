using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.MappingModel
{
    public abstract class ClassMappingBase : MappingBase, INameable, IHasMappedMembers
    {
        private readonly AttributeStore<ClassMappingBase> _attributes;
        private readonly MappedMembers _mappedMembers;
        public Type Type { get; set; }

        protected ClassMappingBase(AttributeStore underlyingStore)
        {
            _attributes = new AttributeStore<ClassMappingBase>(underlyingStore);
            _mappedMembers = new MappedMembers();
        }

        public string Name
        {
            get { return _attributes.Get(x => x.Name); }
            set { _attributes.Set(x => x.Name, value); }
        }

        public bool IsNameSpecified
        {
            get { return _attributes.IsSpecified(x => x.Name); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            _mappedMembers.AcceptVisitor(visitor);
        }

        #region IHasMappedMembers

        public IEnumerable<ManyToOneMapping> References
        {
            get { return _mappedMembers.References; }
        }

        public IEnumerable<ICollectionMapping> Collections
        {
            get { return _mappedMembers.Collections; }
        }

        public IEnumerable<PropertyMapping> Properties
        {
            get { return _mappedMembers.Properties; }
        }

        public IEnumerable<ComponentMapping> Components
        {
            get { return _mappedMembers.Components; }
        }

        public void AddProperty(PropertyMapping property)
        {
            _mappedMembers.AddProperty(property);
        }

        public void AddCollection(ICollectionMapping collection)
        {
            _mappedMembers.AddCollection(collection);
        }

        public void AddReference(ManyToOneMapping manyToOne)
        {
            _mappedMembers.AddReference(manyToOne);
        }

        public void AddComponent(ComponentMapping componentMapping)
        {
            _mappedMembers.AddComponent(componentMapping);
        }

        #endregion

        // TODO: Check NHibernate validation rules.
        public Type Proxy
        {
            get { return _attributes.Get(x => x.Proxy); }
            set { _attributes.Set(x => x.Proxy, value); }
        }

        // TODO: Should this property interact with Proxy somehow?
        public bool Lazy
        {
            get { return _attributes.Get(x => x.Lazy); }
            set { _attributes.Set(x => x.Lazy, value); }
        }

        public bool DynamicUpdate
        {
            get { return _attributes.Get(x => x.DynamicUpdate); }
            set { _attributes.Set(x => x.DynamicUpdate, value); }
        }

        public bool DynamicInsert
        {
            get { return _attributes.Get(x => x.DynamicInsert); }
            set { _attributes.Set(x => x.DynamicInsert, value); }
        }

        public bool SelectBeforeUpdate
        {
            get { return _attributes.Get(x => x.SelectBeforeUpdate); }
            set { _attributes.Set(x => x.SelectBeforeUpdate, value); }
        }

        public bool Abstract
        {
            get { return _attributes.Get(x => x.Abstract); }
            set { _attributes.Set(x => x.Abstract, value); }
        }

		public override string ToString()
		{
			return string.Format("ClassMapping({0})", this.Type.Name);
		}

    }
}
