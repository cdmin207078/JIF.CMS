

# ELK快速搭建安装

[TOC]



## 前言

### 什么是 ELK

**ELK**是 **Elasticsearch**、**Logstash**、**Kibana**的简称，这三者是核心套件，但并非全部。

- **Elasticsearch** 是实时全文搜索和分析引擎，提供搜集、分析、存储数据三大功能；是一套开放REST和JAVA API等结构提供高效搜索功能，可扩展的分布式系统。它构建于Apache Lucene搜索引擎库之上。
- **Logstash** 是一个用来搜集、分析、过滤日志的工具。它支持几乎任何类型的日志，包括系统日志、错误日志和自定义应用程序日志。它可以从许多来源接收日志，这些来源包括 syslog、消息传递（例如 RabbitMQ）和JMX，它能够以多种方式输出数据，包括电子邮件、websockets和Elasticsearch。
- **Kibana **是一个基于Web的图形界面，用于搜索、分析和可视化存储在 Elasticsearch指标中的日志数据。它利用Elasticsearch的REST接口来检索数据，不仅允许用户创建他们自己的数据的定制仪表板视图，还允许他们以特殊的方式查询和过滤数据



### 基本概念术语

> TODO



## 安装

### centos7 安装

> TODO



### docker 安装

#### elasticsearch

> 参考： https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html

拉取镜像, 启动容器

```sh
docker pull docker.elastic.co/elasticsearch/elasticsearch:7.2.0
```

启动容器脚本 run-elastic-search.sh

```sh

# 停止容器
docker stop $(docker ps -a |  grep "elasticsearch"  | awk '{print $1}')
echo "stop docker elasticsearch"
# 删除容器
docker rm $(docker ps -a |  grep "elasticsearch"  | awk '{print $1}')
echo "rm docker elasticsearch"
# 启动容器
docker run \
--name elasticsearch \
-p 9200:9200 \
-p 9300:9300 \
-e "discovery.type=single-node" \
-d docker.elastic.co/elasticsearch/elasticsearch:7.2.0
```



#### kibana

拉取镜像, 启动容器

```sh
docker pull docker.elastic.co/kibana/kibana:7.2.0
```

启动容器脚本 run-kibana.sh

```sh
# 停止容器
docker stop $(docker ps -a |  grep "kibana"  | awk '{print $1}')
echo "stop docker kibana"
# 删除容器
docker rm $(docker ps -a |  grep "kibana"  | awk '{print $1}')
echo "rm docker kibana"
# 启动容器
docker run \
--name kibana \
--link elasticsearch:elasticsearch \
-p 5601:5601 \
-d docker.elastic.co/kibana/kibana:7.2.0

```

#### docker-compose 编排





### kibana 汉化

#### 6.x 版本

- 1、拷贝此项目中的translations`文件夹`到您的kibana目录下的`src/legacy/core_plugins/kibana/`目录。若您的kibana无此目录，那还是尝试使用此项目old目录下的汉化方法吧。
- 2、修改您的kibana配置文件kibana.yml中的配置项：i18n.locale: "zh-CN"
- 3、重启Kibana，汉化完成

#### 7.x 版本

- 官方自带汉化资源文件（位于您的kibana目录下的`node_modules/x-pack/plugins/translations/translations/`目录。
- 修改您的kibana配置文件kibana.yml中的配置项：`i18n.locale: "zh-CN"`，重启Kibana则汉化完成。

> 参考：https://github.com/anbai-inc/Kibana_Hanization





## 参考

[快速搭建ELK日志分析系统 - 博客园](https://www.cnblogs.com/yuhuLin/p/7018858.html)

[从ELK到EFK演进 - 博客园](https://www.cnblogs.com/tylercao/p/7803520.html)

