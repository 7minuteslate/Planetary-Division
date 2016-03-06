using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class Pod : MonoBehaviour
{
	public float MaxEnginePower = 40f;
	public float RollEffect = 50f;
	public float PitchEffect = 50f;
	public float YawEffect = 1f;
	public float BankedTurnEffect = 0.5f;
	public float AutoTurnPitch = 0.5f;
	public float AutoRollLevel = 0.1f;
	public float AutoPitchLevel = 0.1f;
	public float AirBreaksEffect = 3f;
	public float ThrottleChangeSpeed = 0.3f;
	public float DragIncreaseFactor = 0.001f;


	private float Throttle;
	private bool AirBrakes;
	private float ForwardSpeed;
	private float EnginePower;
	private float cur_MaxEnginePower;
	private float RollAngle;
	private float PitchAngle;
	private float RollInput;
	private float PitchInput;
	private float YawInput;
	private float ThrottleInput;
	private float VerticalInput;


	private float OriginalDrag;
	private float OriginalAngularDrag;
	private float AeroFactor = 1;
	private bool Immobilized = false;
	private float BankedTurnAmount;
	private Rigidbody rigidbody;
	WheelCollider[] cols;



	void Start()
	{
		rigidbody = GetComponent<Rigidbody> ();

		OriginalDrag = rigidbody.drag;
		OriginalAngularDrag = rigidbody.angularDrag;



		for (int i = 0; i < transform.childCount; i++) 
		{
			foreach (var componentsInChild in transform.GetChild(i).GetComponentsInChildren<WheelCollider>()) 
			{
				componentsInChild.motorTorque = 0.18f;
			}
		}
	}
	public void Move(float rollInput, float pitchInput, float yawInput, float throttleInput, float verticalInput , bool airBrakes)
	{
		this.RollInput = rollInput;
		this.PitchInput = pitchInput;
		this.YawInput = yawInput;
		this.ThrottleInput = throttleInput;
		this.AirBrakes = airBrakes;
		this.VerticalInput = verticalInput;

		ClampInput ();
		CalculateRollAndPitchAngles ();
		AutoLevel ();
		CalculateForwardSpeed ();
		ControlThrottle ();
		CalculateDrag ();
		CalculateLinearForces ();
		CalculateTorque ();

		if (Throttle < 0.1f) 
		{
			Vector3 currentVelocity = rigidbody.velocity;
			Vector3 newVelocity = currentVelocity * Time.deltaTime;
			rigidbody.velocity = currentVelocity - newVelocity;
		}
	}

	void ClampInput()
	{
		RollInput = Mathf.Clamp (RollInput, -1, 1);
		PitchInput = Mathf.Clamp (PitchInput, -1, 1);
		YawInput = Mathf.Clamp (YawInput, -1, 1);
		ThrottleInput = Mathf.Clamp (ThrottleInput, -1, 1);
	}

	void CalculateRollAndPitchAngles()
	{
		Vector3 flatForward = transform.forward;
		flatForward.y = 0;

		if (flatForward.sqrMagnitude > 0) 
		{
			flatForward.Normalize ();

			Vector3 localFlatForward = transform.InverseTransformDirection (flatForward);
			PitchAngle = Mathf.Atan2 (localFlatForward.y, localFlatForward.z);

			Vector3 flatRight = Vector3.Cross (Vector3.up, flatForward);

			Vector3 localFlatRight = transform.InverseTransformDirection (flatRight);

			RollAngle = Mathf.Atan2 (localFlatRight.y, localFlatRight.x);
		}
	}

	void AutoLevel()
	{
		BankedTurnAmount = Mathf.Sin (RollAngle);

		if (RollInput == 0f) 
		{
			RollInput = -RollAngle * AutoRollLevel;
		}

		if (PitchInput == 0f) 
		{
			PitchInput = -PitchAngle * AutoPitchLevel;
			PitchInput -= Mathf.Abs (BankedTurnAmount * BankedTurnAmount * AutoTurnPitch);
		}
	}

	void CalculateForwardSpeed()
	{
		Vector3 localVelocity = transform.InverseTransformDirection (rigidbody.velocity);

		ForwardSpeed = Mathf.Max (0, localVelocity.z);
	}

	void ControlThrottle()
	{
		if (Immobilized) 
		{
			ThrottleInput = -0.5f;
		}
		Throttle = Mathf.Clamp01(Throttle + ThrottleInput * Time.deltaTime * ThrottleChangeSpeed);

		EnginePower = Throttle * MaxEnginePower;
	}

	void CalculateDrag()
	{
		float extraDrag = rigidbody.velocity.magnitude * DragIncreaseFactor;
		rigidbody.drag = (AirBrakes ? (OriginalDrag + extraDrag) * AirBreaksEffect : OriginalDrag + extraDrag);
		rigidbody.angularDrag = OriginalAngularDrag * ForwardSpeed / 1000 + OriginalAngularDrag;
	}

	void CalculateLinearForces()
	{
		Vector3 forces = Vector3.zero;

		forces += EnginePower * transform.forward;


		forces += VerticalInput * 1 * transform.up;
		rigidbody.AddForce (forces);
	}

	void CalculateTorque()
	{
		Vector3 torque = Vector3.zero;

		torque += PitchInput * PitchEffect * transform.right;
		torque += YawInput * 1 * transform.up;
		torque += -RollInput * RollEffect * transform.forward;
		torque += BankedTurnAmount * BankedTurnEffect * transform.up;

		rigidbody.AddTorque (torque * AeroFactor);
	}

	public void Immobilize()
	{
		Immobilized = true;
	}
	public void Rest()
	{
		Immobilized = false;
	}
}
