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
            if (animator.gameObject.name == StringHelper.FirstPlayer)
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
            animator.SetTrigger(StringHelper.Win);
            if (animator.gameObject.name == StringHelper.FirstPlayer)
            {
                animator.SetFloat(StringHelper.FirstPositionX, 0);
                animator.SetFloat(StringHelper.FirstPositionY, 1);


                animator.SetBool(StringHelper.FirstIsMoving, false);
            }
            else
            {
                animator.SetFloat(StringHelper.SecondPositionX, 0);
                animator.SetFloat(StringHelper.SecondPositionY, 1);

                animator.SetBool(StringHelper.SecondIsMoving, false);
            }
        }



    }
    void FirstPlayerIdle(Animator animator)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetFloat(StringHelper.FirstPositionX, 0);
            animator.SetFloat(StringHelper.FirstPositionY, -1);

            animator.SetBool(StringHelper.FirstIsMoving, true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool(StringHelper.FirstIsMoving, false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetFloat(StringHelper.FirstPositionX, 0);
            animator.SetFloat(StringHelper.FirstPositionY, 1);

            animator.SetBool(StringHelper.FirstIsMoving, true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool(StringHelper.FirstIsMoving, false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetFloat(StringHelper.FirstPositionX, -1);
            animator.SetFloat(StringHelper.FirstPositionY, 0);

            animator.SetBool(StringHelper.FirstIsMoving, true);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool(StringHelper.FirstIsMoving, false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetFloat(StringHelper.FirstPositionX, 1);
            animator.SetFloat(StringHelper.FirstPositionY, 0);

            animator.SetBool(StringHelper.FirstIsMoving, true);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool(StringHelper.FirstIsMoving, false);
        }
    }
    void SecondPlayerIdle(Animator animator)
    {
        if (Input.GetKey(KeyCode.R))
        {
            animator.SetFloat(StringHelper.SecondPositionX, 0);
            animator.SetFloat(StringHelper.SecondPositionY, -1);

            animator.SetBool(StringHelper.SecondIsMoving, true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            animator.SetBool(StringHelper.SecondIsMoving, false);
        }

        if (Input.GetKey(KeyCode.F))
        {
            animator.SetFloat(StringHelper.SecondPositionX, 0);
            animator.SetFloat(StringHelper.SecondPositionY, 1);

            animator.SetBool(StringHelper.SecondIsMoving, true);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetBool(StringHelper.SecondIsMoving, false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetFloat(StringHelper.SecondPositionX, -1);
            animator.SetFloat(StringHelper.SecondPositionY, 0);

            animator.SetBool(StringHelper.SecondIsMoving, true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool(StringHelper.SecondIsMoving, false);
        }

        if (Input.GetKey(KeyCode.G))
        {
            animator.SetFloat(StringHelper.SecondPositionX, 1);
            animator.SetFloat(StringHelper.SecondPositionY, 0);

            animator.SetBool(StringHelper.SecondIsMoving, true);
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            animator.SetBool(StringHelper.SecondIsMoving, false);
        }
    }
}
