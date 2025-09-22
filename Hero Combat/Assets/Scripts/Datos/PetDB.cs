using SQLite4Unity3d;

[Table("Pet")]
public class PetDB
{
    [PrimaryKey, AutoIncrement]
    public int PetID { get; set; }
    public string Nombre { get; set; }
    public int Vida { get; set; }
    public int Fuerza { get; set; }
    public int Destreza { get; set; }
    public float ChanceDeAtacar { get; set; }
}
