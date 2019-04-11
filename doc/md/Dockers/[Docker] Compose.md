# Docker Compose

[TOC]

## 简介

在日常工作中，经常会碰到需要多个容器相互配合来完成某项任务的情况。例如要实现一个 Web 项目，除了 Web 服务容器本身，往往还需要再加上后端的数据库服务容器，甚至还包括负载均衡容器等。

Compose 恰好满足了这样的需求。它允许用户通过一个单独的 docker-compose.yml 模板文件
（YAML 格式）来定义一组相关联的应用容器为一个项目（project）。

> Compose 项目是 Docker 官方的开源项目，负责实现对 Docker 容器集群的快速编排。
>
> Compose 定位是 「定义和运行多个 Docker 容器的应用（Defining and running multi-
> container Docker applications）」

Compose 中有两个重要的概念：

- **服务 ( service )**：一个应用的容器，实际上可以包括若干运行相同镜像的容器实例。
- **项目 ( project )**：由一组关联的应用容器组成的一个完整业务单元，在 docker-
  compose.yml 文件中定义。

Compose 的默认管理对象是项目，通过子命令对项目中的一组容器进行便捷地生命周期管
理。



## 安装 / 卸载

查看安装版本

```shell
[root@i6gkxhzdz /]# docker-compose --version
docker-compose version 1.23.2, build 1110ad01
```

若默认没有安装 docker-compose, 则需要手动安装。

Linux 系统请使用以下介绍的方法安装

### 二进制包安装

在 Linux 上的也安装十分简单，从 官方 GitHub Release - https://github.com/docker/compose/releases 处直接下载编译好的二进制文件即可。

```shell
# 下载最新版程序
[root@vultr ~]# curl -L https://github.com/docker/compose/releases/download/1.24.0/docker-compose-`uname -s`-`uname -m` -o /usr/local/bin/docker-compose
# 设置可执行权限
[root@vultr ~]# chmod +x /usr/local/bin/docker-compose
# 检查安装版本
[root@vultr ~]# docker-compose --version
docker-compose version 1.24.0, build 0aa59064
```



## 使用 wordpress + mysql

Compose 可以很便捷的让 Wordpress 运行在一个独立的环境中。

1. 假设新建一个名为 wordpress 的文件夹，然后进入这个文件夹。
2. 创建 docker-compose.yml 文件

docker-compose.yml 文件将开启一个 wordpress 服务和一个独立的 MySQL 实例：

```yaml
version: "3"
services:
  db:
    image: mysql:5.7
    volumes:
      - db_data:/var/lib/mysql
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: somewordpress
      MYSQL_DATABASE: wordpress
      MYSQL_USER: wordpress
      MYSQL_PASSWORD: wordpress
  wordpress:
    depends_on:
      - db
    image: wordpress:latest
    ports:
      - "8000:80"
    restart: always
    environment:
      WORDPRESS_DB_HOST: db:3306
      WORDPRESS_DB_USER: wordpress
      WORDPRESS_DB_PASSWORD: wordpress
volumes:
  db_data:
```

运行 **docker-compose up -d** Compose 就会拉取镜像再创建我们所需要的镜像，然后启动
wordpress 和数据库容器。 接着浏览器访问 127.0.0.1:8000 端口就能看到 WordPress 安装
界面了。





## 参考

