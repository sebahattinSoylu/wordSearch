using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButonManager : MonoBehaviour
{
    public void ButonaBasildi()
    {
        string harf=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        
        KelimeManager.instance.SonucuKontrolEt(harf);
    }
}
