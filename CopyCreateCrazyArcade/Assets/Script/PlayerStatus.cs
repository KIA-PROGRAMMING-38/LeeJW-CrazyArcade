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

        TakeItem item;
        public int currentSpeed { get; set; } = 5;
        public int currentExplosionPower { get; set; } = 1;
        public int currentBalloonCount { get; set; } = 1;
        public bool kickBaloon { get; set; } = false;

        public int MAX_SPEED { get; private set; } = 9;
        public int MAX_BALLOON_COUNT { get; private set; } = 6;
        public int MAX_EXPLOSION_POWER { get; private set; } = 7;

        public event Action StatusEvent;

        private void Update()
        {
        

            Debug.Log($"스피드 , 파워 , 카운트, 킥 :{currentSpeed},{currentExplosionPower},{currentBalloonCount},{kickBaloon}");
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {   
           if(collision.gameObject.CompareTag("Explosion"))
            {
                Debug.Log($"{name} : 공격당했다 ");
            }

           if(collision.CompareTag("Item"))
            {
                item = collision.GetComponent<TakeItem>();

                item.PlayerTakeItem(gameObject);
            }
        }



    }
}
