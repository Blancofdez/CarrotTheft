
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





public class EnemyPralController : MonoBehaviour
{
    private const float V = 0.2f;
    [SerializeField]private float speed;
    [SerializeField]private float intervalo;
    [SerializeField] private AudioClip sfxDeath;

    private float tiempoTranscurrido;
    private int accionActiva;

    float delayDestroy = 0.7f;

   
    [SerializeField] List<Transform> waytPoinstAtc;
 
    byte siguientePosicionAtc = 0;
    [SerializeField] private float distanciaCambio;


    public GameObject player;

    Animator anim;
    Collider2D col;
    [SerializeField] private BoxCollider2D hcol;
    [SerializeField] private BoxCollider2D acol;

    

    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        tiempoTranscurrido = 0f;
        accionActiva = 1;


    }


    private void Update()
    {

        tiempoTranscurrido += Time.deltaTime;

        if (tiempoTranscurrido >= intervalo)
        {
            tiempoTranscurrido = 0f;
            accionActiva++;

            if (accionActiva == 7)
            {
                accionActiva = 1;
            }

        }

        if (accionActiva == 1 || accionActiva == 3 || accionActiva == 5)
        {
            hcol.enabled = true;

            
           
            anim.SetBool("atc", false);
           
        }
        else if (accionActiva == 2 || accionActiva == 4 ||  accionActiva == 6)
        {
            anim.SetBool("atc", true);

            hcol.enabled = false;
         
            transform.position = Vector3.MoveTowards(transform.position, waytPoinstAtc[siguientePosicionAtc].transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waytPoinstAtc[siguientePosicionAtc].transform.position) < distanciaCambio)
            {
                siguientePosicionAtc++;
                transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180, 0);

                if (siguientePosicionAtc >= waytPoinstAtc.Count)
                {
                    siguientePosicionAtc = 0;
                }
            }

            
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            GameManager gm = GameManager.GetInstance();

            gm.LoseLife();

            if (!gm.isGameOver())
            {
                
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("isHurt");
            AudioSource.PlayClipAtPoint(sfxDeath, Camera.main.transform.position, 0.5f);
            Destroy(gameObject, delayDestroy);
            GameManager.GetInstance().AddScore(gameObject.tag);
        }
    }

}


