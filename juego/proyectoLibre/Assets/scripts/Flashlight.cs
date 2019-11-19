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
    
    
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        contador = Convert.ToInt32(Math.Ceiling(tiempo));
        
        if (contador <= 46)
        {
            luz.intensity = 1f;
            contentFlash.fillAmount = 1f;
        }
        if (contador >= 46 && contador < 92)
        {
            luz.intensity = 0.9f;
            contentFlash.fillAmount = 0.80f;
        }
        if (contador >= 92 && contador < 138)
        {
            luz.intensity = 0.8f;
            contentFlash.fillAmount = 0.60f;
        }
        if (contador >= 138 && contador < 184)
        {
            luz.intensity = 0.7f;
            contentFlash.fillAmount = 0.40f;
        }
        if (contador >= 184 && contador < 230)
        {
            luz.intensity = 0.5f;
            contentFlash.fillAmount = 0.20f;
        }
        if (contador >= 230 && contador < 240)
        {
            luz.intensity = Mathf.PingPong(Time.time * vel2, duration);
            contentFlash.fillAmount = 0.10f;
        }
        if (contador >= 240)
        {
            luz.intensity = 0f;
            contentFlash.fillAmount = 0f;
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
