using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace WBSceneManagement
{
    public class TransitionPanel : MonoBehaviour
    {
        /// <summary>
        /// シーンを隠すマスク(星形に切り抜かれたマスク)
        /// </summary>
        [SerializeField]private RectTransform unmaskRect;

        /// <summary>
        /// 初期サイズ
        /// マスクを開くときに用いる
        /// </summary>
        private Vector2 initSize;

        /// <summary>
        /// Tweenerが自動再生される設定か否か
        /// (どの設定でも正しく動作させるために用いる)
        /// </summary>
        private bool TweenerAutoPlay =>
            DOTween.defaultAutoPlay == AutoPlay.AutoPlayTweeners || 
            DOTween.defaultAutoPlay == AutoPlay.All;

        /// <summary>
        /// シーンのロードが終わり、マスクを外す直前にtrueとなる
        /// </summary>
        public bool SceneLoaded { get; private set; } = false;


        void Awake()
        {
            //マスクを表示する
            //(初めはマスクのオブジェクトを非アクティブにしないと、ロード画面の設定が面倒になります)
            unmaskRect.gameObject.SetActive(true);

            //初期サイズを記憶する
            //(マスクを開くとき、このサイズを目標にスケーリングする)
            initSize = unmaskRect.sizeDelta;

            //シーン遷移時にロード画面が削除されるのを防ぐ
            DontDestroyOnLoad(this.gameObject);
        }


        /// <summary>
        /// マスクを閉じる
        /// </summary>
        /// <param name="closeTime">閉じるのにかける時間(秒)</param>
        /// <returns></returns>
        internal async UniTask Close(float closeTime)
        {
            await PlayMaskAnimation(closeTime, false);
        }

        /// <summary>
        /// マスクを開く
        /// </summary>
        /// <param name="openTime">開くのにかける時間(秒)</param>
        /// <returns></returns>
        internal async UniTask Open(float openTime)
        {
            SceneLoaded = true;
            await PlayMaskAnimation(openTime, true);
        }


        /// <summary>
        /// マスクのアニメーションの本体
        /// </summary>
        /// <param name="time">アニメーションにかける時間</param>
        /// <param name="open">マスクを開くならtrue 閉じるならfalse</param>
        /// <returns></returns>
        private async UniTask PlayMaskAnimation(float time, bool open)
        {
            //アニメーションが終わったタイミングでtrueとなる
            bool animEnd = false;

            //開くアニメーションなら initSizeを、閉じるアニメーションならゼロを目指す
            Vector2 targetSize = open ? initSize : Vector2.zero;

            //マスクのサイズを変更するアニメーション
            var tween = unmaskRect.DOSizeDelta(targetSize, time)

                //Time.timeScaleに依存せずアニメーションを行う
                .SetUpdate(true)

                //Easeの設定(速度変化を適当につける)
                .SetEase(open ? Ease.InQuad : Ease.OutQuad)

                //アニメーションが終了した際に引数が行われる(animEndがtrueとなる)
                .OnComplete(() => animEnd = true);

            //Tweenが自動再生されない設定の場合、再生メソッドを呼ぶ
            if (!TweenerAutoPlay) tween.Play();

            //アニメーションが終わるまで待つ
            await UniTask.WaitWhile(() => !animEnd);
        }
    }
}
