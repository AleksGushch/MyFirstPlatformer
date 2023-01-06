using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceControllerFinal : MonoBehaviour
{
    [SerializeField] private Image currentHP, currentBossHP;
    [SerializeField] private GameObject mainInterface, bossDialogScene, cutSceneTrigger;
    [SerializeField] private Text countDiamonds, bossDialogText;
    [SerializeField] private CinemachineVirtualCamera main, cut_scene;
    [SerializeField] private float timerForCutSceneAnimation;
    private TriggerCutScene trigger;
    [TextArea(4, 10)]
    public string[] bossSentence;
    private GameObject player, boss, gameOverPanel, infoPanel;
    private HP hp, bossHP;
    private int currentCountDiamond, currentDialogMessage;
    private float currentTimeCutScene;
    private bool cutScene, isShown, timerMessage;

    private void Start()
    {
        player = GameObject.Find("Player");
        boss = GameObject.FindGameObjectWithTag("Boss") ;
        hp = player.GetComponent<HP>();
        bossHP = boss.GetComponent<HP>();
        gameOverPanel = mainInterface.transform.GetChild(0).transform.GetChild(1).gameObject;
        gameOverPanel.SetActive(false);
        infoPanel = mainInterface.transform.GetChild(0).transform.GetChild(3).gameObject;
        infoPanel.SetActive(false);
        cut_scene.Priority = main.Priority - 1;
        currentTimeCutScene = 0;
        currentDialogMessage = 0;
        trigger = cutSceneTrigger.GetComponent<TriggerCutScene>();
        cutScene = false;
        isShown = false;
        timerMessage = false;
    }

    private void Update()
    {
        PlayerHealthBar();
        BossHealthBar();
        ChangePriorityMainCamera();
        
        Debug.Log(currentDialogMessage);
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

    private void PlayerHealthBar() 
    {
        currentHP.fillAmount = hp.GetHP;
        if (!hp.IsAlive)
            gameOverPanel.SetActive(true);
    }

    private void BossHealthBar() 
    {
        currentBossHP.fillAmount = bossHP.GetHP;
        if (!bossHP.IsAlive) 
        {
            mainInterface.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    private void ChangePriorityMainCamera() 
    {
        if (trigger.IsTrigger && !cutScene)
        {
            cut_scene.Priority = main.Priority + 1;
            mainInterface.SetActive(false);
            currentTimeCutScene += Time.deltaTime;
            if (currentTimeCutScene >= 2f)
            {
                bossDialogScene.SetActive(true);
                ShowDialogMessage();
                if (!timerMessage) 
                {
                    StartCoroutine(TimeDelayBossMessage());
                    timerMessage = true;
                }
                
            }
            if (currentTimeCutScene >= timerForCutSceneAnimation)
            {
                cut_scene.Priority = main.Priority - 1;
                mainInterface.SetActive(true);
                bossDialogScene.SetActive(false);
                mainInterface.transform.GetChild(1).gameObject.SetActive(true);
                currentTimeCutScene = 0;
                cutScene = true;
            }
        }
    }

    private void ChangeDialogMessage() 
    {
        if (isShown) 
        {
            isShown = false;
            bossDialogText.enabled = false;
            currentDialogMessage++;
            if (currentDialogMessage < bossSentence.Length)
            {
                ShowDialogMessage();
                timerMessage = false;
            }
        }
        
    }

    private void ShowDialogMessage() 
    {
        bossDialogText.text = bossSentence[currentDialogMessage];
        bossDialogText.enabled = true;
        isShown = true;
    }

    private IEnumerator TimeDelayBossMessage() 
    {
        
        yield return new WaitForSeconds(3f);
        ChangeDialogMessage();
    }
}
