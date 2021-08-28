using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WBSceneManagement.Sample
{
    public class SceneChangeButton : MonoBehaviour
    {

        /// <summary>
        /// ボタンをクリックした際に呼ばれる
        /// </summary>
        public void ButtonClicked()
        {
            Dictionary<string, object> d1 = new Dictionary<string, object>()
        {
            {"key1",495 },
            {"key2","Hello World!" }
        };

            Dictionary<string, object> d2 = new Dictionary<string, object>()
        {
            {"key1",true },
            {"key2",3.2f }
        };

            WBSceneManagement.SceneManager.LoadScene("SceneTransitionSmple2", d1, d2);
        }
    }

}
