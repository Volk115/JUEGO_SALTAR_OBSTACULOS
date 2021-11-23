using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;

    private Vector3 spawnPos = new Vector3(35, 0, 0);
    public PlayerController playerControllerScript;
    private int randomObj;

    public GameObject[] obstaclePrefab;

    void Start()
    {
        //REPETIMOS LA FUNCION SPAWN OBSTACLE (CADA 2s)
        InvokeRepeating("Spawner", startDelay, repeatRate);

        //ACCEDEMOS A "PLAYER CONTROLLER" PARA COMUNICARNOS CON ESE SCRIPT
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Spawner()
    {
        //CUANDO EL JUGADOR NO ESTA MUERTO
        if (!playerControllerScript.gameOver)
        { 
            //SE GENERA UN NUMERO ALEATORIO ENTRE 0 Y LA LONGITUD DE LA RAID (3 OBJETOS)
            Instantiate(obstaclePrefab[randomObj], spawnPos, obstaclePrefab[randomObj].transform.rotation);
            randomObj = Random.Range(0, obstaclePrefab.Length);
        }
    }
}
