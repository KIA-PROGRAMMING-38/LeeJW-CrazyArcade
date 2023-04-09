using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaterBalloon : MonoBehaviour
{
    private float elapsedTime;
    Rigidbody2D _rb;
    Animator anim;
    Collider2D _collider;
    private GameObject _explosion;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        _explosion = Resources.Load("Explosion") as GameObject;

    }
    private bool _isExplosion = true;
    void Update()
    {
        elapsedTime += Time.deltaTime;


        if (elapsedTime >= 3)
        {

            anim.SetBool("BoomBalloon", true);
            Debug.Log("익스플로전");
            if(_isExplosion )
            {
               // Instantiate(_explosion,transform.position,transform.rotation);

                _isExplosion = false;
            }

            if (elapsedTime >= 3.5)
            {
                _isExplosion = true;
                Destroy(gameObject);
                elapsedTime = 0;

            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collider.isTrigger = false;
        }


    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Balloon"))
    //    {
    //        // 시간이 지나지않았더라도 물풍선이 다른 물풍선에 물줄기를 맞췄을때
    //        // 다른 물풍선 또한 터져야하는 로직 구현 할것.
    //    }
    //}
}
