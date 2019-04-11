# docker nginx



下载镜像

```shell
[root@vultr docker-compose-wordpress]# docker pull nginx
```

启动脚本 **start-nginx.sh**

```shell
docker run \
-p 80:80 \
--name nginx \
-v $PWD/www:/www \
-v $PWD/conf/nginx.conf:/etc/nginx/nginx.conf \
-v $PWD/logs:/wwwlogs \
-d nginx
```

可能会遇到的错误：

```shell
[root@vultr docker-nginx]# ./start-nginx.sh 
89b9e70c0cf8215824eed6f9b8823ea16ba82c8c5ec50190aa17723115240aa4
docker: Error response from daemon: OCI runtime create failed: container_linux.go:344: starting container process caused "process_linux.go:424: container init caused \"rootfs_linux.go:58: mounting \\\"/usr/local/data/docker-nginx/conf/nginx.conf\\\" to rootfs \\\"/var/lib/docker/overlay2/94d95499bfea3bcf40d88745ff6ab104ee7e2efba5d530ce2b6fe0536f4e0469/merged\\\" at \\\"/var/lib/docker/overlay2/94d95499bfea3bcf40d88745ff6ab104ee7e2efba5d530ce2b6fe0536f4e0469/merged/etc/nginx/nginx.conf\\\" caused \\\"not a directory\\\"\"": unknown: Are you trying to mount a directory onto a file (or vice-versa)? Check if the specified host path exists and is the expected type.
```

**解决方法**

> 先复制一个nginx.conf文件出来，再重新创建nginx容器

```shell
# 启动一个临时nginx容器
[root@vultr docker-nginx] docker run --name tmp-nginx-container -d nginx
# 创建配置文件所在目录
[root@vultr docker-nginx] mkdir conf
# 复制nginx.conf 到宿主机目录 (之后正式启动使用此配置文件)
[root@vultr docker-nginx] docker cp tmp-nginx-container:/etc/nginx/nginx.conf $PWD/conf/nginx.conf
# 删除临时nginx容器
[root@vultr docker-nginx] docker rm -f tmp-nginx-container
# 启动正式nginx 容器
[root@vultr docker-nginx]# ./start-nginx.sh 
d2aa2b15810e925447905e265e67717de997381cd25bcf4ddbde92f16e004e15
# .. 启动成功
```



## 参考

[nginx in docker - docker 官方文档](https://docs.docker.com/samples/library/nginx/)

[[Docker安装nginx以及负载均衡](https://www.cnblogs.com/xishaohui/p/8871994.html)](http://www.cnblogs.com/xishaohui/p/8871994.html)