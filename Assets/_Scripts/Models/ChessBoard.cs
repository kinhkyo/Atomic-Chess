using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChessBoard : MonoBehaviour
{
    public static ChessBoard Current;
    public GameObject cellPrefap;
    public LayerMask CellLayerMask = 0;
    private Cell _currentHoverCell = null;
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



    private void CheckUserInput()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000,CellLayerMask.value))
        {
            
            Debug.DrawLine(ray.origin, hit.point);
             Debug.Log(hit.collider.name);
            Cell newCell = hit.collider.GetComponent<Cell>();
            if (newCell != _currentHoverCell)
            {
                if (_currentHoverCell != null)
                    _currentHoverCell.SetCellState(ECellState.NORMAL);
                _currentHoverCell = newCell;
                _currentHoverCell.SetCellState(ECellState.HOVER);
            }

        }
        else
        {
            if (_currentHoverCell != null)
            {
                _currentHoverCell.SetCellState(ECellState.NORMAL);
                _currentHoverCell = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_currentHoverCell != null)
            {
                if (_currentSelectedCell != null)
                    _currentSelectedCell.SetCellState(ECellState.UNSELECTED);
                _currentSelectedCell = _currentHoverCell;
                _currentSelectedCell.SetCellState(ECellState.SELECTED);
            }
        }

        //Check position of mouth
    }

    [ContextMenu("InitChessBoard")]
    public void InitChessBoard()
    {
        basePosition = Vector3.zero + new Vector3(-3.5f * CELL_SIZE, 0, 0);
        _cells = new Cell[8][];
        for (int i = 0; i < 8; i++)
        {
            _cells[i] = new Cell[8];
            for (int j = 0; j < 8; j++)
            {
                GameObject c = GameObject.Instantiate(cellPrefap, CanculatePosition(i, j),
                    Quaternion.identity) as GameObject;
                c.transform.parent = this.transform.GetChild(0);

                _cells[i][j] = c.GetComponent<Cell>();

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
		list.Add(new PieceInfo() { Name = "W_PAWN_1", Path = "Pieces/White Pawn", X = 1, Y = 0 });
		list.Add(new PieceInfo() { Name = "W_PAWN_2", Path = "Pieces/White Pawn", X = 1, Y = 1 });
		list.Add(new PieceInfo() { Name = "W_PAWN_3", Path = "Pieces/White Pawn", X = 1, Y = 2 });
		list.Add(new PieceInfo() { Name = "W_PAWN_4", Path = "Pieces/White Pawn", X = 1, Y = 3 });
		list.Add(new PieceInfo() { Name = "W_PAWN_5", Path = "Pieces/White Pawn", X = 1, Y = 4 });
		list.Add(new PieceInfo() { Name = "W_PAWN_6", Path = "Pieces/White Pawn", X = 1, Y = 5 });
		list.Add(new PieceInfo() { Name = "W_PAWN_7", Path = "Pieces/White Pawn", X = 1, Y = 6 });
		list.Add(new PieceInfo() { Name = "W_PAWN_8", Path = "Pieces/White Pawn", X = 1, Y = 7 });

		list.Add(new PieceInfo() { Name = "W_ROOK_1", Path = "Pieces/White Rook", X = 0, Y = 0 });
		list.Add(new PieceInfo() { Name = "W_ROOK_2", Path = "Pieces/White Rook", X = 0, Y = 7 });
		list.Add(new PieceInfo() { Name = "W_KNIGHT_1", Path = "Pieces/White Knight", X = 0, Y = 1 });
		list.Add(new PieceInfo() { Name = "W_KNIGHT_2", Path = "Pieces/White Knight", X = 0, Y = 6 });
		list.Add(new PieceInfo() { Name = "W_BISHOP_1", Path = "Pieces/White Bishop", X = 0, Y = 2 });
		list.Add(new PieceInfo() { Name = "W_BISHOP_2", Path = "Pieces/White Bishop", X = 0, Y = 5 });
		list.Add(new PieceInfo() { Name = "W_KING_1", Path = "Pieces/White King", X = 0, Y = 3 });
		list.Add(new PieceInfo() { Name = "W_QUEEN_2", Path = "Pieces/White Queen", X = 0, Y = 4 });

		//White
		list.Add(new PieceInfo() { Name = "B_PAWN_1", Path = "Pieces/Black Pawn", X = 6, Y = 0 });
		list.Add(new PieceInfo() { Name = "B_PAWN_2", Path = "Pieces/Black Pawn", X = 6, Y = 1 });
		list.Add(new PieceInfo() { Name = "B_PAWN_3", Path = "Pieces/Black Pawn", X = 6, Y = 2 });
		list.Add(new PieceInfo() { Name = "B_PAWN_4", Path = "Pieces/Black Pawn", X = 6, Y = 3 });
		list.Add(new PieceInfo() { Name = "B_PAWN_5", Path = "Pieces/Black Pawn", X = 6, Y = 4 });
		list.Add(new PieceInfo() { Name = "B_PAWN_6", Path = "Pieces/Black Pawn", X = 6, Y = 5 });
		list.Add(new PieceInfo() { Name = "B_PAWN_7", Path = "Pieces/Black Pawn", X = 6, Y = 6 });
		list.Add(new PieceInfo() { Name = "B_PAWN_8", Path = "Pieces/Black Pawn", X = 6, Y = 7 });

		list.Add(new PieceInfo() { Name = "B_ROOK_1", Path = "Pieces/Black Rook", X = 7, Y = 0 });
		list.Add(new PieceInfo() { Name = "B_ROOK_2", Path = "Pieces/Black Rook", X = 7, Y = 7 });
		list.Add(new PieceInfo() { Name = "B_KNIGHT_1", Path = "Pieces/Black Knight", X = 7, Y = 1 });
		list.Add(new PieceInfo() { Name = "B_KNIGHT_2", Path = "Pieces/Black Knight", X = 7, Y = 6 });
		list.Add(new PieceInfo() { Name = "B_BISHOP_1", Path = "Pieces/Black Bishop", X = 7, Y = 2 });
		list.Add(new PieceInfo() { Name = "B_BISHOP_2", Path = "Pieces/Black Bishop", X = 7, Y = 5 });
		list.Add(new PieceInfo() { Name = "B_KING_1", Path = "Pieces/Black King", X = 7, Y = 3 });
		list.Add(new PieceInfo() { Name = "B_QUEEN_2", Path = "Pieces/Black Queen", X = 7, Y = 4 });


		foreach (var item in list)
		{
			GameObject GO = GameObject.Instantiate<GameObject>(ResourcesCTL.Instance.GetGameObject(item.Path));
			GO.transform.parent = this.transform.GetChild(1);
			GO.name = item.Name;

			BasePiece p = GO.GetComponent<BasePiece>();
			p.SetOriginalLocation(item.X, item.Y);
			pieces.Add(p);

		}

    }

    public Vector3 CanculatePosition(int i, int j)
    {
        return basePosition + new Vector3(i * CELL_SIZE, 0, j * CELL_SIZE);
    }
}
