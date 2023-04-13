using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
    public class PlayerStatus : MonoBehaviour
    {
        TakeItem item;
        public int currentSpeed { get; private set; }
        public int currentExplosionPower { get; private set; }
        public int currentBalloonCount { get; private set; }

        public const int  maxSpeed = 9;
        public const int  maxExposionPower = 7;
        public const int  maxBalloonCount = 6;

        private void Awake()
        {
            item = GetComponent<TakeItem>();

            currentSpeed = 5;
            currentExplosionPower = 1;
            currentBalloonCount = 1;

            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
           if(collision.gameObject.CompareTag("Explosion"))
            {
                Debug.Log($"{name} : 공격당했다 ");
            }

           if(collision.CompareTag("Item"))
            {
                int i = (int)item.kind;
                Debug.Log(i);
                Debug.Log($"{collision.name} 아이템 획득");
            }
        }

    }
}
