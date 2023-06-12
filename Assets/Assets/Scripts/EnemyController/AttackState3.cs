using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState3 : StateMachineBehaviour
{
    Transform player;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 playerPosisi = player.position;
        playerPosisi.y = 0;
        animator.transform.LookAt(playerPosisi);
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > 2f)
            animator.SetBool("IsAttacking", false);
    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    
}
