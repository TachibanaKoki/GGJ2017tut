using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectEater : MonoBehaviour
{
    //[SerializeField, Tooltip("説明文")]
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}