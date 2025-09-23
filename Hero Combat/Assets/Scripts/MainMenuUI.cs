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
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el Play en el editor
#else
            Application.Quit(); // Cierra el ejecutable
#endif
    }
}
