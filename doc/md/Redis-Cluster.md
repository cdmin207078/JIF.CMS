# Redis-Cluster 集群初体验

> 参考连接

- http://redisdoc.com/topic/cluster-tutorial.html - 集群教程
- http://redisdoc.com/topic/cluster-spec.html - Redis 集群规范
- https://www.cnblogs.com/PatrickLiu/p/8473135.html - Redis进阶实践之十二 Redis的Cluster集群动态扩容

## 集群简介

Redis 集群是一个可以**在多个 Redis 节点之间进行数据共享**的设施(installation).

Redis 集群**不支持**那些需要同时处理多个键的 Redis 命令， 因为执行这些命令需要在多个 Redis 节点之间移动数据， 并且在高负载的情况下， 这些命令将降低 Redis 集群的性能， 并导致不可预测的行为。

Redis 集群**通过分区(partition)来提供一定程度的可用性(availability)**: 即使集群中有一部分节点失效或者无法进行通讯， 集群也可以继续处理命令请求。

Redis 集群提供了以下两个好处：

- 将数据自动切分（split）到多个节点的能力。
- 当集群中的一部分节点失效或者无法进行通讯时， 仍然可以继续处理命令请求的能力。


## Redis 集群数据共享

Redis 集群使用数据分片(sharding)而非一致性哈希(consistency hashing)来实现: 一个 Redis 集群包含 **`16384`** 个哈希槽(hash slot), 数据库中的每个键都属于这 16384 个哈希槽的其中一个, 集群使用公式 *`CRC16(key) % 16384`* 来计算键 `key` 属于哪个槽, 其中 `CRC16(key)` 语句用于计算键 `key` 的 [CRC16 校验和](http://zh.wikipedia.org/wiki/%E5%BE%AA%E7%92%B0%E5%86%97%E9%A4%98%E6%A0%A1%E9%A9%97).

集群中的每个节点负责处理一部分哈希槽. 举个例子, 一个集群可以有三个哈希槽, 其中：

- 节点 A 负责处理 `0` 号至 `5500` 号哈希槽.
- 节点 B 负责处理 `5501` 号至 `11000` 号哈希槽.
- 节点 C 负责处理 `11001` 号至 `16384` 号哈希槽.

这种将哈希槽分布到不同节点的做法使得用户可以很容易地向集群中添加或者删除节点. 比如说:

- 如果用户将新节点 D 添加到集群中， 那么集群只需要将节点 A 、B 、 C 中的某些槽移动到节点 D 就可以了。
- 与此类似， 如果用户要从集群中移除节点 A ， 那么集群只需要将节点 A 中的所有哈希槽移动到节点 B 和节点 C ， 然后再移除空白（不包含任何哈希槽）的节点 A 就可以了。

因为将一个哈希槽从一个节点移动到另一个节点不会造成节点阻塞， 所以无论是添加新节点还是移除已存在节点， 又或者改变某个节点包含的哈希槽数量， 都不会造成集群下线。

## Redis 集群中的主从复制

为了使得集群在一部分节点下线或者无法与集群的大多数(majority)节点进行通讯的情况下, 仍然可以正常运作, Redis 集群对节点使用了主从复制功能: 集群中的每个节点都有 1 个至 N 个复制品(replica), 其中一个复制品为主节点(master), 而其余的 N-1 个复制品为从节点(slave).

在之前列举的节点 A 、B 、C 的例子中, 如果节点 B 下线了, 那么集群将无法正常运行, 因为集群找不到节点来处理 5501 号至 11000 号的哈希槽.

另一方面, 假如在创建集群的时候(或者至少在节点 B 下线之前), 我们为主节点 B 添加了从节点 B1, 那么当主节点 B 下线的时候, 集群就会将 B1 设置为新的主节点, 并让它代替下线的主节点 B, 继续处理 5501 号至 11000 号的哈希槽， 这样集群就不会因为主节点 B 的下线而无法正常运作了.

不过如果节点 B 和 B1 都下线的话, Redis 集群还是会停止运作.

## Redis 集群的一致性保证(guarantee)

Redis 集群不保证数据的强一致性(strong consistency): 在特定条件下, Redis 集群可能会丢失已经被执行过的写命令.

使用异步复制(asynchronous replication)是 Redis 集群可能会丢失写命令的其中一个原因. 考虑以下这个写命令的例子:

- 客户端向主节点 B 发送一条写命令.
- 主节点 B 执行写命令，并向客户端返回命令回复.
- 主节点 B 将刚刚执行的写命令复制给它的从节点 B1 、 B2 和 B3.

如你所见， 主节点对命令的复制工作发生在返回命令回复之后， 因为如果每次处理命令请求都需要等待复制操作完成的话, 那么主节点处理命令请求的速度将极大地降低 —— 我们必须在性能和一致性之间做出权衡.

> 如果真的有必要的话， Redis 集群可能会在将来提供同步地（synchronou）执行写命令的方法

Redis 集群另外一种可能会丢失命令的情况是, 集群出现网络分裂([network partition](https://en.wikipedia.org/wiki/Network_partition)), 并且一个客户端与至少包括一个主节点在内的少数(minority)实例被孤立.

举个例子, 假设集群包含 A 、 B 、 C 、 A1 、 B1 、 C1 六个节点, 其中 A 、B 、C 为主节点, 而 A1 、B1 、C1 分别为三个主节点的从节点, 另外还有一个客户端 Z1.

假设集群中发生网络分裂, 那么集群可能会分裂为两方, 大多数(majority)的一方包含节点 A 、C 、A1 、B1 和 C1, 而少数(minority)的一方则包含节点 B 和客户端 Z1.

在网络分裂期间, 主节点 B 仍然会接受 Z1 发送的写命令:

- 如果网络分裂出现的时间很短， 那么集群会继续正常运行
- 但是, 如果网络分裂出现的时间足够长, 使得大多数一方将从节点 B1 设置为新的主节点, 并使用 B1 来代替原来的主节点 B, 那么 Z1 发送给主节点 B 的写命令将丢失.

注意, 在网络分裂出现期间, 客户端 Z1 可以向主节点 B 发送写命令的最大时间是有限制的, 这一时间限制称为**节点超时时间**(node timeout), 是 Redis 集群的一个重要的配置选项:

- 对于大多数一方来说, 如果一个主节点未能在节点超时时间所设定的时限内重新联系上集群, 那么集群会将这个主节点视为下线, 并使用从节点来代替这个主节点继续工作.
- 对于少数一方, 如果一个主节点未能在节点超时时间所设定的时限内重新联系上集群, 那么它将停止处理写命令, 并向客户端报告错误.


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

要让集群正常运作至少需要三个主节点, 不过在刚开始试用集群功能时, 强烈建议使用六个节点: 其中三个为主节点, 而其余三个则是各个主节点的从节点.

我们使用 10081 ~ 10086 六个文件夹, 来模拟六个redis 集群实例, 端口分别为 10081 ~ 10086.
六个文件夹, 仅仅端口配置不同.

```Properties
ip:127.0.0.1 port:10081  
ip:127.0.0.1 port:10082 
ip:127.0.0.1 port:10083
ip:127.0.0.1 port:10084
ip:127.0.0.1 port:10085
ip:127.0.0.1 port:10086
```

### 启动 redis 服务

在外层目录, 执行批处理程序, 启动所有redis服务, 代码如下:

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

实例启动之后,打印的日志显示. 因为 nodes.conf 文件不存在, 所以每个节点都为它自身指定了一个新的 ID:

```Properties
[82462] 26 Nov 11:56:55.329 * No cluster configuration found, I'm 97a3a64667477371c4479320d683e4c8db5858b1
```
实例会一直使用同一个 ID ， 从而在集群中保持一个独一无二（unique）的名字.

每个节点都使用 ID 而不是 IP 或者端口号来记录其他节点, 因为 IP 地址和端口号都可能会改变, 而这个独一无二的标识符(identifier)则会在节点的整个生命周期中一直保持不变.

我们将这个标识符称为节点 ID.

> 集群重启时, nodes.conf 存在, 第一行则会显示该节点的 ID 信息.

```Properties
[14240] 24 Feb 23:07:56.586 * Node configuration loaded, I'm efa0477d467e50681d225caf087491b75449f32e
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


运行命令之后, 输出如下:

```Batch
>>> Creating cluster
>>> Performing hash slots allocation on 6 nodes...
Using 3 masters:
127.0.0.1:10081
127.0.0.1:10082
127.0.0.1:10083
Adding replica 127.0.0.1:10085 to 127.0.0.1:10081
Adding replica 127.0.0.1:10086 to 127.0.0.1:10082
Adding replica 127.0.0.1:10084 to 127.0.0.1:10083
>>> Trying to optimize slaves allocation for anti-affinity
[WARNING] Some slaves are in the same host as their master
M: da23a0bdb3268f876fa48e362037833e28bd351e 127.0.0.1:10081
   slots:0-5460 (5461 slots) master
M: 36cfcb38dee01fb1a4e018fde4079f473547c085 127.0.0.1:10082
   slots:5461-10922 (5462 slots) master
M: a2477af9d26aef97fd2e1a44e10a6efd557042bc 127.0.0.1:10083
   slots:10923-16383 (5461 slots) master
S: 2d3c572966d1e07ec9067979d745c03f0a0006c7 127.0.0.1:10084
   replicates a2477af9d26aef97fd2e1a44e10a6efd557042bc
S: 3ac0025bd929d50dbb8fbeceec09b5b6f6db6526 127.0.0.1:10085
   replicates da23a0bdb3268f876fa48e362037833e28bd351e
S: 9c274dd4defaddd6e48d3ef7e41ebae0612d1a30 127.0.0.1:10086
   replicates 36cfcb38dee01fb1a4e018fde4079f473547c085
```

上面 倒数六行, 显示出六个 redis 实例的主从关系. 
此时, 命令行暂停处理, 等待用户收入 "yes", 表示知晓各个服务实例之间的角色关系. 输入 `yes` 之后, 继续运行

```Batch
Can I set the above configuration? (type 'yes' to accept): yes
>>> Nodes configuration updated
>>> Assign a different config epoch to each node
>>> Sending CLUSTER MEET messages to join the cluster
Waiting for the cluster to join..
>>> Performing Cluster Check (using node 127.0.0.1:10081)
M: da23a0bdb3268f876fa48e362037833e28bd351e 127.0.0.1:10081
   slots:0-5460 (5461 slots) master
   1 additional replica(s)
S: 3ac0025bd929d50dbb8fbeceec09b5b6f6db6526 127.0.0.1:10085
   slots: (0 slots) slave
   replicates da23a0bdb3268f876fa48e362037833e28bd351e
S: 9c274dd4defaddd6e48d3ef7e41ebae0612d1a30 127.0.0.1:10086
   slots: (0 slots) slave
   replicates 36cfcb38dee01fb1a4e018fde4079f473547c085
M: a2477af9d26aef97fd2e1a44e10a6efd557042bc 127.0.0.1:10083
   slots:10923-16383 (5461 slots) master
   1 additional replica(s)
S: 2d3c572966d1e07ec9067979d745c03f0a0006c7 127.0.0.1:10084
   slots: (0 slots) slave
   replicates a2477af9d26aef97fd2e1a44e10a6efd557042bc
M: 36cfcb38dee01fb1a4e018fde4079f473547c085 127.0.0.1:10082
   slots:5461-10922 (5462 slots) master
   1 additional replica(s)
[OK] All nodes agree about slots configuration.
>>> Check for open slots...
>>> Check slots coverage...
[OK] All 16384 slots covered.
``` 

以上, 同样显示了, 六台服务实例的各自角色关系, 其中最后三行输出, 表示集群运行正常

```Batch
>>> Check for open slots...
>>> Check slots coverage...
[OK] All 16384 slots covered.
```

### 客户端连接测试

使用 `redis-cli` 为例来进行演示

```Batch

# 连接集群需要加 -c 参数
.\redis-cli.exe -c -p 10081

127.0.0.1:10081> get name
-> Redirected to slot [5798] located at 127.0.0.1:10082
(nil)
127.0.0.1:10082> get 99s
-> Redirected to slot [10957] located at 127.0.0.1:10083
(nil)
```

`redis-cli` 对集群的支持是非常基本的, 所以它总是依靠 Redis 集群节点来将它转向(redirect)至正确的节点.

> 一个真正的（serious）集群客户端应该做得比这更好： 它应该用缓存记录起哈希槽与节点地址之间的映射（map）， 从而直接将命令发送到正确的节点上面。
这种映射只会在集群的配置出现某些修改时变化， 比如说， 在一次故障转移（failover）之后， 或者系统管理员通过添加节点或移除节点来修改了集群的布局（layout）之后， 诸如此类。


## Cluster 动态扩容

> 参考

https://www.cnblogs.com/PatrickLiu/p/8473135.html - Redis进阶实践之十二 Redis的Cluster集群动态扩容

## 持久化

分槽持久化, aof & rdb 只只负责本节点数据 & 指令持久化. 当集群关闭之后, 重新启动时, 各个节点加载自己持久化数据文件, 填充数据.

## 其它注意事项

1. 集群节点个数选择**奇数**, 方便选举