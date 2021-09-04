using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WBTransition.SampleScene
{


    /// <summary>
    /// ボタンが押されたときに指定のシーンに遷移するサンプルコード
    /// </summary>
    public class SceneChangeSample : MonoBehaviour
    {
        [Header("遷移先のシーン")]
        [SerializeField] private WBTransition.Scene nextScene;

        [Header("伝えたい変数")]
        [SerializeField] private InputField field;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Slider slider;


        public void ButtonClicked()
        {
            var d = new Dictionary<string, object>()
            {
                {"inputField",field.text },
                {"toggle",toggle.isOn },
                {"slider",slider.value },
            };

            //シーン遷移を開始
            WBTransition.SceneManager.LoadScene(nextScene,d,null);
        }

    }
}
