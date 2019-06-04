# [Docker] 实训 Nginx + Wordpress + portainer



## 创建网桥

```shell
[root@vultr ~]# docker network create docker-nginx-bridge
9e4557030eee4c628473e929d17b63c5941ac8d04e2eab96c1b30ad826dd3207
```



## Nginx

**start-nginx.sh**

```shell
#! /bin/sh
echo "begin"
# 停止容器
docker stop $(docker ps -a |  grep "docker-nginx"  | awk '{print $1}')
echo "stop docker docker-nginx"
# 删除容器
docker rm $(docker ps -a |  grep "docker-nginx"  | awk '{print $1}')
echo "rm docker docker-nginx"
# 启动容器
docker run \
-p 80:80 \
--name docker-nginx \
--network docker-nginx-bridge \
--network-alias docker-nginx \
-v $PWD/www:/data/www \
-v $PWD/conf/nginx.conf:/etc/nginx/nginx.conf \
-v $PWD/conf/conf.d:/etc/nginx/conf.d \
-v $PWD/logs:/wwwlogs \
-d nginx
echo "start docker docker-nginx"
```



**www.chen-ning.com.conf** 配置文件

```nginx
server {
    listen 80;
    server_name www.chen-ning.com;
    location / {
        index index.html;
        root /data/www/www.chen-ning.com;
    }
}
server {
    listen 80;
    server_name docker.chen-ning.com;
    location / {
        proxy_redirect off;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_pass http://docker-portainer/;
    }
    access_log logs/portainer_access.log;
}

server {
    listen 80;
    server_name wordpress.chen-ning.com;
    location / {
        proxy_redirect off;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_pass http://wordpress-web/;
    }
    access_log logs/wordpress_access.log;
}

```



## Portainer

**start-portainer.sh**

```sh
#! /bin/sh
echo "begin"
# 停止容器
docker stop $(docker ps -a |  grep "docker-portainer"  | awk '{print $1}')
echo "stop docker docker-portainer"
# 删除容器
docker rm $(docker ps -a |  grep "docker-portainer"  | awk '{print $1}')
echo "rm docker docker-portainer"
# 启动容器
docker run \
-p 9000:9000 \
--name docker-portainer \
--network docker-nginx-bridge \
--network-alias docker-portainer \
--restart always \
-v /var/run/docker.sock:/var/run/docker.sock \
-v portainer_data:/data \
-d portainer/portainer
echo "start docker docker-portainer"
```

## Wordpress

**docker-compose.yml**

```yaml
version: "3"
services:
  wordpress-db:
    image: mysql:5.7
    volumes:
      - db_data:/var/lib/mysql
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: somewordpress
      MYSQL_DATABASE: wordpress
      MYSQL_USER: wordpress
      MYSQL_PASSWORD: wordpress
  wordpress-web:
    depends_on:
      - wordpress-db
    image: wordpress:latest
    ports:
      - "8000:80"
    restart: always
    environment:
      WORDPRESS_DB_HOST: wordpress-db:3306
      WORDPRESS_DB_USER: wordpress
      WORDPRESS_DB_PASSWORD: wordpress
volumes:
  db_data:
networks:
  default:
    external:
      name: docker-nginx-bridge
```

**启动 wordpress + mysql**

```shell
[root@vultr ~]# docker-compose up -d
```



