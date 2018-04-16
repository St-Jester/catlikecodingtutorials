using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Maze mazeprefab;
    Maze mazeInstance;
    public Player playerPrefab;

    private Player playerInstance;
    void Start () {
        StartCoroutine(BeginGame());
	}
	
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
	}
    void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
        
        
    }
    IEnumerator BeginGame()
    {
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        mazeInstance = Instantiate(mazeprefab) as Maze;

        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandCoord));
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);

    }

}
