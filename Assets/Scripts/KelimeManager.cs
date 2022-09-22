using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq;


public class KelimeManager : MonoBehaviour
{
    public static KelimeManager instance;

    [SerializeField]
    Transform dogruPanel,butonlarPanel;

    [SerializeField]
    Image hayvanImg;


   [SerializeField]
   List<KelimeData> kelimeler;


    [SerializeField]
    GameObject harfPrefab;
   List<string> dogruCevap=new List<string>();
   List<string> secilecekHarfler=new List<string>();



   int kacinciKelime;

   int harfAdet,butonAdet;

    private void Awake() {
         instance=this;
    }
   private void Start()
   {
     kacinciKelime=0;

     KelimeyiAcFNC();
   }

   void KelimeyiAcFNC()
   {
        hayvanImg.sprite=kelimeler[kacinciKelime].animalImg;
        hayvanImg.GetComponent<RectTransform>().DOAnchorPosX(500,.5f).From();

        for (int i = 0; i < kelimeler[kacinciKelime].dogruKelime.Length; i++)
        {
            dogruCevap.Add(kelimeler[kacinciKelime].dogruKelime[i].ToString().ToUpper());
        }

        for (int i = 0; i < dogruCevap.Count; i++)
        {
            GameObject harf=Instantiate(harfPrefab);
            harf.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=dogruCevap[i];
            harf.transform.GetChild(0).gameObject.SetActive(false);
            harf.transform.parent=dogruPanel;
        }


        for (int i = 0; i < kelimeler[kacinciKelime].secilecekHarfler.Length; i++)
        {
            secilecekHarfler.Add(kelimeler[kacinciKelime].secilecekHarfler[i].ToString().ToUpper());
        }

        secilecekHarfler=secilecekHarfler.OrderBy(i=>Random.value).ToList();

        for (int i = 0; i < butonlarPanel.childCount; i++)
        {
            butonlarPanel.GetChild(i).GetComponent<CanvasGroup>().alpha=0f;
            butonlarPanel.GetChild(i).GetComponent<RectTransform>().localScale=Vector3.zero;
            butonlarPanel.GetChild(i).GetComponent<Button>().enabled=false;

            butonlarPanel.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text=secilecekHarfler[i];
        }

        StartCoroutine(DogruHarfleriAcRoutine());

        StartCoroutine(ButonlariAcRoutine());
   }

   IEnumerator DogruHarfleriAcRoutine()
   {
        harfAdet=0;

        while (harfAdet<dogruCevap.Count)
        {
           dogruPanel.GetChild(harfAdet).GetComponent<CanvasGroup>().DOFade(1,.3f);
           dogruPanel.GetChild(harfAdet).GetComponent<RectTransform>().DOScale(1,.3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.2f);
            harfAdet++;
        }
   }

   IEnumerator ButonlariAcRoutine()
   {
        butonAdet=0;

         while (butonAdet<butonlarPanel.childCount)
        {
           butonlarPanel.GetChild(butonAdet).GetComponent<CanvasGroup>().DOFade(1,.3f);
           butonlarPanel.GetChild(butonAdet).GetComponent<RectTransform>().DOScale(1,.3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.2f);
            butonAdet++;
        }

         for (int i = 0; i < butonlarPanel.childCount; i++)
        {
          
            butonlarPanel.GetChild(i).GetComponent<Button>().enabled=true;

            
        }
       
   }

   public void SonucuKontrolEt(string gelenHarf)
   {
        if(dogruCevap.Contains(gelenHarf))
        {
            for (int i = 0; i < dogruPanel.childCount; i++)
            {
                if(!dogruPanel.GetChild(i).GetChild(0).gameObject.activeInHierarchy)
                {
                    if(gelenHarf==dogruPanel.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text)
                    {
                        dogruPanel.GetChild(i).GetChild(0).gameObject.SetActive(true);
                        break;
                    }
                }
                
            }
        } else
        {
            print("yanlış");
        }

        if(HepsiAcildimiFNC())
        {
            Invoke("YeniSoruyaGec",1f);
        }
   }

    void YeniSoruyaGec()
    {
        kacinciKelime++;

        dogruCevap.Clear();
        secilecekHarfler.Clear();

        if(kacinciKelime<kelimeler.Count)
        {
            foreach (Transform child in dogruPanel)
            {
                Destroy(child.gameObject);
            }

            Invoke("KelimeyiAcFNC",1f);
        }   else
        {
            print("oyun bitti");
        }
    }

   bool HepsiAcildimiFNC()
   {
        for (int i = 0; i < dogruPanel.childCount; i++)
        {
            if(!dogruPanel.GetChild(i).GetChild(0).gameObject.activeInHierarchy)
            {
                return false;
            }
        }

        return true;
   }

     
}
