# WBSceneTransition
Unityでシーン遷移を簡単に実装できるパッケージです。

<br>

# DEMO


# 特徴

- 非同期なシーン遷移であり、ロード画面を簡単に作ることができます。
- シーン遷移後、マスクを開く前とマスクを開ききったタイミングで処理を行うことができます。
- 遷移先のシーンに変数を伝えることができます。

<br>


# 導入

https://github.com/FraMari495/WBSceneTransition/releases/
からunitypackageをダウンロードします。<u>**Unityプロジェクトのバックアップをとった後**</u>ダウンロードしたファイルをエディタ上にドラッグアンドドロップしてください。

<br>

# 使用方法

## <b>Demoを再現する方法</b>
Plugins/WBTransition/SampleScene にある二つのシーンをBuild Settings のScenes in Buildに登録し、SceneTransitionSampleシーンを実行してください。

<br>

## <b>シーン遷移</b>
```c#
// シーン遷移先に伝えたい変数（マスクが開く前）
var dic_before = new Dictionary<string,object>();

// シーン遷移先に伝えたい変数（マスクが開いたあと）
var dic_after = new Dictionary<string,object>();

WBTransition.SceneManager.LoadScene("シーン名",dic_before,dic_before);
```

<br>

## <b>シーン遷移が完了した際に処理を行う</b>

1. シーンに存在する`MonoBehaviour`なクラス(<u>**シーンにつき1つのみ**</u>)に `WBTransition.ISceneInitializer`を実装し
2. `BeforeOpenMask()`と`AfterOpenMask()`内に好きな処理を記述してください。

`BeforeOpenMask()`はシーン遷移後、マスクが開く前に呼ばれます。シーンの準備(サーバーとの通信など)といった、プレイヤーに見せるべきではない処理を行うと良いと思います。引数には、シーン遷移時に用いた変数`dic_before`が入ります。

`AfterOpenMask()`はマスクが開ききった後に呼ばれます。このタイミングでゲームを開始するとよいです。引数には、シーン遷移時に用いた変数`dic_after`が入ります。


<br>

## <b>ロードシーンを飾る</b>

Plugins/WBTransition/Resources/LoadPanel を開き、好きなようにデコレーションしてください。

フェードアウト以外のアニメーションに変更したい場合、
1. TransitionPanelBaseを継承したクラスを作成し、`Open()`と`Close()`、`WaitTime`を実装します。
2. Plugins/WBTransition/Resources/LoadPanel にアタッチされているTransitionPanelFadeを削除し、
3. 代わりに作成したスクリプトをアタッチします。

<br>

# 注意

- `ISceneInitializer`を実装してよいのは、シーンにつき1インスタンスのみです。
- デフォルトでは、遷移中の`Time.timeScale`はゼロとなります。ロード画面上にアニメーションするオブジェクトを加える場合は注意が必要です。\
`Time.timeScale`をゼロとしたくない場合はPlugins/WBTransition/Resources/LoadPanelにアタッチされたTransitionPanelFadeコンポーネントのtimeScale_zeroをfalseにしてください。

<br>

# Author

白黒Unity

<br>

# License

WBSceneTransition is under [MIT license](https://en.wikipedia.org/wiki/MIT_License).
