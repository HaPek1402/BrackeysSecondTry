using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;  // Prefab szefowej
    public Transform[] spawnPoints; // Miejsca spawnu
    public float minTimeBetweenSpawns = 5f; // Minimalny czas przed pojawieniem się
    public float maxTimeBetweenSpawns = 10f; // Maksymalny czas przed pojawieniem się

    private GameObject currentBoss; // Przechowujemy referencję do aktualnej szefowej

    private void Start()
    {
        // Rozpoczynamy proces spawnowania po losowym czasie
        Invoke("SpawnBoss", Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
    }

    void SpawnBoss()
    {
        // Jeśli jest już szefowa na scenie, usuń ją
        if (currentBoss != null)
        {
            Destroy(currentBoss);
        }

        // Losowy wybór miejsca spawnu
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Tworzenie szefowej w wybranym miejscu
        currentBoss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);

        // Uruchomienie animacji
        Animator animator = currentBoss.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Appear"); // Musisz ustawić ten trigger w Animatorze
        }

        // Po pojawieniu się, za losowy czas usuwamy szefową i ponownie ją spawnujemy
        float randomTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        Invoke("SpawnBoss", randomTime);
    }
}

