using NHibernate.Dialect;
using NHibernate.Driver;

namespace FluentNHibernate.Cfg
{
	public class SQLiteConfiguration : PersistenceConfiguration<SQLiteConfiguration>
	{
		public SQLiteConfiguration()
		{
			Driver<SQLite20Driver>();
			Dialect<SQLiteDialect>();
			Raw("query.substitutions", "true=1;false=0");  
		}

		public SQLiteConfiguration InMemory()
		{
            Raw("connection.release_mode", "on_close");
			return ConnectionString.Is("Data Source=:memory:;Version=3;New=True;");
            
		}

		public SQLiteConfiguration UsingFile(string fileName)
		{
			return ConnectionString.Is(string.Format("Data Source={0};Version=3;New=True;", fileName));
		}
	}
}