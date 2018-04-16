using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	public Material material;
    public int maxDepth;
    public float childScale;
    public float spawnProbability;
    public float maxTwist;
    public Mesh[] meshes;
    public float maxRotationSpeed;

    float rotationSpeed;
    int depth;
    Material[,] materials;
   

    private static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };
    private static Quaternion[] chOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f,0f,0f),
        Quaternion.Euler(-90f,0f,0f)
    };
    
    private void Start ()
    {
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
        if (materials == null)
        {
            InitializeMaterials();
        }
		gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0,meshes.Length)];
		gameObject.AddComponent<MeshRenderer>().material = materials[depth,Random.Range(0,2)];
        if(depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
    }

    void Initialize(Fractal parent, int chIndex)
    {
        maxTwist = parent.maxTwist;
        meshes = parent.meshes;
        material = parent.material;
        materials = parent.materials;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        transform.parent = parent.transform;
        childScale = parent.childScale;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[chIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = chOrientations[chIndex];
        spawnProbability = parent.spawnProbability;
    }

    private void InitializeMaterials()
    {
        materials = new Material[maxDepth + 1,2];
        for(int i = 0; i <= maxDepth; i++)
        {
            float t = i / (maxDepth - 1f);
            t *= t;
            materials[i,0] = new Material(material);
            materials[i,0].color = Color.Lerp(Color.white, Color.green, t);

            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.Lerp(Color.white, Color.blue, t);

        }
        materials[maxDepth, 0].color = Color.magenta;
        materials[maxDepth, 1].color = Color.red;

    }

    IEnumerator CreateChildren()
    {
        for (int i = 0; i < childDirections.Length; i++)
        {
            if (Random.value < spawnProbability)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
            }
        }
    }
    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}