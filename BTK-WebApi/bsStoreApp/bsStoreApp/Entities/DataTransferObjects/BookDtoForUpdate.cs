namespace Entities.DataTransferObjects
{
    /// <summary>
    /// DTO'lar Hakkında Genel Bilgi
    /// DTO'lar readonly ve immutable(içeriği değişmeyen) yapılardır
    /// Referans Type'dır(Classlar gibi), (Struct Value Type'dır)
    /// </summary>


    public record BookDtoForUpdate(int Id, String Title, decimal Price);

    //public record BookDtoForUpdate
    //{
    //    //init ile initialize edilirken değerini set eder
    //    //ve initialize'dan sonra değeri değiştirilemez
    //    public int Id { get; init; }
    //    public String Title { get; init; }
    //    public decimal Price { get; init; }
    //}

    //Record yapısını şu şekilde de kısaca  ctor gibi tanımlayabiliriz
    //
    // public record BookDtoForUpdate(int Id, String Title, decimal Price);

    //burada da aslında property'ler otomatik olarak init tipinde tanımlanıyor
}
