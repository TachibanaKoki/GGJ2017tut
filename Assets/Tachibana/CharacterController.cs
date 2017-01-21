﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    
    public int TensionPoint;

    [SerializeField]
    float m_MoveSpeed = 10.0f;

    [SerializeField]
    float m_ReactionDestance =100.0f;

    [SerializeField]
    float m_StopDistance = 10.0f;

    [SerializeField]
    float TensionDownSpeed = 1.0f;

    [SerializeField]
    GameObject DethObject;

    private Camera m_Camera;
    private Vector3 m_Velocity;
    private Vector3 m_TargetPosition;

    bool isMove=false;


    void Start()
    {
        m_Camera = Camera.main;
        TensionPoint = 10;
        m_Velocity = Vector3.zero;
        StartCoroutine(TensionDown());
        TapUtils.I.OnTapDown += TapAction;
    }

    void TapAction(Vector3 pos)
    {
        pos = m_Camera.ScreenToWorldPoint(pos);
        if ((Vector3.Distance(pos, transform.position) - 10) < m_ReactionDestance)
        {
            Vector3 p = new Vector3(pos.x, pos.y, 0);
            TensionPoint++;
            MoveTo(p);
        }
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,(float)TensionPoint/10.0f);

        if (isMove)
        {
            transform.Translate(m_Velocity);
            if (Vector3.Distance(transform.position, m_TargetPosition) < m_StopDistance)
            {
                isMove = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag== "Player")
        {
            GameObject.Instantiate(DethObject, transform.position,Quaternion.identity);
            TapUtils.I.OnTapDown -= TapAction;
            Destroy(gameObject);
        }
    }

    IEnumerator TensionDown()
    {
        WaitForSeconds wait = new WaitForSeconds(TensionDownSpeed);
        yield return wait;
        while (true)
        {
            TensionPoint--;
            if (TensionPoint < 0)
            {
                //todo死んだとき
                Destroy(gameObject);
            }
            yield return wait;
        }
    }

     public  void MoveTo(Vector3 pos)
    {
        isMove = true;
        m_TargetPosition = pos;
        m_Velocity = (pos - transform.position).normalized * m_MoveSpeed*TensionPoint*0.1f;
    } 
}
