using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    float AttackTimeSpan = 6.0f;

    [SerializeField]
    float forcePow = 10.0f;

    float timer;
    GameObject[] Characters;

    AudioSource se;

    // Use this for initialization
    void Start()
    {
        timer = 0.0f;
        se = GetComponent<AudioSource>();
        Characters = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(Delay(() => { transform.DOScale(0.13f, 3.0f).OnKill(Attack); }));
    }

    IEnumerator Delay(System.Action action)
    {
        yield return new WaitForSeconds(3.0f);
        action();
    }

    void ChageStart()
    {
        timer = 0.0f;
        transform.DOScale(0.13f, 3.0f).OnKill(Attack);
    }

    void Attack()
    {
        GameObject target = null;
        for (int i = 0; i < Characters.Length; i++)
        {
            if (Characters[i] != null)
            {
                if (target != null && (Vector3.Distance(target.transform.position, transform.position) > Vector3.Distance(Characters[i].transform.position, transform.position)))
                {
                    target = Characters[i];
                }
                else if(target==null)
                {
                    target = Characters[i];
                }
            }
        }
        se.Play();
        Vector3 v3 = target.transform.position - transform.position;
        Vector2 v = new Vector2(v3.x,v3.y);
        GetComponent<Rigidbody2D>().AddForce(v.normalized*forcePow,ForceMode2D.Impulse);
        transform.DOScale(0.07f, 1).OnKill(ChageStart);
    }
}
