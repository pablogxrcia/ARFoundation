using UnityEngine;
using System.Collections.Generic;

public class TrackSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject trackPrefab;
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

    [Header("Configuración de Movimiento")]
    public float speed = 2f;
    public float pieceLength = 4f;
    public int initialPieces = 7;

    [Header("Posicionamiento de Objetos")]
    public float trackWidth = 1.2f; // El ancho total donde pueden salir cosas
    public float coinHeight = 2.2f; // Altura de las monedas
    public float obstacleHeight = 0.3f; // Altura de los obstáculos

    [Header("Probabilidades (0 a 1)")]
    [Range(0, 1)] public float obstacleChance = 0.4f;
    [Range(0, 1)] public float coinChance = 0.3f;

    private Queue<GameObject> trackQueue = new Queue<GameObject>();
    private float nextSpawnZ = 0f;

    void Start()
    {
        // Generamos las piezas iniciales sin que se muevan todavía para llenar el camino
        for (int i = 0; i < initialPieces; i++)
        {
            SpawnPiece(true);
        }
    }

    void Update()
    {
        // Movemos todas las piezas
        foreach (GameObject piece in trackQueue)
        {
            piece.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        // Si la primera pieza de la cola ya pasó detrás del jugador, la reciclamos
        // Usamos un margen de seguridad (-pieceLength)
        if (trackQueue.Count > 0 && trackQueue.Peek().transform.position.z < -pieceLength)
        {
            RemovePiece();
            SpawnPiece(false);
        }
    }

    void SpawnPiece(bool isInitial)
    {
        GameObject piece = Instantiate(trackPrefab);

        // Calculamos la posición: si es el inicio, las ponemos en fila. 
        // Si es durante el juego, la ponemos justo al final de la última pieza.
        float zPos = isInitial ? nextSpawnZ : GetLastPieceZ() + pieceLength;

        piece.transform.position = new Vector3(0, -0.3f, zPos);
        nextSpawnZ = zPos + pieceLength;

        // Decidir qué spawnear
        float rand = Random.value;
        if (rand < obstacleChance)
        {
            SpawnItem(piece.transform, obstaclePrefab, obstacleHeight);
        }
        else if (rand < (obstacleChance + coinChance))
        {
            SpawnItem(piece.transform, coinPrefab, coinHeight);
        }

        trackQueue.Enqueue(piece);
    }

    void SpawnItem(Transform parent, GameObject prefab, float height)
    {
        if (prefab == null) return;

        // CAMBIO AQUÍ: Ahora usamos Random.Range para que salgan por CUALQUIER lado
        // en lugar de solo 3 carriles fijos.
        float randomX = Random.Range(-trackWidth / 2f, trackWidth / 2f);

        GameObject item = Instantiate(prefab);
        item.transform.SetParent(parent);
        item.transform.localPosition = new Vector3(randomX, height, 0);
    }

    float GetLastPieceZ()
    {
        // Buscamos la posición Z de la última pieza en la cola para pegar la siguiente
        GameObject[] pieces = trackQueue.ToArray();
        if (pieces.Length == 0) return 0f;
        return pieces[pieces.Length - 1].transform.position.z;
    }

    void RemovePiece()
    {
        GameObject oldPiece = trackQueue.Dequeue();
        Destroy(oldPiece);
    }
}