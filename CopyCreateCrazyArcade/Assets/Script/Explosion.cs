using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Assets.Script
{

    public class Explosion : MonoBehaviour
    {
        Collider2D[] target = new Collider2D[2];

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                Destroy(collision.gameObject);
            }

        }
        public void DestroyAfter(float second)
        {
            Destroy(gameObject, second);
        }

        public void SetDirection(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }



    }

}
