using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameBoard : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    private GameObject normalTile;


    [SerializeField]
    private GameObject bombTile;

    [SerializeField]
    private GameObject allColorTile;

    [SerializeField]
    private int column;

    [SerializeField]
    private int row;

    private Tile[,] tileArray;

    private Tile[,] TileCreatedArray;

    private Stack<Tile> selectedTile = new();

    [SerializeField]
    Vector3 pos;

    float xOffset, yOffset;

    RaycastHit2D hit;

    private bool isChoosing;

    private bool IsHighlightActive = false;

    private bool IsBoardBusy = false;

    private void Awake()
    {
        var FPB = Utility.FPB(Screen.width, Screen.height);
        //Debug.Log($"height = {Camera.main.pixelHeight}, width = {Camera.main.pixelWidth},FPB = {FPB} , ratio = {Camera.main.pixelHeight/FPB}:{Camera.main.pixelWidth/FPB}");
        //var resolution = Screen.resolutions;
        xOffset = pos.x * 2 / (column - 1);
        yOffset = pos.y * 2 / (row - 1);
        tileArray = new Tile[column, row];
        TileCreatedArray = new Tile[column, row];
        EventContainer.OnchooseTile += Choose;
        EventContainer.OnReleaseTile += Release;
        EventContainer.OnSelectedTile += OnSelectedTile;
        EventContainer.OnLastTileDestroyed += AssignAvailableTile;
        EventContainer.OnBombClicked += Explode;
    }

    private void OnDestroy()
    {
        EventContainer.OnchooseTile -= Choose;
        EventContainer.OnReleaseTile -= Release;
        EventContainer.OnSelectedTile -= OnSelectedTile;
        EventContainer.OnLastTileDestroyed -= AssignAvailableTile;
        EventContainer.OnBombClicked -= Explode;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //Debug.Log(tileArray.Length);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var tempTile = Instantiate(normalTile);
                tileArray[j, i] = tempTile.GetComponent<Tile>();
                tileArray[j, i].Index = j + (i * row) + 1;
                tileArray[j, i].OnCreateTile(j, i);
                tempTile.transform.parent = transform;
                tempTile.transform.position = new Vector3(-pos.x + (j * xOffset), pos.y - (i * yOffset));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isChoosing)
        {
            hit = (Physics2D.Raycast(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, float.MaxValue));
            if (hit.collider != null)
            {
                //Debug.Log("choose");
                if (hit.collider.TryGetComponent<ITile>(out var tile))
                {
                    Debug.Log("haha");
                    tile.OnChoose();
                }
            }
        }
    }

    void Choose()
    {
        isChoosing = true;
    }

    void Release()
    {
        isChoosing = false;
        OnReleaseTile();
    }

    void OnReleaseTile(bool normal = true)
    {
        bool firstTime = true;
        if (selectedTile.Count >= 6 && normal)
        {
            if (selectedTile.Count >= 9)
            {
                while (true)
                {
                    if (firstTime)
                    {
                        var tempTile = selectedTile.Pop();
                        tempTile.OnRelease();
                        tempTile.OnDestroyTile();
                        tempTile.SetLastItem(true);


                        var temp = Instantiate(allColorTile).GetComponent<Tile>();

                        temp.Index = (int)tempTile.GetRowAndColumn().y + ((int)tempTile.GetRowAndColumn().x * row) + 1;
                        temp.OnCreateTile((int)tempTile.GetRowAndColumn().y, (int)tempTile.GetRowAndColumn().x);
                        temp.transform.parent = transform;
                        temp.transform.position = new Vector3(-pos.x + ((int)tempTile.GetRowAndColumn().y * xOffset), pos.y - ((int)tempTile.GetRowAndColumn().x * yOffset));
                        tileArray[(int)tempTile.GetRowAndColumn().y, (int)tempTile.GetRowAndColumn().x] = temp;
                        firstTime = false;
                    }
                    else if (selectedTile.Count > 1)
                    {
                        var tempTile = selectedTile.Pop();
                        tempTile.OnRelease();
                        tempTile.OnDestroyTile();
                        tileArray[(int)(tempTile.GetRowAndColumn().y), (int)(tempTile.GetRowAndColumn().x)] = null;
                    }
                    else
                    {

                        var tempTile = selectedTile.Pop();
                        tempTile.OnRelease();
                        tempTile.OnDestroyTile();
                        tileArray[(int)(tempTile.GetRowAndColumn().y), (int)(tempTile.GetRowAndColumn().x)] = null;
                        //tileArray[(int)tempTile.GetRowAndColumn().y, (int)tempTile.GetRowAndColumn().x] = new NormalTile();
                        break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    if (firstTime)
                    {
                        var tempTile = selectedTile.Pop();
                        tempTile.OnRelease();
                        tempTile.OnDestroyTile();
                        tempTile.SetLastItem(true);


                        var temp = Instantiate(bombTile).GetComponent<Tile>();

                        temp.Index = (int)tempTile.GetRowAndColumn().y + ((int)tempTile.GetRowAndColumn().x * row) + 1;
                        temp.OnCreateTile((int)tempTile.GetRowAndColumn().y, (int)tempTile.GetRowAndColumn().x);
                        temp.transform.parent = transform;
                        temp.transform.position = new Vector3(-pos.x + ((int)tempTile.GetRowAndColumn().y * xOffset), pos.y - ((int)tempTile.GetRowAndColumn().x * yOffset));
                        tileArray[(int)tempTile.GetRowAndColumn().y, (int)tempTile.GetRowAndColumn().x] = temp;
                        firstTime = false;
                    }
                    if (selectedTile.Count > 1)
                    {
                        var tempTile = selectedTile.Pop();
                        tempTile.OnRelease();
                        tempTile.OnDestroyTile();
                        tileArray[(int)(tempTile.GetRowAndColumn().y), (int)(tempTile.GetRowAndColumn().x)] = null;
                    }
                    else
                    {
                        var tempTile = selectedTile.Pop();
                        tempTile.OnRelease();
                        tempTile.OnDestroyTile();
                        tileArray[(int)(tempTile.GetRowAndColumn().y), (int)(tempTile.GetRowAndColumn().x)] = null;

                        //tileArray[(int)tempTile.GetRowAndColumn().y, (int)tempTile.GetRowAndColumn().x] = new NormalTile();
                        break;
                    }
                }
            }
        }
        else if (selectedTile.Count >= 3 && normal)
        {
            while (true)
            {
                if (selectedTile.Count > 1)
                {
                    var tempTile = selectedTile.Pop();
                    tempTile.OnRelease();
                    tempTile.OnDestroyTile();
                    tileArray[(int)(tempTile.GetRowAndColumn().y), (int)(tempTile.GetRowAndColumn().x)] = null;
                }
                else if (selectedTile.Count == 1)
                {
                    var tempTile = selectedTile.Pop();
                    tempTile.OnRelease();
                    tempTile.OnDestroyTile();
                    tempTile.SetLastItem(true);
                    tileArray[(int)(tempTile.GetRowAndColumn().y), (int)(tempTile.GetRowAndColumn().x)] = null;
                }
                else
                {
                    break;
                }
            }
        }
        else if (selectedTile.Count > 0 && normal)
        {
            while (true)
            {
                if (selectedTile.Count > 0)
                {
                    var tempTile = selectedTile.Pop();
                    tempTile.OnRelease();
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            while (true)
            {
                if (selectedTile.Count > 1)
                {
                    var tempTile = selectedTile.Pop();
                    tempTile.OnRelease();
                    tempTile.OnDestroyTile();
                    tileArray[(int)(tempTile.GetRowAndColumn().y), (int)(tempTile.GetRowAndColumn().x)] = null;
                }
                else if (selectedTile.Count == 1)
                {
                    var tempTile = selectedTile.Pop();
                    tempTile.OnRelease();
                    tempTile.OnDestroyTile();
                    tempTile.SetLastItem(true);
                    tileArray[(int)(tempTile.GetRowAndColumn().y), (int)(tempTile.GetRowAndColumn().x)] = null;
                }
                else
                {
                    break;
                }
            }
        }
    }

    void OnSelectedTile(Tile tile)
    {
        DisableHighlight();
        selectedTile.TryPeek(out var firstTile);
        if (firstTile == tile) return;

        if (selectedTile.Contains(tile))
        {
            while (true)
            {
                var tempTile = selectedTile.Peek();
                if (tempTile != tile)
                {
                    selectedTile.Pop();
                    tempTile.OnRelease();
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            if (firstTile == null)
            {
                selectedTile.Push(tile);
                tile.OnMatch();
            }
            else if (firstTile.type == TileType.Normal)
            {
                if (tile.GetType() == typeof(NormalTile))
                {
                    if (Utility.Step(firstTile.GetRowAndColumn().x, firstTile.GetRowAndColumn().y, tile.GetRowAndColumn().x, tile.GetRowAndColumn().y) > 1)
                    {
                        return;
                    }
                    if (firstTile.Color == tile.Color)
                    {
                        firstTile.LineRenderer.SetPosition(0, firstTile.transform.position);
                        firstTile.LineRenderer.SetPosition(1, tile.transform.position);
                        selectedTile.Push(tile); tile.OnMatch();
                    }
                }
            }
            else
            {
                //special here

                if (firstTile.GetType() == typeof(AllColorTile))
                {
                    if (Utility.Step(firstTile.GetRowAndColumn().x, firstTile.GetRowAndColumn().y, tile.GetRowAndColumn().x, tile.GetRowAndColumn().y) > 1)
                    {
                        return;
                    }
                    AllColor(tile.Color);
                }
            }
        }
    }


    public void Shuffle()
    {
        Tile tempTile;
        int randomColumn;
        int randomRow;
        int tilex;
        int tiley;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                randomColumn = Random.Range(0, column);
                randomRow = Random.Range(0, row);
                tiley = i;
                tilex = j;

                tempTile = tileArray[j, i];
                tileArray[j, i] = tileArray[randomColumn, randomRow];
                tileArray[randomColumn, randomRow] = tempTile;

                tileArray[j, i].Index = j + (i * row) + 1;
                tileArray[randomColumn, randomRow].Index = randomColumn + (randomRow * row) + 1;

                tileArray[j, i].SetRowAndColumn(i, j);
                tileArray[randomColumn, randomRow].SetRowAndColumn(randomRow, randomColumn);


                tileArray[j, i].transform.position = new Vector3(-pos.x + (j * xOffset), pos.y - (i * yOffset));
                tileArray[randomColumn, randomRow].transform.position = new Vector3(-pos.x + (randomColumn * xOffset), pos.y - (randomRow * yOffset));
            }
        }
    }

    #region Highlight
    public void HighLightMatch()
    {
        if (IsHighlightActive) return;

        bool[,] visited = new bool[column, row];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                visited = new bool[column, row];
                if (!visited[j, i])
                {
                    List<Tile> group = new List<Tile>();
                    Tile startTile = tileArray[j, i];
                    DFS(j, i, startTile.Color, group, visited);

                    if (group.Count >= 3)
                    {
                        foreach(var obj in group)
                        {
                            obj.ActivateHighlight(true);
                            IsHighlightActive = true;
                        }
                        return;
                    }
                }
            }
        }
    }

    bool DFS(int x, int y, TileColor color, List<Tile> group, bool[,] visited)
    {
        if (x < 0 || y < 0 || x >= column || y >= row|| visited[x, y])
            return false;

        Tile tile = tileArray[x, y];
        if (tile == null || tile.Color != color)
            return false;

        visited[x, y] = true;
        group.Add(tile);

        // Check 4 directions (or 8 if you allow diagonal matching)
        if (DFS(x + 1, y, color, group, visited)) { }
        else if (DFS(x - 1, y, color, group, visited)) { }
        else if (DFS(x, y + 1, color, group, visited)) { }
        else if (DFS(x, y - 1, color, group, visited)) { }
        return true;
    }


    void DisableHighlight()
    {
        if (!IsHighlightActive) return;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (tileArray[j, i].TryGetComponent(out NormalTile tileTemp))
                {
                    tileTemp.ActivateHighlight(false); IsHighlightActive = false;
                }
            }
        }
    }

    #endregion

    #region NewTileCreate
    void AssignAvailableTile()
    {
        for (int i = row - 1; i >= 0; i--)
        {
            for (int j = column - 1; j >= 0; j--)
            {
                if (tileArray[j, i] == null)
                {
                    for (int k = i; k >= 0; k--)
                    {
                        if (tileArray[j, k] != null)
                        {
                            tileArray[j, i] = tileArray[j, k];
                            tileArray[j, i].SetRowAndColumn(i, j);
                            tileArray[j, i].Index = j + (i * row) + 1;
                            tileArray[j, k] = null;
                            break;
                        }
                    }
                }
            }
        }

        CreateNewTile();
        MoveTempTileToTile();
        MoveAnimation();
    }
    public void CreateNewTile()
    {
        ClearNewTile();
        for (int i = 0; i < this.row; i++)
        {
            for (int j = 0; j < this.column; j++)
            {
                if (tileArray[j, i] == null)
                {
                    for (int k = 0; k < this.row; k++)
                    {
                        if (TileCreatedArray[j, k] == null)
                        {
                            var tempTile = Instantiate(normalTile);
                            TileCreatedArray[j, k] = tempTile.GetComponent<Tile>();
                            TileCreatedArray[j, k].Index = 0;
                            TileCreatedArray[j, k].OnCreateTile(j, k);
                            tempTile.transform.parent = transform;
                            tempTile.transform.position = new Vector3(-pos.x + (j * xOffset), pos.y + ((k + 1) * (yOffset)));
                            break;
                        }
                    }
                }
            }
        }
    }

    public void ClearNewTile()
    {
        for (int i = 0; i < this.row; i++)
        {
            for (int j = 0; j < this.column; j++)
            {
                TileCreatedArray[j, i] = null;
            }
        }
    }

    public void MoveTempTileToTile()
    {
        for (int i = row - 1; i >= 0; i--)
        {
            for (int j = column - 1; j >= 0; j--)
            {
                if (tileArray[j, i] == null)
                {
                    for (int k = 0; k < row; k++)
                    {
                        if (TileCreatedArray[j, k] != null)
                        {
                            tileArray[j, i] = TileCreatedArray[j, k];
                            tileArray[j, i].SetRowAndColumn(i, j);
                            tileArray[j, i].Index = j + (i * row) + 1;
                            TileCreatedArray[j, k] = null;
                            break;
                        }
                    }
                }

            }
        }
    }

    public void MoveAnimation()
    {
        for (int i = 0; i < this.row; i++)
        {
            for (int j = 0; j < this.column; j++)
            {
                tileArray[i, j].LineRenderer.enabled = false;
                tileArray[j, i].MoveTile(tileArray[j, i].transform.position, new Vector3(-pos.x + (j * xOffset), pos.y - (i * yOffset)));
            }
        }
    }
    #endregion

    #region Power
    public void Explode(int column, int row)
    {
        isChoosing = false;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (column + j >= 0 && column + j < this.column && row + i >= 0 && row + i < this.row)
                {
                    selectedTile.Push(tileArray[column+j,row+i]);
                }
            }
        }

        OnReleaseTile(false);
    }

    public void AllColor(TileColor color)
    {
        isChoosing = false;
        for (int i = 0; i < this.row; i++)
        {
            for (int j = 0; j < this.column; j++)
            {
                if (tileArray[j, i].Color == color)
                {
                    selectedTile.Push(tileArray[j, i]);
                }
            }
        }

        OnReleaseTile(false);
    }
    #endregion
}
