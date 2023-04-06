using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetFloat("PositionX", 0);
            animator.SetFloat("PositionY", -1);

            animator.SetBool("IsMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetFloat("PositionX", 0);
            animator.SetFloat("PositionY", 1);

            animator.SetBool("IsMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetFloat("PositionX", -1);
            animator.SetFloat("PositionY", 0);

            animator.SetBool("IsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetFloat("PositionX", 1);
            animator.SetFloat("PositionY", 0);

            animator.SetBool("IsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("IsMoving", false);
        }

    }

}
