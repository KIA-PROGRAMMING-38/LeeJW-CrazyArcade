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

    void Update()
    {
        elapsedTime += Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    _collider.isTrigger = true;

        //}

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
}
