using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("PlayerInputUp", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("PlayerInputUp", false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("PlayerInputDown", true);
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("PlayerInputDown", false);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("PlayerInputLeft", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("PlayerInputLeft", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("PlayerInputRight", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("PlayerInputRight", false);
        }

    }

}
