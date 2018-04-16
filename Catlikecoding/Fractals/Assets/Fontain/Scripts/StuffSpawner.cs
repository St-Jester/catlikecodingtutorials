using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StuffSpawner : MonoBehaviour {

    public Stuff[] stuffs;
    public FloatRange timeBetween, scale,randomVelocity, angularVelocity;
    public float velocity;
    public Material[] materials;

    float currentSpawnDelay;
    float timeSinceLast;

    private void FixedUpdate()
    {
        timeSinceLast += Time.deltaTime;
        if(timeSinceLast >= currentSpawnDelay)
        {
            timeSinceLast -= currentSpawnDelay;
            currentSpawnDelay = timeBetween.RandomInRange;
            Spawn();
        }
    }
    void Spawn()
    {
        Stuff prefab = stuffs[Random.Range(0, stuffs.Length)];
        Stuff spawn  = prefab.GetPooledInstance<Stuff>();
        spawn.transform.localPosition = transform.position;
        spawn.transform.localScale = Vector3.one * scale.RandomInRange;
        spawn.transform.localRotation = Random.rotation;
        spawn.body.velocity = transform.up * velocity+Random.onUnitSphere*randomVelocity.RandomInRange;
        spawn.body.angularVelocity = Random.onUnitSphere * angularVelocity.RandomInRange;
        spawn.SetMaterial(materials[Random.Range(0,materials.Length)]);
    }
}
