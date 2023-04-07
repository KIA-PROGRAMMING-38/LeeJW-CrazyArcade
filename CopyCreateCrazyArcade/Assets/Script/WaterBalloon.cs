using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloon : MonoBehaviour
{
    private float elapsedTime;
    Rigidbody2D _rb;
    Animator anim;
    Collider2D _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }
    private bool _IsExplosion;

    void Update()
    {
        elapsedTime += Time.deltaTime;


        if (elapsedTime >= 3)
        {
            anim.SetBool("BoomBalloon", true);
            
            if(elapsedTime >= 3.5)
            {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Balloon"))
        {
            // 시간이 지나지않았더라도 물풍선이 다른 물풍선에 물줄기를 맞췄을때
            // 다른 물풍선 또한 터져야하는 로직 구현 할것.
        }
    }
}
