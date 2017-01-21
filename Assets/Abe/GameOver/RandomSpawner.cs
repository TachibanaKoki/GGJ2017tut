using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("岩のプレハブ")]
    List<GameObject> stones;

    [SerializeField, Tooltip("生成する数"), Range(1, 50)]
    int   instancenumbers;

    [SerializeField, Tooltip("生成の間隔"), Range(0, 5)]
    float intervalTime;
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
}