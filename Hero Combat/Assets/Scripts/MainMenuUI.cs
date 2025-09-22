using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    public void OnCombatirClicked() {

        SceneManager.LoadScene("CombatScene"); 
    }

    public void OnEntrenamientoClicked() {
        SceneManager.LoadScene("CombatScene"); // puede ser la misma por ahora
    }

    public void OnPersonajeClicked() {
        SceneManager.LoadScene("CaracterScene"); 
    }
    public void OnSalirClicked() {
        Application.Quit();
    }
}