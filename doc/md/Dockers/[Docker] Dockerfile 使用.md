# [Docker] Dockerfile 使用

[TOC]



## Dockerfile 指令

Dockerfile 由一行行命令语句组成，并且支持以 `#` 开头的注释行。

一般的，**Dockerfile** 分为四部分：**基础镜像信息**、**维护者信息**、**镜像操作指令**和**容器启动时执行指令**。

> **Dockerfile的指令是忽略大小写的，建议使用大写，使用 # 作为注释，每一行只支持一条指令**



### FROM

> 解释：指定基础镜像
> 用法：`FROM <image>:<tag>`

所谓定制镜像，那一定是以一个镜像为基础，在其上进行定制。就像我们之前运行了一个
nginx 镜像的容器，再进行修改一样，基础镜像是必须指定的。而 FROM 就是指定基础镜
像，因此一个 Dockerfile 中， **FROM 是必备的指令，并且必须是第一条指令**。

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

可以使用 `docker inspect` 命令查看 image 设置的元数据信息，如下：

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

> 解释：执行命令
> 用法： shell 格式  `RUN <command>` 
>
> ​	      exec 格式   `RUN ["executable","command-1","command-2"]`

常用于接受命令作为参数并用于安装软件包，创建镜像。
**不像CMD命令，RUN命令用于创建镜像** , 每条 RUN 指令将在当前镜像基础上执行指定命令，并提交为新的镜像。
当命令较长时可以使用 \ 来换行。



既然 RUN 就像 Shell 脚本一样可以执行命令，那么我们是否就可以像 Shell 脚本一样把每个
命令对应一个 RUN 呢？比如这样：

```dockerfile
FROM debian:jessie
RUN apt-get update
RUN apt-get install -y gcc libc6-dev make
RUN wget -O redis.tar.gz "http://download.redis.io/releases/redis-3.2.5.tar.gz"
RUN mkdir -p /usr/src/redis
RUN tar -xzf redis.tar.gz -C /usr/src/redis --strip-components=1
RUN make -C /usr/src/redis
RUN make -C /usr/src/redis install
```

Dockerfile 中每一个指令都会建立一层， RUN 也不例外。每一个 RUN 的行为，
就和刚才我们手工建立镜像的过程一样：新建立一层，在其上执行这些命令，执行结束
后， commit 这一层的修改，构成新的镜像。

而上面的这种写法，创建了 7 层镜像。这是完全没有意义的，而且很多运行时不需要的东
西，都被装进了镜像里，比如编译环境、更新的软件包等等。结果就是产生非常臃肿、非常
多层的镜像，不仅仅增加了构建部署的时间，也很容易出错。 

> Union FS 是有最大层数限制的，比如 AUFS，曾经是最大不得超过 42 层，现在是不得超过
> 127 层。

上面的 Dockerfile 正确的写法应该是这样：

```dockerfile
FROM debian:jessie
RUN buildDeps='gcc libc6-dev make' \
&& apt-get update \
&& apt-get install -y $buildDeps \
&& wget -O redis.tar.gz "http://download.redis.io/releases/redis-3.2.5.tar.gz" \
&& mkdir -p /usr/src/redis \
&& tar -xzf redis.tar.gz -C /usr/src/redis --strip-components=1 \
&& make -C /usr/src/redis \
&& make -C /usr/src/redis install \
&& rm -rf /var/lib/apt/lists/* \
&& rm redis.tar.gz \
&& rm -r /usr/src/redis \
&& apt-get purge -y --auto-remove $buildDeps
```

首先，之前所有的命令只有一个目的，就是编译、安装 redis 可执行文件。因此没有必要建立很多层，这只是一层的事情。因此，这里没有使用很多个 RUN 对一一对应不同的命令，而是仅仅使用一个 RUN 指令，并使用 && 将各个所需命令串联起来。将之前的 7 层，简化为了1 层。在撰写 Dockerfile 的时候，要经常提醒自己，这并不是在写 Shell 脚本，而是在定义每一层该如何构建。
并且，这里为了格式化还进行了换行。Dockerfile 支持 Shell 类的行尾添加 \ 的命令换行方
式，以及行首 # 进行注释的格式。良好的格式，比如换行、缩进、注释等，会让维护、排障
更为容易，这是一个比较好的习惯。
此外，还可以看到这一组命令的最后添加了清理工作的命令，删除了为了编译构建所需要的
软件，清理了所有下载、展开的文件，并且还清理了 apt 缓存文件。这是很重要的一步，我
们之前说过，镜像是多层存储，每一层的东西并不会在下一层被删除，会一直跟随着镜像。
因此镜像构建时，一定要确保每一层只添加真正需要添加的东西，任何无关的东西都应该清
理掉。
很多人初学 Docker 制作出了很臃肿的镜像的原因之一，就是忘记了每一层构建的最后一定要
清理掉无关文件。



### CMD

> 解释：容器启动命令
> 用法：shell 格式   `CMD command` 
> 	     exec 格式    `CMD [“executable”,”param1″,”param2″]`

**Docker 不是虚拟机，容器就是进程**。既然是进程，那么在启动容器的时候，需要指定所运行的程序及参数。 **CMD 指令就是用于指定默认的容器主进程的启动命令的**。

在指令格式上，一般推荐使用 exec 格式，这类格式在解析时会被解析为 JSON 数组，因此
一定要使用双引号 " ，而不要使用单引号。

如果使用 shell 格式的话，实际的命令会被包装为 sh -c 的参数的形式进行执行。比如：

```dockerfile
# shell格式
CMD echo $HOME
# 实际执行 exec格式
CMD ["sh", "-c", "echo $HOME"]
```

**每个 Dockerfile 只能有一条 CMD 命令。如果指定了多条命令，只有最后一条会被执行。如果用户启动容器时候指定了运行的命令，则会覆盖掉 CMD 指定的命令。**

> 而 **ENTRYPOINT** 会把容器后面的所有内容都当成参数传递给其指定的命令(**不会对命令覆盖**)

下面以例子说明：

```dockerfile
FROM ubuntu
CMD ["echo", "Hello Ubuntu from container"]
```

构建镜像，并运行

```shell
# 生成镜像
[root@vultr docker-lesson-command-CMD]# docker build -t cn/lesson-cmd .
Sending build context to Docker daemon  2.048kB
Step 1/2 : FROM ubuntu
 ---> 94e814e2efa8
Step 2/2 : CMD ["echo", "Hello Ubuntu from container"]
 ---> Running in 1eafc639eb8c
Removing intermediate container 1eafc639eb8c
 ---> 52c3f964da90
Successfully built 52c3f964da90
Successfully tagged cn/lesson-cmd:latest
# 
[root@vultr docker-lesson-command-CMD]# docker run cn/lesson-cmd
Hello Ubuntu from container
# 
[root@vultr docker-lesson-command-CMD]# docker run cn/lesson-cmd echo "Hello Ubuntu from host"
Hello Ubuntu from host
```







**其它注意 <待续>**

提到 CMD 就不得不提容器中应用在前台执行和后台执行的问题。这是初学者常出现的一个混
淆。

**Docker 不是虚拟机**，容器中的应用都应该以前台执行，而不是像虚拟机、物理机里面那样，
用 upstart/systemd 去启动后台服务，容器内没有后台服务的概念。

一些初学者将 CMD 写为：

```dockerfile
CMD service nginx start
```

然后发现容器执行后就立即退出了。甚至在容器内去使用 systemctl 命令结果却发现根本执
行不了。这就是因为没有搞明白前台、后台的概念，没有区分容器和虚拟机的差异，依旧在
以传统虚拟机的角度去理解容器。

对于容器而言，其启动程序就是容器应用进程，容器就是为了主进程而存在的，主进程退
出，容器就失去了存在的意义，从而退出，其它辅助进程不是它需要关心的东西。

而使用 service nginx start 命令，则是希望 upstart 来以后台守护进程形式启动 nginx 服
务。而刚才说了 CMD service nginx start 会被理解为 CMD [ "sh", "-c", "service nginx
start"] ，因此主进程实际上是 sh 。那么当 service nginx start 命令结束后， sh 也就结
束了， sh 作为主进程退出了，自然就会令容器退出。

正确的做法是直接执行 nginx 可执行文件，并且要求以前台形式运行。比如：

```dockerfile
CMD ["nginx", "-g", "daemon off;"]
```



### ENTRYPOINT

> 解释：入口点
> 用法：shell 格式 `ENTRYPOINT command param1 param2`
> 	     exec 格式  `ENTRYPOINT ["executable", "param1", "param2"]`

容器启动后执行的命令，并且不可被 docker run 提供的参数覆盖。
**每个 Dockerfile 中只能有一个 ENTRYPOINT ，当指定多个时，只有最后一个起效。**

该指令的使用分为两种情况， 一种是独自使用。当独自使用时，如果你还使用了CMD命令且CMD是一个完整的可执行的命令，那么CMD指令和ENTRYPOINT会互相覆盖只有最后一个CMD或者ENTRYPOINT有效，例如：

```dockerfile
# CMD指令将不会被执行，只有ENTRYPOINT指令被执行  
CMD echo “Hello, World!”  
ENTRYPOINT ls -l  
```


另一种用法和CMD指令配合使用来指定ENTRYPOINT的默认参数，这时CMD指令不是一个完整的可执行命令，仅仅是参数部分；ENTRYPOINT指令只能使用JSON方式指定执行命令，而不能指定参数。

```dockerfile
FROM ubuntu  
CMD ["-l"]  
ENTRYPOINT ["/usr/bin/ls"] 
```

最终执行的命令为：

```shell
 [root@localhost docker-nginx-owen] /usr/bin/ls -l
```



### RUN、CMD、ENTRYPOINT 三者区别

简单的说：

1. RUN 执行命令并创建新的镜像层，RUN 经常用于安装软件包。
2. CMD 设置容器启动后默认执行的命令及其参数，但 CMD 能够被 `docker run` 后面跟的命令行参数替换。
3. ENTRYPOINT 配置容器启动时运行的命令。



> 参考:
> [RUN vs CMD vs ENTRYPOINT - 每天5分钟玩转 Docker 容器技术（17）- 博客园](https://www.cnblogs.com/CloudMan6/p/6875834.html)



### EXPOSE

> 解释：声明端口
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

> 解释：定义匿名卷
> 用法：`VOLUME /data`
>
> ​	     `VOLUME ["/data"]`

容器运行时应该尽量保持容器存储层不发生写操作，对于数据库类需要保存动态数据的应用，其数据库文件应该保存于卷(volume)中，后面的章节我们会进一步介绍Docker 卷的概念。为了防止运行时用户忘记将动态文件所保存目录挂载为卷，在`Dockerfile` 中，我们可以事先指定某些目录挂载为匿名卷，这样在运行时如果用户不指定挂载，其应用也可以正常运行，不会向容器存储层写入大量数据。

```dockerfile
VOLUME ["/data1","/data2"]
```

这里的 `/data1`,`/data2` 目录就会在运行时自动挂载为匿名卷，任何向 /data 中写入的信息都不会记
录进容器存储层，从而保证了容器存储层的无状态化。

**通过 VOLUME 指令创建的挂载点，无法指定主机上对应的目录，是自动生成的。** 

上面的dockfile文件通过VOLUME指令指定了两个挂载点 /data1 和 /data2.
我们通过docker inspect 查看通过该dockerfile创建的镜像生成的容器，可以看到如下信息

![1553849583050]([Docker] Dockerfile 使用.assets/1553849583050.png)



**要想自定义挂载位置，可在容器运行时 使用 `-v <source>:<destination>` 指定**

```shell
docker run -d -v mydata:/data xxxx
```

在这行命令中，就使用了 mydata 这个命名卷挂载到了 /data 这个位置，替代了Dockerfile 中定义的匿名卷的挂载配置。



> 延伸阅读：
>
> [Docker Volume入门介绍 - 掘金](http://www.dockerinfo.net/1857.html)
>
> [Volume 使用 - 官网](https://docs.docker.com/storage/volumes/)
>
> [Dockerfile 指令 VOLUME 介绍 - 博客园](http://www.cnblogs.com/51kata/p/5266626.html)



### WORKDIR

> 解释：指定工作目录 
> 用法：`WORKDIR </path>`

使用 WORKDIR 指令可以来指定工作目录（或者称为当前目录），以后各层的当前目录就被改
为指定的目录，**如该目录不存在， WORKDIR 会帮你建立目录**。

为后续的 RUN  / CMD / ENTRYPOINT  / COPY / ADD  指令配置工作目录
可以使用多个 WORKDIR 指令，后续命令如果参数是相对路径，则会基于之前命令指定的路径。

```dockerfile
# 多次设置工作目录, 最终定位到的工作目录为 /a/b/c
WORKDIR /a
WORKDIR b
WORKDIR c
RUN pwd
```

一些初学者常犯的错误是把 Dockerfile 等同于 Shell 脚本来书写，这种错误的理解
还可能会导致出现下面这样的错误：

```dockerfile
RUN cd /app
RUN echo "hello" > world.txt
```

如果将这个 Dockerfile 进行构建镜像运行后，会发现找不到 /app/world.txt 文件，或者其内容不是 hello 。原因其实很简单，在 Shell 中，连续两行是同一个进程执行环境，因此前一个命令修改的内存状态，会直接影响后一个命令；**而在 Dockerfile 中，这两行 RUN 命令的执行环境根本不同，是两个完全不同的容器。这就是对 Dockerfile 构建分层存储的概念不了解所导致的错误**。

**每一个 RUN 都是启动一个容器、执行命令、然后提交存储层文件变更**。第一层 RUN
cd /app 的执行仅仅是当前进程的工作目录变更，一个内存上的变化而已，其结果不会造成任
何文件变更。而到第二层的时候，启动的是一个全新的容器，跟第一层的容器更**完全没关系**，自然不可能继承前一层构建过程中的内存变化。

因此如果需要改变以后各层的工作目录的位置，那么应该使用 `WORKDIR` 指令。



### COPY

> 解释：复制本地主机的目录到目标容器系统中
> 用法：`COPY <src> <dest>`

**当使用本地目录为源目录时，推荐使用 COPY。**

```dockerfile
# 复制 "test" 到 `WORKDIR`/relativeDir/
COPY test relativeDir/
# 复制 "test" 到 /absoluteDir/
COPY test /absoluteDir/
# 正则匹配复制文件
COPY check* /testdir/           
COPY check?.log /testdir/
```

**COPY 命令区别于 ADD 命令的一个用法是在 multistage 场景下。**
关于 multistage 的介绍和用法请参考笔者的《[Dockerfile 中的 multi-stage](https://www.cnblogs.com/sparkdev/p/8508435.html)》一文。在 multistage 的用法中，可以使用 COPY 命令把前一阶段构建的产物拷贝到另一个镜像中，比如：

```dockerfile
FROM golang:1.7.3
WORKDIR /go/src/github.com/sparkdevo/href-counter/
RUN go get -d -v golang.org/x/net/html
COPY app.go    .
RUN CGO_ENABLED=0 GOOS=linux go build -a -installsuffix cgo -o app .

FROM alpine:latest
RUN apk --no-cache add ca-certificates
WORKDIR /root/
COPY --from=0 /go/src/github.com/sparkdevo/href-counter/app .
CMD ["./app"]
```

这段代码引用自《[Dockerfile 中的 multi-stage](https://www.cnblogs.com/sparkdev/p/8508435.html)》一文，其中的 COPY 命令通过指定 --from=0 参数，把前一阶段构建的产物拷贝到了当前的镜像中。

> 延伸阅读：[Dockerfile 中的 COPY 与 ADD 命令](https://www.cnblogs.com/sparkdev/p/9573248.html)



### ADD

> 解释：从源系统的文件系统上复制文件到目标容器的文件系统
> 用法：`ADD <src directory or URL> <destination directory>`

与 **COPY** 指令相比具有有以下两个特点：

**如果源是一个URL，那该URL的内容将被下载并复制到容器中。**
**如果源是一个tar文件，则会自动解压为目录。**

```dockerfile
# 复制 "test" 到 `WORKDIR`/relativeDir/
ADD test relativeDir/
# 复制 "test" 到 /absoluteDir/
ADD test /absoluteDir/
# 正则匹配复制文件
ADD hom* /mydir/
ADD hom?.txt /mydir/
```

**COPY** 命令是为最基本的用法设计的，概念清晰，操作简单。而 **ADD** 命令基本上是 COPY 命令的超集(除了 multistage 场景)，可以实现一些方便、酷炫的拷贝操作。

> 延伸阅读：[Dockerfile 中的 COPY 与 ADD 命令](https://www.cnblogs.com/sparkdev/p/9573248.html)



### COPY、ADD 二者区别

<待续>



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

> 解释：设置运行容器的UID
> 用法：`USER <user>[:<group>]`
> 	     `USER <UID>[:<GID>]`

设置启动容器的用户，默认是root用户。指定 memcached 的运行用户daemon

```dockerfile
# 指定memcached的运行用户  
ENTRYPOINT ["memcached"]  
USER daemon  
或  
ENTRYPOINT ["memcached", "-u", "daemon"]  
```



## 镜像构建上下文（Context） 

如果注意，会看到 docker build 命令最后有一个 . 。 . 表示当前目录，而 Dockerfile
就在当前目录，因此不少初学者以为这个路径是在指定 Dockerfile 所在路径，这么理解其
实是不准确的。如果对应上面的命令格式，你可能会发现，这是在指定上下文路径。那么什
么是上下文呢？

首先我们要理解 docker build 的工作原理。Docker 在运行时分为 Docker 引擎（也就是服
务端守护进程）和客户端工具。**Docker 的引擎提供了一组 REST API，被称为 Docker**
**Remote API，而如 docker 命令这样的客户端工具，则是通过这组 API 与 Docker 引擎交**
**互，从而完成各种功能。因此，虽然表面上我们好像是在本机执行各种 docker 功能，但实**
**际上，一切都是使用的远程调用形式在服务端（Docker 引擎）完成。**也因为这种 C/S 设计，
让我们操作远程服务器的 Docker 引擎变得轻而易举。

**当我们进行镜像构建的时候，并非所有定制都会通过 RUN 指令完成，经常会需要将一些本地**
**文件复制进镜像，比如通过 COPY 指令、 ADD 指令等。而 docker build 命令构建镜像，其**
**实并非在本地构建，而是在服务端，也就是 Docker 引擎中构建的。**那么在这种客户端/服务
端的架构中，如何才能让服务端获得本地文件呢？

这就引入了上下文的概念。**当构建的时候，用户会指定构建镜像上下文的路径， docker**
**build 命令得知这个路径后，会将路径下的所有内容打包，然后上传给 Docker 引擎。这样**
**Docker 引擎收到这个上下文包后，展开就会获得构建镜像所需的一切文件**。

如果在 Dockerfile 中这么写：

```dockerfile
COPY ./package.json /app/
```

这并不是要复制执行 docker build 命令所在的目录下的 package.json ，也不是复制 Dockerfile 所在目录下的 package.json ，而是复制 上下文（context） 目录下的package.json 。

因此， COPY 这类指令中的源文件的路径都是相对路径。这也是初学者经常会问的为什么
COPY ../package.json /app 或者 COPY /opt/xxxx /app 无法工作的原因，因为这些路径已经
超出了上下文的范围，Docker 引擎无法获得这些位置的文件。如果真的需要那些文件，应该
将它们复制到上下文目录中去。

现在就可以理解刚才的命令 `docker build -t nginx:v3 .` 中的这个 `.` ，实际上是在指定上下
文的目录， docker build 命令会将该目录下的内容打包交给 Docker 引擎以帮助构建镜像。



理解构建上下文对于镜像构建是很重要的，避免犯一些不应该的错误。比如有些初学者在发
现 COPY /opt/xxxx /app 不工作后，于是干脆将 Dockerfile 放到了硬盘根目录去构建，结果
发现 docker build 执行后，在发送一个几十 GB 的东西，极为缓慢而且很容易构建失败。那
是因为这种做法是在让 docker build 打包整个硬盘，这显然是使用错误。

一般来说，应该会将 Dockerfile 置于一个空目录下，或者项目根目录下。如果该目录下没
有所需文件，那么应该把所需文件复制一份过来。如果目录下有些东西确实不希望构建时传
给 Docker 引擎，那么可以用 .gitignore 一样的语法写一个 .dockerignore ，该文件是用于
剔除不需要作为上下文传递给 Docker 引擎的。

那么为什么会有人误以为 . 是指定 Dockerfile 所在目录呢？这是因为在默认情况下，如果
不额外指定 Dockerfile 的话，会将上下文目录下的名为 Dockerfile 的文件作为
Dockerfile。

这只是默认行为，实际上 Dockerfile 的文件名并不要求必须为 Dockerfile ，而且并不要求
必须位于上下文目录中，比如可以用 -f ../Dockerfile.php 参数指定某个文件作为
Dockerfile 。

当然，一般大家习惯性的会使用默认的文件名 Dockerfile ，以及会将其置于镜像构建上下文
目录中。

## Dockerfile实例 





## 构建镜像





## 参考

[Dockerfile reference - Official Website](https://docs.docker.com/engine/reference/builder/#from)

[创建自己的docker及Dockerfile语法 - 掘金](https://juejin.im/entry/5a5f367cf265da3e3f4cb88c)

[dockerfile 介绍 - 博客园](https://www.cnblogs.com/boshen-hzb/p/6400272.html)

[Dockerfile入门介绍 - dockerinfo.net](http://www.dockerinfo.net/695.html)