using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerAttacker : MonoBehaviour
    {
        public Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }
       
        public void HandleLightAttack(WeaponItem weapon)
        {
            animator.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animator.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        }
        
    }
}
