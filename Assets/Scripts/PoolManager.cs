using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A struct for objects to be pooled.
[System.Serializable]         //Making the whole to visible in heirarchy window
public class ObjectToPool
{
	public GameObject prefab;
	public int initialCapacity;
	public Button shootButton;

}

public class PoolManager : MonoBehaviour
{
	#region PUBLIC VARIABLES
	// Objects to be pooled at initialization.
	public ObjectToPool[] prefabsToPool;
	public Transform[] points;
	public Transform bulletPoint;
	//`public GameObject Bullet;
	#endregion

	#region PRIVATE VARIABLES
	private Dictionary<string, ObjectPool> pools;
	#endregion

	#region SINGLETON

	private static PoolManager instance;
	public static PoolManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<PoolManager>();
				if (instance == null)
				{
					GameObject container = new GameObject("PoolManager");
					instance = container.AddComponent<PoolManager>();
				}
			}
			return instance;
		}
	}
	#endregion

	void Start()
	{
		
		for (int i = 0; i < prefabsToPool.Length; i++)
		{
			CreatePool(prefabsToPool[i].prefab, prefabsToPool[i].initialCapacity);
		}

			Debug.Log("Started");
			StartCoroutine("WaitToLoad");	
	}

    public void Shoot()
    {
        
		bulletPoint.transform.rotation = Quaternion.identity;
		Spawn("Bullet", bulletPoint.position);

	}


    #region PUBLIC METHODS
    // Create a new pool of objects at runtime.
    public void CreatePool(GameObject prefab, int initialCapacity)
	{
		if (pools == null)
			pools = new Dictionary<string, ObjectPool>();                 //Dictionary are in great use when we have large list

		ObjectPool newPool = new ObjectPool(prefab, initialCapacity);
		pools.Add(prefab.name, newPool);
	}

	// Spawn an object with the given name.
	public GameObject Spawn(string prefabName,Vector3 position)
	{
		if (!pools.ContainsKey(prefabName))
			return null;
		Debug.Log("Entered");
		//int x = Random.Range(0, points.Length);
		//Vector3 temp= points[x].position;
		return pools[prefabName].Spawn(position);
	}

	// Recycle an object with the given name.
	public void Recycle(string prefabName, GameObject obj)
	{
		if (!pools.ContainsKey(prefabName))
			return;

		pools[prefabName].Recycle(obj);
	}
	#endregion

	IEnumerator WaitToLoad()
	{
		for (int i = 0; i < 8; i++)
		{
			
			int x = Random.Range(0, points.Length);
			//tVector3 temp = points[x].position;
			Spawn("Enemy", points[x].position);
			yield return new WaitForSeconds(2f);
			Spawn("HardEnemy", points[x].position);

			Debug.Log("wait");
			yield return new WaitForSeconds(4f);

			Debug.Log("Dont");
		}
	}
}