using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    [SerializeField] private GameObject movingPlatform;
    private bool switchTrigger;
    private PlayerInput playerInput;
    private GameObject player;
    private Rigidbody2D rbPlayer;
    private GameObject Interface;
    private InterfaceController interfaceController;
    [SerializeField] private float switchTimer;
    private float timer;

    public bool GetSwitch => switchTrigger;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerInput = player.GetComponent<PlayerInput>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        switchTrigger = false;
        Interface = GameObject.Find("Interface");
        interfaceController = Interface.GetComponent<InterfaceController>();
        timer = 0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interfaceController.InfoPanelActivate("Нажмите Е для активации переключателя!");
            rbPlayer.constraints=RigidbodyConstraints2D.FreezeAll;
            switchTrigger = true;
        }
    }
    private void Update()
    {
        if (playerInput.Enter) 
        {
            rbPlayer.constraints = RigidbodyConstraints2D.None;
            rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.GetComponentInChildren<Animator>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            interfaceController.InfoPanelDeactivate();
            timer += Time.deltaTime;
            if (timer >= switchTimer)
            {
                movingPlatform.GetComponent<SliderJoint2D>().enabled = true;
                timer = 0;
                switchTrigger = false;
            }
        }
    }
}
