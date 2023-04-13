using System.Collections;
using System.Collections.Generic;
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
        private int PlayerTakeItem()
        {
            switch (kind)
            {
                case ItemKind.Skate:

                    return 1;

                case ItemKind.Balloon:

                    return 2;

                case ItemKind.Shoes:
                    return 3;

                case ItemKind.Flask:
                    return 4;

                default: return 0;

            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerTakeItem();
            }
        }
    }
}
