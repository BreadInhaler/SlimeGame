using UnityEngine;
using UnityEngine.UI;
public class HUDHandler : MonoBehaviour{
    [Header("HUD")]
    public bool HUDActive = true;
    public GameObject HUD;
    public GameObject itemsObject;
    public GameObject EnemyHPBarObjects;
    [Header("Bars")]
    public Image playerHPBar;
    public Image playerSPBar;
    public Image enemyHPBar;
    public TMPro.TMP_Text enemyName;
    [Header("Ability")]
    public TMPro.TMP_Text abilityText;
    public Image abilityIcon;
    public TMPro.TMP_Text memoryText;
    public Image memoryIcon;
    [Header("Items")]
    public TMPro.TMP_Text prevItemText;
    public TMPro.TMP_Text currItemText;
    public TMPro.TMP_Text nextItemText;
    public Image prevItem;
    public Image currItem;
    public Image nextItem;
    [Header("Money")]
    public TMPro.TMP_Text moneyText;
    public Image moneyIcon;
    //public Text[] itemsText;
    public void Start(){
        HUD.SetActive(HUDActive);
        moneyIcon.sprite=Globals.moneyIcon;
    }
    public void UpdateUI(HUDData data){
        if(playerHPBar!=null) playerHPBar.fillAmount = data.playerHP;
        if(playerSPBar!=null) playerSPBar.fillAmount = data.playerSP;
        if(abilityText!=null) abilityText.text = data.playerAbility;
        if(memoryText!=null) memoryText.text = data.playerMemory;
        if(abilityIcon!=null) abilityIcon.sprite=data.playerAbilityIcon;
        if(abilityIcon!=null) memoryIcon.sprite=data.playerMemoryIcon;
        if(moneyText!=null) moneyText.text=data.playerMoney.ToString();
        if(enemyHPBar!=null) {
            if(data.enemyHP<=0) EnemyHPBarObjects.SetActive(false);
            else{
                enemyHPBar.fillAmount = data.enemyHP;
                EnemyHPBarObjects.SetActive(true);
                if(enemyName!=null) enemyName.text=data.enemyName;
            }
        }
        if(prevItemText!=null && currItemText!=null && nextItemText!=null) {
            if(data.items == null || data.items.Count == 0){
                itemsObject.SetActive(false);
                return;
            }else itemsObject.SetActive(true);
            if(data.items.Count==3){
                prevItem.sprite=data.items[0].item.icon;
                currItem.sprite=data.items[1].item.icon;
                nextItem.sprite=data.items[2].item.icon;
                prevItemText.text=data.items[0].quantity.ToString();
                currItemText.text=data.items[1].quantity.ToString();
                nextItemText.text=data.items[2].quantity.ToString();
            }

        }
        /*
        for (int i = 0; i < itemsText.Length; i++){
            itemsText[i].text = data.items[i].id;
        }
        */
    }
}