﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   private bool municion;


   public disparo recarga;

   private void Start()
   {
   }

   private void Awake()
   {
      charController = GetComponent<CharacterController>();
   }

   private void Update()
   {
      PlayerMovement();
      if (municion)
      {
         if (Input.GetKeyDown(KeyCode.E))
         {
            recarga.SetRecarga(12);
            municion = false;
            //Destroy(other.gameObject);
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
   public void OnTriggerEnter(Collider other)
   {
      //agarrar balas
      if(other.gameObject.CompareTag("balas"))
      {
         municion = true;
      }
   }
}
