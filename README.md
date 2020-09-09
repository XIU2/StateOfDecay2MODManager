# XIU2/StateOfDecay2MODManager

[![Release Version](https://img.shields.io/github/v/release/XIU2/StateOfDecay2MODManager.svg?style=flat-square&label=Release&color=1784ff)](https://github.com/XIU2/StateOfDecay2MODManager/releases/latest)
[![GitHub license](https://img.shields.io/github/license/XIU2/StateOfDecay2MODManager.svg?style=flat-square&label=License&color=ca6b2b)](https://github.com/XIU2/StateOfDecay2MODManager/blob/master/LICENSE)
[![GitHub Star](https://img.shields.io/github/stars/XIU2/StateOfDecay2MODManager.svg?style=flat-square&label=Star&color=ca6b2b)](https://github.com/XIU2/StateOfDecay2MODManager/stargazers)
[![GitHub Fork](https://img.shields.io/github/forks/XIU2/StateOfDecay2MODManager.svg?style=flat-square&label=Fork&color=ca6b2b)](https://github.com/XIU2/StateOfDecay2MODManager/network/members)

**腐烂国度2：主宰版 MOD 管理器**

有效解决《腐烂国度2：主宰版》MOD 混在一起难以管理、卸载的痛点！  

****

## 软件界面

![软件界面](https://raw.githubusercontent.com/XIU2/StateOfDecay2MODManager/master/img/04.png)  

****

## 下载地址

* 蓝奏云 ：[https://www.lanzoux.com/b073munlc](https://www.lanzoux.com/b073munlc)
* Github：[https://github.com/XIU2/StateOfDecay2MODManager/releases](https://github.com/XIU2/StateOfDecay2MODManager/releases)

****

## 使用说明：

我把使用步骤都写在软件里的 **[使用方法]** 中了，鼠标指向它就能看到！  
**请勿修改软件所在目录中的 Mods 文件夹及其子目录，以及 Mods.xml 文件，否则会影响软件正常使用！**  
*如果要更新 MOD，请先卸载并删除 MOD，然后再载入并安装 MOD！*  

> MOD管理器放哪里都行！没有要求！另外，一个 压缩包 里只能有一个 MOD ，MOD 合集请自行拆分并打包为压缩包！  

****

### 压缩包要求：

如果你无法载入 MOD 压缩包，那说明该 MOD 压缩包 不符合下面的目录结构要求！或者尝试重新打包压缩包为 .zip 格式。  

![软件界面](https://raw.githubusercontent.com/XIU2/StateOfDecay2MODManager/master/img/01.png)  
**支持！** MOD 实质文件（Items、Art、GameSystems 之类的文件夹）必须要有上级目录（Content）  

![软件界面](https://raw.githubusercontent.com/XIU2/StateOfDecay2MODManager/master/img/02.png)  
**支持！** MOD 实质文件（Items、Art、GameSystems 之类的文件夹）必须要有上级目录（Content）  

![软件界面](https://raw.githubusercontent.com/XIU2/StateOfDecay2MODManager/master/img/03.png)  
**不支持！** MOD 实质文件（Items、Art、GameSystems 之类的文件夹）直接位于压缩包根目录是无法导入的！需要解压后 按照目录结构要求 重新打包为压缩包再去导入！  

****

## 其他说明：

```
> 注意(1)：如果想要识别 已安装的 MOD，需要把这些 MOD 的压缩包找回来，载入软件后右键 [安装] 就会自动识别为 [已安装] 状态！  
> 注意(2)：MOD 压缩包内目录结构要求：Content\xxxx (item 之类的实质性 MOD 文件上级目录)，载入 MOD 时，软件会去寻找 Content 目录。  
> 注意(3)：因为本软件所用的解压缩库(dll)对 7z 支持一般，所以 **超过 1MB 的 [.7z 压缩包] 解压速度特别慢，造成卡死的假象，强烈建议使用 .zip 格式！  
> 注意(4)：如果 无法拖入压缩文件（鼠标是禁止符号），那说明你正在以管理员身份运行本软件，请通过普通用户身份运行本软件！  
```

### 运行提示 .NET Framework 错误？

本软件最低依赖是 .NET Framework 4.5，报错说明你系统的该依赖版本低于 4.5（Win10 默认满足该依赖），请安装更高版本的 [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) ！

****

## 更新日志

### 2020年03月31日，版本 v1.4

1. **新增** 冲突检测哈希算法校验。
```
—— 安装 MOD 时，如果检查到该 MOD 的所有文件都已经存在时，将会进行 哈希算法校验，对比文件是不是完全一致。
—— 假设 MOD A 包含了 x、y、z 这3个文件，而 MOD B 包含了 x、y 这2个文件，如果你先安装 MOD A ，再去安装 MOD B ，就会出现 MOD B 的所有文件都已存在（实际上是 MOD A 的文件），但实际上文件内容并不一样。
—— 这时候只需要用 哈希算法校验 对比一下，只要有任何一个文件不一样，就说明不是同一个 MOD。
—— 如果 MOD 文件校验完全一致，说明是同一个 MOD，才会把状态改为：已安装。
```  

### 2020年03月30日，版本 v1.3

1. **修复** 没有正确显示手动方式安装的 MOD 文件冲突信息。
`—— 如果你以前手动方式安装过 MOD，却没有通过本软件重新安装，那么安装新 MOD 在检测冲突时没有正确显示这些手动方式安装的 MOD 文件冲突信息。`
2. **修复** 安装 MOD 冲突时，冲突文件数量显示错误的问题。
`—— 应该以当前 MOD 自身的冲突文件数量为准，而不是以其他 MOD 的冲突文件数量总数为准。`  

### 2020年03月28日，版本 v1.2

1. **修复** 当载入 MOD 未发现 Content 文件夹时，依然会添加到列表框中的问题。
2. **修复** 当载入从 Mods 目录下手动打包的 MOD 压缩包时，文件索引不准确的问题。
`—— 有点绕，其实就是如果你或者别人把 Mods 下的 MOD 文件打包为压缩包，然后再去载入该压缩包时，建立文件索引不准确的问题。`  

### 2020年03月28日，版本 v1.1

1. **新增** 安装 MOD 冲突时，提示与哪些 MOD 及文件冲突（输出到错误日志中）。
2. **新增** 载入 MOD 时，对大于 1MB 的 7z 压缩包会提示是否继续。
`—— 在没有解决 7z 压缩包解压速度的情况下，只能先这样的，强烈建议使用 .zip 压缩格式！`
3. **修复** 安装 MOD 冲突时，冲突文件数量显示错误的问题（忘记-2了）。
4. **修复** 载入 MOD 同名时，无法正确建立文件索引的问题。
5. **优化** 载入 MOD 同名时，随机生成 MOD 名称功能。
6. **优化** 游戏数据位置 [浏览] 功能（不再强制指定默认位置了）。  

### 2020年03月26日，版本 v1.0

1. **发布** 第一个版本。  

## 许可证

The GPL-3.0 License.

本软件仅供学习交流，请勿用于商用。  

软件所有权归 念着倒才狗小(XIU2) 所有。  

> 该项目只在 [3DM](https://bbs.3dmgame.com/thread-6023553-1-1.html) 发布过，其他网站均为转载。当然，**欢迎转载！** 