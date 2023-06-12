using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum Behaviour
    {
        idle,
        searchPlayer,
        attack,
    }
    private Behaviour enemyBehaviour;
    private Behaviour prevBehaviour;
    public Animator animatorAttack;



    private void Update()
    {
        if (enemyBehaviour != prevBehaviour)
        {
            switch (enemyBehaviour)
            {
                case Behaviour.idle:
                    //idle
                    break;
                case Behaviour.searchPlayer:
                    GameObject.FindGameObjectsWithTag("Player");
                    break;
                case Behaviour.attack:
                    animatorAttack.SetBool("IsAttacking", true);
                    Debug.Log("Nyerang");
                    break;
            }
        }
    }
}
