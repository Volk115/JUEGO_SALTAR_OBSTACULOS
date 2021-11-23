using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    //MODIFICA LA GRAVEDAD
    private float jumpForce = 550f;
    private float gravityMod = 1.5f;

    //SUELO Y GAMEOVER
    private bool isOnGround = true;
    public bool gameOver;

    //ANIMACIONES
    private Animator playerAnimator;
    private Animator playerdeath;

    //MUSICA
    private AudioSource playerAudioSource;
    private AudioSource cameraAudioSource;

    //EXPLOSION DE PARTICULAS
    public AudioClip explosionClip;
    public AudioClip jumpClip;

    public ParticleSystem explosionParticleSystem;
    public ParticleSystem dirtParticleSystem;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerdeath = GetComponent<Animator>();
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        //MODIFICA LA GRAVEDAD
        Physics.gravity *= gravityMod;
    }

    void Update()
    { //SALTO (CUANDO SE TOCA LA BARRA ESPACIADORA, TOCA EL SUELO Y NO ES GAMEOVER)

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            //CUANDO SE SALTA, SE REALIZA UN IMPULSO HACIA ARRIBA
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnimator.SetTrigger("Jump_trig");

            //CUANDO GAMEOVER, LAS PARTICULAS DE TIERRA PARAN
            dirtParticleSystem.Stop();
            
            //CUANDO NO TOCA EL SUELO, HACE VFX DE SALTO (A UN SONIDO DE 1)
            playerAudioSource.PlayOneShot(jumpClip, 1);
            isOnGround = false;
        }
    }

    //CUANDO SE COLISIONA CON EL SUELO, IS ON THE GROUND = TRUE (HAY COLISION CON EL SUELO)
    private void OnCollisionEnter(Collision otherCollider)
    {
        if (!gameOver)
        {
            if (otherCollider.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
                dirtParticleSystem.Play();
            }

            //CUANDO CHOCA CON LOS OBJETOS LLAMADOS OBSTACLE
            if (otherCollider.gameObject.CompareTag("Obstacle"))
            {
                //CADA VEZ QUE EL PERSONAJE GAMEOVER, SERA DE UNA ANIMACION ALEATORIA
                int randomDeathType = Random.Range(1, 3);
                Debug.Log("GAME OVER");

                playerdeath.SetBool("Death_b", true);
                playerAnimator.SetInteger("DeathType_int", randomDeathType);

                //SE REALIZA UNA EXPLOSION
                explosionParticleSystem.Play();

                //SE DESACTIVA EL SISTEMA DE PARTIULAS
                dirtParticleSystem.Stop();

                //SE BAJA EL VOLUMEN DE LA MUSICA
                cameraAudioSource.volume = 0.04f;

                //EFECTO DE SONIDO DE EXPLOSION
                playerAudioSource.PlayOneShot(explosionClip, 1);

                //COMUNICAMOS QUE HEMOS MUERTO (GAMEOVER)
                gameOver = true;

                isOnGround = false;
            }
        }
    }
}
