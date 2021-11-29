using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //LA VELOCIDAD DE LOS OBJETOS SERA DE (10)
    public float speed = 10f;
    public PlayerController playerControllerScript;
 
    void Start()
    {
        //TIENE UN COLISIONADOR CON EL SUJETO "PLAYER"
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();


    }
    
    void Update()
    {
        //LOS OBJETOS SE MOVERAN HACIA LA IZQUIERDA
        if(!playerControllerScript.gameOver)
        { 
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        //LOS OBJETOS, DESAPARECERAN AL CAER A MENOS DE 0, -1, 0
        if(transform.position.y <= -1)
        { Destroy(gameObject); }
    }
}
