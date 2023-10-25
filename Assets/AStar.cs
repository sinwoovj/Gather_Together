using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public float X { get; } // 노드의 X 좌표
    public float Y { get; } // 노드의 Y 좌표
    public List<Node> Neighbors { get; } = new List<Node>(); // 이웃 노드
    public bool IsWalkable { get; set; } // 지나갈 수 있는 노드인지 여부

    public bool IsWalkableChecked = false;


    public int ix, iy;
    public Node(float x, float y, int ix, int iy)
    {
        X = x;
        Y = y;
        this.ix = ix;
        this.iy = iy;
    }
}

public class IterNode 
{
    public Node node;

    public double GScore;
    public double FScore;
}

public class NodeComparer : IComparer<IterNode>
{
    public int Compare(IterNode x, IterNode y)
    {
        return x.FScore.CompareTo(y.FScore);
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


    public IEnumerable<Node> FindNeighborsIE(Node node,GameObject exceptObj)
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

                if (neighbor.IsWalkableChecked == false) 
                {
                    var pos = new Vector2(neighbor.X, neighbor.Y);
                    var c = Physics2D.OverlapBox(pos, new Vector2(cellSize, cellSize), 0, ObjectLayer);
                    if(c!=null &&c.gameObject== exceptObj)
                    {
                        c = null;
                    }
                    neighbor.IsWalkable = c == null;
                    neighbor.IsWalkableChecked = true;

                }

                // 이웃 노드가 지나갈 수 있는 노드라면 추가
                if (neighbor.IsWalkable)
                {
                    yield return neighbor;
                }
            }
        }
    }

    public void Reset()
    {
        foreach (var n in Nodes)
            n.IsWalkableChecked = false;
    }
}




public class AStar : DIMono
{
    private AStarGrid grid;

    public float width, height;
    public float cellSize;
    public Vector2 offset;

    public Transform endTf;

    public LayerMask ObjectLayer;


    public IEnumerator TraceIE()
    {
        float duration = 30f;

        float interval = 0.5f;
        float leftTime = interval;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            leftTime -= Time.deltaTime;
            if(leftTime < 0)
            {   
                needExcute = true;
                leftTime += interval;
            }
            yield return null;

        }

    }

    protected override void Initialize()
    {

        StartCoroutine(TraceIE());
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


    
    public bool needExcute;
    public float speed=3;

    List<Node> pathes=null;

    
    public void Update()
    {
        if (pathes != null && pathes.Count >0)
        {
            Vector3 destination = new Vector3(pathes[0].X,pathes[0].Y);

            Vector3 dir = destination - this.transform.position;
            var leftDist= dir.magnitude;
            var dirNorm = dir.normalized;
            Debug.Log($"X:{dirNorm.x} Y:{dirNorm.y}");
            var moveDist = speed * Time.deltaTime;
            if (leftDist <= moveDist)
            {
                moveDist = leftDist;
                pathes.RemoveAt(0);              

            }

            this.transform.position += dir.normalized * moveDist;
            
        }


        if (needExcute)
        {

            needExcute = false;
            var startPos = this.transform.position;
            var endPos = endTf.transform.position;  

            pathes =FindPath(startPos, endPos);
            if (pathes == null)
            {
                Debug.Log("Not Found");
                return;
            }
            foreach(var p in pathes)
            {
                //Debug.Log($"path {p.X},{p.Y}");
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
            Gizmos.DrawSphere(new Vector3(node.X, node.Y), 0.1f);
        }
    }


    // A* 알고리즘을 사용하여 최단 경로 찾기
    public List<Node> FindPath(Vector2 startPosition, Vector2 goalPosition)
    {
        Node startNode = NodeFromWorldPoint(startPosition);
        Node goalNode = NodeFromWorldPoint(goalPosition);

        SortedSet<IterNode> openSet = new SortedSet<IterNode>(new NodeComparer());
        var cameFrom = new Dictionary<Node, Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        IterNode iterNode = new IterNode()
        {
            node = startNode,

        };

        grid.Reset();


        iterNode.FScore = HeuristicCostEstimate(startNode, goalNode);

        openSet.Add(iterNode);

        Debug.Log("goalNode.node" + goalNode.X + " " + goalNode.Y);
        int loopCnt = 0;
        while (openSet.Count != 0)
        {
            loopCnt++;
            if (loopCnt > 200)
            {
                Debug.Log("LoopCnt >1000");
                return null;
            }

            IterNode current = openSet.Min;
            openSet.Remove(current);
            closeSet.Add(current.node);
            Debug.Log("current.node" + current.node.X + " " + current.node.Y + " " + current.node.ix + " " + current.node.iy);


            if (current.node == goalNode)
            {
                cameFrom.Remove(startNode);

                return ReconstructPath(cameFrom, current.node);
            }


            foreach (var neighbor in grid.FindNeighborsIE(current.node,endTf.gameObject))
            {
                if (closeSet.Contains(neighbor))
                {
                    continue;
                }

                double tentativeGScore = current.GScore + Vector2.Distance(
                    new Vector2(current.node.X, current.node.Y),
                new Vector2(neighbor.X, neighbor.Y));

                cameFrom[neighbor] = current.node;
                IterNode iternode = new IterNode()
                {
                    node = neighbor,
                    GScore = tentativeGScore,
                    FScore = tentativeGScore + HeuristicCostEstimate(neighbor, goalNode),
                };


                var one = openSet.FirstOrDefault(l => l.node == neighbor);
                if (one != null && one.GScore > tentativeGScore)
                {
                    continue;
                }


                openSet.Add(iternode);

            }
        }

        return null; // 경로가 없음
    }

    private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        var path = new List<Node> { current };
        var loopCnt = 0;
        while (cameFrom.ContainsKey(current))
        {
            loopCnt++;
            if (loopCnt > 1000)
            {
                Debug.Log("ReconstructPath LoopCnt >1000");
                return null;
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌함");
        needExcute = true;
    }
}

// 특정 NPC를 원하는 목적지로 보내는 코드 작성
// 이동할 때 애니메이션 포함 (AStar의 dirNorm을 참고해서 상하좌우를 판별하고[방향] 애니메이션을 실행한다.)