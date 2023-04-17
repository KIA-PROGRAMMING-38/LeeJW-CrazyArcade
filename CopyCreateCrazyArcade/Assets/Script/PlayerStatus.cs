using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
    public class PlayerStatus : MonoBehaviour
    {
        Animator _anim;
        TakeItem item;
        public float currentSpeed { get; set; } = 5;
        public int currentExplosionPower { get; set; } = 1;
        public int currentBalloonCount { get; set; } = 1;
        public bool kickBalloon { get; set; } = false;
        public bool dieWaitState { get; set; } =  false;


        public int MAX_SPEED { get; private set; } = 9;
        public int MAX_BALLOON_COUNT { get; private set; } = 6;
        public int MAX_EXPLOSION_POWER { get; private set; } = 7;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Explosion"))
            {
                _anim.SetBool("DieWait", true);
            }

           if(collision.gameObject.layer == LayerMask.NameToLayer("Item") && dieWaitState == false)
            {
                item = collision.GetComponent<TakeItem>();

                item.PlayerTakeItem(gameObject);
                Destroy(collision.gameObject);
            }
        }

        public void WaitDie()
        {
            currentSpeed = 0.5f;
            currentBalloonCount = 0;
        }
        public void DieConfirmation()
        {
            currentSpeed = 0;
        }

        public void GameEnd()
        {
            //게임 끝 조건
        }

    }
}
