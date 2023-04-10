using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Script
{
    public class Explosion : MonoBehaviour
    {
        // Animator _anim;

        private void Awake()
        {
            // _anim = GetComponent<Animator>();
            // _anim.SetBool("boolExplosion" , true);
            Debug.Log("AWake");
        }

        private float _elpasedTime;
        private void Update()
        {
            _elpasedTime += Time.deltaTime;

            if(_elpasedTime >= 0.5f)
            {
                Destroy(gameObject);

                _elpasedTime = 0;
            }

        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if(collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("피격됨");
            }

        }
    }


}
