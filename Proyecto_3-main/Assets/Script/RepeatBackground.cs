using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    public float repeatWidth;
    
    void Start()
    {
        //GUARDAMOS LA POSICION INICIAL DEL FONDO
        startPos = transform.position;

        //TOMAMOS LA MITAD DE LA ANCHURA DEL FONDO
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }
 
    void Update()
    {
        //SI NOS DESPLAZADMOS REPEATWITHMETROS
        if (transform.position.x < startPos.x - repeatWidth)
        {
            //VOLVEMOS A LA POSICION INICIAL
            transform.position = startPos;
        }
    }
}
