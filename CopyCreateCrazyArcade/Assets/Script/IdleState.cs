using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    PlayerInput _input;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _input = animator.GetComponent<PlayerInput>();
        if (_input._manager.gamePlay)
        {
            if (animator.gameObject.name == "1PCharacter(Clone)")
            {
                FirstPlayerIdle(animator);
            }
            else
            {
                SecondPlayerIdle(animator);
            }

        }

        if (_input._manager.gamePlay == false && _input._manager.playerLive == false)
        {
            animator.SetTrigger("Win");
            if (animator.gameObject.name == "1PCharacter(Clone)")
            {
                animator.SetFloat("FirstPositionX", 0);
                animator.SetFloat("FirstPositionY", 1);

                animator.SetBool("FirstIsMoving", false);
            }
            else
            {
                animator.SetFloat("SecondPositionX", 0);
                animator.SetFloat("SecondPositionY", 1);

                animator.SetBool("SecondIsMoving", false);
            }
        }



    }
    void FirstPlayerIdle(Animator animator)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetFloat("FirstPositionX", 0);
            animator.SetFloat("FirstPositionY", -1);

            animator.SetBool("FirstIsMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("FirstIsMoving", false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetFloat("FirstPositionX", 0);
            animator.SetFloat("FirstPositionY", 1);

            animator.SetBool("FirstIsMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("FirstIsMoving", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetFloat("FirstPositionX", -1);
            animator.SetFloat("FirstPositionY", 0);

            animator.SetBool("FirstIsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("FirstIsMoving", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetFloat("FirstPositionX", 1);
            animator.SetFloat("FirstPositionY", 0);

            animator.SetBool("FirstIsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("FirstIsMoving", false);
        }
    }
    void SecondPlayerIdle(Animator animator)
    {
        if (Input.GetKey(KeyCode.R))
        {
            animator.SetFloat("SecondPositionX", 0);
            animator.SetFloat("SecondPositionY", -1);

            animator.SetBool("SecondIsMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            animator.SetBool("SecondIsMoving", false);
        }

        if (Input.GetKey(KeyCode.F))
        {
            animator.SetFloat("SecondPositionX", 0);
            animator.SetFloat("SecondPositionY", 1);

            animator.SetBool("SecondIsMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetBool("SecondIsMoving", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetFloat("SecondPositionX", -1);
            animator.SetFloat("SecondPositionY", 0);

            animator.SetBool("SecondIsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("SecondIsMoving", false);
        }

        if (Input.GetKey(KeyCode.G))
        {
            animator.SetFloat("SecondPositionX", 1);
            animator.SetFloat("SecondPositionY", 0);

            animator.SetBool("SecondIsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            animator.SetBool("SecondIsMoving", false);
        }
    }
}
