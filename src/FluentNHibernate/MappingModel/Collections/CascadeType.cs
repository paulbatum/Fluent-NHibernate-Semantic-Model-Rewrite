namespace FluentNHibernate.MappingModel.Collections
{
    public class CascadeType
    {
        private readonly string _value;

        private CascadeType(string value)
        {
            _value = value;
        }

        public static readonly CascadeType None = new CascadeType("none");
        public static readonly CascadeType All = new CascadeType("all");
        public static readonly CascadeType SaveUpdate = new CascadeType("save-update");
        public static readonly CascadeType Delete = new CascadeType("delete");

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CascadeType;
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