using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WBTransition
{
    public abstract class TransitionPanelBase : MonoBehaviour
    {
        [Header("遷移時はtimeScaleをゼロとする")]
        [SerializeField] private bool timeScale_zero = true;

        private bool endWaiting = false;

        /// <summary>
        /// 最低限、ロード画面を見せたい時間(秒)
        /// </summary>
        protected abstract float WaitTime { get; }

        /// <summary>
        /// WaitTimeだけロード画面を表示するため
        /// 処理を止める
        /// </summary>
        /// <returns></returns>
        private IEnumerator Wait()
        {
            yield return new WaitForSecondsRealtime(WaitTime);
            endWaiting = true;
        }

        /// <summary>
        /// 画面を閉じるアニメーション
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator Close();

        /// <summary>
        /// 画面を開くアニメーション
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator Open();


        /// <summary>
        /// シーン遷移を行う
        /// </summary>
        /// <param name="newScene">遷移先のシーン名</param>
        /// <param name="onEndTransition">遷移直後に呼ばれるメソッドに渡す変数</param>
        /// <param name="onEndRemoveMask">マスク削除直後に呼ばれるメソッドに渡す変数</param>
        internal void LoadScene(TransitionPackage package)
        {
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(StartTransition(package));
        }

        /// <summary>
        /// シーン遷移本体
        /// </summary>
        /// <param name="package">シーン遷移に必要な情報(遷移先、伝えたい変数)</param>
        /// <returns></returns>
        private IEnumerator StartTransition(TransitionPackage package)
        {
            string newScene = package.NewScene;
            var onEndTransition = package.OnEndTransition;
            var onEndRemoveMask = package.OnEndRemoveMask;

            float defaultTimeScale = Time.timeScale;
            if (timeScale_zero) Time.timeScale = 0;

            //マスクを閉じる。閉じ終わるまで待機
            yield return Close();

            //シーン遷移を開始すると同時に、ロード画面を指定時間見せる
            StartCoroutine(Wait());
            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Single);

            //SceneInitializerを探す
            var initializer = FindInitializer();

            //SceneInitializerがあれば、、、
            if (initializer != null)
            {
                //マスクを外す直前の処理を行う
                yield return initializer.BeforeOpenMask(onEndTransition);
            }

            //ロード画面は、最低限WaitTimeだけは表示する
            //(もし遷移先が軽いシーンならば遷移は一瞬で終わる。この処理を書かないと、ロード画面が一瞬だけ映り不自然となる)
            yield return new WaitWhile(() => !endWaiting);

            //マスクを外す。外し終わるまで待機
            yield return Open();

            //SceneInitializerがあれば、、、
            if (initializer != null)
            {
                //マスクを外した後の処理を行う
                initializer.AfterOpenMask(onEndRemoveMask);
            }


            if (timeScale_zero) Time.timeScale = defaultTimeScale;

            //マスク(このオブジェクト)を削除
            GameObject.Destroy(this.gameObject);
        }

        /// <summary>
        /// Initializerを探す
        /// </summary>
        /// <returns></returns>
        private static ISceneInitializer FindInitializer()
        {
            var initializers = new List<ISceneInitializer>();

            foreach (var item in GameObject.FindObjectsOfType<Component>())
            {
                if (item is ISceneInitializer initializer) initializers.Add(initializer);
            }

            if (initializers.Count > 1)
            {
                Debug.LogError($"「ISceneInitializer」を実装できるのは、1シーンにつき1つまでです。現在{initializers.Count}個のインスタンスが実装しています。");
                return null;
            }
            else if (initializers.Count == 0)
            {
                return null;
            }
            else
            {
                return initializers[0];
            }
        }

    }
}
