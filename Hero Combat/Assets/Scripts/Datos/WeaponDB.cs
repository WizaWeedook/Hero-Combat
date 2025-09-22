using SQLite4Unity3d;

[Table("Weapon")]
public class WeaponDB
{
    [PrimaryKey, AutoIncrement]
    public int WeaponID { get; set; }
    public string Nombre { get; set; }
    public string Rareza { get; set; }
    public int DañoBase { get; set; }
    public string Tipo { get; set; }
    public float VelocidadGolpe { get; set; }
    public float ChanceAgarrada { get; set; }
    public float Alcance { get; set; }
}