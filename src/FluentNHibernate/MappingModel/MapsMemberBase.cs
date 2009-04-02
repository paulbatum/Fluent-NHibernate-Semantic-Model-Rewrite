using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FluentNHibernate.MappingModel
{
    public abstract class MapsMemberBase : MappingBase, IMapsMember
    {
        private readonly AttributeStore<MapsMemberBase> _attributes;
        
        protected MapsMemberBase(AttributeStore underlyingStore)
        {
            _attributes = new AttributeStore<MapsMemberBase>(underlyingStore);
            _attributes.SetDefault(x => x.MemberAccess, MemberAccess.CreateDefault());
        }

        public MemberInfo MappedMember { get; private set; }
        
        public MemberAccess MemberAccess
        {
            get { return _attributes.Get(x => x.MemberAccess); }
            set { _attributes.Set(x => x.MemberAccess, value); }
        }

        public void BindToMember(MemberInfo member)
        {
            this.MappedMember = member;

            if (member.MemberType == MemberTypes.Field)
                this.MemberAccess = MemberAccess.Create(AccessStrategy.Field, NamingStrategy.None);

            if(member.MemberType == MemberTypes.Property)
                BindToProperty((PropertyInfo)member);
        }

        private void BindToProperty(PropertyInfo property)
        {
            if (property.GetSetMethod(true) == null)
            {
                this.MemberAccess = MemberAccess.Create(AccessStrategy.NoSetter, TryToFindFieldNamingStrategy(property));
            }
        }

        private NamingStrategy TryToFindFieldNamingStrategy(MemberInfo memberInfo)
        {
            return NamingStrategy.AllStrategies
                .FirstOrDefault(ns => memberInfo.DeclaringType.GetField(ns.ApplyTo(memberInfo.Name), BindingFlags.Instance | BindingFlags.NonPublic) != null)
                ?? NamingStrategy.None;
        }

    }
}