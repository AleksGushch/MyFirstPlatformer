using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    [SerializeField] private GameObject movingPlatform;
    private bool switchTrigger;
    private PlayerInput playerInput;
    private GameObject player;
    private GameObject Interface;
    private InterfaceController interfaceController;
    [SerializeField] private float switchTimer;
    private float timer;

    public bool GetSwitch => switchTrigger;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerInput = player.GetComponent<PlayerInput>();
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
            collision.gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezePositionX;
            if (playerInput.Enter)
            {
                switchTrigger = true;
                collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                gameObject.GetComponentInChildren<Animator>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                interfaceController.InfoPanelDeactivate();
            }
        }
    }
    private void Update()
    {
        if (switchTrigger) 
        {
            timer += Time.deltaTime;
            if (timer >= switchTimer)
            {
                movingPlatform.GetComponent<SliderJoint2D>().useMotor = true;
                timer = 0;
                switchTrigger = false;
            }
        }
    }
}
