using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Galassia.Civilian
{
	using Generic;

	public enum BodyPart
	{
		None,
		Head,
		Leg,
		Body,
	}

	public class Hit
	{
		public float damageValue;
		public BodyPart hitBodyPart;
		public InventoryType hitWeaponType;
		public Transform hitTransform;
		public Transform attackerTransfrom;
		public bool isExplosion;
	}

	public class CivilianHit : MonoBehaviour
	{
		[SerializeField] private BodyPart currentPart;
		[Tooltip ("Specify with body Parts  if it is head than Damage multiplier is max ")]
		[Range (0.1f, 10)]
		[SerializeField] private float damageMultiplier = 1;
		private Transform myTransform;
		private CivilianAI _civilian;
		private	Hit currentHit = new Hit ();
		//damage NPC
		public void ApplyDamage (float damage, Transform attacker, bool isExplosion, InventoryType damageWeapon = InventoryType.None)
		{
			if (_civilian) {
				currentHit.attackerTransfrom = attacker;
				currentHit.damageValue = damage * damageMultiplier;
				currentHit.hitBodyPart = currentPart;
				currentHit.hitTransform = myTransform;
				currentHit.hitWeaponType = damageWeapon;
				currentHit.isExplosion = isExplosion;
				_civilian.ApplyDamage (currentHit);
			}
		}


		void OnEnable ()
		{
			myTransform = transform;
			_civilian = this.gameObject.GetComponentInParent<CivilianAI> ();
		}

	}
}
