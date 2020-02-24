using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Database
    {
        public static bool Upsert<T>(T data)
        {
            using (var db = new LiteDatabase(@"./SerseDioRe.db"))
            {
                return db.GetCollection<T>().Upsert(data);
            }
        }

        public static T GetData<T>(string fieldName, BsonValue data)
        {
            using (var db = new LiteDatabase(@"./SerseDioRe.db"))
            {
                return db.GetCollection<T>().FindOne(Query.EQ(fieldName, data));
            }
        }

        public static bool Update<T>(T data)
        {
            using (var db = new LiteDatabase(@"./SerseDioRe.db"))
            {
                return db.GetCollection<T>().Update(data);
            }
        }

        public static T GetById<T>(int id)
        {
            using (var db = new LiteDatabase(@"./SerseDioRe.db"))
            {
                return db.GetCollection<T>().FindById(id);
            }
        }
    }
}
