using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A pool of objects that can be reused.
public class ObjectPool : MonoBehaviour
{

	#region PRIVATE VARIABLES
	private Queue<GameObject> pool;
	private GameObject prefab;

	private Transform parent;

	
	#endregion

	#region CONSTRUCTOR
	// Create a new object pool.
	public ObjectPool(GameObject _prefab, int initialCapacity)
	{
		pool = new Queue<GameObject>();
		prefab = _prefab;
		parent = new GameObject(prefab.name + " Pool").transform;

		for (int i = 0; i < initialCapacity; i++)
		{
			GameObject obj = GameObject.Instantiate(prefab) as GameObject;
			obj.transform.parent = parent;
			obj.SetActive(false);
			pool.Enqueue(obj);
		}
	}
	#endregion

	// Spawn an object from the pool.
	public GameObject Spawn(Vector3 position)
	{
		GameObject obj;
		Debug.Log("Entered2");
		if (pool.Count > 0)
			obj = pool.Dequeue();

		else
		{
			obj = GameObject.Instantiate(prefab) as GameObject;
			obj.transform.parent = parent;

		}

		obj.SetActive(true);
		
		//obj.transform.position += new Vector3(157, 77, 0);
		//obj.transform.position += new Vector3(135, 47, 0);

		
		obj.transform.position = new Vector3(position.x,position.y,0f);
		//obj.transform.rotation = Quaternion.identity;

		return obj;
	}

	public void Recycle(GameObject obj)
	{
		obj.SetActive(false);
		pool.Enqueue(obj);
	}
	
}