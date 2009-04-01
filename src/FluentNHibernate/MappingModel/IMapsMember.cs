using System.Reflection;

namespace FluentNHibernate.MappingModel
{
    public interface IMapsMember
    {
        MemberInfo MemberInfo { get; set; }
    }
}