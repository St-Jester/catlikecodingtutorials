using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {


    [Range(0f, 1f)]
    public float doorProbability;

    public IntVector2 size;
    public MazeCell[,] cells;
    public MazeCell mazeCellPrefab;
    public MazeRoomSettings[] roomSettings;
    public MazeDoor doorPrefab;
    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;
    

    private List<MazeRoom> rooms = new List<MazeRoom>();

    public MazeCell GetCell(IntVector2 c)
    {
        return cells[c.x, c.z];
    }
    
    public IntVector2 RandCoord
    {
        get { return new IntVector2(UnityEngine.Random.Range(0, size.x), UnityEngine.Random.Range(0, size.z)); }
    }
    public bool Contains(IntVector2 c)
    {
         return c.x >= 0 && c.x < size.x && c.z >= 0 && c.z < size.z; 
    }

    public IEnumerator Generate()
    {
        yield return -1;
        //WaitForSeconds delay = new WaitForSeconds(generationDelay);
        cells = new MazeCell[size.x, size.z];

        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirst(activeCells);

        while (activeCells.Count > 0)
        {
            //yield return delay;
            DoNext(activeCells);     
        }
    }
    MazeCell CreateCell(IntVector2 v)
    {
        MazeCell newCell = Instantiate(mazeCellPrefab) as MazeCell;
        
        cells[v.x, v.z] = newCell;
        newCell.coordinates = v;
        newCell.name = "Maze Cell " + v.x + ", " + v.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(v.x - size.x * 0.5f + 0.5f, 0f, v.z - size.z * 0.5f + 0.5f);
        return newCell;
	}
    void DoFirst(List<MazeCell> active)
    {
        MazeCell newCell = CreateCell(RandCoord);
        newCell.Initialize(CreateRoom(-1));
        active.Add(newCell);
    }
    void DoNext(List<MazeCell> active)
    {
        int currIndx = active.Count - 1;

        MazeCell currentCell = active[currIndx];
        if(currentCell.isFullyInitialized)
            {
            active.RemoveAt(currIndx);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirections;
        IntVector2 c = currentCell.coordinates + direction.ToIntVec2();

        if (Contains(c))
        {
            MazeCell neighbor = GetCell(c);
            if(neighbor == null)
            {
                neighbor = CreateCell(c);
                CreatePassage(currentCell, neighbor, direction);
                active.Add(neighbor);
            }
            else if(currentCell.room.settingsIndex == neighbor.room.settingsIndex)
            {
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
                
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }  
    }

    private void CreatePassage(MazeCell currentCell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = UnityEngine.Random.value < doorProbability ? doorPrefab: passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;
        passage.Initialize(currentCell, otherCell, direction);
        passage = Instantiate(prefab) as MazePassage;
        if(passage is MazeDoor)
        {
            otherCell.Initialize(CreateRoom(currentCell.room.settingsIndex));
        }
        else
        {
            otherCell.Initialize(currentCell.room);
        }
        passage.Initialize(otherCell, currentCell, direction.GetOpposite());
    }

    private void CreatePassageInSameRoom(MazeCell currentCell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(currentCell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, currentCell, direction.GetOpposite());
        if(currentCell.room !=otherCell.room)
        {
            MazeRoom roomToAssimilate = otherCell.room;
            currentCell.room.Assimilate(roomToAssimilate);
            rooms.Remove(roomToAssimilate);
            Destroy(roomToAssimilate);
        }
    }

    private void CreateWall(MazeCell currentCell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefabs[UnityEngine.Random.Range(0,wallPrefabs.Length)]) as MazeWall;
        wall.Initialize(currentCell, otherCell, direction);
        if(otherCell != null)
        {
            wall = Instantiate(wallPrefabs[UnityEngine.Random.Range(0, wallPrefabs.Length)]) as MazeWall;
            wall.Initialize(otherCell, currentCell, direction.GetOpposite());

        }
    }
    private MazeRoom CreateRoom(int IndexToExclude)
    {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = UnityEngine.Random.Range(0, roomSettings.Length);
        if(newRoom.settingsIndex == IndexToExclude)
        {
            newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
        }
        newRoom.settings = roomSettings[newRoom.settingsIndex];
        rooms.Add(newRoom);
        return newRoom;

    }
}
