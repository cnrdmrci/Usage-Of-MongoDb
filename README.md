# Mongo Db Kullanımı
https://mlab.com -> online 500mb free
nosqlclient.com

-Koleksiyonlari görme
show collections

-Belirli bir collection'da tüm verileri arama
db.books.find({})

-Verileri duzenli getirme
db.books.find({}).pretty()

-Database oluşturma
use BookStoreDb

-Database silme
db.dropDatabase()

-Database'leri gösterme
show databases
show dbs

-Collection oluşturma (Yada veri eklerken otomatik oluşturulur.)
db.createCollection('Books')

-Collection silme
db.COLLECTION_NAME.drop()

-Veri ekleme
db.COLLECTION_NAME.insert(document)
db.books.insert({title:"new title"})
db.books.find({"title":"new title"})

-Çoklu veri ekleme
db.Books.insertMany([
{'Name':'Design Patterns','Price':54.93,'Category':'Computers','Author':'Ralph Johnson'}, 
{'Name':'Clean Code','Price':43.15,'Category':'Computers','Author':'Robert C. Martin'}])

-Arama Query
db.COLLECTION_NAME.find()
db.COLLECTION_NAME.findOne()
db.COLLECTION_NAME.find().pretty()



Equality: 			
where by = 'tutorials point' 	--- 	db.mycol.find({"by":"tutorials point"}).pretty()

Less Than:			
where likes < 50				---		db.mycol.find({"likes":{$lt:50}}).pretty()

Less Than Equals:	
where likes <= 50				---		db.mycol.find({"likes":{$lte:50}}).pretty()

Greater Than:		
where likes > 50				---		db.mycol.find({"likes":{$gt:50}}).pretty()

Greater Than Equals:	
where likes >= 50				---		db.mycol.find({"likes":{$gte:50}}).pretty()

Not Equals:
where likes != 50				---		db.mycol.find({"likes":{$ne:50}}).pretty()

AND:
where key1 = value1 AND key2 = value2
db.mycol.find(
   {
      $and: [
         {key1: value1}, {key2:value2}
      ]
   }
).pretty()

OR:
where key1 = value1 OR key2 = value2
db.mycol.find(
   {
      $or: [
         {key1: value1}, {key2:value2}
      ]
   }
).pretty()

AND and OR Together:
where likes>10 AND (by = 'tutorials point' OR title = 'MongoDB Overview')
db.mycol.find({"likes": {$gt:10}, $or: [{"by": "tutorials point"},
   {"title": "MongoDB Overview"}]}).pretty()
{
   "_id": ObjectId(7df78ad8902c),
   "title": "MongoDB Overview", 
   "description": "MongoDB is no sql database",
   "by": "tutorials point",
   "url": "http://www.tutorialspoint.com",
   "tags": ["mongodb", "database", "NoSQL"],
   "likes": "100"
 }

IN:
where tags in ('mongodb','SQL')
db.mycol.find( { tags: { $in: [ "mongodb", "SQL" ] } } )


-Veri güncelleme
db.mycol.update({'title':'MongoDB Overview'},{$set:{'title':'New MongoDB Tutorial'}})
db.mycol.update({'title':'MongoDB Overview'},{$set:{'title':'New MongoDB Tutorial'}},{multi:true})

db.COLLECTION_NAME.save({_id:ObjectId(),NEW_DATA})
db.mycol.save(
   {
      "_id" : ObjectId(5983548781331adf45ec5), "title":"Tutorials Point New Topic",
      "by":"Tutorials Point"
   }
)

-Veri silme
db.COLLECTION_NAME.remove(DELLETION_CRITTERIA)
db.mycol.remove({'title':'MongoDB Overview'})

Remove Only One
db.COLLECTION_NAME.remove(DELETION_CRITERIA,1)

Remove All Documents
db.mycol.remove({})

-Projection (Sadece belirli alanlari al)
db.COLLECTION_NAME.find({},{KEY:1})
db.Books.find({},{"Name":1,"Price":1,_id:0})
{ "Name" : "Design Patterns", "Price" : 54.93 }
{ "Name" : "Clean Code", "Price" : 43.15 }

-Limit Record (Belirli sayida kayit getirme)
db.COLLECTION_NAME.find().limit(NUMBER)
db.Books.find({},{"Name":1,"Price":1,_id:0}).limit(1)
{ "Name" : "Design Patterns", "Price" : 54.93 }

db.COLLECTION_NAME.find().limit(NUMBER).skip(NUMBER)
db.Books.find({},{"Name":1,"Price":1,_id:0}).limit(1).skip(1)
{ "Name" : "Clean Code", "Price" : 43.15 }
the default value in skip() method is 0.

-Sorting Record (Verileri siralama)
db.COLLECTION_NAME.find().sort({KEY:1})
To specify sorting order 1 and -1 are used. 1 is used for ascending order while -1 is used for descending order.
db.Books.find({},{"Name":1,"Price":1,_id:0}).sort({"Price":-1, "Name" : -1})
{ "Name" : "Design Patterns", "Price" : 54.93 }
{ "Name" : "Clean Code", "Price" : 43.15 }

-Index olusturma
To create an index you need to use ensureIndex() method of MongoDB.
db.COLLECTION_NAME.ensureIndex({KEY:1})
Here key is the name of the field on which you want to create index and 1 is for ascending order. 
To create index in descending order you need to use -1.
db.mycol.ensureIndex({"title":1})
db.mycol.ensureIndex({"title":1,"description":-1})

-Aggregation
Aggregations operations process data records and return computed results.
db.COLLECTION_NAME.aggregate(AGGREGATE_OPERATION)

db.Books.aggregate([{$group : {_id: "$Name",num_name :{$sum :1}}}])
{ "_id" : "Clean Code", "num_name" : 1 }
{ "_id" : "Design Patterns", "num_name" : 1 }
db.Books.aggregate([{$group : {_id: "$Category",num_category :{$sum :1}}}])
{ "_id" : "Computers", "num_category" : 2 }

$sum		db.mycol.aggregate([{$group : {_id : "$by_user", num_tutorial : {$sum : "$likes"}}}])
$avg		db.mycol.aggregate([{$group : {_id : "$by_user", num_tutorial : {$avg : "$likes"}}}])
$min 		db.mycol.aggregate([{$group : {_id : "$by_user", num_tutorial : {$min : "$likes"}}}])
$max		db.mycol.aggregate([{$group : {_id : "$by_user", num_tutorial : {$max : "$likes"}}}])
$push 		db.mycol.aggregate([{$group : {_id : "$by_user", url : {$push: "$url"}}}])
$addToSet 	db.mycol.aggregate([{$group : {_id : "$by_user", url : {$addToSet : "$url"}}}])
$first		db.mycol.aggregate([{$group : {_id : "$by_user", first_url : {$first : "$url"}}}])
$last 		db.mycol.aggregate([{$group : {_id : "$by_user", last_url : {$last : "$url"}}}])

Pipeline Concept
$project − Used to select some specific fields from a collection.
$match − This is a filtering operation and thus this can reduce the amount of documents that are given as input to the next stage.
$group − This does the actual aggregation as discussed above.
$sort − Sorts the documents.
$skip − With this, it is possible to skip forward in the list of documents for a given amount of documents.
$limit − This limits the amount of documents to look at, by the given number starting from the current positions.
$unwind − This is used to unwind document that are using arrays. When using an array, the data is kind of pre-joined and this operation will be undone with this to have individual documents again. Thus with this stage we will increase the amount of documents for the next stage.


# Relationship #

//----------------------------------------------------
One-to-One Relationship

db.users.insert(
    {
        _id : 2,
        username : "cnrdmrci",
        address :   {
                        street : "AAA",
                        city : "BBB",
                        state : "CCC",
                        country : "DDD"
                    }
    }
)

//----------------------------------------------------

One-to-Many Relationship

db.users.insert(
    {
        _id : 3,
        username : "caner",
        books : [
                    {
                        bookname : "aaa",
                        releaseyear : 2001
                    }, 
                    {
                        bookname : "bbb",
                        releaseyear : 2002
                    }
                ]
    }
)

//----------------------------------------------------

Document Referenced Relationships

-Parent Document
db.users.insert(
    {
        _id : 2,
        username : "caner"
    }
)

-Child Documents
db.books.insert(
    {
        _id : 9,
        name : "book1",
        story : [ "horror", "comedy", "funny" ],
        user_id : 2
    }
)
db.books.insert(
    {
        _id : 10,
        name : "book2",
        instrument : [ "horror", "funny" ],
        user_id : 2
    }
)

-Querying the Relationship
db.users.aggregate([
    {
      $lookup:
        {
          from: "books",
          localField: "_id",
          foreignField: "user_id",
          as: "members"
        }
   },
   { $match : { username : "caner" } }
]).pretty()

{
	"_id" : 2,
	"username" : "caner",
	"members" : [
		{
			"_id" : 9,
			name : "book1",
        	story : [ 
        		"horror", 
        		"comedy", 
        		"funny" 
        	]
			"user_id" : 2
		},
		{
			"_id" : 10,
			"name" : "book2",
			"story" : [
				"horror", 
        		"comedy"
			],
			"user_id" : 2
		}
	]
}

db.users.find( { username : "caner" } )
{ "_id" : 2, "username" : "caner" }

Embedded biçim daha hızlı işlem yapılmasını sağlar.
Update işlemlerinin atomik olmasını sağlar.
Ancak, gömülü ilişkiler tüm durumlar için uygun değildir. Belge referanslı bir ilişki oluşturmanın daha anlamlı olduğu durumlar olabilir.
Bir document boyutu en fazla 16 mb olabilir.
Bir belge birçok yerde tekrarlanıyorsa referans tipinde tanımlamak mantıklı olabilir.
Sonrasında yapılacak işlemlerin atomik olmayacağını göz önünde bulundurarak.

//----------------------------------------------------

