using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Galassia.Store
{
	

	public class ItemModels : MonoBehaviour
	{
		[System.Serializable]
		public class Model
		{
			[SerializeField]private string name;
			public Galassia.Generic.InventoryType type;
			public GameObject model;
		}

		[System.Serializable]
		public class ModelGroup
		{
			[SerializeField] string name;
			public Generic.InventoryGroup groupType;
			public List<Model> models;
		}

		public List<ModelGroup> groups;
	}
}