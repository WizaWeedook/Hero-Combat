using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterUI : MonoBehaviour
{
    public TextMeshProUGUI textoStats;
    public GameObject panelSubidaNivel;
    public Button botonMejoraAleatoria;

    void Start()
    {
        botonMejoraAleatoria.onClick.AddListener(AplicarMejora);
        ActualizarUI();
    }

    public void ActualizarUI()
    {
        PlayerData p = GameManager.Instance.player;

        textoStats.text =
            $"Nombre: {p.jugadorID}\n" +
            $"Nivel: {p.nivel}\n" +
            $"XP: {p.experiencia}\n\n" +
            $"Vida: {p.vidaMaxima}\n" +
            $"Fuerza: {p.fuerzaBase}\n" +
            $"Velocidad: {p.velocidadBase}\n" +
            $"Inteligencia: {p.inteligenciaBase}\n" +
            $"Destreza: {p.destrezaBase}\n" +
            $"Resistencia: {p.resistenciaBase}";

        // Mostrar u ocultar el panel de mejora según corresponda
        panelSubidaNivel.SetActive(p.mejoraPendiente);
    }

    void AplicarMejora()
    {
        PlayerData p = GameManager.Instance.player;
        if (!p.mejoraPendiente) return;

        // Aplica una mejora aleatoria
        p.AplicarMejoraAleatoria();

        // Guarda progreso en la base de datos (si existe un DatabaseManager en escena)
        DatabaseManager dbm = FindObjectOfType<DatabaseManager>();
        if (dbm != null)
            dbm.GuardarJugador(p.ToPlayerDB());

        // Refresca UI y oculta el panel
        ActualizarUI();
    }

    public void OnVolverClicked()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
