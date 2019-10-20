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
    public static int balasRes; 
    public Text balasResTxt;
    private string alerta;
    private string alerta2;
    public Text alertaTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        alerta = "Press R to reload";
        alerta2 = "No more bullets!";
        balas = 6;
        balasRes = 36;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetButtonDown("Fire1") && Time.time > inicioDisparo && balas != 0)
        {
            balas = balas - 1;
            Debug.Log(balas);
            
            
            inicioDisparo = Time.time + tiempoDisparo;
            Rigidbody balaPrefInstanc;

            balaPrefInstanc = Instantiate(balaPrefab, jugador.position, Quaternion.identity);
            balaPrefInstanc.AddForce(jugador.forward * velDisparo * 100);
            
        }

        if (Input.GetKeyDown(KeyCode.R) && balasRes != 0)
        {
            alerta = "";
            alertaTxt.text = alerta;
            if (balasRes != 0 && balas == 0)
            {
                balas = 6;
                balasRes = balasRes - 6;

            }
            if (balasRes != 0 && balas == 1)
            {
                balas = 6;
                balasRes = balasRes - 5;

            }
            if (balasRes != 0  && balas == 2)
            {
                balas = 6;
                balasRes = balasRes - 4;

            }
            if (balasRes != 0 && balas == 3)
            {
                balas = 6;
                balasRes = balasRes - 3;

            }
            if (balasRes != 0 && balas == 4)
            {
                balas = 6;
                balasRes = balasRes - 2;
            }
            if (balasRes != 0 && balas == 5)
            {
                balas = 6;
                balasRes = balasRes - 1;

            }
            if (balasRes != 0 && balas == 6)
            {
                balas = 6;
            }
        }
    }

    public void agarragBalas()
    {
        if (balasRes <= 88)
        {
            balasRes = balasRes + 12;
        } 
    }

    
}
