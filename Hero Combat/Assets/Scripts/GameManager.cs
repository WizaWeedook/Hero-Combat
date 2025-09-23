using UnityEngine;
using HeroCombat.Datos;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerData player;
    public PlayerData player2; // enemigo o segundo jugador


    [Header("Panel de nombre")]
    public NombreJugadorPanel nombreJugadorPanel;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CargarJugadorOMostrarPanel();
    }

    void CargarJugadorOMostrarPanel()
    {
        if (DatabaseManager.Instance != null && DatabaseManager.Instance.TieneJugadorGuardado())
        {
            string nombreGuardado = DatabaseManager.Instance.ObtenerNombreJugadorGuardado();
            if (!string.IsNullOrEmpty(nombreGuardado))
            {
                PlayerDB dbPlayer = DatabaseManager.Instance.CargarJugador(nombreGuardado);
                if (dbPlayer != null)
                {
                    player = new PlayerData(dbPlayer);
                    return; // jugador cargado correctamente
                }
            }
        }

        // No hay jugador guardado → mostrar panel de nombre
        if (nombreJugadorPanel != null)
            nombreJugadorPanel.gameObject.SetActive(true);
        else
            Debug.LogWarning("NombreJugadorPanel no asignado en GameManager!");
    }

    // Llamado desde el panel de NombreJugadorPanel
    public void CrearNuevoJugador(string nombre)
    {
        if (string.IsNullOrEmpty(nombre)) return;

        player = new PlayerData(nombre);

        // Guardar en DB
        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.GuardarJugador(player.ToPlayerDB());
        }

        // Ocultar panel de nombre
        if (nombreJugadorPanel != null)
            nombreJugadorPanel.gameObject.SetActive(false);
    }

    // Método para borrar jugador
    public void BorrarJugador()
    {
        if (player != null && DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.BorrarJugador(player.jugadorID);
        }

        player = null;

        // Mostrar panel de ingreso de nombre nuevamente
        if (nombreJugadorPanel != null)
            nombreJugadorPanel.gameObject.SetActive(true);
    }
}

