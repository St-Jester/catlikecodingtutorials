using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour {

    public MazeCell currentCell, otherCell;
    public MazeDirection direction;
    public virtual void OnPlayerEntered() { }

    public virtual void OnPlayerExited() { }
    public virtual void Initialize(MazeCell cell, MazeCell otCell, MazeDirection dir)
    {
        currentCell = cell;
        otherCell = otCell;
        direction = dir;

        cell.SetEdge(dir, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }
}
