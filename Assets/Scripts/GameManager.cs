using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject panelVictoria;
    public GameObject panelDerrota;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        // Asegurarnos de que los paneles estén ocultos al empezar
        panelVictoria.SetActive(false);
        panelDerrota.SetActive(false);
        Time.timeScale = 1f; // Asegurar que el juego no esté pausado
    }

    public void GanarJuego()
    {
        panelVictoria.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
    }

    public void PerderJuego()
    {
        panelDerrota.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
    }

    // Función para los botones de "Reintentar" o "Jugar de nuevo"
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        // Carga la escena actual otra vez
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}