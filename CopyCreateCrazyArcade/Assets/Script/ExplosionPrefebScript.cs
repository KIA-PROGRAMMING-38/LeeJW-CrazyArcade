using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ExplosionPrefebScript : MonoBehaviour
{
    private const float _explosionTime = 0.36f;
    private float _elapsedTime;

    GameObject _blockPrefebs;
    GameObject _balloonPrebebs;
    GameObject _character;
    public int itemCount = 3;
    private Animator _anim;

    Vector3[] asd = new Vector3[4];

    private void Awake()

    {
        _character = Resources.Load("Character") as GameObject;
        _blockPrefebs = Resources.Load("NonMovingBlock") as GameObject;
        _balloonPrebebs = Resources.Load("Balloon") as GameObject;
        _anim = GetComponent<Animator>();

        asd[0] = new Vector3(0, 1, 0);
        asd[1] = new Vector3(0, -1, 0);
        asd[2] = new Vector3(-1, 0, 0);
        asd[3] = new Vector3(+1, 0, 0);
    }
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _explosionTime)
        {
            Destroy(gameObject);
            _elapsedTime = 0;
        }

    }

    public void SetAnimator(int setBoolNumber)
    {
        if (setBoolNumber == 0)
        {
            Debug.Log("셋애니메이터 펄스");

            _anim.SetBool("MiddleExplosion", false);
        }
        if (setBoolNumber == 1)
        {
            Debug.Log("셋애니메이터 트루");

            _anim.SetBool("MiddleExplosion", true);
        }
    }

    Vector3 normalVec = Vector3.zero;
    

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.layer == 8)
        {

            Debug.Log(transform.position);

            for(int i = 0; i < asd.Length; ++i)
            {
               normalVec =  transform.position += asd[i];

                if (normalVec == collision.transform.position)
                {

                }

            }

        }
    }
}
