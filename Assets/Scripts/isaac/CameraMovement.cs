﻿/// CameraMovement.cs Script
/// This is a modified script from an an online tutorial.
/// This script alters the movement of the 
/// camera by making it smoother and delaying
/// movement of the camera until the character has
/// moved a certain distance. There is also a ability to
/// zoom in and out using the scroll wheel.
/// 
/// -Written by Isaac Meisner

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
		public Transform player;        // The target that the camera must follow.
		public float xMargin = 1f;      // Distance in the x axis the player can move before the camera follows.
		public float yMargin = 1f;      // Distance in the y axis the player can move before the camera follows.
		public float xSmooth = 8f;      // How smoothly the camera catches up with it's target movement in the x axis.
		public float ySmooth = 8f;      // How smoothly the camera catches up with it's target movement in the y axis.
		public Vector2 maxXAndY;        // The maximum x and y coordinates the camera can have.
		public Vector2 minXAndY;        // The minimum x and y coordinates the camera can have.
		float cameraDistance;
		float cameraMin = 2.0f;
		float cameraMax = 10.0f;
		float zoomIncrement = 0.5f;
	          
		bool CheckXMargin()
		{
			// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
			return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
		}
		
		
		bool CheckYMargin()
		{
			// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
			return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
		}
		
		void Update()
		{
			cameraDistance = Camera.main.orthographicSize;
			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
						cameraDistance += zoomIncrement;
						cameraDistance = Mathf.Clamp (cameraDistance, cameraMin, cameraMax);
						Camera.main.orthographicSize = cameraDistance;
				}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
					cameraDistance -= zoomIncrement;
					cameraDistance = Mathf.Clamp (cameraDistance, cameraMin, cameraMax);
					Camera.main.orthographicSize = cameraDistance;
				}
		}

		
		void LateUpdate ()
		{
			TrackPlayer();
		}
		
		
		void TrackPlayer ()
		{
			// By default the target x and y coordinates of the camera are it's current x and y coordinates.
			float targetX = transform.position.x;
			float targetY = transform.position.y;
			
			// If the player has moved beyond the x margin...
			if(CheckXMargin())
				// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
				targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
			
			// If the player has moved beyond the y margin...
			if(CheckYMargin())
				// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
				targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
			
			// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
			targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
			targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

			// Set the camera's position to the target position with the same z component.
			transform.position = new Vector3(targetX, targetY, transform.position.z);

		}
}