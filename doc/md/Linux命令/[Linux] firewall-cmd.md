# [Linux] firewall-cmd 使用

**查看防火墙状态**

```bash
firewall-cmd --state            ## 结果显示为running或not running
```

**关闭防火墙firewall**

```bash
systemctl stop firewalld.service
systemctl disable firewalld.service
```

**关闭防火墙firewall后开启**

```
systemctl start firewalld.service
```

**开启端口**

```
# zone -- 作用域
# add-port=80/tcp -- 添加端口，格式为：端口/通讯协议
# permanent -- 永久生效，没有此参数重启后失效

firewall-cmd --zone=public --add-port=3306/tcp --permanent
```

开启3306端口后，workbench或naivcat 就能连接到MySQL数据库了

**重启防火墙**

```bash
firewall-cmd --reload
```

**常用命令介绍**

```bash
firewall-cmd --state                           ##查看防火墙状态，是否是running
firewall-cmd --reload                          ##重新载入配置，比如添加规则之后，需要执行此命令
firewall-cmd --get-zones                       ##列出支持的zone
firewall-cmd --get-services                    ##列出支持的服务，在列表中的服务是放行的
firewall-cmd --query-service ftp               ##查看ftp服务是否支持，返回yes或者no
firewall-cmd --add-service=ftp                 ##临时开放ftp服务
firewall-cmd --add-service=ftp --permanent     ##永久开放ftp服务
firewall-cmd --remove-service=ftp --permanent  ##永久移除ftp服务
firewall-cmd --add-port=80/tcp --permanent     ##永久添加80端口 
iptables -L -n                                 ##查看规则，这个命令是和iptables的相同的
man firewall-cmd                               ##查看帮助
systemctl status firewalld.service             ##查看防火墙状态
systemctl [start|stop|restart] firewalld.service  ##启动|关闭|重新启动  防火墙
```

**查询端口号80 是否开启**

```bash
firewall-cmd --query-port=80/tcp
```



更多命令，使用 **firewall-cmd --help** 查看帮助文件


## 参考
[Centos7 firewall开放3306端口](https://www.cnblogs.com/huizhipeng/p/10127333.html)