<h3>CQRS ve MediatR</h3>

<p>Uygulamanın bu kısmında;
<b>1-</b> “Address”, “Note”, “NoteCategory” entity'lerinin database konfigürasyonları gerçekleştirilmiştir.<br>
<b>2-</b> User identity'si için birden fazla Address entity'sine sahip olabilecek şekilde bire-çok(one-to-many) ilişkisi verilmiştir.<br>
<b>3-</b> Note ile Category entity'leri arasında çoka-çok(many-to-many) ilişkisi verilmiştir. Yani bir not birden fazla kategoriye ve bir kategori birden fazla nota sahip olabilir.<br>
<b>4-</b> “Address” entity'si için “CQRS” yapısında Add, Update, Delete ve HardDelete Command’leri oluşturulmuştur.<br>
<b>5-</b> “Address” için “CQRS” yapısında GetById ve GetAll Query’leri oluşturulmuştur.<br>
"Address" için controller ayarları yapılıp bu işlemler api üzerinde gözlemlenmiştir.</p>
