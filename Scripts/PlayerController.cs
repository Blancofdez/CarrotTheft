using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed;


    //--------------------------JUMP
    [SerializeField] float jumpSpeed;
    bool jump;

    //--------------------------RUN

    float moveX;
    float moveY;


    //---------------------------COMPONENT

    Rigidbody2D rb;
    Collider2D col;
    Animator anim;


    //-----------------------------SFX
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxHurt;


    //-------------------------REBOTE

    bool canMove = true;
    bool canClimb = false;
    bool caida;
    

    //---------------------------GAMEMANAGER
    GameManager game;

    [SerializeField] Image mesPanelWin;

    [SerializeField] Text txtMesWin;

    [SerializeField] private AudioClip sfxWin;

    [SerializeField] private GameObject fade;
    Animator animFade;

    bool final = false;

    private ParticleSystem.EmissionModule emisioPolvoPies;
   




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        animFade = fade.GetComponent<Animator>();
        game = GameManager.GetInstance();
    

        StartCoroutine("Entrada");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            animFade.SetTrigger("isGameIn");
        }

    }

    void FixedUpdate()
    {

        if (canMove)
        {
            Run();
            Flip();
            Jump();

            if (canClimb && Input.GetKey(KeyCode.UpArrow))
            {
                Climb();
            }
        }

    }

    void Update()
    {


        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 5 )
        {
            canMove = false;
            Vector2 vel = new Vector2(1 * 120 * Time.fixedDeltaTime, rb.velocity.y);
            rb.velocity = vel;
            anim.SetBool("isRunning", Math.Abs(rb.velocity.x) > Mathf.Epsilon);

        }
        else if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");


            if(moveX != 0 || moveY != 0)
            {
                Cursor.visible = false;
            }
        


            if (!jump && !game.isGamePaused() && Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        ContactPoint2D[] contactos = other.contacts;

        foreach (ContactPoint2D contacto in contactos)
        {
            caida = false;

            Vector3 direccionDeContacto = contacto.normal;

            if (direccionDeContacto.y > 0)
            {
                caida = true;
            }

        }


        if (other.gameObject.layer == 10 || other.gameObject.layer == 19)
        {

            anim.SetTrigger("isHurt");
            StartCoroutine("ReboteDaño");
            AudioSource.PlayClipAtPoint(sfxHurt, Camera.main.transform.position, 0.5f);
        }


        if ((other.gameObject.tag == "Terrain" || other.gameObject.tag == "Platforms"))
        {
            if (other.gameObject.tag == "Terrain" && caida)
            {
                anim.SetBool("floor", true);
                anim.SetBool("isClimbing", false);

            }
            else if (other.gameObject.tag == "Platforms" && caida)
            {

                anim.SetBool("floor", true);
                anim.SetBool("isClimbing", false);

            }
        }


    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Meta")
        {
            Debug.Log("Meta");
            Camera.main.GetComponent<AudioSource>().Stop();
            AudioSource.PlayClipAtPoint(sfxWin, rb.position, 1f);
            mesPanelWin.gameObject.SetActive(!mesPanelWin.gameObject.activeSelf);
            txtMesWin.text = "NIVEL " + SceneManager.GetActiveScene().buildIndex + " SUPERADO";
            canMove = false;
            Vector2 vel = new Vector2(1 * 120 * Time.fixedDeltaTime, rb.velocity.y);
            rb.velocity = vel;
            game.SaveData();
            StartCoroutine(DelayWin());
        }


        if (other.gameObject.layer == 18)
        {
            Camera.main.GetComponent<AudioSource>().Stop();
            AudioSource.PlayClipAtPoint(sfxWin, rb.position, 1f);
            mesPanelWin.gameObject.SetActive(!mesPanelWin.gameObject.activeSelf);
            txtMesWin.text = "NIVEL " + SceneManager.GetActiveScene().buildIndex + " SUPERADO";
            canMove = false;
            game.SaveData();
            final = true;
            StartCoroutine(Final());
        }

        if (other.gameObject.tag == "Stairs")
        {
            canClimb = true;

        }

        if (other.gameObject.layer == 19)
        {

            anim.SetTrigger("isHurt");
            StartCoroutine("ReboteDaño");
            AudioSource.PlayClipAtPoint(sfxHurt, Camera.main.transform.position, 0.5f);
        }

        if (other.gameObject.layer == 10)
        {
            anim.SetBool("floor", true);
            StartCoroutine("ReboteEnemigo");
            AudioSource.PlayClipAtPoint(sfxJump, Camera.main.transform.position, 0.5f);
        }

        if (other.gameObject.tag == "Gem")
        {
            Debug.Log("Gem");
            game.GemCol();

        }

        if (other.gameObject.tag == "EnemyPral")
        {
            
            StartCoroutine("DelayArdilla");

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Stairs")
        {
            canClimb = false;
            anim.SetBool("isClimbing", false);
        }

    }

    public IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(11.0f);
        canMove = true;
        StartCoroutine(LoadNextScene());
    }


    public IEnumerator DelayArdilla()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.8f);
        col.enabled = true;
    }

    public IEnumerator Final()
    {
        int i = 0;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = new Vector2(0, 0);
        anim.SetBool("isRunning", false);


        while (i <= 4)
        {
            jump = true;
            yield return new WaitForSeconds(2.1f);
            Jump();
            jump = false;
            i++;
        }
        canMove = true;
        animFade.SetTrigger("isGameOut");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(5);
    }

    public IEnumerator LoadNextScene()
    {
        animFade.SetTrigger("isGameOut");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator Entrada()
    {
        canMove = false;
        Vector2 vel = new Vector2(1 * 120 * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = vel;
        anim.SetBool("isRunning", Math.Abs(rb.velocity.x) > Mathf.Epsilon);
        yield return new WaitForSeconds(3.0f);
        canMove = true;
    }

    IEnumerator ReboteDaño()
    {
        canMove = false;
        rb.velocity = new Vector2(-4, 15);
        yield return new WaitForSeconds(0.7f);
        rb.velocity = new Vector2(0, 0);
        canMove = true;

    }

    IEnumerator ReboteEnemigo()
    {
        anim.SetBool("floor", true);
        rb.velocity = new Vector2(rb.velocity.x + 2, 15);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("floor", false);
        anim.SetTrigger("isJumping");

    }

    void Run()
    {
        Vector2 vel = new Vector2(moveX * speed * Time.fixedDeltaTime, rb.velocity.y);

        rb.velocity = vel;

        anim.SetBool("isRunning", Math.Abs(rb.velocity.x) > Mathf.Epsilon);

    }

    void Climb()
    {
        jump = false;
        Vector2 vel = new Vector2(rb.velocity.x, moveY * 250 * Time.fixedDeltaTime);
        rb.velocity = vel;
        anim.SetBool("isClimbing", true);

    }

    void Flip()
    {
        float vx = rb.velocity.x;

        if (Mathf.Abs(vx) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(vx), 1);
        }

    }

    void Jump()
    {
        if (!jump) return;

        jump = false;

        if (!col.IsTouchingLayers(LayerMask.GetMask("Terrain", "Platforms")))
            return;

        if (!final)
        {
            AudioSource.PlayClipAtPoint(sfxJump, Camera.main.transform.position, 0.5f);
        }

        rb.velocity += new Vector2(0, jumpSpeed);

        anim.SetBool("floor", false);
        anim.SetTrigger("isJumping");
    }
}
