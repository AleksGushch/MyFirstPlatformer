using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(HP))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private PlayerController playerController;
    private HP hp;
    private PlayerInput playerInput;
    private Rigidbody2D player;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        hp = GetComponent<HP>();
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (playerController.IsGround)//Персонаж на земле
        {
            if (Mathf.Round(player.velocity.x) < 0)// Перс движется
            {
                playerAnimator.SetBool("IsRunning", true);
                playerRenderer.flipX = true;
            }
            else if (Mathf.Round(player.velocity.x) > 0)
            {
                playerAnimator.SetBool("IsRunning", true);
                playerRenderer.flipX = false;
            }
            else//Перс стоит
            {
                playerAnimator.SetBool("IsRunning", false);
                //Ближний бой на земле
                if (playerInput.IsFire1)
                {
                    playerAnimator.SetBool("isAttack", true);
                }
                else
                    playerAnimator.SetBool("isAttack", false);
            }
            playerAnimator.SetBool("IsJumping", false);

            if (Mathf.Round(player.velocity.y) != 0) 
            {
                playerAnimator.SetBool("IsRunning", true);
            }
        }
        else //Персонаж в воздухе
        {
            playerAnimator.SetBool("IsRunning", false);
            playerAnimator.SetBool("IsJumping", true);
            //Ближний бой в воздухе
            if (playerInput.IsFire1)
                playerAnimator.SetBool("isJumpAttack", true);
            else
                playerAnimator.SetBool("isJumpAttack", false);
        }
        //Дистанционная атака
        if (playerInput.IsFire2)
            playerAnimator.SetBool("isDistAttack", true);
        else
            playerAnimator.SetBool("isDistAttack", false);
        //Жив ли игрок?
        if (!hp.IsAlive)
        {
            playerAnimator.SetBool("isAlive", false);
        }
        else
            playerAnimator.SetBool("isAlive", true);
        //Игрок получает урон
        if (hp.IsDamage)
            playerAnimator.SetBool("isHit", true);
        else
            playerAnimator.SetBool("isHit", false);
    }
}
