using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Assets.Script
{

    public class Explosion : MonoBehaviour
    {
        private float _explosionActiveTime = 0.36f;

        private float _elpasedTime;

        private const int _upBubble = 2;
        private const int _downBubble = 3;
        private const int _leftBubble = 4;
        private const int _rightBubble = 5;

        private Vector2 currentColliderScalX = new Vector2(2.4f, 1f);
        private Vector2 currentColliderScalY = new Vector2(1f, 2.4f);

        //item 구현시 itemPowerCount 로 변경할것
        private int itemCount = 2;

        private void Awake()
        {
            currentColliderScalX.x += itemCount;
            currentColliderScalY.y += itemCount;

            transform.GetChild(_upBubble).localPosition = new Vector2(0, itemCount);
            transform.GetChild(_downBubble).localPosition = new Vector2(0, -itemCount);

            transform.GetChild(_leftBubble).localPosition = new Vector2(-itemCount,0);
            transform.GetChild(_rightBubble).localPosition = new Vector2(itemCount,0);

            transform.GetChild(0).localScale = currentColliderScalY;
            transform.GetChild(1).localScale = currentColliderScalX;
        }
        private void Update()
        {
            _elpasedTime += Time.deltaTime;

            if (_elpasedTime >= _explosionActiveTime)
            {
                Destroy(gameObject);

                _elpasedTime = 0;
            }

        }
       


    }


}
