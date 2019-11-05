using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{

    Light luz;
    
    [SerializeField] 
    private Image content;
    
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
        
        if (contador <= 36)
        {
            luz.intensity = 1f;
            content.fillAmount = 1f;
        }
        if (contador >= 36 && contador < 72)
        {
            luz.intensity = 0.9f;
            content.fillAmount = 0.80f;
        }
        if (contador >= 72 && contador < 108)
        {
            luz.intensity = 0.8f;
            content.fillAmount = 0.60f;
        }
        if (contador >= 108 && contador < 144)
        {
            luz.intensity = 0.7f;
            content.fillAmount = 0.40f;
        }
        if (contador >= 144 && contador < 170)
        {
            luz.intensity = 0.5f;
            content.fillAmount = 0.20f;
        }
        if (contador >= 170 && contador < 180)
        {
            luz.intensity = Mathf.PingPong(Time.time * vel2, duration);
            content.fillAmount = 0.10f;
        }
        if (contador >= 180)
        {
            luz.intensity = 0f;
            content.fillAmount = 0f;
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
