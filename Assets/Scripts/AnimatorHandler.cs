using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator animator;
        int vertical;
        int horizontal;
        //public bool isInteracting;
        
        public void Intialize()
        {
            animator.GetComponent<Animator>();
            //vertical = animator.StringToHash("Vetical");
            //horizontal = animator.StringToHash("Horizontal");
        }

        //public void Update AnimatorValues(float verticalMovement, float HorizontalMovement)
        

        public void PlayTargetAnimation(string targetAnim, bool IsInteracting)
        {
            animator.applyRootMotion = IsInteracting;
            animator.SetBool("IsInteracting", IsInteracting);

            animator.CrossFade(targetAnim, 0.2f);
        }
    } 

}