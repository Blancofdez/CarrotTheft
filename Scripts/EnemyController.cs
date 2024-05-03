
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    Collider2D col;
 
    Rigidbody2D rb;
    float delayDestroy = 0.5f;
    [SerializeField] List<Transform> waytPoinst;
    [SerializeField] private float speed;
    [SerializeField] private float distanciaCambio;
    [SerializeField] private  AudioClip sfxDeath;
    [SerializeField] private BoxCollider2D bcol;
    byte siguientePosicion = 0;

   
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();     
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
            
            bcol.enabled = false;
            col.enabled = false;
            anim.SetTrigger("isHited");
            AudioSource.PlayClipAtPoint(sfxDeath, Camera.main.transform.position, 0.5f);
            Destroy(gameObject, delayDestroy);
            GameManager.GetInstance().AddScore(gameObject.tag);  
        } 
    }

    void Move()
    {
       
        transform.position = Vector3.MoveTowards(transform.position,waytPoinst[siguientePosicion].transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position,waytPoinst[siguientePosicion].transform.position) < distanciaCambio)
        {
            siguientePosicion++;
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180, 0);

            if (siguientePosicion >= waytPoinst.Count)
            {
                siguientePosicion = 0;
            }
        }
    }
}
