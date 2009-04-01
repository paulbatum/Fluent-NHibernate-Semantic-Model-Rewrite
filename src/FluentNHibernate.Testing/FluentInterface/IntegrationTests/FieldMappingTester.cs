using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.FluentInterface;
using FluentNHibernate.MappingModel.Conventions;
using FluentNHibernate.Testing.MappingModel;
using NUnit.Framework;

namespace FluentNHibernate.Testing.FluentInterface.IntegrationTests
{
    [TestFixture]
    public class FieldMappingTester
    {
        [Test]
        public void EntityWithFieldsMap_should_be_valid_against_schema()
        {
            var model = new PersistenceModel();
            model.Add(new EntityWithFieldsMap());

            model.AddConvention(new NamingConvention());
            var hibernateMapping = model.BuildHibernateMapping();
            model.ApplyVisitors(hibernateMapping);
            hibernateMapping.ShouldBeValidAgainstSchema();
        }

        [Test]
        public void Should_save_entity_with_mapped_fields()
        {
            var helper = new IntegrationTestHelper();
            helper.PersistenceModel.Add(new EntityWithFieldsMap());

            helper.Execute(session =>
                {
                    var entity = new EntityWithFields();
                    entity.StringField = "this is a field";

                    session.Save(entity);
                    
                    int id = entity.IDField;
                    session.Clear();

                    var entityAgain = session.Load<EntityWithFields>(id);
                    entityAgain.StringField.ShouldEqual("this is a field");
                    
                });
        }

        private class EntityWithFieldsMap : ClassMap<EntityWithFields>
        {
            public EntityWithFieldsMap()
            {
                Id(x => x.IDField);
                Map(x => x.StringField);
            }
        }

    }

    internal class EntityWithFields
    {
        public int IDField;
        public string StringField;
    }
}