using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    [SerializeField] private Image currentHP;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Text countDiamonds;
    
    private GameObject player;
    private HP hp;
    private int currentCountDiamond;

    private void Start()
    {
        infoPanel.SetActive(false);
        player = GameObject.Find("Player");
        hp = player.GetComponent<HP>();
        messagePanel.SetActive(false);
    }

    private void Update()
    {
        currentHP.fillAmount = hp.GetHP;

        if (!hp.IsAlive)
            messagePanel.SetActive(true);

    }

    public void InfoPanelActivate(string infoText) 
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<Text>().text=infoText;
    }

    public void InfoPanelDeactivate() 
    {
        infoPanel.SetActive(false);
    }

    public void DiamondCount(int countDiamond) 
    {
        currentCountDiamond += countDiamond;
        countDiamonds.text = currentCountDiamond.ToString();
    }
}
