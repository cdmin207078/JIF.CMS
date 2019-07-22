

# Nginx 反向代理 ElasticSearch \ Kibana

[TOC]

## ElasticSearch 

**Nginx 配置**

```nginx
location /elasticsearch/ {
    proxy_redirect off;
    proxy_set_header Host $host;
    proxy_set_header X-Real-IP $remote_addr;
    proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
    proxy_pass http://127.0.0.1:9200;
    rewrite ^/elasticsearch/(.*)$ /$1 break;
}
```

重启 Nginx

## Kibana

**Kibana 配置**

Kibana 配置文件 `$kibana/config/kibana.yml` 中添加

```yaml
server.basePath: "/kibana"
```

**Nginx 配置**

```nginx
...
location /kibana/ {
    proxy_http_version 1.1;
    proxy_set_header Upgrade $http_upgrade;
    proxy_set_header Connection 'upgrade';
    proxy_set_header Host $host;
    proxy_cache_bypass $http_upgrade;

    proxy_pass http://127.0.0.1:5601;
    rewrite ^/kibana/(.*)$ /$1 break;
}
...
```

配置好后, 重启 Kibana 与 Nginx

**添加 Basic 认证**

> https://www.cnblogs.com/configure/p/7607302.html

## 参考

[elastichsearch反向代理设置 - 博客园](https://ox0spy.github.io/post/elasticsearch/elasticsearch-reverse-proxy/)

[Nginx子目录反向代理kibana并添加basic认证 - 博客园](https://www.cnblogs.com/Kevin-1967/p/9996396.html)