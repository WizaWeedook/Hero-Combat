using SQLite4Unity3d;

[Table("PlayerPassive")]
public class PlayerPassiveDB
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int PlayerID { get; set; }
    public int PassiveID { get; set; }
}