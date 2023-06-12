using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour, IPunObservable
{
    private Animator animator;
    public float maxHealth = 100f;
    float currentHealth;

    public HealthBar healthBar;
    
    

    private bool isDead = false;

    
    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    public void Kena(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthBar?.SetHealth(currentHealth);
            animator.SetTrigger("Hurt");
        }

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    

    void Die()
    {
        if (!isDead)
        {
            animator.SetTrigger("Hurt");
            animator.SetBool("IsDead", true);

            GetComponent<Collider>().enabled = false;
            

            StartCoroutine(fadeOut());

            isDead = true;
        }
    }


    IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(currentHealth);
        }
        else
        {
            //Network player, receive data
            currentHealth = (float)stream.ReceiveNext();
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    

}