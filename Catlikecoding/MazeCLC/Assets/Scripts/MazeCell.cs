using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

    public MazeRoom room;
    public IntVector2 coordinates;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    private int visitedEdgesCount;
    

    public void Initialize(MazeRoom room)
    {
        room.Add(this);
        transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
    }

    public bool isFullyInitialized
    {
        get
        {
            return visitedEdgesCount == MazeDirections.Count;
        }
    }
    public void SetEdge(MazeDirection dir, MazeCellEdge edge)
    {
        edges[(int)dir] = edge;
        visitedEdgesCount++;
    }
    public MazeCellEdge GetEdge(MazeDirection dir)
    {
        return edges[(int)dir];
    }
    public MazeDirection RandomUninitializedDirections
    {
        get
        {
            int skips = Random.Range(0, MazeDirections.Count - visitedEdgesCount);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if(edges[i] == null)
                {
                    if(skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    --skips;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }
    public void OnPlayerExited()
    {
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i].OnPlayerExited();
        }
    }
    public void OnPlayerEntered()
    {
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i].OnPlayerEntered();
        }
    }

}
