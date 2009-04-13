using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.FluentInterface;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Reflection;
using FluentNHibernate.Testing.DomainModel;
using NUnit.Framework;

namespace FluentNHibernate.Testing.FluentInterface
{
    [TestFixture]
    public class OneToManyPartTester
    {
        [Test]
        public void Can_get_cascade_part()
        {
            var oneToManyPart = new OneToManyPart<Artist, Album>(null);
            oneToManyPart.Cascade.ShouldNotBeNull();
        }
    }
}
