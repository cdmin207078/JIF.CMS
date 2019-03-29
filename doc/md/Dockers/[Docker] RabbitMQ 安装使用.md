# [Docker] RabbitMQ 安装使用



[TOC]

## 安装

**拉取镜像**

```shell
[root@vultr docker-lesson-dockfile]# docker pull rabbitmq:3.8-rc-management
```

> 注意：拉取带有管理界面的 `management` 版本，包含web管理界面
> 获取最新版镜像地址：https://hub.docker.com/_/rabbitmq



**start.sh 启动脚本**

```shell
docker run \
--name rabbitmq5672 \
-p 5672:5672 \
-p 15672:15672 \
-v $PWD/data:/var/lib/rabbitmq \
--hostname myRabbit \
-e RABBITMQ_DEFAULT_VHOST=my_vhost \
-e RABBITMQ_DEFAULT_USER=admin \
-e RABBITMQ_DEFAULT_PASS=admin \
-d rabbitmq:3.8-rc-management
```

说明：

**`-d`** 后台运行容器；
**`--name`** 指定容器名；
**`-p`** 指定服务运行的端口（`5672`：`应用访问端口`；`15672`：`控制台Web端口号`）；
**`-v`** 映射目录或文件；
**`--hostname`**  主机名（RabbitMQ的一个重要注意事项是它根据所谓的 “节点名称” 存储数据，默认为主机名）；
**`-e`** 指定环境变量；（`RABBITMQ_DEFAULT_VHOST`：默认虚拟机名；`RABBITMQ_DEFAULT_USER`：默认的用户名；`RABBITMQ_DEFAULT_PASS`：默认用户名的密码）







## 参考

[docker 安装rabbitMQ](https://www.cnblogs.com/yufeng218/p/9452621.html)