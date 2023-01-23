# Rain AVG Engine

一个简易AVG引擎，核心是基于xLua和xml的脚本规则+解析器（其实基于此也可以RPG的脚本化）。



## 功能

对话、选项、剧情跳转，支线主线跳转，数据保存读取，图片展示


## 依赖库：

RainsUnityLib [https://github.com/rainssong/RainsUnityLib]()

xLua 已引入


## 基本使用方法

1. Assets\StreamingAssets\放入需要的图片音频文件
2. 填写Assets\StreamingAssets\resource.json，标注需要加载的资源
3. 参考Sample，创建场景，添加StoryMono
4. 编写StreamingAssets\assets\Scripts\Main.xml


## 高级使用方法

使用Unity自行编写视图接口，使用ScriptManager运行xml脚本
