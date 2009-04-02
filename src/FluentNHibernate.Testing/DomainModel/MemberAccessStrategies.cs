using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentNHibernate.Testing.DomainModel
{
    internal class EntityWithFields
    {
        public int IDField = 0;
        public string StringField = null;
    }

    internal class EntityWithPublicGettersAndPrivateFields
    {
        private EntityWithPublicGettersAndPrivateFields()
        {
            
        }

        public EntityWithPublicGettersAndPrivateFields(string val1, string val2, string val3, string val4, string val5, string val6)
        {
            fieldIsCamelCase = val1;
            _fieldIsCamelcaseUnderscore = val2;            
            fieldislowercase = val3;
            _fieldislowercaseunderscore = val4;
            m_FieldIsPascalCaseMUnderscore = val5;
            _FieldIsPascalCaseUnderscore = val6;
        }

        public int IDField = 0;

        public string StringProperty { get; set; }
        public string StringField = null;

        private string fieldIsCamelCase = null;
        public string FieldIsCamelCase
        {
            get { return fieldIsCamelCase; }
        }

        private string _fieldIsCamelcaseUnderscore = null;
        public string FieldIsCamelcaseUnderscore
        {
            get { return _fieldIsCamelcaseUnderscore; }
        }

        private string fieldislowercase = null;
        public string FieldIsLowerCase
        {
            get { return fieldislowercase; }
        }

        private string _fieldislowercaseunderscore = null;
        public string FieldIsLowerCaseUnderscore
        {
            get { return _fieldislowercaseunderscore; }
        }

        private string m_FieldIsPascalCaseMUnderscore = null;
        public string FieldIsPascalCaseMUnderscore
        {
            get { return m_FieldIsPascalCaseMUnderscore; }
        }

        private string _FieldIsPascalCaseUnderscore = null;
        public string FieldIsPascalCaseUnderscore
        {
            get { return _FieldIsPascalCaseUnderscore; }
        }

    }
}
