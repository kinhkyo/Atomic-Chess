using UnityEngine;
using System.Collections;

public class P_Pawn : BasePiece
{
	private bool isFirstMoved = true;

	
	public override void Move(Cell targetedCell)
	{
        isFirstMoved = false;
        //1. Move
        this.SetNewLocation(targetedCell);

        //2. Kill
        BeUnselected();


        BaseGameCTL.Current.SwichTurn();
	}

	public override void Attack(Cell targetedCell)
	{

	}

	public override void BeSelected()
	{
		switch (this._player)
		{
			case EPlayer.BLACK:
				BeSelected_Black();
				break;
			case EPlayer.WHITE:
				BeSelected_White();
				break;
		}
	}

	private void BeSelected_White()
	{
		//Hiển thị các nước đi có thể
		if (isFirstMoved)
		{
            //2. Có khả năng đi 2 bước về phía trước
            // Location
            if (ChessBoard.Current.Cells[Location.x][Location.y + 2].CurrentPiece == null
                && ChessBoard.Current.Cells[Location.x][Location.y + 1].CurrentPiece == null)
            {
                _targetedCells.Add(ChessBoard.Current.Cells[Location.x][Location.y + 2]);
            }
		}

        if (ChessBoard.Current.Cells[Location.x][Location.y + 1].CurrentPiece == null ){
			////1. Có khả năng đi 1 bước về phía trước
			_targetedCells.Add(ChessBoard.Current.Cells[Location.x][Location.y + 1]); 
        }

		

		////3. Xác định 2 ô chéo phía trước có quân ăn hay không
        /// If that is enemy
        if (Location.x > 0 && ChessBoard.Current.Cells[Location.x - 1][Location.y + 1].CurrentPiece != null){
            //Left ahead

            //Check that cell is Enemy 
            if (this._currentCell.CurrentPiece.Player != ChessBoard.Current.Cells[Location.x - 1][Location.y + 1].CurrentPiece.Player){
                _targetedCells.Add(ChessBoard.Current.Cells[Location.x - 1][Location.y + 1]);
            }
			
        }
		   

        if (Location.x < 7 && ChessBoard.Current.Cells[Location.x + 1][Location.y + 1].CurrentPiece != null){
            //Right ahead

            if (this._currentCell.CurrentPiece.Player != ChessBoard.Current.Cells[Location.x + 1][Location.y + 1].CurrentPiece.Player)
            { 
                _targetedCells.Add(ChessBoard.Current.Cells[Location.x + 1][Location.y + 1]);
            }
			
        }
		   

        //4. Xác định trường hợp bắt tốt qua đường

        foreach(var item in _targetedCells){

            item.SetCellState(ECellState.TARGETED);
        }
	}


	private void BeSelected_Black()
	{
		//Hiển thị các nước đi có thể
		if (isFirstMoved)
		{
            //2. Có khả năng đi 2 bước về phía trước
            // Location
            if (ChessBoard.Current.Cells[Location.x][Location.y - 2].CurrentPiece == null
               && ChessBoard.Current.Cells[Location.x][Location.y - 1].CurrentPiece == null)
            { 
                _targetedCells.Add(ChessBoard.Current.Cells[Location.x][Location.y - 2]);
            }
			
		}


        //Check the cell has current piece.
        if (ChessBoard.Current.Cells[Location.x][Location.y - 1].CurrentPiece == null){
			////1. Có khả năng đi 1 bước về phía trước
			_targetedCells.Add(ChessBoard.Current.Cells[Location.x][Location.y - 1]);
        }
		

		////3. Xác định 2 ô chéo phía trước có quân ăn hay không
		/// If that is enemy
        if (Location.x > 0 && ChessBoard.Current.Cells[Location.x - 1][Location.y - 1].CurrentPiece != null){
            if (this._currentCell.CurrentPiece.Player != ChessBoard.Current.Cells[Location.x - 1][Location.y - 1].CurrentPiece.Player)
            { 
                _targetedCells.Add(ChessBoard.Current.Cells[Location.x - 1][Location.y - 1]);
            }
        }
			

        if (Location.x < 7 && ChessBoard.Current.Cells[Location.x + 1][Location.y - 1].CurrentPiece != null){
            if (this._currentCell.CurrentPiece.Player != ChessBoard.Current.Cells[Location.x + 1][Location.y - 1].CurrentPiece.Player){
                _targetedCells.Add(ChessBoard.Current.Cells[Location.x + 1][Location.y - 1]);
            }
        }
			

		//4. Xác định trường hợp bắt tốt qua đường

		foreach (var item in _targetedCells)
		{
			item.SetCellState(ECellState.TARGETED);
		}
	}
}
