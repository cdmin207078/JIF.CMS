# redis主从复制 & sentinel监控

> 参考: 

- http://redisdoc.com/topic/replication.html - 复制(Replication)
- http://redisdoc.com/topic/sentinel.html - Sentinel
- http://blog.csdn.net/liuchuanhong1/article/details/53206028 - redis sentinel部署(Windows下实现)
- http://www.cnblogs.com/tdws/p/7710545.html - Redis4.0 Cluster — Centos7


## 主从复制, 读写分离

我们采用一主(master)二从(slave)三sentinel的架构模式来做演示

```Properties
master ip:127.0.0.1 port:10080  
slave1 ip:127.0.0.1 port:10081 
slave2 ip:127.0.0.1 port:10082 
```


## 主库关闭, 重设主库


## Sentinel - 哨兵, 自动监控

