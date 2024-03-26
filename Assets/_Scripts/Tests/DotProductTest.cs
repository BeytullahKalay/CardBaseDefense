using UnityEngine;

public class DotProductTest : MonoBehaviour
{
	[SerializeField] private Transform square;


	void Update()
	{
		var targetDir = (square.position - transform.position).normalized;

		Debug.Log(Vector2.Dot(transform.up, targetDir));
	}
}
