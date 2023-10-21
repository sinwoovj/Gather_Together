using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Node
{
    public float X { get; } // 노드의 X 좌표
    public float Y { get; } // 노드의 Y 좌표
    public List<Node> Neighbors { get; } = new List<Node>(); // 이웃 노드
    public bool IsWalkable { get; set; } // 지나갈 수 있는 노드인지 여부

    public double GScore, FScore;

    public int ix, iy;
    public Node(float x, float y,int ix,int iy)
    {
        X = x;
        Y = y;
        this.ix = ix;
        this.iy = iy;
    }
}

public class AStarGrid
{
    public Node[,] Nodes { get; } // 그리드의 모든 노드 배열

    private readonly float cellSize;
    Vector2 offset;

    public LayerMask ObjectLayer;

    public AStarGrid(float width, float height, float cellSize,Vector2 offset, LayerMask ObjectLayer)
    {
        this.ObjectLayer=ObjectLayer;

        int iwidth = Mathf.RoundToInt(width / cellSize);
        int iheight = Mathf.RoundToInt(height / cellSize);
        this.offset = offset;

        this.cellSize = cellSize;
        Nodes = new Node[iwidth, iheight];

        for (int x = 0; x < iwidth; x++)
        {
            for (int y = 0; y < iheight; y++)
            {
                var node = new Node(x * cellSize+offset.x, y * cellSize+offset.y, x, y); ;
                Nodes[x, y] = node;
                var pos= new Vector2(node.X,node.Y);
                var c = Physics2D.OverlapBox(pos, new Vector2(cellSize, cellSize), 0, ObjectLayer);
                node.IsWalkable =c  == null;


            }
        }
    }

    // 이웃 노드 찾기
    public void FindNeighbors(Node node)
    {
        int[] dx = { 1, 0, -1, 0 };
        int[] dy = { 0, 1, 0, -1 };

        for (int i = 0; i < 4; i++)
        {
            int newX = node.ix + dx[i];
            int newY = node.iy + dy[i];

            // 이웃 노드가 그리드 범위 내에 있는지 확인
            if (newX >= 0 && newX < Nodes.GetLength(0) && newY >= 0 && newY < Nodes.GetLength(1))
            {
                Node neighbor = Nodes[newX, newY];

                // 이웃 노드가 지나갈 수 있는 노드라면 추가
                if (neighbor.IsWalkable)
                {
                    node.Neighbors.Add(neighbor);
                }
            }
        }
    }

    public IEnumerable<Node> FindNeighborsIE(Node node)
    {
        int[] dx = { 1, 0, -1, 0 };
        int[] dy = { 0, 1, 0, -1 };

        for (int i = 0; i < 4; i++)
        {
            int newX = node.ix + dx[i];
            int newY = node.iy + dy[i];

            // 이웃 노드가 그리드 범위 내에 있는지 확인
            if (newX >= 0 && newX < Nodes.GetLength(0) && newY >= 0 && newY < Nodes.GetLength(1))
            {
                Node neighbor = Nodes[newX, newY];

                // 이웃 노드가 지나갈 수 있는 노드라면 추가
                if (neighbor.IsWalkable)
                {
                    yield return neighbor;
                }
            }
        }
    }
}

public class AStar : MonoBehaviour
{
    private AStarGrid grid;

    public float width, height;
    public float cellSize;
    public Vector2 offset;

    public LayerMask ObjectLayer;
    private void Start()
    {


        grid = new AStarGrid(width, height, cellSize,offset,ObjectLayer);

        // 이웃 노드 찾기
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid.FindNeighbors(grid.Nodes[x, y]);
            }
        }
    }

    public Vector2 startPos, endPos;
    public bool needExcute;

    List<Node> pathes;

    public void Update()
    {
        if (needExcute)
        {

            needExcute = false;
            pathes =FindPath(startPos, endPos);
            if (pathes == null)
            {
                Debug.Log("Not Found");
                return;
            }
            foreach(var p in pathes)
            {
                Debug.Log($"path {p.X},{p.Y}");
            }
        }
       
    }

    private void OnDrawGizmos()
    {
        if (grid == null)
            return;
        
        foreach (var cell in grid.Nodes)
        {
            if (cell.IsWalkable)
            {
                Gizmos.color = new Color(0, 1, 0, 0.5f);

            }
            else
            {
                Gizmos.color = new Color(1, 0, 0, 0.5f);

            }

            Gizmos.DrawCube(new Vector3(cell.X, cell.Y), new Vector3(cellSize*0.8f, cellSize*0.8f, 1));
        }
        if (pathes == null)
        {
            return;
        }


        Gizmos.color = Color.red;
        //각 path에 원을 그려준다
        foreach (Node node in pathes)
        {
            Gizmos.DrawSphere(new Vector3(node.X, node.Y), 1);
        }
    }


    // A* 알고리즘을 사용하여 최단 경로 찾기
    public List<Node> FindPath(Vector2 startPosition, Vector2 goalPosition)
    {
        Node startNode = NodeFromWorldPoint(startPosition);
        Node goalNode = NodeFromWorldPoint(goalPosition);

        List<Node> openSet = new List<Node> { startNode };
        var cameFrom = new Dictionary<Node, Node>();
        startNode.GScore = 0;
        startNode.FScore = HeuristicCostEstimate(startNode, goalNode);

        while (openSet.Count > 0)
        {
            Node current = openSet[0];
            foreach (var node in openSet)
            {
                if (node.FScore < current.FScore)
                {
                    current = node;
                }
            }

            if (current == goalNode)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            foreach (var neighbor in grid.FindNeighborsIE(current))
            {
                double tentativeGScore = current.GScore + Vector2.Distance(
                    new Vector2(current.X, current.Y),
                    new Vector2(neighbor.X, neighbor.Y));

                if (tentativeGScore < neighbor.GScore)
                {
                    cameFrom[neighbor] = current;
                    neighbor.GScore = tentativeGScore;
                    neighbor.FScore = neighbor.GScore + HeuristicCostEstimate(neighbor, goalNode);
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null; // 경로가 없음
    }

    private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        var path = new List<Node> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        return path;
    }

    private double HeuristicCostEstimate(Node start, Node goal)
    {
        // 유클리드 거리를 휴리스틱(예측) 비용으로 사용
        return Vector2.Distance(
            new Vector2(start.X, start.Y),
            new Vector2(goal.X, goal.Y));
    }

    private Node NodeFromWorldPoint(Vector2 worldPosition)
    {

        var pos = worldPosition - offset;
        int x = Mathf.FloorToInt(pos.x/cellSize);
        int y = Mathf.FloorToInt(pos.y / cellSize);

        // 그리드 범위 내에 있는지 확인
        if (x >= 0 && x < grid.Nodes.GetLength(0) && y >= 0 && y < grid.Nodes.GetLength(1))
        {
            return grid.Nodes[x, y];
        }

        return null;
    }
}
