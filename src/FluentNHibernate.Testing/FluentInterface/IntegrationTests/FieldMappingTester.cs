using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.FluentInterface;
using NUnit.Framework;

namespace FluentNHibernate.Testing.FluentInterface.IntegrationTests
{
    [TestFixture]
    public class FieldMappingTester
    {
        private class EntityWithFields
        {
            public int IDField;
            public string StringField;
        }

        private class EntityWithFieldsMap : ClassMap<EntityWithFields>
        {
            public EntityWithFieldsMap()
            {
                Id(x => x.IDField);
                Map(x => x.StringField);
            }
        }


        [Test]
        public void Should_save_entity_with_mapped_fields()
        {
            var helper = new IntegrationTestHelper();
            helper.PersistenceModel.Add(new EntityWithFieldsMap());

            helper.Execute(session =>
                {
                    var entity  = new EntityWithFields();
                    entity.StringField = "this is a field";

                    session.Save(entity);
                });
        }

    }
}