using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Script
{

    public class TakeItem : MonoBehaviour
    {

        public ObjectPool<TakeItem> Pool { private get; set; }

        public enum ItemKind
        {
            Skate,
            Balloon,
            Shoes,
            Flask,
            MaxPower,
            Needle
        }
        public ItemKind kind;

        private Vector3 spawnScale = new Vector3(1, 1, 0);
        private bool spawnLimitTime = true;
        private PlayerStatus _playerStatus;
        private float spawnTime;
        private float spawnSpeed = 1.9f;

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

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag(StringHelper.Player))
            {
                Pool.Release(this);
            }
        }
        public void PlayerTakeItem(GameObject status)
        {
            _playerStatus = status.GetComponent<PlayerStatus>();
            switch (kind)
            {

                case ItemKind.Skate:

                    if (_playerStatus.MAX_SPEED > _playerStatus.currentSpeed)
                        _playerStatus.currentSpeed += 0.5f;

                    break;
                case ItemKind.Balloon:

                    if (_playerStatus.MAX_BALLOON_COUNT > _playerStatus.currentBalloonCount)
                    {
                        _playerStatus.currentBalloonCount += 1;
                        _playerStatus.storageAttackCount = _playerStatus.currentBalloonCount;
                    }

                    break;
                case ItemKind.Flask:

                    if (_playerStatus.MAX_EXPLOSION_POWER > _playerStatus.currentExplosionPower)
                        _playerStatus.currentExplosionPower += 1;

                    break;

                case ItemKind.MaxPower:
                    _playerStatus.currentExplosionPower = _playerStatus.MAX_EXPLOSION_POWER;
                    break;
                case ItemKind.Needle:
                    if(_playerStatus.needleMaxCount > _playerStatus.needleCount)
                    _playerStatus.needleCount += 1;

                    if (_playerStatus.name == StringHelper.FirstPlayer)
                    {
                        _playerStatus._input.FirstNeedleUpdate();
                    }
                    if(_playerStatus.name == StringHelper.SecondPlayer)
                    {
                        _playerStatus._input.SecondNeedleUpdate();
                    }

                    break;
                default:
                    break;

            }
        }

    }
}
