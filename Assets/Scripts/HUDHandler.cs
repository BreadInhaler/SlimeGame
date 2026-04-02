using UnityEngine;
using UnityEngine.UI;
public class HUDHandler : MonoBehaviour{
    [Header("HUD")]
    public bool HUDActive = true;
    public GameObject HUD;
    [Header("Bars")]
    public Image playerHPBar;
    public Image playerSPBar;
    public Image enemyHPBar;
    [Header("Ability")]
    public TMPro.TMP_Text abilityText;
    public Image abilityIcon;
    public TMPro.TMP_Text memoryText;
    public Image memoryIcon;
    [Header("Items")]
    public TMPro.TMP_Text prevItem;
    public TMPro.TMP_Text currItem;
    public TMPro.TMP_Text nextItem;
    //public Text[] itemsText;
    public void Start(){
        HUD.SetActive(HUDActive);
    }
    public void UpdateUI(HUDData data){
        if(playerHPBar!=null) playerHPBar.fillAmount = data.playerHP;
        if(playerSPBar!=null) playerSPBar.fillAmount = data.playerSP;
        if(abilityText!=null) abilityText.text = data.playerAbility;
        if(memoryText!=null) memoryText.text = data.playerMemory;
        if(abilityIcon!=null) abilityIcon.sprite=data.playerAbilityIcon;
        if(abilityIcon!=null) memoryIcon.sprite=data.playerMemoryIcon;
        if(prevItem!=null && currItem!=null && nextItem!=null) {
            if(data.items.Count>=3){
                prevItem.text=data.items[0];
                currItem.text=data.items[1];
                nextItem.text=data.items[2];
            } 
        }
        //enemyHPBar.fillAmount = data.enemyHP;
        /*
        for (int i = 0; i < itemsText.Length; i++){
            itemsText[i].text = data.items[i].id;
        }
        */
    }
}