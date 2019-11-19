using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Slasher : MonoBehaviour
{
    public float walkingDistance;
    public float attackingDistance;
    private Animator anim;

    private RaycastHit hit;

    public PlayerMove jug;

    public Transform player;
    NavMeshAgent enemy;
    
    [SerializeField] 
    private Image contentSlash;

    public GameObject slasherObj;
    
    private float inicioAtaque;
    public float tiempoAtaque;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ray = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, new Vector3(0,100, 0), Color.green, 300, false);
        
        //idle
        if (Vector3.Distance(player.position, enemy.transform.position) > walkingDistance)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isAttacking", false);
        }
        
        //if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), ray, out hit, 100))
        //{
            //if (hit.transform.gameObject.CompareTag("player"))
            //{
                //walking
                if(Vector3.Distance(player.position, enemy.transform.position) < walkingDistance && Vector3.Distance(player.position, enemy.transform.position) > attackingDistance)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isAttacking", false);
                    enemy.destination = player.position;
                } 
                
                //attack
                else if (Vector3.Distance(player.position, enemy.transform.position) <= attackingDistance && Time.time > inicioAtaque)
                {
                    inicioAtaque = Time.time + tiempoAtaque;
                    
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isAttacking", true);
                    
                    jug.bajarVidaPlayer(); 
                }
            //}
        //}
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bala"))
        {
            bajarVidaSlasher();
            Destroy(other.gameObject);
        }
    }
    
    public void bajarVidaSlasher()
    {
        if (contentSlash.fillAmount > 0.0f)
        {
            contentSlash.fillAmount -= 0.25f;
        }

        if (contentSlash.fillAmount == 0.0f)
        {
            Destroy(slasherObj);
        }
    }
    
    public void acuchillarSlash()
    {
        if (contentSlash.fillAmount > 0.0f)
        {
            contentSlash.fillAmount -= 0.11f;
        }

        if (contentSlash.fillAmount == 0.0f)
        {
            Destroy(slasherObj);
        }
    }
}
