using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{

    Light luz;
    
    [SerializeField] 
    private Image contentFlash;
    
    private int contador;
    private float tiempo;
    private float duration = 0.6f;
    private float vel = 1.22f;
    private float vel2 = 2.22f;

    public PlayerMove bat;
    
    public AudioClip stress;
    public AudioClip off;
    private AudioSource audioD;
    
    
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
        audioD = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        contador = Convert.ToInt32(Math.Ceiling(tiempo));
        
        if (contador <= 30)
        {
            luz.intensity = 1f;
            contentFlash.fillAmount = 1f;
        }
        if (contador >= 30 && contador < 60)
        {
            audioD.clip = stress;
            audioD.Play();
            luz.intensity = 0.9f;
            contentFlash.fillAmount = 0.80f;
        }
        if (contador >= 60 && contador < 90)
        {
            luz.intensity = 0.8f;
            contentFlash.fillAmount = 0.60f;
            
        }
        if (contador >= 90 && contador < 120)
        {
            luz.intensity = 0.7f;
            contentFlash.fillAmount = 0.40f;
        }
        if (contador >= 120 && contador < 150)
        {
            luz.intensity = 0.5f;
            contentFlash.fillAmount = 0.20f;
        }
        if (contador >= 150 && contador < 160)
        {
            luz.intensity = Mathf.PingPong(Time.time * vel2, duration);
            contentFlash.fillAmount = 0.10f;
        }
        if (contador >= 160)
        {
            luz.intensity = 0.2f;
            contentFlash.fillAmount = 0f;
            audioD.clip = off;
            audioD.Play();
        }
    }
    
    public void resetLuz()
    {
        if (bat.bateria > 0)
        {
            tiempo = 0;
            bat.bateria = bat.bateria - 1;
        }
        else
        {
            bat.bateria = 0;
        }
    }
    
}
