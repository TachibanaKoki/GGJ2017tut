using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    NORMAL, MOVE, BATTLE, ATTACK
}

public class CharacterController : MonoBehaviour
{


    public int TensionPoint;
    public CharacterState m_state = CharacterState.MOVE;

    [SerializeField]
    float m_MoveSpeed = 10.0f;

    [SerializeField]
    float m_ReactionDestance = 100.0f;

    [SerializeField]
    float m_StopDistance = 10.0f;

    [SerializeField]
    float TensionDownSpeed = 1.0f;

    [SerializeField]
    GameObject DethObject;

    [SerializeField]
    private ParticleSystem m_TensionEffect;

    private Camera m_Camera;
    private Vector3 m_Velocity;
    private Vector3 m_TargetPosition;

    private bool isMove = false;
    private bool isPatrol = true;
    private float timer;


    void Start()
    {
        m_Camera = Camera.main;
        TensionPoint = 10;
        timer = 0;
        m_Velocity = Vector3.zero;
        StartCoroutine(TensionDown());
        StartCoroutine(TensionEffect());
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

    void FixedUpdate()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)TensionPoint / 10.0f);

        switch (m_state)
        {
            case CharacterState.MOVE:
                transform.Translate(m_Velocity);
                Vector3 v = m_TargetPosition - transform.position;
                if (Vector3.Dot(v, m_Velocity) < 0)
                {
                    m_state = CharacterState.NORMAL;
                    m_Velocity = Vector3.zero;
                }
                break;
            case CharacterState.NORMAL:
                timer += Time.deltaTime;
                if (timer > 1.0f)
                {
                    timer = 0.0f;
                    m_Velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * m_MoveSpeed * TensionPoint * 0.03f;
                }
                transform.Translate(m_Velocity);
                break;
            case CharacterState.ATTACK:
                break;
        }
    }

    IEnumerator TensionEffect()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        yield return wait;
        while (true)
        {
            if (TensionPoint > 10.0f)
            {
                m_TensionEffect.Emit(TensionPoint - 10);
            }
            yield return wait;
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

    public void MoveTo(Vector3 pos)
    {
        m_state = CharacterState.MOVE;
        m_TargetPosition = pos;
        float m = (pos - transform.position).magnitude;
        if (m < 1)
            m = 1;
        float n = 1 / m;

        m_Velocity = (pos - transform.position).normalized * n * m_MoveSpeed * TensionPoint * 0.1f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            GameObject.Instantiate(DethObject, transform.position, Quaternion.identity);
            TapUtils.I.OnTapDown -= TapAction;
            Destroy(gameObject);
        }
        else
        {
            m_state = CharacterState.ATTACK;
            m_Velocity = Vector3.zero;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            if (m_state == CharacterState.ATTACK)
            {
                m_state = CharacterState.NORMAL;
                m_Velocity = Vector3.zero;
            }
        }
    }
}
