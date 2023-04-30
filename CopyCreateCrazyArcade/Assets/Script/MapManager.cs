using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Assets.Script
{

    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = GameObject.FindObjectOfType<MapManager>();
                }

                return s_instance;
            }
        }

        private static MapManager s_instance;

        private Grid _grid;

        void Awake()
        {
            _grid = GetComponent<Grid>();

            if (s_instance == null)
            {
                s_instance = this;
            }
        }

        Vector3 _localPosition;
        private const float _plusPositionX = 0.42f;

        public Vector3 LocalToCellPosition(Transform targetTransform)
        {
            _localPosition = targetTransform.position;

            _localPosition.x = _localPosition.x + _plusPositionX;

            return _grid.LocalToCell(_localPosition);

        }
    }
}
