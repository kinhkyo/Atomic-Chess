using UnityEngine;
using System.Collections;

public abstract class BasePiece : MonoBehaviour
{

	[SerializeField]
	private Vector3 offsetPosition;

	[SerializeField]
	protected EPlayer _player;
	public EPlayer Player { get { return _player; } protected set { _player = value; } }

	private Vector2 originalLocation;
	public Vector2 Location { get; private set; }

	public void SetOriginalLocation(int hang, int cot)
	{
		originalLocation = new Vector2(hang, cot);
		this.transform.position = offsetPosition + new Vector3(cot * ChessBoard.Current.CELL_SIZE, 0, hang * ChessBoard.Current.CELL_SIZE);
	}

	public abstract void Move();
}
