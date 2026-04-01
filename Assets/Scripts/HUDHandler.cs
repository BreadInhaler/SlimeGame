using UnityEngine;
using UnityEngine.UI;
public class HUDHandler : MonoBehaviour{
    public bool HUDActive = true;
    public GameObject HUD;
    public Image playerHPBar;
    public Image playerSPBar;
    public TMPro.TMP_Text abilityText;
    public TMPro.TMP_Text memoryText;
    public Image enemyHPBar;
    //public Text[] itemsText;
    public void Start(){
        HUD.SetActive(HUDActive);
    }
    public void UpdateUI(HUDData data){
        playerHPBar.fillAmount = data.playerHP;
        playerSPBar.fillAmount = data.playerSP;
        abilityText.text = data.playerAbility;
        memoryText.text = data.playerMemory;
        //enemyHPBar.fillAmount = data.enemyHP;
        /*
        for (int i = 0; i < itemsText.Length; i++){
            itemsText[i].text = data.items[i].id;
        }
        */
    }
    public void SetBarValue(Image bar,int value){
        bar.fillAmount=value;
    }
}