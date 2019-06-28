# <u>Nginx</u> 初体验

[TOC]



## 安装



### 编译安装



### yum安装





## 常用命令

```sh
# 启动
./nginx 

# 检查 nginx.conf配置文件
./nginx -t

# 重启
./nginx -s reload

# 停止
./nginx -s stop
```





## nginx.conf



### 文件结构

```nginx
...              #全局块
events {         #events块
   ...
}
http      #http块
{
    ...   #http全局块
    server        #server块
    { 
        ...       #server全局块
        location [PATTERN]   #location块
        {
            ...
        }
        location [PATTERN] 
        {
            ...
        }
    }
    server
    {
      ...
    }
    ...     #http全局块
}
```

1. **全局块** -  配置影响nginx全局的指令。一般有运行nginx服务器的用户组，nginx进程pid存放路径，日志存放路径，配置文件引入，允许生成worker process数等
2. **events块** - 配置影响nginx服务器或与用户的网络连接。有每个进程的最大连接数，选取哪种事件驱动模型处理连接请求，是否允许同时接受多个网路连接，开启多个网络连接序列化等
3. **http块** - 可以嵌套多个server，配置代理，缓存，日志定义等绝大多数功能和第三方模块的配置。如文件引入，mime-type定义，日志自定义，是否使用sendfile传输文件，连接超时时间，单连接请求数等
4. **server块** - 配置虚拟主机的相关参数，一个http中可以有多个server
5. **location块** - 配置请求的路由，以及各种页面的处理情况



### 语法规范

1. 每个指令必须有分号结束





### nginx.conf 实例

```nginx
user  nginx;
worker_processes  1;

error_log  /var/log/nginx/error.log warn;
pid        /var/run/nginx.pid;


events {
    worker_connections  1024;
}


http {
    include       /etc/nginx/mime.types;
    default_type  application/octet-stream;

    log_format main escape=json '{ "@timestamp": "$time_iso8601", '
                         '"time": "$time_iso8601", '
                         '"requestIp": "$remote_addr", '
                         '"remote_user": "$remote_user", '
                         '"body_bytes_sent": "$body_bytes_sent", '
                         '"request_time": "$request_time", '
                         '"status": "$status", '
                         '"host": "$host", '
                         '"request": "$request", '
                         '"request_method": "$request_method", '
                         '"uri": "$uri", '
                         '"http_referrer": "$http_referer", '
                         '"body_bytes_sent":"$body_bytes_sent", '
                         '"http_x_forwarded_for": "$http_x_forwarded_for", '
                         '"service": "nginx", '
                         '"http_user_agent": "$http_user_agent", '
                         '"pramdata": "$request_body", '
                         '"requestHead": "$http_HEADER " '
                    '}';

    access_log  /var/log/nginx/access.log  main;

    sendfile        on;
    # tcp_nopush     on;
    keepalive_timeout  65;
    # gzip  on;

    # include /etc/nginx/conf.d/*.conf;
    
    server {
        listen 80;
        server_name localhost;
        
        # nginx welcome page
        location / {
            index index.html;
            root /data/www/nginx;
        }

        # portainer
        location /docker/ {
            rewrite ^/portainer(/.*)$ /$1 break;
            # proxy_pass http://127.0.0.1:9000/;
            # 使用docker网桥中网络别名
            proxy_pass http://portainer:9000/;
            proxy_http_version 1.1;
            proxy_set_header Connection "";
        }

        location /docker/api {
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection 'upgrade';
            # proxy_pass http://127.0.0.1:9000/api;
            # 使用docker网桥中网络别名
            proxy_pass http://portainer:9000/api;
            proxy_http_version 1.1;
        }

        # robot
        location /robot.manager {
            proxy_redirect off;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_pass http://172.19.159.1:8001;
        }
        location /robot.web {
            proxy_redirect off;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection "upgrade";
            proxy_read_timeout 3600s;
            proxy_pass http://172.19.159.1:8002;
        }
        location /robot-customer {
            proxy_redirect off;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_pass http://172.19.159.1:8006;
        }
        location /robot.api {
            proxy_redirect off;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_pass http://172.19.159.1:8004;
        }
        location ^~ /robot.crm.api {
            proxy_redirect off;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_pass http://172.19.159.1:8085;
        }
        error_page 500 502 503 504  /50x.html;
    }

    # nexus
    server {
        client_max_body_size 200M;
        listen 80;
        server_name nexus.zihuitong.com.cn;
        location / {
            proxy_redirect off;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_pass http://172.19.159.1:8801;
        }
    }
}

```



## 参考

[nginx服务器安装及配置文件详解](https://seanlook.com/2015/05/17/nginx-install-and-config/)

[Nginx配置详解](https://www.cnblogs.com/knowledgesea/p/5175711.html)