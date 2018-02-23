# Redis-Cluster 集群初体验

## 环境准备

启动 redis 服务

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

创建集群

```Batch
.\redis-trib.rb create --replicas 1 127.0.0.1:10081 127.0.0.1:10082 127.0.0.1:10083 127.0.0.1:10084 127.0.0.1:10085 127.0.0.1:10086
```

## 测试

> 客户端测试使用

## 实例

> .net 访问 redis 集群