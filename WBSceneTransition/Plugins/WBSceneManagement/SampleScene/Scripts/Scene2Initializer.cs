using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WBSceneManagement;

namespace WBSceneManagement.Sample
{
    public class Scene2Initializer : MonoBehaviour, ISceneInitializer
    {
        public void AfterOpenMask(Dictionary<string, object> args)
        {
            Debug.Log("マスクが開きました");

            bool b = (bool)args["key1"];
            float f = (float)args["key2"];
            Debug.Log(b);
            Debug.Log(f);
        }

        public IEnumerator BeforeOpenMask(Dictionary<string, object> args)
        {
            Debug.Log("マスクが開く直前");
            int n = (int)args["key1"];
            string s = (string)args["key2"];
            Debug.Log(n);
            Debug.Log(s);
            yield return null;
        }
    }
}
