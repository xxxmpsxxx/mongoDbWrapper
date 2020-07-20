using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBWrapper.Helpers;
using MongoDBWrapper.Models;
using System;

namespace MongoDBWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new MongDBHelper("YOUR_CONNECTION_STRING", "EVO");

            var c1 = new Clinica()
            {
                Category = "Categoria1",
                Author = "Autor",
                BookName = "Nome do Livro",
                Price = 19.99M
            };

            var result = helper.Insert<Clinica>("Clinicas", c1);

            Console.WriteLine($"Incluido: {result}");

            var selected = helper.Select<Clinica>("Clinicas", null);

            var filter = Builders.FilterEq("_id", selected[0].Id);
            var res = helper.SelectOne<Clinica>("Clinicas", filter);

            Console.WriteLine("Gravou os seguintes dados:");
            Console.WriteLine($"{nameof(res.Author)}: {res.Author}");
            Console.WriteLine($"{nameof(res.BookName)}: {res.BookName}");
            Console.WriteLine($"{nameof(res.Category)}: {res.Category}");
            Console.WriteLine($"{nameof(res.Price)}: {res.Price}");

            res.Author = "Marcus Paulo da Silva Augusto OO";
            res.Price = 22.75M;
            res.BookName = "O novo nome do livro";

            var filterUpd = Builders.FilterEq("_id", selected[0].Id);
            //var updDef = Builders.Update<string>("Author", res.Author);
            //var updDef = Builders<Clinica>.Update.Set(c => c.Author, res.Author).
            //                                      Set(c => c.BookName, res.BookName).
            //                                      Set(c => c.Category, res.Category).
            //                                      Set(c => c.Price, res.Price);
            var updDef = Builders<BsonDocument>.Update.Set(nameof(res.Author), res.Author).
                                                       Set(nameof(res.BookName), res.BookName).
                                                       Set(nameof(res.Category), res.Category).
                                                       Set(nameof(res.Price), res.Price);
            var upd = helper.UpdateOne("Clinicas", filterUpd, updDef);

            Console.WriteLine("Alterou para os seguintes dados:");
            Console.WriteLine($"{nameof(res.Author)}: {res.Author}");
            Console.WriteLine($"{nameof(res.BookName)}: {res.BookName}");
            Console.WriteLine($"{nameof(res.Category)}: {res.Category}");
            Console.WriteLine($"{nameof(res.Price)}: {res.Price}");

            var count = helper.SelectCount("Clinicas");

            Console.WriteLine($"Qtde: {count}");

            Console.WriteLine($"Removendo o registro incluido de id: {res.Id}");

            var filterDel = Builders.FilterEq("_id", res.Id);
            helper.DeleteOne("Clinicas", filterDel);

            count = helper.SelectCount("Clinicas");

            Console.WriteLine($"Qtde atual: {count}");

            Console.WriteLine("Hello World!");
        }
    }
}
