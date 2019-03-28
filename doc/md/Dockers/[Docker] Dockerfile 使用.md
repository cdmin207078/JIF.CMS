# [Docker] Dockerfile 使用

[TOC]



## Dockerfile 指令

Dockerfile 由一行行命令语句组成，并且支持以 `#` 开头的注释行。

一般的，**Dockerfile** 分为四部分：**基础镜像信息**、**维护者信息**、**镜像操作指令**和**容器启动时执行指令**。

> **Dockerfile的指令是忽略大小写的，建议使用大写，使用 # 作为注释，每一行只支持一条指令**



### FROM

> 解释：基础的image，表示新程序基于哪个image构建新image
> 用法：`FROM <image>:<tag>`

一开始必须指明所基于的镜像名称, **第一条指令必须为 FROM 指令**

```dockerfile
# 基于 python
FROM python
```



### MAINTAINER [`弃用 转 Label`]

>解释：维护者信息
>用法：`MAINTAINER <author>`

```dockerfile
# 标注维护者信息
MAINTAINER cdmin207078@gmail.com
# 新的定义方法，参考 LABEL 指令
LABEL maintainer="cdmin207078@gmail.com"
```



### LABEL

> 解释：镜像的元数据
> 用法：`LABEL <key>=<value> <key>=<value> <key>=<value> ...`

LABEL 采用键值对形式设置

```dockerfile
# 一行定义一个
LABEL "com.example.vendor"="ACME Incorporated"
LABEL com.example.label-with-value="foo"
LABEL version="1.0"
# 多个在一行定义
LABEL multi.label1="value1" multi.label2="value2" other="value3"
# 多行情况时，使用 \ 换行
LABEL description="This text illustrates \
that label-values can span multiple lines."
```

定义好的元数据，可以使用 `docker inspect` 命令查看 image 设置的元数据信息，如下：

```json
"Labels": {
    "com.example.vendor": "ACME Incorporated"
    "com.example.label-with-value": "foo",
    "version": "1.0",
    "description": "This text illustrates that label-values can span multiple lines.",
    "multi.label1": "value1",
    "multi.label2": "value2",
    "other": "value3"
},
```



### RUN

> 解释：运行任何被基础image支持的命令
> 用法：`RUN <command>` 或 `RUN ["executable","command-1","command-2"]`

常用于接受命令作为参数并用于创建镜像。
**不像CMD命令，RUN命令用于创建镜像** , 每条 RUN 指令将在当前镜像基础上执行指定命令，并提交为新的镜像。
当命令较长时可以使用 \ 来换行。

```dockerfile
# 执行屏幕输出
RUN echo 'hello RUN.'
# 换种方式
RUN ["/bin/bash","-c","echo hello RUN."]
..
# 
```



### CMD

> 解释：启动容器时执行的命令
> 用法：`CMD [“executable”,”param1″,”param2″]`
>
> ​	     `CMD command param1 param2`
>
> ​             `CMD [”param1″,”param2″]`

**每个 Dockerfile 只能有一条 CMD 命令。如果指定了多条命令，只有最后一条会被执行。如果用户启动容器时候指定了运行的命令，则会覆盖掉 CMD 指定的命令。**

```dockerfile
#
CMD "echo" "Hello docker!"
```



### COPY





### EXPOSE

> 解释：容器暴露的端口
> 用法：`EXPOSE <port> [<port>...]`

```dockerfile
# mysql 默认需要暴露3306端口
EXPOSE 3306
# Apache
EXPOSE 80
# MongoDB
EXPOSE 27017
# 多个端口
EXPOSE 9001 9002 9003
...
# 默认开启 TCP,可以手动指定开启哪个
EXPOSE 80/UDP
EXPOSE 80/TCP
```



### VOLUME





> 延伸阅读：[Docker Volume入门介绍 - 掘金](http://www.dockerinfo.net/1857.html)



### ADD





### WORKDIR

> 解释：设置后续指令工作目录 
> 用法：`WORKDIR </path>`

为后续的 RUN  / CMD / ENTRYPOINT  / COPY / ADD  指令配置工作目录
可以使用多个 WORKDIR 指令，后续命令如果参数是相对路径，则会基于之前命令指定的路径。

```dockerfile
# 多次设置工作目录, 最终定位到的工作目录为 /a/b/c
WORKDIR /a
WORKDIR b
WORKDIR c
RUN pwd
```



### ENTRYPOINT

> 解释：容器启动后执行的命令 
> 用法：`WORKDIR </path>`

容器启动后执行的命令，并且不可被 docker run 提供的参数覆盖。
**每个 Dockerfile 中只能有一个 ENTRYPOINT ，当指定多个时，只有最后一个起效。**

```dockerfile
#
ENTRYPOINT 
```





### ENV

> 解释：设置环境变量
> 用法：`ENV <key> <value>` 或 `ENV <key>=<value> ...`

```dockerfile
# 单个定义
ENV JAVA_HOME /usr/local/java8
# 多个定义
ENV SERVER_WORKS=4 SERVER_IP=192.168.0.1
# 值带空格 & 换行
ENV myName="John Doe" myDog=Rex\ The\ Dog \
    myCat=fluffy
```



### USER





## Dockerfile实例 





## 创建镜像





## 参考

[Dockerfile reference - Official Website](https://docs.docker.com/engine/reference/builder/#from)

[创建自己的docker及Dockerfile语法 - 掘金](https://juejin.im/entry/5a5f367cf265da3e3f4cb88c)

[dockerfile 介绍 - 博客园](https://www.cnblogs.com/boshen-hzb/p/6400272.html)

[Dockerfile入门介绍 - dockerinfo.net](http://www.dockerinfo.net/695.html)