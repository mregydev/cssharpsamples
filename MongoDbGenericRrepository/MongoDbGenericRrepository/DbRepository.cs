using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbGenericRrepository
{

    public class DbRepository<T> where T : class
    {
        private IMongoDatabase _db;

        public DbRepository(string serviceUrl,string dbName)
        {
            var client=new MongoClient(serviceUrl);

            this._db=client.GetDatabase(dbName);
        }

        public List<T> GetAll()
        {
            
            var entityType = typeof(T);

            var collection = this._db.GetCollection<BsonDocument>(entityType.Name).Find(new BsonDocument()).ToList();

            var list = new List<T>();

            foreach (var item in collection)
            {
                list.Add(Activator.CreateInstance<T>());

                foreach (var elem in item.Elements)
                {
                    if (elem.Value.GetType() == typeof(BsonObjectId) || elem.Value.GetType() == typeof(BsonString))
                    {
                        entityType.GetProperty(elem.Name).SetValue(list.Last(), elem.Value.ToString());
                    }
                    else if (elem.Value.GetType() == typeof(Int32))
                    {
                        entityType.GetProperty(elem.Name).SetValue(list.Last(), int.Parse(elem.Value.ToString()));
                    }
                }
            }

            return list;
        }
        public bool AddEntity(T entity)
        {
            try
            {
                var properties = entity.GetType().GetProperties();

                var dic = new Dictionary<string, object>();


                foreach (var property in properties)
                {
                    dic.Add(property.Name, property.GetValue(entity));
                }


                var document = new BsonDocument(dic);

                this._db.GetCollection<BsonDocument>(entity.GetType().Name).InsertOne(document);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateEntity(T entity)
        {
            try
            {
                var entityType = typeof(T);

                var properties = entityType.GetProperties();


                var dic = new Dictionary<string, object>();


                foreach (var property in properties)
                {
                    dic.Add(property.Name, property.GetValue(entity));
                }

                var document = new BsonDocument(dic);


                this._db.GetCollection<BsonDocument>(entityType.Name).ReplaceOne(new BsonDocument { { "_id", document.GetValue("_id") } }, document);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReomveEntity(string id)
        {
            try
            {
                var client = new MongoClient(ConfigurationSettings.AppSettings["mongoServiceUrl"]);

                var db = client.GetDatabase(ConfigurationSettings.AppSettings["mongoDataBase"]);

                var entityType = typeof(T);

                var properties = entityType.GetProperties();


                var document = new BsonDocument { { "_id", id } };

                this._db.GetCollection<BsonDocument>(entityType.Name).DeleteOne(document);

                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
