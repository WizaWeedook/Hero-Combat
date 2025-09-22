using SQLite4Unity3d;

[Table("Spell")]
public class SpellDB
{
    [PrimaryKey, AutoIncrement]
    public int SpellID { get; set; }
    public string Nombre { get; set; }
    public int PoderBase { get; set; }
    public string Descripcion { get; set; }
}