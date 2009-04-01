using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.FluentInterface.AutoMap
{
    public class IdAutoMapper : IAutoMapper
    {
        public void Map(ClassMapping classMap)
        {
            var idProperty = classMap.Type.GetProperty("Id");
            var columnMapping = new ColumnMapping { Name = "Id", MappedMember = idProperty };
            var mapping = new IdMapping(columnMapping)
                              {
                                  Name = "Id",
                                  Generator = new IdGeneratorMapping()
                              };
            mapping.BindToMember(idProperty);
            classMap.Id = mapping;
        }
    }
}