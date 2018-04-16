using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRing : MonoBehaviour {
    public int spawnernumber;
    public float tilt, radius;
    public StuffSpawner spawnerPref;

    private void Awake()
    {
        for (int i = 0; i < spawnernumber; i++)
        {
            CreateSpawner(i);
        }
    }
    void CreateSpawner(int index)
    {
        Transform rotator = new GameObject("Rotator").transform;
        rotator.SetParent(transform, false);
        rotator.localRotation = Quaternion.Euler(0f,  index*360/spawnernumber, 0f);

        StuffSpawner spawn = Instantiate<StuffSpawner>(spawnerPref);
        spawn.transform.SetParent(rotator,false);
        spawn.transform.localPosition = new Vector3(0f ,0f, radius);
        spawn.transform.localRotation = Quaternion.Euler(tilt, 0f, 0f);
    }

}
