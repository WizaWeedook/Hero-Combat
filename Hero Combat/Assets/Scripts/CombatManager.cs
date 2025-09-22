using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public TextMeshProUGUI textoLog;
    private int turno = 1;
    private bool combateTerminado = false;
    private Coroutine combateCoroutine;

    void Start() {
        textoLog.text = "Comienza el combate...\n";
        // Reiniciar stats temporales al iniciar combate
        GameManager.Instance.player.ReiniciarStatsTemporales();
        GameManager.Instance.player2.ReiniciarStatsTemporales();

        combateCoroutine = StartCoroutine(EjecutarCombate());
    }

    IEnumerator EjecutarCombate() {
        PlayerData jugador = GameManager.Instance.player;
        PlayerData enemigo = GameManager.Instance.player2;
        System.Random rng = new();

        while (!combateTerminado) {
            yield return new WaitForSeconds(1f);
            string log = $"-- Turno {turno} --\n";

            // Turno jugador
            if (jugador.vida > 0) {
                string accion = jugador.Atacar(enemigo, rng);
                log += accion;
            }

            if (enemigo.vida <= 0) {
                FinalizarCombate(jugador, enemigo);
                combateTerminado = true;
                yield break;
            }

            // Turno enemigo
            if (enemigo.vida > 0) {
                string accion = enemigo.Atacar(jugador, rng);
                log += accion;
            }

            if (jugador.vida <= 0) {
                FinalizarCombate(enemigo, jugador);
                combateTerminado = true;
                yield break;
            }

            textoLog.text += log;
            turno++;
        }
    }

    public void FinalizarCombate(PlayerData ganador, PlayerData perdedor) {
        // Suma experiencia solo al ganador
        int expGanar = 10;
        ganador.GanarExperiencia(expGanar);

        // Guardamos en DB
        DatabaseManager.Instance.GuardarJugador(ganador.ToPlayerDB());

        // Mensaje en log
        textoLog.text += $"\n{ganador.jugadorID} gana el combate y recibe {expGanar} XP.\n";

        // Reiniciamos solo la vida para próximos combates
        ganador.ReiniciarStatsTemporales();
        perdedor.ReiniciarStatsTemporales();
    }

    public void VolverAlMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
