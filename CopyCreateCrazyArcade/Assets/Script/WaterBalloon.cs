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
     GameObject _explosion;
    public bool asdasd = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        _explosion = Resources.Load("Explosion") as GameObject;
        Debug.Log("블룬 어웨이크");
    }
    private bool _isExplosion = true;
    void Update()
    {
        elapsedTime += Time.deltaTime;


        if (elapsedTime >= 3)
        {
               
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Explosion"))
        {
            //BoomBalloon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collider.isTrigger = false;
        }


    }
    void BoomBalloon()
    {
        Instantiate(_explosion, transform.position, transform.rotation);

        _isExplosion = false;
        Destroy(gameObject);
        elapsedTime = 0;
        _isExplosion = true;
    }

}
