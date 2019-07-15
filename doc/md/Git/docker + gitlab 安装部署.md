# docker + gitlab 安装部署

**获取镜像**

```shell
[root@vultr gitlab]# docker pull gitlab/gitlab-ce
```

**启动**

```sh
docker run -d \
-p 9443:443 \
-p 9080:80 \
-p 9022:22 \
--name gitlab \
--restart always \
-v $PWD/config:/etc/gitlab \
-v $PWD/logs:/var/log/gitlab \
-v $PWD/data:/var/opt/gitlab \
gitlab/gitlab-ce
```



## 可能遇到的问题

### 项目URL访问地址为容器 hostname

>**参考：[docker下gitlab安装配置使用(完整版)](https://www.jianshu.com/p/080a962c35b6)**

按上面的方式，gitlab容器运行没问题，但在gitlab上创建项目的时候，生成项目的**URL访问地址是按容器的hostname来生成的，也就是容器的id**。作为gitlab服务器，我们需要一个固定的URL访问地址，于是需要配置 **gitlab.rb**

```ruby
# 配置http协议所使用的访问地址,不加端口号默认为80
external_url 'http://192.168.0.106:9080'

# 配置ssh协议所使用的访问地址和端口
gitlab_rails['gitlab_ssh_host'] = '192.168.0.106:9022'
# 此端口是run时 22端口 映射的 9022端口
gitlab_rails['gitlab_shell_ssh_port'] = 9022 
```



### 不适用gitlab 自带nginx , 使用外部独立 nginx

> [gitlab修改默认端口](https://cloud.tencent.com/developer/article/1139779)









## 参考

[使用 Docker 搭建 GitLab](https://zhuanlan.zhihu.com/p/63786567)

[GitLab Docker images - office doc](https://docs.gitlab.com/omnibus/docker/)

[docker下gitlab安装配置使用(完整版)](https://www.jianshu.com/p/080a962c35b6)