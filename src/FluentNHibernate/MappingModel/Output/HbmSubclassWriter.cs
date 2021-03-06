using System;
using FluentNHibernate.MappingModel.Collections;
using NHibernate.Cfg.MappingSchema;

namespace FluentNHibernate.MappingModel.Output
{
    public class HbmSubclassWriter : NullMappingModelVisitor, IHbmWriter<SubclassMapping>
    {
        private readonly HbmMappedMemberWriterHelper _mappedMemberHelper;

        private HbmSubclass _hbm;

        private HbmSubclassWriter(HbmMappedMemberWriterHelper helper)
        {
            _mappedMemberHelper = helper;
        }

        public HbmSubclassWriter(IHbmWriter<ICollectionMapping> collectionWriter, IHbmWriter<PropertyMapping> propertyWriter, IHbmWriter<ManyToOneMapping> manyToOneWriter, IHbmWriter<ComponentMapping> componentWriter)
            : this(new HbmMappedMemberWriterHelper(collectionWriter, propertyWriter, manyToOneWriter, componentWriter))
        { }

        public object Write(SubclassMapping mappingModel)
        {
            _hbm = null;
            mappingModel.AcceptVisitor(this);
            return _hbm;
        }

        public override void ProcessSubclass(SubclassMapping subclassMapping)
        {
            _hbm = new HbmSubclass();
            _hbm.name = subclassMapping.Name;

            if (subclassMapping.Attributes.IsSpecified(x => x.DiscriminatorValue))
                _hbm.discriminatorvalue = subclassMapping.DiscriminatorValue.ToString();

            if (subclassMapping.Attributes.IsSpecified(x => x.Proxy))
                _hbm.proxy = subclassMapping.Proxy.AssemblyQualifiedName;

            if (subclassMapping.Attributes.IsSpecified(x => x.Lazy))
            {
                _hbm.lazy = subclassMapping.Lazy;
                _hbm.lazySpecified = true;
            }

            if (subclassMapping.Attributes.IsSpecified(x => x.DynamicUpdate))
                _hbm.dynamicupdate = subclassMapping.DynamicUpdate;

            if (subclassMapping.Attributes.IsSpecified(x => x.DynamicInsert))
                _hbm.dynamicinsert = subclassMapping.DynamicInsert;

            if (subclassMapping.Attributes.IsSpecified(x => x.SelectBeforeUpdate))
                _hbm.selectbeforeupdate = subclassMapping.SelectBeforeUpdate;

            if (subclassMapping.Attributes.IsSpecified(x => x.Abstract))
            {
                _hbm.@abstract = subclassMapping.Abstract;
                _hbm.abstractSpecified = true;
            }
        }

        public override void Visit(SubclassMapping subclassMapping)
        {
            var writer = new HbmSubclassWriter(_mappedMemberHelper);
            var subclassHbm = (HbmSubclass)writer.Write(subclassMapping);
            subclassHbm.AddTo(ref _hbm.subclass1);
        }

        public override void Visit(ICollectionMapping collectionMapping)
        {
            object collectionHbm = _mappedMemberHelper.Write(collectionMapping);
            collectionHbm.AddTo(ref _hbm.Items);
        }

        public override void Visit(PropertyMapping propertyMapping)
        {
            object propertyHbm = _mappedMemberHelper.Write(propertyMapping);
            propertyHbm.AddTo(ref _hbm.Items);
        }

        public override void Visit(ManyToOneMapping manyToOneMapping)
        {
            object manyHbm = _mappedMemberHelper.Write(manyToOneMapping);
            manyHbm.AddTo(ref _hbm.Items);
        }

        public override void Visit(ComponentMapping componentMapping)
        {
            object componentHbm = _mappedMemberHelper.Write(componentMapping);
            componentHbm.AddTo(ref _hbm.Items);
        }
    }
}