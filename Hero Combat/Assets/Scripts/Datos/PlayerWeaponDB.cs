using SQLite4Unity3d;

[Table("PlayerWeapon")]
public class PlayerWeaponDB
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int PlayerID { get; set; }
    public int WeaponID { get; set; }
}