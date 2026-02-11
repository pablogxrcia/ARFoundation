using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public int pointsValue = 10; // Cuántos puntos da cada moneda

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le decimos al ScoreManager que sume puntos
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(pointsValue);
            }

            // Opcional: Podrías añadir un sonido aquí antes de destruir
            Destroy(gameObject);
        }
    }
}