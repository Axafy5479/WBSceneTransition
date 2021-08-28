using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WBSceneManagement
{
    public static class SceneManager
    {
        private const float CLOSE_TIME = 0.5f;
        private const float WAIT_TIME = 0.3f;
        private const float OPEN_TIME = 0.5f;

        /// <summary>
        /// シーン遷移中か否か
        /// </summary>
        public static bool IsTransitioning { get; private set; } = false;


        /// <summary>
        /// シーン遷移を行う
        /// </summary>
        /// <param name="newScene">遷移先のシーン名</param>
        /// <param name="onEndTransition">遷移直後に呼ばれるメソッドに渡す変数</param>
        /// <param name="onEndRemoveMask">マスク削除直後に呼ばれるメソッドに渡す変数</param>
        public static void LoadScene(string newScene,
            Dictionary<string,object> onEndTransition = null, Dictionary<string, object> onEndRemoveMask = null)
        {
            //シーン遷移が同時に起こっていないか確認
            if (IsTransitioning)
            {
                Debug.LogError("シーン遷移中です");
                return;
            }


            //シーン遷移
            StartTransition(newScene,onEndTransition, onEndRemoveMask).Forget();
        }

        /// <summary>
        /// シーン遷移本体
        /// </summary>
        /// <param name="newScene"></param>
        /// <param name="onEndTransition"></param>
        /// <param name="onEndRemoveMask"></param>
        /// <returns></returns>
        private static async UniTask StartTransition(string newScene, 
            Dictionary<string, object> onEndTransition = null, Dictionary<string, object> onEndRemoveMask = null)
        {
            float timeScale = Time.timeScale;

            //遷移中フラグを立て、重ねて遷移が起こらないようにする
            IsTransitioning = true;
            Time.timeScale = 0;

            //シーン遷移のマスクを生成する
            TransitionPanel transitionPanel = GameObject.Instantiate(Resources.Load<GameObject>("LoadPanel")).GetComponent<TransitionPanel>();

            //マスクを閉じる
            await transitionPanel.Close(CLOSE_TIME);

            //シーン遷移
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(newScene);

            //SceneInitializerを探す
            var initializer = FindInitializer();

            //SceneInitializerがあれば、、、
            if (initializer != null)
            {
                //マスクを外す直前の処理を行う
                await initializer.BeforeOpenMask(onEndTransition);
            }

            //1秒まつ
            await UniTask.Delay(TimeSpan.FromSeconds(WAIT_TIME),true);

            //マスクを外す
            await transitionPanel.Open(OPEN_TIME);

            //SceneInitializerがあれば、、、
            if (initializer != null)
            {
                //マスクを外した後の処理を行う
                initializer.AfterOpenMask(onEndRemoveMask);
            }

            //マスクを削除
            GameObject.Destroy(transitionPanel.gameObject);

            //シーン遷移フラグをfalseにする
            IsTransitioning = false;
            Time.timeScale = timeScale;


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
                Debug.LogError("SceneInitializerが複数存在します");
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
