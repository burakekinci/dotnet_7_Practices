namespace Entities.DataTransferObjects
{
    [Serializable]
    public record BookDto(int Id, String Title, decimal Price);

    /*
    bu bir record tipinde olduğundan, normal book model'i gibi serialize edilemeyebilir ve postman'de request sonrası
    serialize hatası alınabilir. Bu durumu önlemek için serializable ekliyoruz. Fakat bu durumda da response'da şöyle bir şey 
    ile karşılaşılabilir. 
        <BookDto>
            <_x003C_Id_x003E_k__BackingField>1</_x003C_Id_x003E_k__BackingField>
            <_x003C_Price_x003E_k__BackingField>75.00</_x003C_Price_x003E_k__BackingField>
            <_x003C_Title_x003E_k__BackingField>Hacivat ve Karagöz</_x003C_Title_x003E_k__BackingField>
        </BookDto>
    bunun gibi böyle isimlerinde sorununu çözmek için record'ı şöyle yazabiliriz

    public record BookDto
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }
    }

    */

}
