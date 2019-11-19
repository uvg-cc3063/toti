using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float walkingDistance;
    public float attackingDistance;
    private Animator anim;

    private RaycastHit hit;

    public PlayerMove juga;

    public Transform player;
    NavMeshAgent enemy;
    
    [SerializeField] 
    private Image contentBoss;

    public GameObject bossObj;
    
    public Transform boss;
    public float velDisparo;
    private float inicioDisparo;
    public float tiempoDisparo;
    public Rigidbody fireballPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 ray = transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(transform.position, new Vector3(0,100, 0), Color.green, 300, false);
        
        //idle
        if (Vector3.Distance(player.position, enemy.transform.position) > walkingDistance)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsAttacking", false);
        }
        
        //if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), ray, out hit, 100))
        //{
            //if (hit.transform.gameObject.CompareTag("player"))
            //{
                //walking
                if(Vector3.Distance(player.position, enemy.transform.position) < walkingDistance && Vector3.Distance(player.position, enemy.transform.position) > attackingDistance)
                {
                    anim.SetBool("IsWalking", true);
                    anim.SetBool("IsIdle", false);
                    anim.SetBool("IsAttacking", false);
                    enemy.destination = player.position;
                } 
                
                //attack
                else if (Vector3.Distance(player.position, enemy.transform.position) <= attackingDistance && Time.time > inicioDisparo)
                {
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsIdle", false);
                    anim.SetBool("IsAttacking", true);
                    
                    inicioDisparo = Time.time + tiempoDisparo;

                    Rigidbody fireballPrefInstanc;
                    fireballPrefInstanc = Instantiate(fireballPrefab, boss.position, Quaternion.identity);
                    fireballPrefInstanc.AddForce(boss.forward * velDisparo * 100);
                    enemy.destination = player.position;
                }
            //}
        //}
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bala"))
        {
            bajarVidaBoss();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("player"))
        {
            juga.bajarVidaPlayer();
        }
    }
    
    public void bajarVidaBoss()
    {
        if (contentBoss.fillAmount > 0.0f)
        {
            contentBoss.fillAmount -= 0.23f;
        }

        if (contentBoss.fillAmount == 0.0f)
        {
            Destroy(bossObj);
        }
    }

    public void acuchillarBoss()
    {
        if (contentBoss.fillAmount > 0.0f)
        {
            contentBoss.fillAmount -= 0.11f;
        }

        if (contentBoss.fillAmount == 0.0f)
        {
            Destroy(bossObj);
        }
    }
}
