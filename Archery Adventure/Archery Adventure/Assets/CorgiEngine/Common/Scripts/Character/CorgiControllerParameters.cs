﻿using System;
using UnityEngine;
using System.Collections;

namespace MoreMountains.CorgiEngine
{	
	[RequireComponent(typeof(Collider2D))]

	/// <summary>
	/// Parameters for the Corgi Controller class.
	/// This is where you define your slope limit, gravity, and speed dampening factors
	/// </summary>

	[Serializable]
	public class CorgiControllerParameters 
	{
		/// Maximum velocity for your character, to prevent it from moving too fast on a slope for example
		public Vector2 MaxVelocity = new Vector2(200f, 200f);
		/// Maximum angle (in degrees) the character can walk on
		[Range(0,90)]
		public float MaximumSlopeAngle = 45;		
		/// Gravity
		public float Gravity = -15;	
		/// Speed factor on the ground
		public float SpeedAccelerationOnGround = 20f;
		/// Speed factor in the air
		public float SpeedAccelerationInAir = 5f;	
		[Space(10)]	
		[Header("Physics2D Interaction [Experimental]")]
		/// if set to true, the character will transfer its force to all the rigidbodies it collides with horizontally
		public bool Physics2DInteraction=true;
		/// the force applied to the objects the character encounters
		public float Physics2DPushForce=2.0f;
		[Space(10)]	
		/// if set to true, will draw the various raycasts used by the CorgiController to detect collisions in scene view if gizmos are active
		public bool DrawRaycastsGizmos = true;
		/// the speed multiplier to apply when walking on a slope
		public AnimationCurve SlopeAngleSpeedFactor = new AnimationCurve(new Keyframe(-90f,1f),new Keyframe(0f,1f),new Keyframe(90f,1f));
	}
}