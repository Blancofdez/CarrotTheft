using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapController : MonoBehaviour
{
    private TilemapCollider2D traps;
    private TilemapRenderer tilemap;

 
    private void Start()
    {
        traps = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<TilemapRenderer>();
        StartCoroutine("trapControl");
    }


    public IEnumerator trapControl()
    {
        while(true){
           
            traps.enabled = false;
            tilemap.enabled = false;

            yield return new WaitForSeconds(1.5f);
           
            traps.enabled = true;
            tilemap.enabled = true;
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player")
        {
            GameManager gm = GameManager.GetInstance();
                
            gm.LoseLife();

                if (!gm.isGameOver())
                {
                    
                }
        }

    }
}
