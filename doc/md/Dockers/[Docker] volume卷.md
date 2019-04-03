# Docker 中 volume 使用

在介绍VOLUME指令之前，我们来看下如下场景需求：

- 容器是基于镜像创建的，最后的容器文件系统包括镜像的只读层+可写层，容器中的进程操作的数据持久化都是保存在容器的可写层上。一旦容器删除后，这些数据就没了，除非我们人工备份下来（或者基于容器创建新的镜像）。能否可以让容器进程持久化的数据保存在主机上呢？这样即使容器删除了，数据还在。
- 当我们在开发一个web应用时，开发环境是在主机本地，但运行测试环境是放在docker容器上。
  这样的话，我在主机上修改文件（如html，js等）后，需要再同步到容器中。这显然比较麻烦。

- 多个容器运行一组相关联的服务，如果他们要共享一些数据怎么办？
  对于这些问题，我们当然能想到各种解决方案。而docker本身提供了一种机制，可以将主机上的某个目录与容器的某个目录（称为挂载点、或者叫卷）关联起来，容器上的挂载点下的内容就是主机的这个目录下的内容，这类似linux系统下mount的机制。 这样的话，我们修改主机上该目录的内容时，不需要同步容器，对容器来说是立即生效的。 挂载点可以让多个容器共享。



下面我们来介绍具体的机制。



## dockerfile创建挂载点



## docker run 命令 -v 参数



## 数据卷容器





## 参考

[尚硅谷 docker 容器数据卷 P18~P21 - bilibii 视频教程](https://www.bilibili.com/video/av27122140/?p=18)

[Dockerfile 指令 VOLUME 介绍 - 博客园](http://www.cnblogs.com/51kata/p/5266626.html)

[Dockerfile Volume指令与docker -v的区别 - 博客园](https://www.cnblogs.com/walterfong/p/10017976.html)

[Docker Volume入门介绍 - 掘金](http://www.dockerinfo.net/1857.html)

[Volume 使用 - 官网](https://docs.docker.com/storage/volumes/)

[深入理解Docker Volume（一）](http://dockone.io/article/128)