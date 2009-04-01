using System.Reflection;

namespace FluentNHibernate.MappingModel
{
    public interface IMapsMember
    {
        MemberInfo MappedMember { get; }
        MemberAccess MemberAccess { get; set; }
        void BindToMember(MemberInfo member);
    }
}