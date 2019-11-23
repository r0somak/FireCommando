using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
