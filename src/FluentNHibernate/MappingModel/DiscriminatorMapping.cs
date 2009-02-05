﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHibernate.MappingModel
{
    public class DiscriminatorMapping : MappingBase
    {
        private readonly AttributeStore<DiscriminatorMapping> _attributes;
        private readonly IList<ColumnMapping> _columns;

        public DiscriminatorMapping()
        {
            _columns = new List<ColumnMapping>();
            _attributes = new AttributeStore<DiscriminatorMapping>();
            _attributes.SetDefault(x => x.IsNotNullable, true);
            _attributes.SetDefault(x => x.DiscriminatorType, DiscriminatorType.String);
            
        }

        public AttributeStore<DiscriminatorMapping> Attributes
        {
            get { return _attributes; }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessDiscriminator(this);
        }
        
        public IEnumerable<ColumnMapping> Columns
        {
            get { return _columns; }
        }

        public string Column
        {
            get { return _attributes.Get(x => x.Column); }
            set { _attributes.Set(x => x.Column, value); }
        }

        public bool IsNotNullable
        {
            get { return _attributes.Get(x => x.IsNotNullable); }
            set { _attributes.Set(x => x.IsNotNullable, value); }
        }

        public int Length
        {
            get { return _attributes.Get(x => x.Length); }
            set { _attributes.Set(x => x.Length, value); }
        }

        public bool Force
        {
            get { return _attributes.Get(x => x.Force); }
            set { _attributes.Set(x => x.Force, value); }
        }

        public bool Insert
        {
            get { return _attributes.Get(x => x.Insert); }
            set { _attributes.Set(x => x.Insert, value); }
        }

        public string Formula
        {
            get { return _attributes.Get(x => x.Formula); }
            set { _attributes.Set(x => x.Formula, value); }
        }

        public DiscriminatorType DiscriminatorType
        {
            get { return _attributes.Get(x => x.DiscriminatorType); }
            set { _attributes.Set(x => x.DiscriminatorType, value); }
        }


    }

    public enum DiscriminatorType
    {
        String,
        Char,
        Int32,
        Byte,
        Short,
        Boolean,
        YesNo,
        TrueFalse
    }
}
