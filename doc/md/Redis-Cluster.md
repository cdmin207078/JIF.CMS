# Redis-Cluster 集群初体验

## 环境准备

### 配置集群

Redis 集群由多个运行在集群模式（cluster mode）下的 Redis 实例组成， 实例的集群模式需要通过配置来开启， 开启集群模式的实例将可以使用集群特有的功能和命令。

以下是一个包含了最少选项的集群配置文件示例:

```Properties
# 开启或关闭集群功能
cluster-enabled yes

# cluster-conf-file 选项则设定了保存节点配置文件的路径, 默认值为 nodes.conf
# 节点配置文件无须人为修改, 它由 Redis 集群在启动时创建, 并在有需要时自动进行更新
cluster-config-file nodes.conf

# 节点超时时间
cluster-node-timeout 5000
```

### 启动 redis 服务

```Batch

@echo off

cd .\10081\
start start-server.bat
cd ..

cd .\10082\
start start-server.bat
cd ..

cd .\10083\
start start-server.bat
cd ..

cd .\10084\
start start-server.bat
cd ..

cd .\10085\
start start-server.bat
cd ..

cd .\10086\
start start-server.bat
cd ..

@pause
```

### 创建集群

现在我们已经有了六个正在运行中的 Redis 实例， 接下来我们需要使用这些实例来创建集群， 并为每个节点编写配置文件。

通过使用 Redis 集群命令行工具 redis-trib ， 编写节点配置文件的工作可以非常容易地完成： redis-trib 位于 Redis 源码的 src 文件夹中， 它是一个 Ruby 程序， 这个程序通过向实例发送特殊命令来完成创建新集群， 检查集群， 或者对集群进行重新分片（reshared）等工作。

我们需要执行以下命令来创建集群：

```Batch
.\redis-trib.rb create --replicas 1 127.0.0.1:10081 127.0.0.1:10082 127.0.0.1:10083 127.0.0.1:10084 127.0.0.1:10085 127.0.0.1:10086
```

命令的意义如下：

- 给定 redis-trib.rb 程序的命令是 create ， 这表示我们希望创建一个新的集群。
- 选项 --replicas 1 表示我们希望为集群中的每个主节点创建一个从节点。
- 之后跟着的其他参数则是实例的地址列表， 我们希望程序使用这些地址所指示的实例来创建新集群。

简单来说， 以上命令的意思就是让 redis-trib 程序创建一个包含三个主节点和三个从节点的集群。

## 测试

> 客户端测试使用

## 实例

> .net 访问 redis 集群