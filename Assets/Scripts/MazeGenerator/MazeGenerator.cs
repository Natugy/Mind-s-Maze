using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    [Header("Paramètres de la Grille")]
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;

    [Header("Assets")]
    public GameObject wallPrefab;

    private MazeCell[,] grid;

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        // 1. Initialiser la grille
        grid = new MazeCell[width, height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                grid[x, y] = new MazeCell();
            }
        }

        // 2. Lancer l'algorithme de parcours
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int start = new Vector2Int(0, 0);
        grid[start.x, start.y].Visited = true;
        stack.Push(start);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                Vector2Int chosen = neighbors[Random.Range(0, neighbors.Count)];
                RemoveWall(current, chosen);
                grid[chosen.x, chosen.y].Visited = true;
                stack.Push(chosen);
            }
            else
            {
                stack.Pop();
            }
        }

        // 3. Instancier les murs dans Unity
        DrawMaze();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Debug.Log("WT:"+grid[x,y].WallTop+"WL:"+grid[x,y].WallLeft+"WB:"+grid[x,y].WallBottom+"WR:"+grid[x,y].WallRight);
                
            }
        }
    }

    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int p)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        if (p.x > 0 && !grid[p.x - 1, p.y].Visited) neighbors.Add(new Vector2Int(p.x - 1, p.y));
        if (p.x < width - 1 && !grid[p.x + 1, p.y].Visited) neighbors.Add(new Vector2Int(p.x + 1, p.y));
        if (p.y > 0 && !grid[p.x, p.y - 1].Visited) neighbors.Add(new Vector2Int(p.x, p.y - 1));
        if (p.y < height - 1 && !grid[p.x, p.y + 1].Visited) neighbors.Add(new Vector2Int(p.x, p.y + 1));
        return neighbors;
    }

    void RemoveWall(Vector2Int current, Vector2Int next)
    {
        // Si on se déplace horizontalement
        if (current.x != next.x)
        {
            if (current.x > next.x){ // On va vers la gauche
                grid[current.x, current.y].WallLeft = false;
                grid[next.x, next.y].WallRight = false;
            }
            else {// On va vers la droite
                grid[next.x, next.y].WallLeft = false;
                grid[current.x, current.y].WallRight = false;
            }
        }
        // Si on se déplace verticalement
        else if (current.y != next.y)
        {
            if (current.y > next.y) {// On descend
                grid[current.x, current.y].WallBottom = false;
                grid[next.x, next.y].WallTop = false;
            }
            else{ // On monte
                grid[next.x, next.y].WallBottom = false;
                grid[current.x, current.y].WallTop = false;
            }
        }
    }

    void DrawMaze()
    {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);

                if (grid[x, y].WallLeft) {
                    Instantiate(wallPrefab, pos + new Vector3(-cellSize, 1, cellSize/2), Quaternion.Euler(0, 90, 0), transform);
                }

                if (grid[x, y].WallBottom) {
                    Instantiate(wallPrefab, pos + new Vector3(-cellSize/2, 1, 0), Quaternion.identity, transform);
                }
                
                if (grid[x, y].WallRight) {
                    Instantiate(wallPrefab, pos + new Vector3(0, 1, cellSize/2), Quaternion.Euler(0, 90, 0), transform);
                }

                if (grid[x, y].WallTop) {
                    Instantiate(wallPrefab, pos + new Vector3(-cellSize/2, 1, cellSize), Quaternion.identity, transform);
                }
                
            }
        }
    }

   
}
