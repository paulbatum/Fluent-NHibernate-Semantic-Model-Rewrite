using System;
using NHibernate.Cfg.MappingSchema;

namespace FluentNHibernate.MappingModel.Output
{

    public interface IHbmWriter<T> 
    {
        object Write(T mappingModel);        
    }    

}