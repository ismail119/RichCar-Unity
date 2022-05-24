using System;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
        private Animator playerAnimator;


        private void Awake()
        {
                playerAnimator = GetComponent<Animator>();
        }

        public void PlayerDriving()
        {
                playerAnimator.Play("Driving");       
        }
        public void PlayerIdle()
        {
                playerAnimator.Play("Idle");       
        }

        public void PlayerWalking()
        {
                playerAnimator.Play("Walk");
        }

        public void PlayerDance()
        {
                playerAnimator.Play("Dance");
        }
}
