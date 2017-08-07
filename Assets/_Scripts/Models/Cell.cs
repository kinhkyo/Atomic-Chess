using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    private Transform cellHoverObj;
    private Transform cellSelectedObj;
    private BasePiece _currentPiece;
    private ECellColor _color;
    public ECellColor Color
    {
        get { return _color; }
        set
        {
            _color = value;
            Debug.Log("Check : " + _color);
            switch (_color)
            {
                case ECellColor.BLACK:
                    GetComponent<Renderer>().material = ResourcesCTL.Instance.BlackCellMaterial;
                    break;
                case ECellColor.WHITE:
                    GetComponent<Renderer>().material = ResourcesCTL.Instance.WhiteCellMaterial;
                    break;
                default:
                    break;
            }
        }
    }


    private ECellState _state;
    public ECellState State
    {
        get { return _state; }
        private set
        {
            _state = value;

            switch (_state)
            {
                case ECellState.NORMAL:
                    cellHoverObj.gameObject.SetActive(false);
                    cellSelectedObj.gameObject.SetActive(false);
                    break;
                case ECellState.HOVER:
                    cellHoverObj.gameObject.SetActive(true);
                    cellSelectedObj.gameObject.SetActive(false);
                    break;

                case ECellState.SELECTED:
                    cellHoverObj.gameObject.SetActive(false);
                    cellSelectedObj.gameObject.SetActive(true);
                    break;
                case ECellState.TARGETED:

                    break;
                default:
                    cellHoverObj.gameObject.SetActive(false);
                    cellSelectedObj.gameObject.SetActive(false);
                    break;
            }
        }
    }


    public float SIZE
    {
        get
        {
            return GetComponent<Renderer>().bounds.size.x;
        }
    }


    protected void Start()
    {
        cellHoverObj = this.transform.GetChild(0);
        cellSelectedObj = this.transform.GetChild(1);

        State = ECellState.NORMAL;
    }


    /// <summary>
    /// Chỉ có thể thay đổi được cell state thông qua hàm SetCellState
    /// </summary>
    public void SetCellState(ECellState cellState)
    {
        if (cellState != ECellState.SELECTED)
        {
            if (this.State != ECellState.SELECTED)
                this.State = cellState;
        }
        else
        {
            if (this.State == ECellState.SELECTED)
                this.State = ECellState.HOVER;
            else this.State = ECellState.SELECTED;
        }

        if (cellState == ECellState.UNSELECTED)
            this.State = ECellState.UNSELECTED;
    }
}
