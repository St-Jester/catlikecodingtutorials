using UnityEngine;

public class MazeWall : MazeCellEdge {
    public Transform wall;
    public override void Initialize(MazeCell cell, MazeCell otCell, MazeDirection dir)
    {
        base.Initialize(cell, otCell, dir);
        wall.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
    }
}
