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

# CAP Teoremi #
CAP dediğimiz teoremi 1998'de Eric Brewer tarafından ortaya atılmıştır.Teorem basit olarak dağıtık bir sistemde veri üzerinden sunulan hizmet için aynı anda 3 özelligin sağlanamayacağıdır. Yukarıdaki resimlerdende görüldüğü gibi tasarlardığınız sistem CA, CP veya AP olabilir. CAP olamaz.

Consistency(Tutarlılık): Dağıtık bir sistemde bir sunucuya x değeri olarak 5 yazdınız. Aynı sorguyu ağınızdaki başka sunucuya yaptınız ve cevap olarak 5 değerini aldınız. Bu tutarlılıktır.

Availability(Erişebilirlik): Dağıtık her zaman eriştiniz. Hem x değerini yazabildiniz, hemde okuyabildiniz.

Partition Tolarance (Bölünebilme Toleransı): Ağınızdaki sunucuların arasındaki bağlantı gittiğinde de çalışmaya devam edebilme.

CAP Teoremi ne diyor Aynı anda 3ünü birden yapamazsın diyor. Neden ? Bu durumu analiz edelim.

(CA) İse Neden (P) olamıyor
Sunucularınızın arasındaki ağ bağlantısı gitmiş. Siz gittiniz birinci sunucuya yazdınız, yukarıdaki resimde görüldüğü gibi diğer sunuculara verinin güncel halini gönderemedi. 2nci, 3ncü sunucuya veriyi sorduğunuzda verinin olmadığını veya 1nci sunucu ile aynı olamadığını göreceksiniz. Yani sistem CA olduğu zaman P(Partition Tolerance) olamıyor. Veriniz parçalanmaya toleranslı değil.
Verinin doğruluğunun kesin olmasını istediğiniz. Müşteri işlemleri, finansal işlemler vb.. bu çok önemlidir. İlişkisel veritabanları ve transactional işlemler buna uygun veriler tutar.

(AP) İse Neden (C) olamıyor
2 sunucunuz(server) olduğunu düşünelim X değeri 2 sunucuda da bulunuyor. Sunucular arası ağınız çöktü. Sunuculardan birinde X değerini güncellediniz. Sorguladığınızda iki sunucu ayrı değer vereceği için bu durumda C(Consistent) olamıyor. Bir sunucu X değeri için 5 , bir diğeri 4 değerini dönebilir.
Sosyal medyadan twitter veya facebook akan verilerde bunlar kullanılabilir. Like sayısını düşünün o anda her kişinin doğru şekilde görmesi/görmemesi önemli değildir. Burada bu veriler parçalanarak tutulabilir. NoSQL veritabanlarından Cassandra, Dynamo gibi sistemler buna uygundur. Eventual Consistency yöntemi ile arkaplanda Node’lardaki veriler eşleştirilir. Bu eşleştirmeden önce yapılan bir sorgularda veri değerlerinde tutarsızlıklar olabilir.

(CP) İse Neden (A) olamıyor
Eğer amacınız hem tutarlılık, hemde verinin parçalara bölünerek kaydedilebilmesi ise. 3 sunucu arasında bağlantı koptuğu andan itibaren A(Availibility) yazma özelliğiniz ortadan kalkacaktır. Eğer ki yazarsanız tutarlılığı bozarsınız. Bu yüzden sadece okuma yapabilirsiniz bu durumda.
MongoDB veritabanı gibi sistemler CP uygundur. Default’ta strongly consistent’ dır.

Tatmin edici performans değerlerine ulaşabilmek için sisteminizde iki tür ölçeklendirme yapabilirsiniz; Dikey ölçeklendirme (Vertical Scale Out) ve yatay ölçeklendirme (Horizontal Scale Out). Mesela; cihazınızın donanımlarını güçlendirdiğinizde (örneğin; bellek, işlemci vs. eklediğinizde) dikey ölçeklendirme yapmış olursunuz. Birden fazla cihazı aynı amaç uğruna kullanabilmek için birbirine bağladığınızda ise yatay ölçeklendirme yapmış olursunuz.

Yatay Ölçeklendirme ile Gelen Yeni Oyun Kuralı : CAP Teoremi

Eğer tek bir bilgisayarın performansı size yetmiyorsa. Özellikle geleneksel veritabanları ile çalışma konusunda bir engeliniz de yoksa tercihinizi birden fazla bilgisayarın görev aldığı yatay ölçeklendirmeden yana kullanabilirsiniz. Hatta bazen yatay ölçeklendirme bir zorunluluk haline gelir.

Yatay ölçeklendirme yapıldığında ortaya dağıtık sistemler çıkar. Dağıtık sistemler birden fazla birbirine bağlı düğümden (basitçe bilgisayar olarak kabul edebiliriz. Sanal makine, bir uygulama kurulumu vs. olabilir.) oluşan ve aralarında çeşitli kurallara göre verileri paylaşabilen bir ekosistem oluşturur. Bu ekosistemdeki temel kural da CAP teoremi ile ifade edilmektedir. CAP teoremi birden fazla düğümün iş birliği yaptığı sistemlerde ortaya çıkan durumları açıklar.

CAP teoremi der ki; Dağıtık sistemlerde aynı anda aşağıdaki üç özelliğin sağlanması mümkün değildir. 
Özellikler şunlar;
Consistency (Tutarlılık): Tüm düğümlerde aynı anda aynı veri görünür. Yani en son değişikliklerin tüm düğümlerde yerini alması garantidir.
Availability (Erişebilirlik - Müsait Olmak):  Her istek sistem tarafından cevaplanır. Yani sistem hatalı veya başarılı tüm taleplere beklendiği gibi cevap verir.
Partition Tolerance (Parça Toleransı - Parçanın Eksilmesine Tolerans): Bir kısmı zarar görse de sistem çalışmaya devam eder. Yani bazı düğümler çalışmasa da veya düğümler arası ağda bir hasar olsa da işler aksamaz. Tabi ki makul seviyede bir bozukluktan bahsediyoruz. Tüm sistemi çalışmaz hale getirecek hasarlar her zaman olasıdır.

Teorem gereği dağıtık sistemlerde bu özelliklerden aynı anda en fazla iki tanesi sağlanabilir. Bir tanesinden feragat edilir. Ortaya çıkan alternatif hallerden iş modelinize uygun olanını belirlemeniz 500'den fazla NoSQL veritabanı teknolojileri arasından size en uygun olanını bulmanızda büyük kolaylık sağlar. Mevcut sisteminizin iş modelinizle uyumlu olup olmadığını bir dereceye kadar bu teorem üzerinden sorgulayabilirsiniz.

CAP teoremi, dağıtık sistemlerdeki alternatif hallerin şunlar olduğunu bize ifade eder:

Consistency & Availability (CA): Tüm değişiklikler aynı anda tüm düğümlerde görünür ve sistem tüm isteklere cevap verir. Ancak bir şekilde düğümler zarar görürse sistem kitlenir. İstemciler bloklanır. Bütün RDBMS'ler bu gruba dahildir. Tabi ki bu tarz durumlar için bazı RDBMS teknolojilerinde sadece okumaya yönelik de olsa hizmet devam edebilmektedir. Çoğunlukla veri tutarlılığının her şeyden daha fazla ön planda olduğu sistemler için ideal durumdur. Mesela para transfer işlemleri böyledir.

Consistency & Partition Tolerance (CP): Bir kısmı zarar görse de sistem çalışmaya devam eder ve tüm düğümlerde aynı veri görünür. Tutarlı olmayan verinin gösterilmemesi için bazı verilere erişim sağlanamayabilir. Yani ‘Availability’den feragat edilir. Fakat erişilen veride tutarlılık korunur. Çoğunlukla veri yazılan iş modelleri için ideal durumdur. Örneğin bir cihaz veya uygulama hareketlerinin loglanması böyledir.

Availability & Partition Tolerance (AP): Bir kısmı zarar görse de sistem hizmet vermeye devam eder ve iş yüküne bağlı olarak değişen cevaplar istemciye iletilir. Güncel olmak veya tutarlılık garanti edilmez. Çoğunlukla veri okuma yapılan, tutarlılığın değil de erişilebilirliğin ön planda olduğu iş modelleri için ideal durumdur. Örneğin sosyal medya mesajlaşmaları böyledir.

CA ve CP modellerinde genellikle tüm talep ve takipten sorumlu bir master olur. AP modelinde ise genellikle master olmaz. Genellikle ifadesini kullanıyorum çünkü farklı mimari tasarımlar yapılabilmektedir.

Bir önceki yazımızda bahsettiğimiz ACID ve BASE isimli veri tutma yaklaşımları da CAP teoremi ile ilişkilidir. Ancak birlikte anlatıldığında kafa karışıklığına yol açabilmektedir. CAP teoremi dağıtık sistemlerin hangi alternatif durumlarda olabileceğini ifade eder. ACID ve BASE ise veritabanı sistemlerinin tutarlılığı sağlama konusundaki yaklaşım tarzını ifade eder. Tabiri caizse ACID tutarsızlık olmaması için çok pimpirikli davranıldığını anlatırken, BASE bu konuda oldukça rahat davranıldığını ifade eder. Dağıtık sistemlerin doğasında BASE yaklaşımı vardır.

CA -> Sql server, Mysql, Oracle,PostgreSQL, Diger RDBMS'ler
CP -> MongoDb, Redis, DocumentDb
AP -> Cassandra, CouchDb

- consistency: butun nodelarda aynı anda aynı data vardır, commit edilen data her node'da commitlenmistir.
- availability: sisteme yapılan butun requestlere cevap alınır. bir sunucu duyduğunda diğer sunucular hizmet verir.
- partition tolerance: sistemdeki nodelardan bazıları bir sekilde kendi aralarındaki iletişimi kaybederse (crash olur, network outage olur) sistem calismaya devam eder.


# ACID nedir ? #
ACID, işlem odaklı veri tabanı kurtarma ilkeleri olarak tasarlanmıştır. Dolayısıyla, verilerin bir çeşit başarısızlık sonucu bozulmamasını sağlamak için veri tabanı işlemlerinin(transactions) uyması gereken ilkeleri sağlar.

- Atomicity: Ya hep ya da hiç anlamına gelmektedir. Bir transaction (işlem) içinde bütün işlemler yapılır veya biri dahi gerçekleştirilemiyorsa diğer işlemler de gerçekleştirilmez.

- Consistency: Her bir işlemin gerçekleştirilmesi sonrasında alınan çıktı, girdi ve yapılan işlemler ile olan tutarlılığını, tanımlanan kurallara uygunluğunu ifade eder.

- Isolation: Bir transaction gerçekleştirilirken, transaction’ın çalışmış olduğu alana müdahale edilemeyeceğini ifade eder.

- Durability: Kullanıcıya, transaction’ın başarıyla gerçekleştirildiğini belirtmeden önce, gerçekleştirdiği işlemin ileri zamanda geri alınabilecek (recovery) şekilde loglanmasını ifade eder. İşlem sonucunda alınan “Başarılı” cevabının kesinliğine güvenilmelidir.

# BASE #
- ba -> basically available: parcasal hatalara okayiz. genelimiz uygun (available) olsun.
- s -> soft state: sistemde kirilganlik olabilir. sistemin state'i zaman icinde degisebilir.
- e(c) -> eventually consistency: bu sistem zamanla consistent bir hale gelecek zaten. her transaction sonrasi bunu kontrol etmek masrafli.

Basic Availability: Sistem CAP teoremine uygun olarak sürekli çalışır. Her bir talebe cevap verir. Fakat bu zorunluluk bir hata durumunda bile geçerli olduğu için veri tutarlılığını garanti etmez ve tüm veriye erişimi mümkün kılmaz. Yani bir nevi verinin bir kısmından feragat etmek suretiyle daha basit bir erişilebilirlik hizmeti almış olursunuz.

Soft State: Verileri yazılır ancak tutarlı olmayabilir. Bu developerin görevi olarak görünür. Ayrıca veriler tüm cihazlarda aynı şekilde görünmesi garanti edilmez. 

Eventualy Consistency: İşlemlerin etkileri sistemin durumuna bağlı olarak ancak bir süre sonra diğer cihazlara yansır. Yani neredeyse tutarlı bir sistem.

### nosql kategorileri asagidaki gibidir

- key-value store (bkz: redis) (bkz: dynomite)
- document store (bkz: mongodb) (bkz: couchdb)
- wide column store (bkz: apache hbase) (bkz: apache cassandra)
- graph database (bkz: neo4j)

# Online Transactional Processing, OLTP 
OLTP, sistemler genellikle ilişkisel veri tabanları üzerine kurulmuş, üzerinde sürekli işlem yapılan veri tabanı sistemleridir. Adından da anlaşılacağı gibi çok fazla transactional işlemler içeren yani Insert, Update, Delete (Data Manipulation Language, DML)  işlemleri içeren veri depolama sistemleridir.

OLTP sistemlerde sürekli yoğun işlemler yapılır. Sağlam bir ilişkisel yapı üzerine kurulmuştur, günlük hayatta kullanılan sistemlerin çoğu OLTP ürüne kurulu sistemlerdir.

OLTP sistemleri için temel vurgu, çok erişimli ortamlarda veri bütünlüğünü koruyan ve çok hızlı sorgu işleyebilmesi üzerinedir. Sorgu işleme hızı, saniye başına işlem sayısı ile ölçülen bir değerdir.

# Online Analytical Processing, OLAP 
Çevrimiçi Analitik İşleme (OLAP), geniş çaplı iş veri tabanlarını düzenlemek ve karar destek sistemini desteklemek için kullanılan bir teknolojidir. İlişkisel veri tabanının aksine veriyi tekrarlayarak depolayan, raporlama ve analiz için kullanılan, veriye hızlı erişim sağlayan yapılardır.

OLAP verileri hiyerarşik olarak da düzenlenir ve tablo yerine küplerde depolanır. Çözümlenecek verilere hızlı erişim sağlamak için çok boyutlu yapılar kullanan karmaşık bir teknolojidir.

### OLTP ve OLAP Sistemlerin Karşılaştırılması 
LTP ve OLAP sistemler birbiriyle iç içe çalışan ama bir o kadar da farklı özelliklere sahip olan sistemlerdir. Şimdi bu sistemler arasındaki ilişkiden bahsedelim.

OLAP sistemler veri kaynağı olarak genelde OLTP sistemleri kullanırlar. Bu ilişkiye günlük hayattan örnek vermek gerekirse, OLTP sistemler OLAP sistemler için yakıt görevini görür diyebiliriz.

- OLTP türü sistemler, her gün çok sayıda işleme, girdi-çıktıya ve güncellemeye uygun sistemlerdir. Fakat canlı sistemlerde karmaşık sorgulama işlemleri sistemde bir takım sorunlara ve yavaşlamalara yol açabilir. Bunun için OLAP sistemleri geliştirilmiştir.
- OLTP günlük operasyonel kullanım için uygun bir yöntem iken, OLAP arka planda uzun soluklu analizler için uygun bir yöntemdir.
- OLTP tipi sistemlerde OLAP’dan çok daha fazla sayıda detay barındırılmaktadır.
- OLTP üzerinde SQL sorguları kullanılırken, OLAP üzerinde ise SQL diline benzeyen ve MDX (Multi Dimensional eXpression Language) sorgulama dili kullanılır.
- OLTP sistemleri “Şu an  neler oluyor?” sorusuna cevap vermek için kullanabilecekken,   OLAP sistemleri “Gelecekte ne olacak?” veya “Geçmişte neler olmuş?” sorularına cevap vermek için kullanabiliriz.
