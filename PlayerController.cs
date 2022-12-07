using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius;
    private bool isGround;
    private Rigidbody2D player;

    private Fighting fighting;
    [SerializeField] private float rateOfFightLimit;
    private float rateOfFight;

    private Fireball fireball;
    [SerializeField] private float rateOfSpeedLimit;
    private float rateOfSpeed;

    private HP hp;
    private PlayerInput playerInput;

    private GameObject ladder;
    private LadderController ladderController;

    public bool IsGround => isGround;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        fireball = GetComponent<Fireball>();
        fighting = GetComponent<Fighting>();
        hp = GetComponent<HP>();
        playerInput = GetComponent<PlayerInput>();
        ladder = GameObject.FindGameObjectWithTag("Ladder");
        ladderController = ladder.GetComponent<LadderController>();
    }

    private void Update()
    {
        if (hp.IsAlive)
        {
            //Прыжок
            JumpLogic();
            //Дистанционная атака
            if (rateOfSpeed <= rateOfSpeedLimit)
            {
                rateOfSpeed += Time.deltaTime;
                if (rateOfSpeed > rateOfSpeedLimit)
                    playerInput.IsFire2 = false;
            }
            if (playerInput.IsFire2 && rateOfSpeed > rateOfSpeedLimit)
            {
                rateOfSpeed = 0;
                fireball.Shooting(playerInput.Direction);
            }
            //Ближний бой
            if (rateOfFight <= rateOfFightLimit)
            {
                rateOfFight += Time.deltaTime;
                if (rateOfFight > rateOfFightLimit)
                    playerInput.IsFire1 = false;
            }
            if (playerInput.IsFire1 && rateOfFight > rateOfFightLimit)
            {
                rateOfFight = 0;
                fighting.FightingDirection(playerInput.Direction);
            }
        }
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundTransform.position, checkRadius, groundLayer);
        MoveLogic();
    }

    private void MoveLogic() 
    {
        if (!playerInput.IsFire1)
        {
            if (Mathf.Abs(playerInput.Movement) > 0.01f && hp.IsAlive)
            {
                player.velocity = new Vector2(playerInput.Movement * speed, player.velocity.y);
            }
            else if (Mathf.Abs(playerInput.UpMovement) > 0.01f && ladderController.IsUp)
            {
                player.velocity = new Vector2(player.velocity.x, playerInput.UpMovement * speed/2);
                player.gravityScale = 0;
            }
            else 
            {
                player.gravityScale = 1;
            }
        }
        else
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y);
        }
    }

    private void JumpLogic() 
    {
        if (playerInput.IsJumping && isGround)
            player.velocity = new Vector2(player.velocity.x, jumpForce);
    }
}
