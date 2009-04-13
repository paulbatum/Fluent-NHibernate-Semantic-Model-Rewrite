namespace FluentNHibernate.MappingModel
{
    public class ReferenceCascadeType
    {
        private readonly string _value;

        private ReferenceCascadeType(string value)
        {
            _value = value;
        }

        public static readonly ReferenceCascadeType None = new ReferenceCascadeType("none");
        public static readonly ReferenceCascadeType All = new ReferenceCascadeType("all");
        public static readonly ReferenceCascadeType SaveUpdate = new ReferenceCascadeType("save-update");
        public static readonly ReferenceCascadeType Delete = new ReferenceCascadeType("delete");

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ReferenceCascadeType;
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