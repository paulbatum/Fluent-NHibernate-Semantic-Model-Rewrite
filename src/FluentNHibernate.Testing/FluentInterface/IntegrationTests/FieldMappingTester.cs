using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.FluentInterface;
using FluentNHibernate.MappingModel.Conventions;
using FluentNHibernate.Testing.DomainModel;
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

        [Test]
        public void EntityWithPublicGettersAndPrivateFieldsMap_should_be_valid_against_schema()
        {
            var model = new PersistenceModel();
            model.Add(new EntityWithPublicGettersAndPrivateFieldsMap());

            model.AddConvention(new NamingConvention());
            var hibernateMapping = model.BuildHibernateMapping();
            model.ApplyVisitors(hibernateMapping);
            hibernateMapping.ShouldBeValidAgainstSchema();
        }

        [Test]
        public void Should_automatically_map_to_the_correct_private_members()
        {
            var helper = new IntegrationTestHelper();
            helper.PersistenceModel.Add(new EntityWithPublicGettersAndPrivateFieldsMap());

            helper.Execute(session =>
            {
                var entity = new EntityWithPublicGettersAndPrivateFields("1", "2", "3", "4", "5", "6");
                session.Save(entity);

                int id = entity.IDField;
                session.Clear();

                var entityAgain = session.Load<EntityWithPublicGettersAndPrivateFields>(id);
                entityAgain.FieldIsCamelCase.ShouldEqual("1");
                entityAgain.FieldIsCamelcaseUnderscore.ShouldEqual("2");
                entityAgain.FieldIsLowerCase.ShouldEqual("3");
                entityAgain.FieldIsLowerCaseUnderscore.ShouldEqual("4");
                entityAgain.FieldIsPascalCaseMUnderscore.ShouldEqual("5");
                entityAgain.FieldIsPascalCaseUnderscore.ShouldEqual("6");

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

        private class EntityWithPublicGettersAndPrivateFieldsMap : ClassMap<EntityWithPublicGettersAndPrivateFields>
        {
            public EntityWithPublicGettersAndPrivateFieldsMap()
            {
                Id(x => x.IDField);
                Map(x => x.FieldIsCamelCase);
                Map(x => x.FieldIsCamelcaseUnderscore);
                Map(x => x.FieldIsLowerCase);
                Map(x => x.FieldIsLowerCaseUnderscore);
                Map(x => x.FieldIsPascalCaseMUnderscore);
                Map(x => x.FieldIsPascalCaseUnderscore);                   
            }
        }

    }



}