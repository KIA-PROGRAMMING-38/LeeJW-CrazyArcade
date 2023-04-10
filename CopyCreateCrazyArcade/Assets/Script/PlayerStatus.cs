using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class PlayerStatus : MonoBehaviour
    {


        public int currentSpeed { get; private set; }
        public int currentPower = 1;
        public int currentAttackCount = 1;

        public const int  maxSpeed = 8;
        public const int  maxPower = 8;
        public const int  maxAttackCount = 8;

        private void Start()
        {
            currentSpeed = 8;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Explosion"))
            {
                // 게임 이 끝날 수 있게 설정할것.
                Debug.Log("아야야");

            }
        }

    }
}
