using System;
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


        private Vector3 spawnScale = new Vector3(1, 1, 0);

        private bool spawnLimitTime = true;
        private PlayerStatus _playerStatus;
        private float spawnTime;
        private float spawnSpeed = 1.9f;
        private Vector3 MaxPositionY = new Vector3(0, 0.5f, 0);
        Vector3 asdf = new Vector3(0,0.5f,0);
        
        private Vector3 defaultPositionY = Vector3.zero;

        private void Awake()
        {
            defaultPositionY = transform.position;
            MaxPositionY += defaultPositionY;
        }
        bool www = true;
        float asd = 0f;

        private void Update()
        {
            if (spawnLimitTime)
            {
                spawnTime += Time.deltaTime;
                transform.localScale = spawnScale * (spawnTime * spawnSpeed);

            }
            if (spawnTime >= 0.5f)
            {
                spawnLimitTime = false;
            }


            if(transform.position.y >= MaxPositionY.y)
            {
                transform.position -= asdf  * Time.deltaTime / 2; 
            }
            if(transform.position.y <= defaultPositionY.y)
            {
                transform.position += asdf * Time.deltaTime;
            }

        }
        IEnumerator wowo2()
        {
            transform.position += MaxPositionY * Time.deltaTime * spawnSpeed;
            yield return new WaitForSeconds(1f);
            transform.position -= MaxPositionY * Time.deltaTime * spawnSpeed;
            StopCoroutine(wowo2());

        }

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
                case ItemKind.Flask:

                    if (_playerStatus.MAX_EXPLOSION_POWER > _playerStatus.currentExplosionPower)
                        _playerStatus.currentExplosionPower += 1;

                    break;

                default:
                    break;

            }
        }

    }
}
