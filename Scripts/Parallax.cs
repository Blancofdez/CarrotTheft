using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Parallax : MonoBehaviour
{

    [SerializeField] float parallax;

    Material mat;

    Transform cam; 

    Vector3 initialPos;
   
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;

        cam = Camera.main.transform;

        initialPos = transform.position;
    }

    
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {

            transform.position = new Vector3(initialPos.x, cam.position.y, initialPos.z);

            mat.mainTextureOffset = new Vector2(0, cam.position.y * parallax);
            
        }
        else
        {
            transform.position = new Vector3(cam.position.x, initialPos.y, initialPos.z);

            mat.mainTextureOffset = new Vector2(cam.position.x * parallax, 0);

        }
     
    }
}
