using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Script
{

    public class TakeItem : MonoBehaviour
    {

        public enum ItemKind
        {
            Skate,
            Balloon,
            Shoes,
            Flask
        }
        public ItemKind kind;

        private PlayerStatus _playerStatus;
        public void PlayerTakeItem(GameObject status)
        {
            _playerStatus = status.GetComponent<PlayerStatus>();
            switch (kind)
            {

                case ItemKind.Skate:

                    if (_playerStatus.MAX_SPEED > _playerStatus.currentSpeed)
                        _playerStatus.currentSpeed += 1;
                    
                    break;
                case ItemKind.Balloon:

                    if (_playerStatus.MAX_BALLOON_COUNT > _playerStatus.currentBalloonCount)
                        _playerStatus.currentBalloonCount += 1;
                    
                    break;

                case ItemKind.Shoes:

                    if (_playerStatus.kickBaloon == false)
                        _playerStatus.kickBaloon = true;
                    
                    break;

                case ItemKind.Flask:

                    if (_playerStatus.MAX_EXPLOSION_POWER > _playerStatus.currentExplosionPower)
                        _playerStatus.currentExplosionPower += 1;
                    
                    break;

                default:
                    break;

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
