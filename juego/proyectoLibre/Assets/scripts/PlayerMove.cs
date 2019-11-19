using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
   [SerializeField] private string horizontalInputName;
   [SerializeField] private string verticalInputName;
   
   
   private float movementSpeed;
   
   [SerializeField] private float walkSpeed, runSpeed;
   [SerializeField] private float runBuildUpSpeed;
   [SerializeField] private KeyCode runKey;
   
   
   [SerializeField] private float slopeForce;
   [SerializeField] private float slopeForceRayLength;

   private CharacterController charController;

   [SerializeField] private AnimationCurve jumpFallOff;
   [SerializeField] private float jumpMultiplier;
   [SerializeField] private KeyCode jumpKey;
   
   private bool isJumping;
   private bool municionBool;
   private bool cofreBool;
   private bool llaveBool;
   private bool bateriaBool;
   private bool puerta;
   private bool danado;
   private bool pelea;
   
   private bool boss;
   private bool slash;

   private float inicioCuchillo;
   public float tiempoCuchillo;
   private float inicioRegen;
   public float tiempoRegen;


   public int bateria;
   public Text bateriaTxt;
   
   public int llave;
   public Text llaveTxt;

   public Image contentPlayer;

   private GameObject destruir;
   
   public disparo recarga;
   public Flashlight flash;
   private Slasher sl;
   private Boss bo;
   
   public Animator cofre;
   
   

   private void Start()
   {
      sl = GetComponent<Slasher>();
      bo = GetComponent<Boss>();
      bateria = 1;
      bateriaTxt.text = bateria.ToString();
   }

   private void Awake()
   {
      charController = GetComponent<CharacterController>();
   }

   private void Update()
   {
      PlayerMovement();
      bateriaTxt.text = bateria.ToString();
      llaveTxt.text = llave.ToString();
      if (Input.GetKeyDown(KeyCode.E))
      {
         if (municionBool)
         {
            recarga.SetRecarga(12);
            municionBool = false;
            Destroy(destruir);
         }
         if (bateriaBool)
         {
            bateria += 1;
            bateriaBool = false;
            Destroy(destruir);
         }
         if (cofreBool)
         {
            //animacion
            if (llaveBool)
            {
               llave += 1;
               llaveBool = false;
               cofreBool = false;
               Destroy(destruir); 
            }
            llaveBool = true;
         }

         if (puerta && llave == 7)
         {
            //gano();
         }
         else if (puerta)
         {
            
         }
      }

      if (Input.GetKeyDown(KeyCode.F))
      {
         flash.resetLuz();
      }

      if (Input.GetButtonDown("Fire2") && Time.time > inicioCuchillo)
      {
         inicioCuchillo = Time.time + tiempoCuchillo;
         if (boss)
         {
            bo.acuchillarBoss();
         }

         if (slash)
         {
            Debug.Log("entra");
            sl.acuchillarSlash();
         }
      }
      
      
      //regenerar
      if (danado && pelea == false)
      {
         Debug.Log("ouch");
         if (contentPlayer.fillAmount < 100f && Time.time > inicioRegen)
         {
            Debug.Log("subo"); 
            inicioRegen = Time.time + tiempoRegen; 
            contentPlayer.fillAmount += 20.0f;
         } 
         if (contentPlayer.fillAmount >= 100.0f)
         {
            danado = false;
         }
      }
   }

   //movimiento
   private void PlayerMovement()
   {
      float horInput = Input.GetAxis(horizontalInputName);
      float verInput = Input.GetAxis(verticalInputName);

      Vector3 forwardMovement = transform.forward * verInput;
      Vector3 rightMovement = transform.right * horInput;

      charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

      if ((verInput != 0 || horInput != 0) && OnSlope())
      {
         charController.Move(Vector3.down * charController.height/2 * slopeForce * Time.deltaTime);
      }

      SetMovementSpeed();
      JumpInput();
   }

   //velocidad de movimiento
   private void SetMovementSpeed()
   {
      if (Input.GetKey(runKey))
      {
         movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
      }
      else
      {
         movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
      }
   }
   
   private bool OnSlope()
   {
      if (isJumping)
      {
         return false;
      }
      
      RaycastHit hit;
      if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height/2 * slopeForceRayLength))
      {
         if (hit.normal != Vector3.up)
         {
            return true;
         }
      }

      return false;
   }

   //metodo de salto
   private void JumpInput()
   {
      if (Input.GetKeyDown(jumpKey) && !isJumping)
      {
         isJumping = true;
         StartCoroutine(JumpEvent());
      }
   }

   //corrutina para saltar 
   private IEnumerator JumpEvent()
   {
      charController.slopeLimit = 90.0f;
      float timeAir = 0.0f;
      
      do
      {
         float jumpForce = jumpFallOff.Evaluate(timeAir);
         charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
         timeAir += Time.deltaTime; 
         
         yield return null;
      } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

      charController.slopeLimit = 45.0f;
      isJumping = false; 
   } 
   
   //trigger del jugador
   private void OnTriggerEnter(Collider other)
   {
      //agarrar balas
      if(other.gameObject.CompareTag("balas"))
      {
         municionBool = true;
         bateriaBool = false;
         destruir = other.gameObject;
      }
      
      if(other.gameObject.CompareTag("bateria"))
      {
         municionBool = false;
         bateriaBool = true;
         destruir = other.gameObject;
      }
      if(other.gameObject.CompareTag("cofre"))
      {
         municionBool = false;
         bateriaBool = false;
         cofreBool = true;
         Llave llave = other.GetComponentInChildren<Llave>();
         destruir = llave.gameObject;
      }
      if (other.gameObject.CompareTag("fuego"))
      {
         bajarVidaPlayer2();
         Destroy(other.gameObject);
      }
      if(other.gameObject.CompareTag("puerta"))
      {
         municionBool = false;
         bateriaBool = false;
         cofreBool = false;
         puerta = true;
      }
      if(other.gameObject.CompareTag("slasher"))
      {
         pelea = true;
         slash = true;
      }
      if(other.gameObject.CompareTag("boss"))
      {
         pelea = true;
         boss = true;
      }
   }

   private void OnTriggerExit(Collider otherE)
   {
      if(otherE.gameObject.CompareTag("balas"))
      {
         bateriaBool = false;
      }
      
      if(otherE.gameObject.CompareTag("bateria"))
      {
         bateriaBool = false;
      }
      if(otherE.gameObject.CompareTag("cofre"))
      {
         cofreBool = false;
         llaveBool = false;
      }
      if(otherE.gameObject.CompareTag("puerta"))
      {
         puerta = false;
      }
      if(otherE.gameObject.CompareTag("slasher"))
      {
         pelea = false;
         slash = false;
      }
      if(otherE.gameObject.CompareTag("boss"))
      {
         pelea = false;
         boss = false;
      }
   }
   
   
   public void bajarVidaPlayer()
   {
      if (contentPlayer.fillAmount > 0.0f)
      {
         contentPlayer.fillAmount -= 0.20f;
         danado = true;
      }
      
      if (contentPlayer.fillAmount <= 0f)
      {
         Debug.Log("perdio");
         //endGame();
      }
   }
   
   public void bajarVidaPlayer2()
   {
      if (contentPlayer.fillAmount > 0.0f)
      {
         contentPlayer.fillAmount -= 0.34f;
         danado = true;
      }
      
      if (contentPlayer.fillAmount <= 0f)
      {
         Debug.Log("perdio");
         //endGame();
      }
   }
}
