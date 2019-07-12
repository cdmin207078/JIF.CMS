# Docker 初体验



[TOC]

## 安装 Docker

>Docker 划分为 CE 和 EE。CE 即社区版（免费，支持周期三个月），EE 即企业版，强
>调安全，付费使用。

**卸载旧版本**

旧版本的 Docker 称为 docker 或者 docker-engine ，使用以下命令卸载旧版本：

```shell
[root@vultr ~]# yum remove docker \
                docker-client \
                docker-client-latest \
                docker-common \
                docker-latest \
                docker-latest-logrotate \
                docker-logrotate \
                docker-selinux \
                docker-engine-selinux \
                docker-engine
```

**安装一些必要的系统工具**

```shell
[root@vultr ~]# yum install -y yum-utils \
device-mapper-persistent-data \
lvm2
```

 **添加软件源信息**

```shell
[root@vultr ~]# sudo yum-config-manager --add-repo http://mirrors.aliyun.com/docker-ce/linux/centos/docker-ce.repo
```

**更新 yum 缓存**

```shell
[root@vultr ~]# sudo yum makecache fast
```

**安装 Docker-ce**

```shell
[root@vultr ~]# sudo yum -y install docker-ce
```

**启动 Docker 后台服务**

```shell
[root@vultr ~]# systemctl start docker
```

![docker 启动成功](Docker初体验.assets/1554989463490.png)



## hello docker

<待续>



## 基本概念

Docker 包括三个基本概念

- 镜像（ Image ）
- 容器（ Container ）
- 仓库（ Repository ）

理解了这三个概念，就理解了 Docker 的整个生命周期。



### 镜像

我们都知道，操作系统分为内核和用户空间。对于 Linux 而言，内核启动后，会挂载 `root` 文件系统为其提供用户空间支持。而 Docker 镜像（Image），就相当于是一个 `root` 文件系统。比如官方镜像 `ubuntu:18.04` 就包含了完整的一套 Ubuntu 18.04 最小系统的 `root` 文件系统。

Docker 镜像是一个特殊的文件系统，除了提供容器运行时所需的程序、库、资源、配置等文件外，还包含了一些为运行时准备的一些配置参数（如匿名卷、环境变量、用户等）。镜像不包含任何动态数据，其内容在构建之后也不会被改变。

**分层存储**

我们都知道，操作系统分为内核和用户空间。对于 Linux 而言，内核启动后，会挂载 `root` 文件系统为其提供用户空间支持。而 Docker 镜像（Image），就相当于是一个 `root` 文件系统。比如官方镜像 `ubuntu:18.04` 就包含了完整的一套 Ubuntu 18.04 最小系统的 `root` 文件系统。

Docker 镜像是一个特殊的文件系统，除了提供容器运行时所需的程序、库、资源、配置等文件外，还包含了一些为运行时准备的一些配置参数（如匿名卷、环境变量、用户等）。镜像不包含任何动态数据，其内容在构建之后也不会被改变。



### 容器

镜像（`Image`）和容器（`Container`）的关系，就像是面向对象程序设计中的 `类` 和 `实例` 一样，镜像是静态的定义，容器是镜像运行时的实体。容器可以被创建、启动、停止、删除、暂停等。

容器的实质是进程，但与直接在宿主执行的进程不同，容器进程运行于属于自己的独立的 [命名空间](https://en.wikipedia.org/wiki/Linux_namespaces)。因此容器可以拥有自己的 `root` 文件系统、自己的网络配置、自己的进程空间，甚至自己的用户 ID 空间。容器内的进程是运行在一个隔离的环境里，使用起来，就好像是在一个独立于宿主的系统下操作一样。这种特性使得容器封装的应用比直接在宿主运行更加安全。也因为这种隔离的特性，很多人初学 Docker 时常常会混淆容器和虚拟机。

前面讲过镜像使用的是分层存储，容器也是如此。每一个容器运行时，是以镜像为基础层，在其上创建一个当前容器的存储层，我们可以称这个为容器运行时读写而准备的存储层为 **容器存储层**。

容器存储层的生存周期和容器一样，容器消亡时，容器存储层也随之消亡。因此，任何保存于容器存储层的信息都会随容器删除而丢失。

按照 Docker 最佳实践的要求，容器不应该向其存储层内写入任何数据，容器存储层要保持无状态化。所有的文件写入操作，都应该使用 [数据卷（Volume）](https://docker_practice.gitee.io/data_management/volume.html)、或者绑定宿主目录，在这些位置的读写会跳过容器存储层，直接对宿主（或网络存储）发生读写，其性能和稳定性更高。

数据卷的生存周期独立于容器，容器消亡，数据卷不会消亡。因此，使用数据卷后，容器删除或者重新运行之后，数据却不会丢失。



### 仓库

镜像构建完成后，可以很容易的在当前宿主机上运行，但是，如果需要在其它服务器上使用这个镜像，我们就需要一个集中的存储、分发镜像的服务，[Docker Registry](https://docker_practice.gitee.io/repository/registry.html) 就是这样的服务。

一个 **Docker Registry** 中可以包含多个 **仓库**（`Repository`）；每个仓库可以包含多个 **标签**（`Tag`）；每个标签对应一个镜像。

通常，一个仓库会包含同一个软件不同版本的镜像，而标签就常用于对应该软件的各个版本。我们可以通过 `<仓库名>:<标签>` 的格式来指定具体是这个软件哪个版本的镜像。如果不给出标签，将以 `latest` 作为默认标签。

以 [Ubuntu 镜像](https://hub.docker.com/_/ubuntu) 为例，`ubuntu` 是仓库的名字，其内包含有不同的版本标签，如，`16.04`, `18.04`。我们可以通过 `ubuntu:14.04`，或者 `ubuntu:18.04` 来具体指定所需哪个版本的镜像。如果忽略了标签，比如 `ubuntu`，那将视为 `ubuntu:latest`。

仓库名经常以 *两段式路径* 形式出现，比如 `jwilder/nginx-proxy`，前者往往意味着 Docker Registry 多用户环境下的用户名，后者则往往是对应的软件名。但这并非绝对，取决于所使用的具体 Docker Registry 的软件或服务。



#### 使用镜像加速



**阿里云镜像加速器**

- 使用 阿里云或淘宝账号登录 `http://dev.aliyun.com` 网站，获取自己的镜像加速地址

- 设置docker镜像配置
- 重启服务

```shell
sudo mkdir -p /etc/docker
sudo tee /etc/docker/daemon.json <<-'EOF'
{
  "registry-mirrors": ["https://xxxxxxx.mirror.aliyuncs.com"]
}
EOF
sudo systemctl daemon-reload
sudo systemctl restart docker
```

<待续>



### hello-docker







### 可视化管理工具 - portainer

`Portainer`是Docker的图形化管理工具，提供状态显示面板、应用模板快速部署、容器镜像网络数据卷的基本操作（包括上传下载镜像，创建容器等操作）、事件日志显示、容器控制台操作、Swarm集群和服务等集中管理和操作、登录用户管理和控制等功能。功能十分全面，基本能满足中小型单位对容器管理的全部需求。

官方文档：[Docker 从入门到实践 - 帮助文档](https://docker_practice.gitee.io/introduction/)

**下载、运行**

```shell
# 下载镜像
[root@localhost docker]# docker pull portainer/portainer
# 启动
[root@localhost docker]# docker run -d -p 9000:9000 --name portainer --restart always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer
```

**访问 `http://192.168.20.72:9000` 初始化 portainer**

![初始化 用户名/密码](Docker初体验.assets/1554126723979.png)

选 择Docker 管理环境，这里我们选择本地

![选择管理环境](Docker初体验.assets/1554126833517.png)



![本机docker](Docker初体验.assets/1554126930921.png)



![主要功能界面](Docker初体验.assets/1554127065605.png)



**Nginx 外网访问可能遇到， 添加了转发映射，但是还是 404， 是因为需要映射两个**

https://github.com/portainer/portainer/issues/754#issuecomment-471368760

```nginx
location /docker {
    rewrite ^/portainer(/.*)$ /$1 break;
    proxy_pass http://127.0.0.1:9000/;
    proxy_http_version 1.1;
    proxy_set_header Connection "";
}

location /docker/api {
    proxy_set_header Upgrade $http_upgrade;
    proxy_pass http://127.0.0.1:9000/api;
    proxy_set_header Connection 'upgrade';
    proxy_http_version 1.1;
}
```





## 常用一体化脚本

```sh
#! /bin/sh
echo "begin"
# 停止容器
docker stop $(docker ps -a |  grep "robot.crm.api"  | awk '{print $1}')
echo "stop docker robot.crm.api"
# 删除容器
docker rm $(docker ps -a |  grep "robot.crm.api"  | awk '{print $1}')
echo "rm docker robot.crm.api"
# 删除镜像
echo "rmi docker robot.crm.api"
# 构建镜像
docker build -t dahanis/robot.crm.api .
echo "bulid docker robot.crm.api" 

# 启动容器
docker run \
--name robot.crm.api \
-v $PWD/data/logs:/app/logs \
-p 8085:8080 \
-d dahanis/robot.crm.api

```



## 参考

[Docker 教程 - 菜鸟教程](https://www.runoob.com/docker/docker-tutorial.html)

[Docker 从入门到实践 - 帮助文档](https://docker_practice.gitee.io/introduction/)

