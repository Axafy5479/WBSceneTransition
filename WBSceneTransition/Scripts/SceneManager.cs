using System.Collections.Generic;
using UnityEngine;

namespace WBTransition
{
    public static class SceneManager
    {
        /// <summary>
        /// シーン遷移の機能をもつオブジェクト
        /// シーン遷移が重複して起こることを防ぐために用いる変数
        /// </summary>
        private static TransitionPanelBase panel;


        /// <summary>
        /// シーン遷移を行う
        /// </summary>
        /// <param name="newScene">遷移先のシーン名</param>
        /// <param name="onEndTransition">遷移直後に呼ばれるメソッドに渡す変数</param>
        /// <param name="onEndRemoveMask">マスク削除直後に呼ばれるメソッドに渡す変数</param>
        public static void LoadScene(string newScene,
            Dictionary<string, object> onEndTransition = null, Dictionary<string, object> onEndRemoveMask = null)
        {
            //シーン遷移が実行中でないか確認
            if (panel!=null)
            {
                Debug.LogError("シーン遷移中に、新たにシーン遷移を行うことはできません");
                return;
            }

            // 遷移先として空文字が渡されていないことを確認
            if(newScene == "")
            {
                Debug.LogError("遷移先シーンが「\"\"」となっています。インスペクターにSceneAssetが確実に登録されていることを確認してください");
                return;
            }

            //遷移機能を持ったオブジェクトを生成
            panel = GameObject.Instantiate(Resources.Load<GameObject>("LoadPanel")).GetComponent<TransitionPanelBase>();

            var package = new TransitionPackage(newScene, onEndTransition, onEndRemoveMask);

            //シーン遷移を開始
            panel.LoadScene(package);
        }

    }

    /// <summary>
    /// LoadSceneの引数がややこしくなるため、この構造体内にまとめる
    /// </summary>
    internal struct TransitionPackage
    {
        public TransitionPackage(string newScene, Dictionary<string, object> onEndTransition, Dictionary<string, object> onEndRemoveMask)
        {
            NewScene = newScene;
            OnEndTransition = onEndTransition;
            OnEndRemoveMask = onEndRemoveMask;
        }

        internal string NewScene { get; }
        internal Dictionary<string,object> OnEndTransition { get; }
        internal Dictionary<string, object> OnEndRemoveMask { get; }
    }
}
