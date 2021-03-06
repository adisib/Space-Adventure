﻿/*******************/
/* Enemy AI script */
/* Author: Hehfay  */
/*******************/

using UnityEngine;
using System.Collections;

public class JH_Enemy_AI : MonoBehaviour 
{
  /*************/
  /* VARIABLES */
  /*************/

  // Counter used to keep track of how many bullets have been fired by the enemy.

  // Bullet prefab
  public GameObject BulletPrefab;

  // Declare a player game object so we can find it and use it as the enemy fireTo
  public GameObject fireTo;

  // Enemy line of sight
  RaycastHit2D lineOfSight;  

  // What the enenmy line of sight ray will collide with
  public LayerMask whatToHit;

  // Enemy direction
  Vector3 currentDirectionVector;

  public bool canShoot;
  
  public enum directions{
    left,
    right
  }; 
  public directions currentDirection;
  
  public enum alertStatus{
    standing,  
    casualPatrol,
    patrolling,
    attacking
  }; 
  public alertStatus enemyAlertStatus;
  
  static float standingSpeed   = 0.0f;
  static float casualSpeed     = 0.015f;
  static float patrollingSpeed = 0.025f;
  static float attackingSpeed  = 0.075f;  
  float[] speed = {
    standingSpeed,
     casualSpeed,  
    patrollingSpeed, 
    attackingSpeed
  };  

  /*************/
  /* Functions */
  /*************/

  // Use this for initialization 
  void Start(){
    // Set the starting direction based on gui selection
    if( currentDirection == directions.left ){
        currentDirectionVector = Vector3.left; // Left
    }
    else{
      currentDirectionVector = Vector3.right; // Right
    }

    // Find the player game object
//    fireTo = GameObject.FindWithTag( "Player" );
    canShoot = true;
    fireTo = GameObject.FindWithTag( "Player" );
  }

  // Called on fixed intervals
  void FixedUpdate(){      
    Look();   // Enemy line of sight
    rayCollision(lineOfSight); // Modify state based on vision    
    Move(); // Moved based on state
  }
  
  // Collisions between the enemy ray and other tags
  void OnTriggerEnter2D(Collider2D other){  
    if( other.gameObject.CompareTag("Edge Collider") || other.gameObject.CompareTag("Enemy") ){
      changeDirection();
    }  
  }

  void rayCollision(RaycastHit2D lineOfSight){
    if( lineOfSight.collider != null )
    {
      if( lineOfSight.collider.CompareTag("Player") ){ //If enemy sees the player go into attack mode
        enemyAlertStatus = alertStatus.attacking;
				Shoot();
      }
			else{
        enemyAlertStatus = alertStatus.patrolling;  
			}
    }
  } 

  void changeDirection(){
    if( currentDirection == directions.left ){
      currentDirection = directions.right;
      currentDirectionVector = Vector3.right; // Right
    }
    else{
      currentDirection = directions.left;
      currentDirectionVector = Vector3.left; // Left
    }
  }

  void Move(){  

    switch ( enemyAlertStatus ){  

      case alertStatus.standing:  
        transform.Translate(currentDirectionVector * speed[(int)enemyAlertStatus]); // Enemy movement
        break;
        
      case alertStatus.attacking:
  
        break;

      case alertStatus.casualPatrol:
        transform.Translate(currentDirectionVector * speed[(int)enemyAlertStatus]); // Enemy movement
        break;

      case alertStatus.patrolling:
        transform.Translate(currentDirectionVector * speed[(int)enemyAlertStatus]); // Enemy movement  
        break;  
    }
  }

  void Look(){

    switch ( enemyAlertStatus )
    {
      case alertStatus.attacking:
/*        lineOfSight = Physics2D.Raycast ( transform.position
                                        ,  fireTo.transform.position
                                        , 10
                                        , whatToHit );

        Debug.DrawLine ( transform.position
                        , fireTo.transform.position
                        , Color.red );
*/
//        if( lineOfSight.collider.tag == "Player" )
 //       {
  //        Shoot();
   //     }
 //       break;
      case alertStatus.standing:
      case alertStatus.casualPatrol:
      case alertStatus.patrolling:
        lineOfSight = Physics2D.Raycast ( transform.position 
                                        , transform.TransformDirection(currentDirectionVector)
                                        , 10
                                        , whatToHit );  
        break;
    }
  }

  void Shoot(){

    if( canShoot )
    {
      canShoot = false;
      GameObject bullet = Instantiate(BulletPrefab) as GameObject;

      bullet.transform.position = transform.position;

      DT_BulletMovement bulletScript = bullet.GetComponent<DT_BulletMovement>();

      bulletScript.sourceAI = this;

			bulletScript.direction = transform.TransformDirection( currentDirectionVector );
//      bulletScript.direction = fireTo.transform.position;
      bulletScript.speed = 0.25f;
    }  
  }
} /* END OF FILE */

