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
        Animator _anim;
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }
        public void SetAnmation(int setNumber)
        {
            _anim.SetInteger("SetAnimation",setNumber);
        }
        public void SetDirection(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }

        public void DestroyAfter(float seconds)
        {
            Destroy(gameObject, seconds);
        }
    }

}
