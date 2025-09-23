using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NombreJugadorPanel : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button botonAceptar;

    void Start()
    {
        gameObject.SetActive(false); // Panel oculto al inicio
        botonAceptar.onClick.AddListener(AceptarNombre);
    }

    // Mostrar el panel para ingresar nombre
    public void Mostrar()
    {
        inputField.text = ""; // Limpia el input cada vez que se muestra
        gameObject.SetActive(true);
        inputField.Select();
        inputField.ActivateInputField();
    }

    void AceptarNombre()
    {
        string nombre = inputField.text.Trim();
        if (!string.IsNullOrEmpty(nombre))
        {
            GameManager.Instance.CrearNuevoJugador(nombre);
            gameObject.SetActive(false);
        }
    }
}
