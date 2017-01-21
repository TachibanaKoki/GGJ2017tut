using UnityEngine;
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
            int rand = Random.Range(0, stones.Count);
            GameObject randomStone = stones[rand];

            GameObject instance = Instantiate(randomStone, transform.position, Quaternion.identity);
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