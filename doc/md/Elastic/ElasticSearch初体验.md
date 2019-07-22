# ElasticSearch 初体验

[TOC]

## 简介

Elasticsearch是一个高度可扩展的、开源的、基于 Lucene 的全文搜索和分析引擎。它允许您快速，近实时地存储，搜索和分析大量数据，并支持多租户。

Elasticsearch也使用Java开发并使用 Lucene 作为其核心来实现所有索引和搜索的功能，但是它的目的是通过简单的 RESTful API 来隐藏 Lucene 的复杂性，从而让全文搜索变得简单。

不过，Elasticsearch 不仅仅是 Lucene 和全文搜索，我们还能这样去描述它：

- 分布式的实时文件存储，每个字段都被索引并可被搜索
- 分布式的实时分析搜索引擎
- 可以扩展到上百台服务器，处理PB级结构化或非结构化数据

而且，所有的这些功能被集成到一个服务里面，你的应用可以通过简单的 RESTful API、各种语言的客户端甚至命令行与之交互。



## 核心概念

Elasticsearch是一个近乎实时（NRT）的搜索平台。这意味着从索引文档到可搜索文档的时间有一点延迟（通常是一秒）。通常有集群，节点，分片，副本等概念。




### 集群(Cluster)

集群(cluster)是一组具有相同`cluster.name`的节点集合，他们协同工作，共享数据并提供故障转移和扩展功能，当然一个节点也可以组成一个集群。

集群由唯一名称标识，默认情况下为“elasticsearch”。此名称很重要，因为如果节点设置为按名称加入集群的话，则该节点只能是集群的一部分。

确保不同的环境中使用不同的集群名称，否则最终会导致节点加入错误的集群。

**【集群健康状态】**

> 集群状态通过 **绿**，**黄**，**红** 来标识

- **绿色** - 一切都很好（集群功能齐全）。

- **黄色** - 所有数据均可用，但尚未分配一些副本（集群功能齐全）。

- **红色** - 某些数据由于某种原因不可用（集群部分功能）。

**注意：当群集为红色时，它将继续提供来自可用分片的搜索请求，但您可能需要尽快修复它，因为存在未分配的分片。**

要检查群集运行状况，我们可以在 Kibana 控制台中运行以下命令`GET /_cluster/health`，得到如下信息：

```json
{
  "cluster_name" : "docker-cluster",
  "status" : "green",
  "timed_out" : false,
  "number_of_nodes" : 1,
  "number_of_data_nodes" : 1,
  "active_primary_shards" : 5,
  "active_shards" : 5,
  "relocating_shards" : 0,
  "initializing_shards" : 0,
  "unassigned_shards" : 0,
  "delayed_unassigned_shards" : 0,
  "number_of_pending_tasks" : 0,
  "number_of_in_flight_fetch" : 0,
  "task_max_waiting_in_queue_millis" : 0,
  "active_shards_percent_as_number" : 100.0
}
```







## 参考

[Elasticsearch 技术分析（一）： 基础入门 - 博客园](https://www.cnblogs.com/jajian/p/9976900.html)

[Elasticsearch 系列文章 - 博客园](https://www.cnblogs.com/jajian/tag/Elasticsearch/)