using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WBTransition.SampleScene
{

    /// <summary>
    /// シーン遷移後に呼ばれるメソッドを実装したクラスのサンプル
    /// </summary>
    public class InitializerSample : MonoBehaviour, ISceneInitializer
    {
        [SerializeField] private Text inputResult;
        [SerializeField] private Text toggleResult;
        [SerializeField] private Text sliderResult;

        [SerializeField] private GameObject sceneChangeButton;


        /// <summary>
        /// シーン遷移後、マスクが開き始める前に
        /// このメソッドが呼ばれる
        /// </summary>
        /// <param name="args">遷移前のシーンから伝えられた変数</param>
        /// <returns></returns>
        public void AfterOpenMask(Dictionary<string, object> args)
        {
            Debug.Log("AfterOpen");

            sceneChangeButton.SetActive(true);
        }

        /// <summary>
        /// マスクが開き終わった直後に呼ばれる
        /// </summary>
        /// <param name="args">遷移前のシーンから伝えられた変数</param>
        public IEnumerator BeforeOpenMask(Dictionary<string, object> args)
        {
            Debug.Log("BeforOpen");

            var s = (string)args["inputField"];
            var b = (bool)args["toggle"];
            var n = (float)args["slider"];

            inputResult.text = $"入力された文字は「{s}」です";
            toggleResult.text = $"スイッチは{(b ? "on" : "off")}です";
            sliderResult.text = $"スライダーの値は{n}です";

            sceneChangeButton.SetActive(false);

            yield return null;
        }


    }
}

