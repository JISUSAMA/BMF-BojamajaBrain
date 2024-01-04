using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LogoCtrl : MonoBehaviour
{
//    IEnumerator _logo()
//    {
//        mySequence = DOTween.Sequence()
//.SetAutoKill(false) //Ãß°¡
//.OnStart(() => {
//    LogoIMG.gameObject.transform.localScale = Vector3.zero;
//    // LogoIMG.gameObject.GetComponent<CanvasGroup>().alpha = 0;
//})
//.Append(LogoIMG.gameObject.transform.DOScale(1, 1).SetEase(Ease.OutBounce))
////.Join(LogoIMG.gameObject.GetComponent<CanvasGroup>().DOFade(1, 1))
//.SetDelay(0.5f);
//
//        yield return new WaitForSeconds(2f);
//        SceneManager.LoadScene("0.Main");
//        yield return null;
//    }

    public void Load_Main()
    {
        StartCoroutine(_Load_Main());
    }
    IEnumerator _Load_Main()
    {
        AppSoundManager.Instance.PlaySFX("LogoSound1");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("0.Main");
        yield return null;
    }
}
