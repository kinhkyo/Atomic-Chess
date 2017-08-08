using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChessBoard : MonoBehaviour
{
    public static ChessBoard Current;
    public GameObject cellPrefap;
    public LayerMask CellLayerMask = 0;

    private Cell _currentSelectedCell = null;

    private float cell_size = -1;
    public float CELL_SIZE
    {
        get
        {
            if (cell_size < 0)
                cell_size = cellPrefap.GetComponent<Cell>().SIZE;
            return cell_size;
        }
    }

    private void Awake()
    {
        Current = this;
    }

    private Vector3 basePosition = Vector3.zero;

    private Cell[][] _cells;
    public Cell[][] Cells { get { return _cells; } }
    private List<BasePiece> pieces;

    private void Update()
    {
        if(BaseGameCTL.Current.GameState == EGameState.PLAYING){
            CheckUserInput();
        }
        
    }

    private void Start()
    {
        InitChessBoard();
        InitChessPieces();
    }



    private void CheckUserInput()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1000, CellLayerMask.value))
            {
                Cell newCell = hit.collider.GetComponent<Cell>();


                switch (newCell.State)
                {
                    /*
                     * Select to nomal cell -> that cell must same with current player we'll do new move.
                    */
                    case ECellState.NORMAL:
                        if (newCell.CurrentPiece != null && newCell.CurrentPiece.Player == BaseGameCTL.Current.CurrentPlayer){
                            handleSelectNomal(newCell);
                        }

                        break;
                    case ECellState.TARGETED :
                        /*
                         * 1. If that cell is empty we can move. 
                         * 2. If that cell isn't empty and piece in cell is enemy we'll kill.
                         * 3. Another case don't do anything.
                        */
                        handleSelectTargeted(newCell);
                        break;
                    default:
                        break;
                }

            }
        }

        //Check position of mouth
    }

    /// <summary>
    /// Handle Selected Cell 
    /// </summary>

    private void handleSelectNomal(Cell newCell){
        /*
            If we had a piece had selected -> Change status of cell and piece in that cell to NORMAL
        */
        //Ô NORMAL, sau đó chuyển thành SELECTED
        if (newCell.State == ECellState.NORMAL)
        {
            
            //Quân cờ được chọn trước đó
            if (_currentSelectedCell != null)
            {
                _currentSelectedCell.SetCellState(ECellState.NORMAL);
                if (_currentSelectedCell.CurrentPiece != null){
                    _currentSelectedCell.CurrentPiece.BeUnselected();
                }
            }

                _currentSelectedCell = newCell;
                _currentSelectedCell.SetCellState(ECellState.SELECTED);



            Debug.LogFormat("New Cell:D {0}", newCell);

           
        }

    }

    private void handleSelectTargeted(Cell newCell){
        // If one of cells is targeted 
        // And the piece in that cell is enemy we'll handle case kill that enemy

        print("Target ted ========");
        if (newCell.CurrentPiece == null)
        {
            Debug.Log("select a targeted cell!");
            //Just move to cell had been selected
            _currentSelectedCell.MakeAMove(newCell);
        }
        else
        {
            if (newCell.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer){
                //Kill
                _currentSelectedCell.CurrentPiece.Attack(newCell);
            }
            //Move & Kill enemy 
        }
    }





    [ContextMenu("InitChessBoard")]
    public void InitChessBoard()
    {
        basePosition = Vector3.zero + new Vector3(-3.5f * CELL_SIZE, 0, 0);
        _cells = new Cell[8][];
        for (int i = 0; i < 8; i++)
            _cells[i] = new Cell[8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject c = GameObject.Instantiate(cellPrefap, CanculatePosition(i, j),
                    Quaternion.identity) as GameObject;

                c.transform.parent = this.transform.GetChild(0);

                _cells[i][j] = c.GetComponent<Cell>();
                _cells[i][j].setLocation(new CLocation(i, j));

                if ((i + j) % 2 == 0)
                    _cells[i][j].Color = ECellColor.BLACK;
                else _cells[i][j].Color = ECellColor.WHITE;
            }
        }


    }

    [ContextMenu("InitChessPieces")]
    public void InitChessPieces(){
        // Rook , Knight
        pieces = new List<BasePiece>();

        List<PieceInfo> list = new List<PieceInfo>();

        //White
        list.Add(new PieceInfo() { Name = "W_PAWN_1", Path = "Pieces/White Pawn", X = 0, Y = 1 });
        list.Add(new PieceInfo() { Name = "W_PAWN_2", Path = "Pieces/White Pawn", X = 1, Y = 1 });
        list.Add(new PieceInfo() { Name = "W_PAWN_3", Path = "Pieces/White Pawn", X = 2, Y = 1 });
        list.Add(new PieceInfo() { Name = "W_PAWN_4", Path = "Pieces/White Pawn", X = 3, Y = 1 });
        list.Add(new PieceInfo() { Name = "W_PAWN_5", Path = "Pieces/White Pawn", X = 4, Y = 1 });
        list.Add(new PieceInfo() { Name = "W_PAWN_6", Path = "Pieces/White Pawn", X = 5, Y = 1 });
        list.Add(new PieceInfo() { Name = "W_PAWN_7", Path = "Pieces/White Pawn", X = 6, Y = 1 });
        list.Add(new PieceInfo() { Name = "W_PAWN_8", Path = "Pieces/White Pawn", X = 7, Y = 1 });

        list.Add(new PieceInfo() { Name = "W_ROOK_1", Path = "Pieces/White Rook", X = 0, Y = 0 });
        list.Add(new PieceInfo() { Name = "W_ROOK_2", Path = "Pieces/White Rook", X = 7, Y = 0 });
        list.Add(new PieceInfo() { Name = "W_KNIGHT_1", Path = "Pieces/White Knight", X = 1, Y = 0 });
        list.Add(new PieceInfo() { Name = "W_KNIGHT_2", Path = "Pieces/White Knight", X = 6, Y = 0 });
        list.Add(new PieceInfo() { Name = "W_BISHOP_1", Path = "Pieces/White Bishop", X = 2, Y = 0 });
        list.Add(new PieceInfo() { Name = "W_BISHOP_2", Path = "Pieces/White Bishop", X = 5, Y = 0 });
        list.Add(new PieceInfo() { Name = "W_KING_1", Path = "Pieces/White King", X = 3, Y = 0 });
        list.Add(new PieceInfo() { Name = "W_QUEEN_2", Path = "Pieces/White Queen", X = 4, Y = 0 });

        //White
        list.Add(new PieceInfo() { Name = "B_PAWN_1", Path = "Pieces/Black Pawn", X = 0, Y = 6 });
        list.Add(new PieceInfo() { Name = "B_PAWN_2", Path = "Pieces/Black Pawn", X = 1, Y = 6 });
        list.Add(new PieceInfo() { Name = "B_PAWN_3", Path = "Pieces/Black Pawn", X = 2, Y = 6 });
        list.Add(new PieceInfo() { Name = "B_PAWN_4", Path = "Pieces/Black Pawn", X = 3, Y = 6 });
        list.Add(new PieceInfo() { Name = "B_PAWN_5", Path = "Pieces/Black Pawn", X = 4, Y = 6 });
        list.Add(new PieceInfo() { Name = "B_PAWN_6", Path = "Pieces/Black Pawn", X = 5, Y = 6 });
        list.Add(new PieceInfo() { Name = "B_PAWN_7", Path = "Pieces/Black Pawn", X = 6, Y = 6 });
        list.Add(new PieceInfo() { Name = "B_PAWN_8", Path = "Pieces/Black Pawn", X = 7, Y = 6 });

        list.Add(new PieceInfo() { Name = "B_ROOK_1", Path = "Pieces/Black Rook", X = 0, Y = 7 });
        list.Add(new PieceInfo() { Name = "B_ROOK_2", Path = "Pieces/Black Rook", X = 7, Y = 7 });
        list.Add(new PieceInfo() { Name = "B_KNIGHT_1", Path = "Pieces/Black Knight", X = 1, Y = 7 });
        list.Add(new PieceInfo() { Name = "B_KNIGHT_2", Path = "Pieces/Black Knight", X = 6, Y = 7 });
        list.Add(new PieceInfo() { Name = "B_BISHOP_1", Path = "Pieces/Black Bishop", X = 2, Y = 7 });
        list.Add(new PieceInfo() { Name = "B_BISHOP_2", Path = "Pieces/Black Bishop", X = 5, Y = 7 });
        list.Add(new PieceInfo() { Name = "B_KING_1", Path = "Pieces/Black King", X = 3, Y = 7 });
        list.Add(new PieceInfo() { Name = "B_QUEEN_2", Path = "Pieces/Black Queen", X = 4, Y = 7 });


        foreach (var info in list)
        {
            GameObject GO = GameObject.Instantiate<GameObject>(ResourcesCTL.Instance.GetGameObject(info.Path));
            GO.transform.parent = this.transform.GetChild(1);
            GO.name = info.Name;

            BasePiece p = GO.GetComponent<BasePiece>();
            p.SetInfo(info,_cells[info.X][info.Y]);
            pieces.Add(p);


        }

    }

    public Vector3 CanculatePosition(int i, int j)
    {
        return basePosition + new Vector3(i * CELL_SIZE, 0, j * CELL_SIZE);
    }
}
