using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public abstract class BasePiece : MonoBehaviour
{

    protected List<Cell> _targetedCells = new List<Cell>();
    protected Cell _currentCell;
	[SerializeField]
	private Vector3 offsetPosition;
	public PieceInfo Info { get; private set; }

	[SerializeField]
	protected EPlayer _player;
	public EPlayer Player { get { return _player; } protected set { _player = value; } }

	public CLocation Location { get; private set; }

    public void SetInfo(PieceInfo info,Cell cell)
	{
		
		this.Info = info;
        SetNewLocation(cell);
		
	}

    protected void SetNewLocation(Cell cell){
        this._currentCell = cell;
        cell.SetPiece(this);
        this.Location = cell.Location;
		this.transform.DOMove(offsetPosition + new Vector3(this.Location.x * ChessBoard.Current.CELL_SIZE, 0,
			this.Location.y * ChessBoard.Current.CELL_SIZE), 0.75f);
    }
    public abstract void Move(Cell targetedCell);
    // Set current cell state to select.
	public abstract void BeSelected();
    //Handle attack 
    public abstract void Attack(Cell targetedCell);
	//Remove state all cell had been select or target
    public void BeUnselected(){
        foreach(var item in _targetedCells){
            item.SetCellState(ECellState.NORMAL);
        }
        _targetedCells.Clear();
    }
}
