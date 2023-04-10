<h3>CQRS and MediatR</h3>

<p>Bu projede;
1- “Address”, “Note”, “NoteCategory” entity'lerinin database konfigürasyonları gerçekleştirilmiştir.
2- User identity'si için birden fazla Address entity'sine sahip olabilecek şekilde bire-çok(one-to-many) ilişkisi verilmiştir.
3- Note ile Category entity'leri arasında çoka-çok(many-to-many) ilişkisi verilmiştir. Yani bir not birden fazla kategoriye ve bir kategori birden fazla nota sahip olabilir.
4- “Address” entity'si için “CQRS” yapısında Add, Update, Delete ve HardDelete Command’leri oluşturulmuştur.
5- “Address” için “CQRS” yapısında GetById ve GetAll Query’leri oluşturulmuştur.
"Address" için controller ayarları yapılıp bu işlemler api üzerinde gözlemlenmiştir.</p>
