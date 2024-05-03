using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Animator anim;
    [SerializeField] private AudioClip sfxItem;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("IsTouched");
            GameManager.GetInstance().AddScore(gameObject.tag);
            AudioSource.PlayClipAtPoint(sfxItem, Camera.main.transform.position, 0.5f);
            Destroy(gameObject, 0.2f);
        }
        
    }
}
