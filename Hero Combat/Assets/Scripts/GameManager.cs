using System.IO;
using UnityEngine;
using TMPro;
using SQLite4Unity3d;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Jugador")]
    public PlayerData player;        // Jugador principal
    public PlayerData player2;       // Enemigo de ejemplo

    [Header("UI")]
    public GameObject panelNombreJugador; // Panel para ingresar nombre
    public TMP_InputField inputNombre;    // Campo de texto para el nombre

    void Awake()
    {
        // Singleton persistente
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Inicializar DB
            if (DatabaseManager.Instance == null)
            {
                GameObject dbGO = new GameObject("DatabaseManager");
                dbGO.AddComponent<DatabaseManager>();
            }

            InicializarJugador();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InicializarJugador()
    {
        // Revisar PlayerPrefs
        string nombreGuardado = PlayerPrefs.GetString("JugadorID", null);

        if (!string.IsNullOrEmpty(nombreGuardado))
        {
            var dbPlayer = DatabaseManager.Instance.CargarJugador(nombreGuardado);

            if (dbPlayer != null)
            {
                player = new PlayerData();
                player.FromPlayerDB(dbPlayer);

                // Ocultar panel de nombre
                if (panelNombreJugador != null)
                    panelNombreJugador.SetActive(false);

                return;
            }
        }

        // No existe jugador, activar panel
        if (panelNombreJugador != null)
            panelNombreJugador.SetActive(true);
    }

    // Llamada desde el botón "Crear/Confirmar nombre"
    public void CrearNuevoJugador()
    {
        if (string.IsNullOrEmpty(inputNombre.text))
            return;

        string nombre = inputNombre.text;
        player = new PlayerData(nombre);

        // Guardar PlayerPrefs y DB
        PlayerPrefs.SetString("JugadorID", nombre);
        DatabaseManager.Instance.GuardarJugador(player.ToPlayerDB());

        // Ocultar panel
        panelNombreJugador.SetActive(false);
    }

    public void GuardarJugador()
    {
        if (player != null)
            DatabaseManager.Instance.GuardarJugador(player.ToPlayerDB());
    }

    // Inicializa enemigo para pruebas de combate
    public void InicializarEnemigo()
    {
        player2 = new PlayerData("EnemigoDemo");
        player2.nivel = 2;
        player2.vidaMaxima = 28;
        player2.fuerzaBase = 5;
        player2.velocidadBase = 4;
        player2.inteligenciaBase = 3;
        player2.destrezaBase = 3;
        player2.resistenciaBase = 2;
        player2.ReiniciarStats();
    }

    // Llamada al finalizar un combate
    public void FinalizarCombate(PlayerData ganador, PlayerData perdedor)
    {
        // Reinicia stats solo para combate
        ganador.ReiniciarStats();
        perdedor.ReiniciarStats();

        int expGanar = 10;
        ganador.GanarExperiencia(expGanar);

        // Guardar jugador en DB
        GuardarJugador();
    }

    // Función para cambiar nombre desde menú, opcional
    public void CambiarNombre(string nuevoNombre)
    {
        if (player == null) return;

        player.jugadorID = nuevoNombre;
        PlayerPrefs.SetString("JugadorID", nuevoNombre);
        GuardarJugador();
    }
}
