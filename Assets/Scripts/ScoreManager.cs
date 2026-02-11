using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Permite que otros scripts accedan aquí fácilmente
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        // Configuramos la instancia
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
        // Comprobar si llegamos a 100
        if (score >= 100)
        {
            GameManager.instance.GanarJuego();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Monedas: " + score;
    }


}