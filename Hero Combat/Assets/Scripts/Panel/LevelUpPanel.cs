using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    private PlayerData jugador;

    public void Mostrar(PlayerData player)
    {
        jugador = player;
        gameObject.SetActive(true);
    }

    public void ElegirFuerza()        { jugador.MejorarEstadistica(0); Cerrar(); }
    public void ElegirVelocidad()     { jugador.MejorarEstadistica(1); Cerrar(); }
    public void ElegirInteligencia()  { jugador.MejorarEstadistica(2); Cerrar(); }
    public void ElegirDestreza()      { jugador.MejorarEstadistica(3); Cerrar(); }
    public void ElegirResistencia()   { jugador.MejorarEstadistica(4); Cerrar(); }
    public void ElegirVidaMaxima()    { jugador.MejorarEstadistica(5); Cerrar(); }
    public void ElegirAleatorio()
    {
        int stat = Random.Range(0, 6); // 0 a 5, cubre todas las estadísticas
        jugador.MejorarEstadistica(stat);
        Cerrar();
    }
    
    private void Cerrar()
    {
        jugador.mejoraPendiente = false;
        gameObject.SetActive(false);
        jugador.ReiniciarStats();
        // Guarda el nuevo estado si quieres
        DatabaseManager dbm = FindObjectOfType<DatabaseManager>();
        if (dbm != null)
            dbm.GuardarJugador(jugador.ToPlayerDB());
    }
}