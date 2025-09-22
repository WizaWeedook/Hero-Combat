using SQLite4Unity3d;

[Table("Passive")]
public class PassiveDB
{
    [PrimaryKey, AutoIncrement]
    public int PassiveID { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
}