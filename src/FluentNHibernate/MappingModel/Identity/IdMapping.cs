using System;
using System.Reflection;
using NHibernate.Cfg.MappingSchema;
using System.Collections.Generic;

namespace FluentNHibernate.MappingModel.Identity
{
    public class IdMapping : MapsMemberBase, IIdentityMapping, INameable
    {
        private readonly AttributeStore<IdMapping> _attributes;
        private readonly IList<ColumnMapping> _columns;
        public IdGeneratorMapping Generator { get; set; }

        public IdMapping()
            : this(null)
        { }

        public IdMapping(ColumnMapping columnMapping)
            : this(new AttributeStore(), columnMapping)
        {
        }

        protected IdMapping(AttributeStore store, ColumnMapping columnMapping) : base(store)
        {            
            _attributes = new AttributeStore<IdMapping>(store);
            _columns = new List<ColumnMapping>();

            if(columnMapping != null)
                AddIdColumn(columnMapping);            
        }

        public void AddIdColumn(ColumnMapping column)
        {
            _columns.Add(column);
        }

        public IEnumerable<ColumnMapping> Columns
        {
            get { return _columns; }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessId(this);

            if (Generator != null)
                visitor.Visit(Generator);

            foreach (var column in Columns)
                visitor.Visit(column);
        }

        public bool IsNameSpecified
        {
            get { return Attributes.IsSpecified(x => x.Name); }
        }
        
        public AttributeStore<IdMapping> Attributes
        {
            get { return _attributes; }
        }

        public string Name
        {
            get { return _attributes.Get(x => x.Name); }
            set { _attributes.Set(x => x.Name, value); }
        }

        
    }
}