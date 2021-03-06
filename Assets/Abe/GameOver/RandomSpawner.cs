﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("岩のプレハブ")]
    List<GameObject> stones;

    [SerializeField, Tooltip("生成する数"), Range(1, 50)]
    int   instanceNumbers;

    [SerializeField, Tooltip("生成の間隔"), Range(0, 5)]
    float intervalTime;
    
    void Awake()
    {
        
    }
    
    IEnumerator Start()
    {
        for(int i = 0; i < instanceNumbers; i++)
        {
            //ランダムに岩を落とす
            int rand = Random.Range(0, stones.Count);
            GameObject randomStone = stones[rand];

            //生成
            GameObject instance = Instantiate(randomStone, transform.position, Quaternion.identity);
            instance.transform.SetParent(transform);
            instance.transform.localScale = Vector3.one * 4;
            instance.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));

            Rigidbody2D rig = instance.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2()
            {
                x = Random.Range(-1.0f, 1.0f),
                y = Random.Range( 0.0f, 1.0f)
            };
            rig.AddForce(force * 50, ForceMode2D.Impulse);

            yield return new WaitForSeconds(intervalTime);
        }
    }
}