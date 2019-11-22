using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disparo : MonoBehaviour
{

    public Rigidbody balaPrefab;
    public Transform jugador;
    public float velDisparo;
    private float inicioDisparo;
    public float tiempoDisparo;
    private int balas;
    public Text balasTxt;
    public int balasRes; 
    public Text balasResTxt;
    private string alerta;
    private string alerta2;
    public Text alertaTxt;

    private Slasher slash;
    public Pausa pause;
    public PlayerMove playerM;

    public AudioClip shot;
    private AudioSource audioD;
    
    // Start is called before the first frame update
    void Start()
    {
        alerta = "Press R to reload";
        alerta2 = "No more bullets!";
        balas = 6;
        balasRes = 24;
        audioD = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //animShot.SetBool("shot", false);
        //animShot.SetBool("off", false);
        //mesanjes de sin balas/recargar
        if (balas == 0)
        {
            alerta = "Press R to reload";
            alertaTxt.text = alerta;
                
            if (balasRes == 0)
            {
                alertaTxt.text = alerta2;
            }
        }
        
        balasTxt.text = balas.ToString();
        balasResTxt.text = balasRes.ToString();
        //disparo
        if (pause.GamsIsPaused == false)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > inicioDisparo && balas != 0)
            {
                //animShot.SetBool("shot", true);
                //animShot.SetBool("off", false);
                balas = balas - 1;
                audioD.clip = shot;
                audioD.Play();

                inicioDisparo = Time.time + tiempoDisparo;
                Rigidbody balaPrefInstanc;

                balaPrefInstanc = Instantiate(balaPrefab, jugador.position, Quaternion.identity);
                balaPrefInstanc.AddForce(jugador.forward * velDisparo * 100);
            }
        }
        

        //recargar
        if (Input.GetKeyDown(KeyCode.R) && balasRes != 0)
        {
            alerta = "";
            alertaTxt.text = alerta;
            if (balasRes >= 6 && balas == 0)
            {
                balas = 6;
                balasRes = balasRes - 6;
            }
            if (balasRes >= 6 && balas == 1)
            {
                balas = 6;
                balasRes = balasRes - 5;
            }
            if (balasRes >= 6  && balas == 2)
            {
                balas = 6;
                balasRes = balasRes - 4;
            }
            if (balasRes >= 6 && balas == 3)
            {
                balas = 6;
                balasRes = balasRes - 3;
            }
            if (balasRes >= 6 && balas == 4)
            {
                balas = 6;
                balasRes = balasRes - 2;
            }
            if (balasRes >= 6 && balas == 5)
            {
                balas = 6;
                balasRes = balasRes - 1;
            }
            if (balasRes >= 6 && balas == 6)
            {
                balas = 6;
            }
            
            
            if (balasRes == 5 && balas == 6)
            {
                balas = 6;
            }
            if (balasRes == 5 && balas == 5)
            {
                balas = 6;
                balasRes = balasRes - 1;
            }
            if (balasRes == 5 && balas == 4)
            {
                balas = 6;
                balasRes = balasRes - 2;
            }
            if (balasRes == 5 && balas == 3)
            {
                balas = 6;
                balasRes = balasRes - 3;
            }
            if (balasRes == 5 && balas == 2)
            {
                balas = 6;
                balasRes = balasRes - 4;
            }
            if (balasRes == 5 && balas == 1)
            {
                balas = 6;
                balasRes = balasRes - 5;
            }
            if (balasRes == 5 && balas == 0)
            {
                balas = 6;
                balasRes = balasRes - balasRes;
            }
            
            
            if (balasRes == 4 && balas == 6)
            {
                balas = 6;
            }
            if (balasRes == 4 && balas == 5)
            {
                balas = 6;
                balasRes = balasRes - 1;
            }
            if (balasRes == 4 && balas == 4)
            {
                balas = 6;
                balasRes = balasRes - 2;
            }
            if (balasRes == 4 && balas == 3)
            {
                balas = 6;
                balasRes = balasRes - 3;
            }
            if (balasRes == 4 && balas == 2)
            {
                balas = 6;
                balasRes = balasRes - 4;
            }
            if (balasRes == 4 && balas == 1)
            {
                balas = 5;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 4 && balas == 0)
            {
                balas = 4;
                balasRes = balasRes - balasRes;
            }
            
            
            if (balasRes == 3 && balas == 6)
            {
                balas = 6;
            }
            if (balasRes == 3 && balas == 5)
            {
                balas = 6;
                balasRes = balasRes - 1;
            }
            if (balasRes == 3 && balas == 4)
            {
                balas = 6;
                balasRes = balasRes - 2;
            }
            if (balasRes == 3 && balas == 3)
            {
                balas = 6;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 3 && balas == 2)
            {
                balas = 5;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 3 && balas == 1)
            {
                balas = 4;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 3 && balas == 0)
            {
                balas = 3;
                balasRes = balasRes - balasRes;
            }
            
            
            if (balasRes == 2 && balas == 6)
            {
                balas = 6;
            }
            if (balasRes == 2 && balas == 5)
            {
                balas = 6;
                balasRes = balasRes - 1;
            }
            if (balasRes == 2 && balas == 4)
            {
                balas = 6;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 2 && balas == 3)
            {
                balas = 5;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 2 && balas == 2)
            {
                balas = 4;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 2 && balas == 1)
            {
                balas = 3;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 2 && balas == 0)
            {
                balas = 2;
                balasRes = balasRes - balasRes;
            }
            
            
            if (balasRes == 1 && balas == 6)
            {
                balas = 6;
            }
            if (balasRes == 1 && balas == 5)
            {
                balas = 6;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 1 && balas == 4)
            {
                balas = 5;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 1 && balas == 3)
            {
                balas = 4;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 1 && balas == 2)
            {
                balas = 3;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 1 && balas == 1)
            {
                balas = 2;
                balasRes = balasRes - balasRes;
            }
            if (balasRes == 1 && balas == 0)
            {
                balas = 1;
                balasRes = balasRes - balasRes;
            }
        }
    }

    //agarrar balas
    public void SetRecarga(int recarga)
    {
        if (balasRes < 60)
        {
            balasRes = balasRes + recarga;
        }
        else
        {
            balasRes = 66;
        }
    }

    
}
