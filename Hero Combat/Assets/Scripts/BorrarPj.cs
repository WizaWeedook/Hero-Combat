using UnityEngine;

public class BorrarPj : MonoBehaviour
{
    public NombreJugadorPanel panelNombreJugador; // Arrastrar desde inspector

    public void BorrarJugador()
    {
        var jugador = GameManager.Instance.player;

        if (jugador != null)
        {
            DatabaseManager.Instance.BorrarJugador(jugador.jugadorID);
            GameManager.Instance.player = null;
            Debug.Log("Jugador borrado correctamente.");
        }
        else
        {
            Debug.Log("No hay jugador cargado para borrar.");
        }

        // Muestra el panel para ingresar un nuevo nombre
        if (panelNombreJugador != null)
            panelNombreJugador.Mostrar();
        else
            Debug.LogWarning("NombreJugadorPanel no asignado en BorrarPj!");
    }
}
