using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate.Properties;

namespace FluentNHibernate.MappingModel
{
    public class MemberAccess
    {
        public AccessStrategy AccessStrategy { get; private set; }
        public NamingStrategy NamingStrategy { get; private set; }

        private MemberAccess(AccessStrategy accessStrategy, NamingStrategy namingStrategy)
        {
            AccessStrategy = accessStrategy;
            NamingStrategy = namingStrategy;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append(this.AccessStrategy);

            if (this.NamingStrategy != NamingStrategy.None)
            {
                result.Append(".");
                result.Append(this.NamingStrategy);
            }

            return result.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as MemberAccess;
            if(other == null || this.GetType() != other.GetType())
                return false;

            return (this.AccessStrategy == other.AccessStrategy && this.NamingStrategy == other.NamingStrategy);
        }

        public override int GetHashCode()
        {
            return this.AccessStrategy.GetHashCode() ^ this.NamingStrategy.GetHashCode();
        }

        public static MemberAccess CreateDefault()
        {
            return new MemberAccess(AccessStrategy.Property, NamingStrategy.None);
        }

        public static MemberAccess Create(AccessStrategy accessStrategy, NamingStrategy namingStrategy)
        {
            return new MemberAccess(accessStrategy, namingStrategy);
        }

        public static MemberAccess CreateCustom<T>() where T : IPropertyAccessor
        {
            return new MemberAccess(new CustomAccessStrategy(typeof(T)), NamingStrategy.None);
        }

        private class CustomAccessStrategy : AccessStrategy
        {
            public CustomAccessStrategy(Type type)
                : base(type.AssemblyQualifiedName)
            { }
        }


    }

    public class NamingStrategy
    {
        private readonly string _value;

        public static readonly NamingStrategy None = new NamingStrategy("none");
        public static readonly NamingStrategy CamelCase = new NamingStrategy("camelcase");
        public static readonly NamingStrategy CamelCaseUnderscore = new NamingStrategy("camelcase-underscore");
        public static readonly NamingStrategy LowerCase = new NamingStrategy("lowercase");
        public static readonly NamingStrategy LowerCaseUnderscore = new NamingStrategy("lowercase-underscore");
        public static readonly NamingStrategy PascalCaseUnderscore = new NamingStrategy("pascalcase-underscore");
        public static readonly NamingStrategy PascalCase_M_Underscore = new NamingStrategy("pascalcase-m-underscore");

        public static IEnumerable<NamingStrategy> AllStrategies
        {
            get
            {
                return new List<NamingStrategy>
                           {
                               CamelCase,
                               CamelCaseUnderscore,
                               LowerCase,
                               LowerCaseUnderscore,
                               PascalCaseUnderscore,
                               PascalCase_M_Underscore
                           };
            }
        }

        protected NamingStrategy(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as NamingStrategy;
            if (other == null || this.GetType() != other.GetType())
                return false;

            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public string ApplyTo(string input)
        {
            if (this == CamelCase)
                return ToCamelCase(input);
            if(this == CamelCaseUnderscore)
                return "_" + ToCamelCase(input);
            if (this == LowerCase)
                return input.ToLower();
            if (this == LowerCaseUnderscore)
                return "_" + input.ToLower();
            if (this == PascalCaseUnderscore)
                return "_" + ToPascalCase(input);
            if (this == PascalCase_M_Underscore)
                return "m_" + ToPascalCase(input);

            return input;
        }

        private string ToCamelCase(string input)
        {
            return input.Substring(0, 1).ToLower() + input.Substring(1);
        }

        private string ToPascalCase(string input)
        {
            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }
    }

    public class AccessStrategy
    {
        private readonly string _value;

        public static readonly AccessStrategy Property = new AccessStrategy("property");
        public static readonly AccessStrategy Field = new AccessStrategy("field");
        public static readonly AccessStrategy NoSetter = new AccessStrategy("nosetter");

        protected AccessStrategy(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as AccessStrategy;
            if (other == null || this.GetType() != other.GetType())
                return false;

            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
