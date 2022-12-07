using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float horizontalMovement;
    private float verticalMovement;
    private bool isJumping=false;
    private bool isFire1 = false;
    private bool isFire2 = false;
    private bool buttonEnter = false;
    private Vector2 currentDirection;
    private Vector2 defaultDirection;

    public float Movement => horizontalMovement;
    public float UpMovement => verticalMovement;

    public Vector2 Direction => currentDirection;

    public bool IsJumping => isJumping;

    public bool IsFire1 
    {
        get => isFire1;
        set => isFire1 = value;
    }

    public bool IsFire2
    {
        get => isFire2;
        set => isFire2 = value;
    }

    public bool Enter => buttonEnter;

    private void Awake()
    {
        defaultDirection = Vector2.right;
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis(GlobalVarNames.HorAxes);
        verticalMovement = Input.GetAxis(GlobalVarNames.VerAxes);
        //Вектор для атаки:
        if (horizontalMovement > 0)
        {
            defaultDirection = Vector2.right;
            currentDirection = defaultDirection;
        }
        else if (horizontalMovement < 0)
        {
            defaultDirection = Vector2.left;
            currentDirection = defaultDirection;
        }
        else 
        {
            currentDirection = defaultDirection;
        }
        //....
        isJumping = Input.GetButtonDown(GlobalVarNames.Jumping);
        if(Input.GetButtonDown(GlobalVarNames.MilAttk))
            isFire1 = true;
        if(Input.GetButtonDown(GlobalVarNames.DstAttk))
            isFire2 = true;

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            buttonEnter = true;
        }
    }
}
