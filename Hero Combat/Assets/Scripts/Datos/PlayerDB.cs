using SQLite4Unity3d;

[Table("Player")]
public class PlayerDB {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Nombre { get; set; }
    public int Nivel { get; set; }
    public int Experiencia { get; set; }

    public int Vida { get; set; }
    public int Fuerza { get; set; }
    public int Velocidad { get; set; }
    public int Inteligencia { get; set; }
    public int Destreza { get; set; }
    public int Resistencia { get; set; }
}