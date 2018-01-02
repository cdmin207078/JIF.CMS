# Visual Studio 重置

> 参考: http://blog.csdn.net/Yu_Wei_123/article/details/70477353 - VS2015完全重置的方法

## 问题描述

今天在使用 visual studio 2015 开发时, **突然间无法正常附加进程调试**. 标记断点之后亦无法正常进入调试位置, 并出现 **无可用源** 错误页面.

![无可用源错误页面](http://img.blog.csdn.net/20160517122947629?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQv/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/Center)

并且在 visual studio 的 `解决方案管理器` 中, **无法双击打开代码文件**. 

## 解决方法

搞了半天, 不知如何解决. 突然间回忆起之前工作时遇到过, 将 devExpress 组件, 添加到 visual studio 工具箱时, 会导致 visual studio 不能正常使用的情况. 当时是将 **visual studio 重置了一下, 就好了**. 故, 抱着侥幸的心理试了一下, 竟然好了! 得记录一下.

"运行" -> "cmd", 切换到 visual studio 安装路径, 定位到 `devenv.exe` 所在目录. 

> win10 可以直接按 `windows` 键之后直接运行下方命令

执行命令
```
devenv.exe /setup /resetuserdata /resetsettings
```

over.