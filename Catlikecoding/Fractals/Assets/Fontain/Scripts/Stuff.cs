using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Stuff : PooledObject
{

	public Rigidbody body { get; private set; }
    MeshRenderer[] meshRenderers;
    public void SetMaterial(Material m)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = m;
        }
    }
	void Awake ()
    {
        body = GetComponent<Rigidbody>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kill Zone")
            ReturnToPool();
    }
}
