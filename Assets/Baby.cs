using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
