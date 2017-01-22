using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    NORMAL, MOVE, BATTLE, ATTACK
}

public class CharacterController : MonoBehaviour
{


    public float TensionPoint;
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

    [SerializeField]
    private Temp m_temp;

    private Camera m_Camera;
    private Vector3 m_Velocity;
    private Vector3 m_TargetPosition;

    private bool isMove = false;
    private bool isPatrol = true;
    private bool isStop = false;
    private float timer;


    private List<GameObject> BreakRock = null;
    private AudioSource DigSound;

    void Start()
    {
        isStop = true;
        StartCoroutine(Deray(3.0f,Initialize));
        DigSound = GetComponent<AudioSource>();
        DigSound.Pause();
    }

    void Initialize()
    {
        m_Camera = Camera.main;
        TensionPoint = 50;
        timer = 0;
        isStop = false;
        m_Velocity = Vector3.zero;
        StartCoroutine(TensionDown());
        StartCoroutine(TensionEffect());
        TapUtils.I.OnTapDown += TapAction;
        BreakRock = new List<GameObject>();
    }

    void TapAction(Vector3 pos)
    {
        pos = m_Camera.ScreenToWorldPoint(pos);
        try
        {
            if ((Vector3.Distance(pos, transform.position) - 10) < (m_ReactionDestance + (TensionPoint * 0.01f)))
            {
                Vector3 p = new Vector3(pos.x, pos.y, 0);
                int combo = m_Camera.gameObject.GetComponent<Temp>().combo;
                TensionPoint = Mathf.Min(100, TensionPoint + combo + 1);
                MoveTo(p);
            }
        }catch
        { }
    }

    void FixedUpdate()
    {
        if (isStop) return;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)TensionPoint / 25.0f);
        switch (m_state)
        {
            case CharacterState.MOVE:
                CharactorMove();
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
                CharactorMove();
                break;
            case CharacterState.ATTACK:
                break;
        }
    }

    void Update()
    {
        if (isStop) return;
        bool isBreakRock = false;
        for (int i = 0; i < BreakRock.Count; i++)
        {
            if(BreakRock[i]!=null)
            {
                isBreakRock = true;
            }
        }
        GetComponent<Animator>().SetBool("isAttack", isBreakRock);
        if(isBreakRock&&!DigSound.isPlaying)
        {
            DigSound.Play();
        }
        if(!isBreakRock&&DigSound.isPlaying)
        {
            DigSound.Pause();
        }
    }

    void CharactorMove()
    {
        Animator anim = GetComponent<Animator>();
        if (Mathf.Abs(m_Velocity.x) > Mathf.Abs(m_Velocity.y))
        {
            anim.SetBool("isVertical", false);
            if (m_Velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (m_Velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (m_Velocity.y < 0)
        {
            anim.SetBool("isFront", true);
            anim.SetBool("isVertical", true);
        }
        else if (m_Velocity.y > 0)
        {
            anim.SetBool("isFront", false);
            anim.SetBool("isVertical", true);
        }

        transform.Translate(m_Velocity);
    }

    IEnumerator TensionEffect()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        yield return wait;
        while (true)
        {
            if (TensionPoint > 50.0f)
            {
                m_TensionEffect.Emit((int)((TensionPoint-50.0f)/10.0f));
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

        m_Velocity = (pos - transform.position).normalized * n * m_MoveSpeed * ((TensionPoint / 25)+0.1f)*0.5f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            //Deth();
            //isStop = true;
            //StartCoroutine(Deray(3.0f, () => { isStop = false; }));
            //TensionPoint -= 10;
        }
        else if (col.gameObject.tag == "Rock")
        {
            m_state = CharacterState.ATTACK;
            m_Velocity = Vector3.zero;
            BreakRock.Add(col.gameObject);
        }
        else if (col.gameObject.tag == "Enemy")
        {
            Deth();
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            TensionPoint -= Time.deltaTime * 20;
        }
    }

        IEnumerator Deray(float duration, System.Action action)
    {
        yield return new WaitForSeconds(duration);
        action.Invoke();
    }

    void Deth()
    {
        GameObject.Instantiate(DethObject, transform.position, Quaternion.identity);
        TapUtils.I.OnTapDown -= TapAction;
        Destroy(gameObject);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Rock")
        {
            if (m_state == CharacterState.ATTACK)
            {
                m_state = CharacterState.NORMAL;
                m_Velocity = Vector3.zero;

            }
            BreakRock.Remove(col.gameObject);
        }
    }
}
