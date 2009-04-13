namespace FluentNHibernate.MappingModel.Collections
{
    public class CollectionCascadeType
    {
        private readonly string _value;

        private CollectionCascadeType(string value)
        {
            _value = value;
        }

        public static readonly CollectionCascadeType None = new CollectionCascadeType("none");
        public static readonly CollectionCascadeType All = new CollectionCascadeType("all");
        public static readonly CollectionCascadeType SaveUpdate = new CollectionCascadeType("save-update");
        public static readonly CollectionCascadeType Delete = new CollectionCascadeType("delete");
        public static readonly CollectionCascadeType AllDeleteOrphan = new CollectionCascadeType("all-delete-orphan");

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CollectionCascadeType;
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