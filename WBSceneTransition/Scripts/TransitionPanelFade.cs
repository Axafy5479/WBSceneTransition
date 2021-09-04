using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WBTransition
{
    public class TransitionPanelFade : TransitionPanelBase
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GameObject loading;
        [SerializeField] private float openTime = 1;
        [SerializeField] private float waitTime = 2;
        [SerializeField] private float closeTime = 1;


        private void Awake()
        {
            //マスクが閉じ切るまでは、ローディングのクルクルは見せない
            loading.SetActive(false);

            //初め、マスクの色は透明(徐々に不透明度を上げる)
            canvasGroup.alpha = 0;
        }

        /// <summary>
        /// 画面を閉じるアニメーション
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator Close()
        {
            //1秒間かけて、不透明度を0から1まで上昇させる
            for (int i = 0; i < (int)(20*closeTime); i++)
            {
                canvasGroup.alpha = (float)i / (int)(20*closeTime-1);
                yield return new WaitForSecondsRealtime(1f/20);
            }

            //マスクが閉じ切ったので、ローディングのクルクルを表示
            loading.SetActive(true);
        }

        /// <summary>
        /// マスクを開くアニメーション
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator Open()
        {
            //マスクを開く前に、ローディングのクルクルを非表示
            loading.SetActive(false);

            //1秒間かけて、不透明度を1から0まで下げる
            for (int i = (int)(20 * openTime-1); i >=0; i--)
            {
                canvasGroup.alpha = (float)i / (int)(20f * openTime - 1);
                yield return new WaitForSecondsRealtime(1f / 20);
            }
        }

        /// <summary>
        /// 最低限、ロード画面を見せたい時間(秒)
        /// </summary>
        protected override float WaitTime => waitTime;
    }
}
