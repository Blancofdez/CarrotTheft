
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectorMapas : MonoBehaviour
{


    [SerializeField] private GameObject fade;
    Animator anim;

    private void Start() 
    {
        Cursor.visible = true;
        
        anim = fade.GetComponent<Animator>();
    }

   
    public void Mapa1()
    {

        StartCoroutine(LoadNextScene(1));
    }
    public void Mapa2()
    {      
        StartCoroutine(LoadNextScene(2));
    }
    public void Mapa3()
    {

        StartCoroutine(LoadNextScene(3));
    }        
    public void Mapa4()
    {
        StartCoroutine(LoadNextScene(4));
    }

   
    public IEnumerator LoadNextScene(int escena)
    {
        anim.SetTrigger("isGameOut");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(escena);
        
    }
}
