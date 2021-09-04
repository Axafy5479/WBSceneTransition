using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WBTransition
{
    /// <summary>
    /// シーン遷移直後に呼ばれるメソッドを実装する
    /// このインターフェースは1シーンにつき1つまで
    /// </summary>
    public interface ISceneInitializer
    {
        /// <summary>
        /// シーン遷移後、マスクが開き始める前に
        /// このメソッドが呼ばれる
        /// </summary>
        /// <param name="args">遷移前のシーンから伝えられた変数</param>
        /// <returns></returns>
        public IEnumerator BeforeOpenMask(Dictionary<string, object> args);

        /// <summary>
        /// マスクが開き終わった直後に呼ばれる
        /// </summary>
        /// <param name="args">遷移前のシーンから伝えられた変数</param>
        public void AfterOpenMask(Dictionary<string, object> args);


    }

}
