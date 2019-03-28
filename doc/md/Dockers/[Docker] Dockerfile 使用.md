# [Docker] Dockerfile 使用

[TOC]



## Dockerfile 指令

Dockerfile 由一行行命令语句组成，并且支持以 `#` 开头的注释行。

一般的，**Dockerfile** 分为四部分：**基础镜像信息**、**维护者信息**、**镜像操作指令**和**容器启动时执行指令**。

> **Dockerfile的指令是忽略大小写的，建议使用大写，使用 # 作为注释，每一行只支持一条指令**



### FROM

> 解释：指定基础的image，表示新程序基于哪个image构建新image
> 用法：`FROM <image>:<tag>`

一开始必须指明所基于的镜像名称, **第一条指令必须为 FROM 指令**

```dockerfile
# 基于 python
FROM python
```



### MAINTAINER

>解释：指定维护者信息
>用法：`MAINTAINER <author>`

```dockerfile
# 标注创建者
MAINTAINER cdmin207078@gmail.com
```



### RUN

> 解释：接受命令作为参数并用于创建镜像。**不像CMD命令，RUN命令用于创建镜像**
> 用法：`RUN <command>`

RUN可以运行任何被基础image支持的命令 , 每条 RUN 指令将在当前镜像基础上执行指定命令，并提交为新的镜像。
当命令较长时可以使用 \ 来换行。

```dockerfile
# 
RUN 
```



### CMD

> 解释：指定维护者信息
> 用法：`MAINTAINER <author>`



```dockerfile
#
CMD 
```



### EXPOSE









### VOLUME





> 延伸阅读：[Docker Volume入门介绍 - 掘金](http://www.dockerinfo.net/1857.html)





## 参考

[创建自己的docker及Dockerfile语法 - 掘金](https://juejin.im/entry/5a5f367cf265da3e3f4cb88c)

[dockerfile 介绍 - 博客园](https://www.cnblogs.com/boshen-hzb/p/6400272.html)

[Dockerfile入门介绍 - dockerinfo.net](http://www.dockerinfo.net/695.html)