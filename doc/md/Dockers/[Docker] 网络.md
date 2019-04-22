# Docker 网络

[TOC]

Docker 允许通过外部访问容器或容器互联的方式来提供网络服务



## 外部访问容器

容器中可以运行一些网络应用，要让外部也可以访问这些应用，可以通过 **-P**`(大写)` 或 **-p**`(小写)` 参数来指定端口映射。

- 当使用 **-P**`(大写)` 标记时，Docker 会随机映射一个端口到内部容器开放的网络端口。

  ```shell
  # -P 随机映射一个端口到容器暴露的端口
  [root@vultr docker-nginx]# docker run -d -P training/webapp python app.py
  
  # 查看容器详情
  [root@vultr docker-nginx]# docker container ls -l
  ```

  ![1555945323911]([Docker] 网络.assets/1555945323911.png)

  

- 当使用 **-p**`(小写)` 则可以指定要映射的端口，并且，在一个指定端口上只可以绑定一个容器。支持的格式有 

  1. **ip:hostPort:containerPort** - 映射到指定地址的指定端口

     ```shell
     [root@vultr docker-nginx]# docker run -d -p 127.0.0.1:5001:5000 training/webapp python app.py
     94ea50875bd62f1a0c0e73e2c6e4f75ef263918b0b13402c32d3a4975041028c
     ```

     ![1555946228254]([Docker] 网络.assets/1555946228254.png)
     

  2. **ip::containerPort** - 映射到指定地址的任意端口

     ```shell
     [root@vultr docker-nginx]# docker run -d -p 127.0.0.1::5000 training/webapp python app.py
     713dd8cb11b2c35936632c49a80875f491a9a3080b037378fd04613038e889b2
     ```

     ![1555946085018]([Docker] 网络.assets/1555946085018.png)

     这种情况下，只能指定的ip 才能够访问，  适用于多网卡情况，比如：只允许内网访问

     

  3. **hostPort:containerPort** - 映射所有接口地址

     ```shell
     [root@vultr docker-nginx]# docker run -d -p 5000:5000 training/webapp python app.py
     2231c9fd9458910ce85f70058b8f28557004576fea899b70454fea5abca0df2e
     ```

     ![1555946270194]([Docker] 网络.assets/1555946270194.png)



**查看映射端口配置**

使用 **docker port** 来查看当前映射的端口配置，也可以查看到绑定的地址

```shell
[root@vultr docker-nginx]# docker port 713dd8cb11b2
5000/tcp -> 127.0.0.1:32769
```

> 容器有自己的内部网络和 ip 地址 (使用 docker inspect 可以获取所有的变量，Docker
> 还可以有一个可变的网络配置)

**-p 标记可以多次使用来绑定多个端口**

```shell
$ docker run -d \
-p 5000:5000 \
-p 3000:80 \
training/webapp \
python app.py
```



## 容器互联





## 参考

