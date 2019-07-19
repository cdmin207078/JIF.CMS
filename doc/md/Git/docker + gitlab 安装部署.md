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

> 参考:
>
> https://www.jianshu.com/p/5d22b25afd5f
>
> https://blog.csdn.net/qq_34894188/article/details/80468889
> [https://www.daxiblog.com/%e4%ba%b2%e6%b5%8b%e6%9c%89%e6%95%88gitlab%e7%a6%81%e7%94%a8%e8%87%aa%e5%8a%a8nginx%e9%81%bf%e5%85%8d%e7%ab%af%e5%8f%a3%e5%86%b2%e7%aa%81%ef%bc%8c%e4%bd%bf%e7%94%a8%e5%a4%96%e9%83%a8nginx%e4%bb%a3/](https://www.daxiblog.com/亲测有效gitlab禁用自动nginx避免端口冲突，使用外部nginx代/)
> [gitlab修改默认端口](https://cloud.tencent.com/developer/article/1139779)





## 参考

[使用 Docker 搭建 GitLab](https://zhuanlan.zhihu.com/p/63786567)

[GitLab Docker images - office doc](https://docs.gitlab.com/omnibus/docker/)

[docker下gitlab安装配置使用(完整版)](https://www.jianshu.com/p/080a962c35b6)

[docker - Gitlab部署和基本使用](https://istone.dev/2019/07/20/gitlab-install/?tdsourcetag=s_pctim_aiomsg)

