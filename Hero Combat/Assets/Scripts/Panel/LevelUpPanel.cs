using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    public CharacterUI characterUI; // arrastra el objeto CharacterUI en el inspector

    public void Mostrar()
    {
        gameObject.SetActive(true);
    }

    public void ElegirAleatorio()
    {
        PlayerData jugador = GameManager.Instance.player;

        if (jugador != null)
        {
            int stat = Random.Range(0, 6); // 0 a 5
            jugador.MejorarEstadistica(stat);

            // Guardar progreso en DB
            DatabaseManager.Instance.GuardarJugador(jugador.ToPlayerDB());

            // Refrescar stats en la UI
            if (characterUI != null)
                characterUI.ActualizarUI();

            // Cerrar panel
            jugador.mejoraPendiente = false;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("No se encontró jugador en GameManager.");
        }
        
    }

    public void Cerrar()
    {
        PlayerData jugador = GameManager.Instance.player;
        if (jugador != null)
            jugador.mejoraPendiente = false;

        gameObject.SetActive(false);
    }
}
