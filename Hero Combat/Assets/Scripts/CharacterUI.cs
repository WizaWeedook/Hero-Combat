using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public TextMeshProUGUI textoStats;
    public GameObject panelSubidaNivel;
    public Button botonMejoraAleatoria;

    void Start() {
        ActualizarUI();
        botonMejoraAleatoria.onClick.AddListener(AplicarMejora);
        panelSubidaNivel.SetActive(GameManager.Instance.player.mejoraPendiente);
    }

    public void ActualizarUI() {
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
    }

    void AplicarMejora() {
        PlayerData p = GameManager.Instance.player;
        if (!p.mejoraPendiente) return;

        p.AplicarMejoraAleatoria();
        panelSubidaNivel.SetActive(false);
        ActualizarUI();
    }
}
