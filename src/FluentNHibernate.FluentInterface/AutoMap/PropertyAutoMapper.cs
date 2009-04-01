using FluentNHibernate.MappingModel;

namespace FluentNHibernate.FluentInterface.AutoMap
{
    public class PropertyAutoMapper : IAutoMapper
    {
        public void Map(ClassMapping classMap)
        {
            foreach(var property in classMap.Type.GetProperties())
            {
                if (!classMap.HasMappedProperty(property))
                {
                    if (property.PropertyType.Namespace == "System")
                    {
                        var propMapping = new PropertyMapping {Name = property.Name};
                        propMapping.BindToMember(property);

                        classMap.AddProperty(propMapping);
                    }
                }
            }
        }
    }
}