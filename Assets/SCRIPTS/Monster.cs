using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    /// <summary>
    /// The main script to control monsters.
    /// </summary>
    public class Monster : MonoBehaviour
    {
      
        public Animator Animator;
        [SerializeField] private Transform weapon;
        [SerializeField] private Sprite headattackSprite;
        [SerializeField] private Sprite headidleSprite;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        
        public void ThrowLetter()
        {
            if (SpawnManager.instance.currentLetter)
            {
                var letter = Instantiate(SpawnManager.instance.currentLetter, weapon.position, Quaternion.identity);
                letter.SendMessage("SetDynamic", SendMessageOptions.DontRequireReceiver);
            }
        }
        
        public void IdleAction()
        {
            spriteRenderer.sprite = headidleSprite;
        }
          public void AttackAction()
        {
            spriteRenderer.sprite = headattackSprite;

        }
        
        public void Attack()
        {
            Animator.SetTrigger("Attack");
        }

    }
