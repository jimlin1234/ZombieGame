using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.SceneMangement
{
    /// <summary>
    /// ��ӹC�����@�P���H�J�H�X�ĪG�A�ҥH����singleton��ҼҦ�
    /// ���{���X�Ω��{�ù��H�J�H�X�ĪG�A�q�`�Ω���������έ��n�ƥ�o�ͮɡA�H���ɨϥΪ�����C
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
            //N = time / Time.deltaTime N������
            //alpha = 1/N  �C�����1/N��alpha��  
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
