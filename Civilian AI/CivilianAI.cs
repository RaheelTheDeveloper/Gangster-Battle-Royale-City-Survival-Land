using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Galassia.Civilian
{
	using Generic;
	using Controller;
	using AI;

	[RequireComponent (typeof(NavMeshAgent))]
	[RequireComponent (typeof(AnimatorOverrideController))]
	public class CivilianAI : MonoBehaviour
	{
		
		private Animator anim;
		private NavMeshAgent agent;
		private Transform playerTransform;

		[Header ("-------------Controls---------")]
		[Space ()]

		[SerializeField]private float runSpeed = 2.5f;
		[SerializeField]private float walkSpeed = 1;
		[SerializeField]private State currentState = State.IDLE;
		private Vector3 minBoundsPoint;
		private Vector3 maxBoundsPoint;
		private float boundsSize = -500;

		private bool isHit = false;

		private enum State
		{
			IDLE,
			WALK,
			RUN,
			DIE,
		}
			

		// Use this for initialization
		void Awake ()
		{
			anim = GetComponent <Animator> ();
			agent = GetComponent <NavMeshAgent> ();
			playerTransform = GameController2.Instance.fpsPlayer.transform;
//			agent.enabled = true;

		}


		void Start ()
		{
			StartAI ();

		}

		void StartAI ()
		{
			StopCoroutine ("StateMachine");
			StartCoroutine (StateMachine ());
		}



		IEnumerator StateMachine ()
		{
			switch (currentState) {


			case State.IDLE:
				#region Idle
				print ("Idle");
				currentState = State.WALK;

				#endregion
				break;

		
				break;
			case State.WALK:
				#region WALK
				print ("WALK");
				Walk ();
				yield return  new WaitUntil (() => IsReached || isHit);
				if (isHit) {
					
					currentState = State.RUN;
				}


				#endregion


				break;
			case State.RUN:
				#region RUN 
				print ("RUN");
				Run ();
				yield return  new WaitUntil (() => IsReached);
				currentState = State.WALK;
				#endregion

				break;

			default:
				#region 


				#endregion
				break;
			}

			#region Loop Condition
			yield return null;
			if (currentState != State.DIE) {
				StopCoroutine ("StateMachine");
				StartCoroutine (StateMachine ());
			} else {
				print ("Death");
				StopAllCoroutines ();
			}

			#endregion


		}

		[SerializeField] float targetRange = 100;

		private	Vector3  GetRandomPositon ()
		{
			Vector3 pos = Vector3.zero;
			if (RandomPoint (this.transform.position, targetRange, out pos)) {
				return pos;
			}
			return pos;
		}

		bool RandomPoint (Vector3 center, float range, out Vector3 result)
		{
			for (int i = 0; i < 30; i++) {
				Vector3 randomPoint = center + Random.insideUnitSphere * range;
				NavMeshHit hit;
				if (NavMesh.SamplePosition (randomPoint, out hit, range, NavMesh.AllAreas)) {
					result = hit.position;
					return true;
				}
			}
			result = playerTransform.position;
			return false;
		}


		void Walk ()
		{
			if(!agent.enabled)return;
				
			ChecKDistance ();
			agent.isStopped = false;
			agent.speed = walkSpeed;
			agent.SetDestination (GetRandomPositon ());
			while ( (agent.pathStatus == NavMeshPathStatus.PathInvalid) || (agent.pathStatus == NavMeshPathStatus.PathInvalid)) {
				agent.SetDestination (GetRandomPositon ());
				print ("Khalil");
			}

			anim.SetFloat ("Forward", 0.5f);

		}



		bool IsReached {
			get {
				if (agent.hasPath && agent.remainingDistance < (0.5f + agent.stoppingDistance)) {
					return true;
				}

				return false;	
			}
		}

	

		void Run ()
		{
			if(!agent.enabled)return;
			ChecKDistance ();
			isHit = false;
			agent.isStopped = false;
			agent.speed = runSpeed;
			agent.SetDestination (GetRandomPositon ());
			while ((agent.pathStatus == NavMeshPathStatus.PathInvalid) || (agent.pathStatus == NavMeshPathStatus.PathInvalid)) {
				print ("Khalil");
				agent.SetDestination (GetRandomPositon ());
			}
		
			anim.SetFloat ("Forward", 1f);

		}





		#region Health

		[Header ("--------------Health--------------")]
		[Space ()]
		[SerializeField] private float Health = 100;
		[SerializeField] private float currentHealth = 100;
		[SerializeField] private AudioClip hitSound;
		[SerializeField] private AudioClip deathSound;
		[SerializeField] private List<InventoryType> deathGift;
		[SerializeField] List<GameObject> bodyParts;
		private Hit _hit;

		public void ApplyDamage (Hit hit)
		{
			isHit = true;
			if (currentState.Equals (State.DIE)) {
				return;
			}
			currentHealth -= hit.damageValue;
			if (currentHealth <= 0) {
				_hit = hit;
				Died ();
				return;
			}


		}

		void Died ()
		{
			currentState = State.DIE;
			GameObject	dead = PoolingManager.Instance.getPoolGameObject (PoolObjectType.EnemyRagdoll, this.transform.position, transform.rotation) as GameObject;
			if (dead) {
				dead.SetActive (true);
				RagdollSettings setting = dead.GetComponent <RagdollSettings> ();
				if (setting) {
					setting.dropItemList = deathGift;
					CompareBodyParts (setting.bodyParts);
					setting.UpdateTransform (this.transform, _hit.hitTransform, _hit.attackerTransfrom, _hit.isExplosion);
				}
			}

			Destroy (this.gameObject);
		}

		void CompareBodyParts (List<GameObject> parts)
		{
			foreach (var aiPart in bodyParts) {
				foreach (var deadPart in parts) {
					if (aiPart.name == deadPart.name && aiPart.activeInHierarchy) {
						deadPart.SetActive (true);
					}
				}
			}
		}

		#endregion



		#region Hide Enemy

		[Header ("----------Hide---------")]
		[Space ()]
		[SerializeField]private bool isHide = false;
		[SerializeField]private float hideDistance = 100;

		void ChecKDistance ()
		{
			if (!isHide)
				return;


			float _dist = Vector3.Distance (playerTransform.position, this.transform.position);
			if (_dist > hideDistance) {
				DriverSystem.DriverController.Instance.currentHumans--;
				StopAllCoroutines ();
				Destroy (this.gameObject);
			}
		}



		#endregion

	}

}
