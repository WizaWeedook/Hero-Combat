using UnityEngine;
using TMPro;

public class NombreJugadorPanel : MonoBehaviour
{
    public TMP_InputField inputNombre;

    public void CrearJugador()
    {
        string nombre = inputNombre.text.Trim();
        if (!string.IsNullOrEmpty(nombre))
        {
            GameManager.Instance.CrearNuevoJugador();
            gameObject.SetActive(false); // Oculta el panel
            // Aquí puedes cargar la siguiente escena o actualizar la UI
        }
    }
}