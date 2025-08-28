using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.SceneMangement
{
    /// <summary>
    /// 整個遊戲都一致的淡入淡出效果，所以做成singleton單例模式
    /// 此程式碼用於實現螢幕淡入淡出效果，通常用於場景切換或重要事件發生時，以提升使用者體驗。
    /// </summary>
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeTime = 3.0f;
        CanvasGroup canvasGroup;
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOutIn());
        }

       IEnumerator FadeOutIn()
        {
            yield return FadeOut(3);
            yield return FadeIn(1);
        }
       public IEnumerator FadeOut(float time)
        {
            //N = time / Time.deltaTime N為偵數
            //alpha = 1/N  每次減少1/N的alpha值  
            while (canvasGroup.alpha < 1.0f)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while(canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
