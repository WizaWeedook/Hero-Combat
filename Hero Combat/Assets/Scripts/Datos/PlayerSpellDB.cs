using SQLite4Unity3d;

[Table("PlayerSpell")]
public class PlayerSpellDB
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int PlayerID { get; set; }
    public int SpellID { get; set; }
}