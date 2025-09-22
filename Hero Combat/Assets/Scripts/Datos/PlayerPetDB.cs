using SQLite4Unity3d;

[Table("PlayerPet")]
public class PlayerPetDB
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int PlayerID { get; set; }
    public int PetID { get; set; }
}