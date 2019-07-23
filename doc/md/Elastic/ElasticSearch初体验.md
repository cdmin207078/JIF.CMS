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

### Near Realtime (NRT)

Elasticsearch是一个近乎实时（NRT）的搜索平台。这意味着从索引文档到可搜索文档的时间有一点延迟（通常是一秒）。通常有集群，节点，分片，副本等概念。



### 节点(Node)

节点，一个运行的 ES 实例就是一个节点，节点存储数据并参与集群的索引和搜索功能。

就像集群一样，节点由名称标识，默认情况下，该名称是在启动时分配给节点的随机通用唯一标识符（UUID）。如果不需要默认值，可以定义所需的任何节点名称。此名称对于管理目的非常重要，您可以在其中识别网络中哪些服务器与 Elasticsearch 集群中的哪些节点相对应。

可以将节点配置为按集群名称加入特定集群。默认情况下，每个节点都设置为加入一个名为 cluster 的 elasticsearch 集群，这意味着如果您在网络上启动了许多节点并且假设它们可以相互发现 - 它们将自动形成并加入一个名为 elasticsearch 的集群。




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



### 索引（Index）

**索引是具有某些类似特征的文档集合。**例如，您可以拥有店铺数据的索引，商品的一个索引以及订单数据的一个索引。

索引由名称标识（必须全部小写），此名称用于在对其中的文档执行索引，搜索，更新和删除操作时引用索引。



### 类型（Type）

> *Indices created in Elasticsearch 7.0.0 or later no longer accept a `_default_`mapping. Indices created in 6.x will continue to function as before in Elasticsearch 6.x. Types are deprecated in APIs in 7.0, with breaking changes to the index creation, put mapping, get mapping, put template, get template and get field mappings APIs.*
>
> 在Elasticsearch 7.0.0或更高版本中创建的索引不再接受_default_映射。 在6.x中创建的索引将继续像以前一样在Elasticsearch 6.x中运行。 在7.0中的API中不推荐使用类型，对索引创建，放置映射，获取映射，放置模板，获取模板和获取字段映射API进行重大更改。
>
> **参考 -** https://www.elastic.co/guide/en/elasticsearch/reference/7.2/removal-of-types.html

当你想要在同一个index中存储不同类型的documents时，type用作这个index的一个逻辑分类/分区。比如，在一个索引中，用户数据是一个type，帖子是另一个type。在后续的版本中，一个index将不再允许创建多个types，而且整个types这个概念都将被删除。

> type是index的一个逻辑分类（或者叫分区），在当前的版本中，它仍然用于在一个索引下区分不同类型的数据。但是，不建议这样做，因为在后续的版本中type这个概念将会被移除，也不允许一个索引中有多个类型。

### 文档（Document）

文档是可以建立索引的基本信息单元。例如，您可以为单个客户提供文档，为单个产品提供一个文档，为单个订单提供一个文档。该文档以JSON（JavaScript Object Notation）表示，JSON是一种普遍存在的互联网数据交换格式。

在索引/类型中，您可以根据需要存储任意数量的文档。请注意，尽管文档实际上驻留在索引中，但实际上必须将文档编入索引/分配给索引中的类型



### 分片(Shards)

索引可能存储大量可能超过单个节点的硬件限制的数据。例如，占用1TB磁盘空间的十亿个文档的单个索引可能不适合单个节点的磁盘，或者可能太慢而无法单独从单个节点提供搜索请求。

为了解决这个问题，Elasticsearch 提供了将索引细分为多个称为分片的功能。**创建索引时，只需定义所需的分片数即可。**每个分片本身都是一个功能齐全且独立的“索引”，可以托管在集群中的任何节点上。

设置分片的目的及原因主要是：

- 它允许您水平拆分/缩放内容量
- 它允许您跨分片（可能在多个节点上）分布和并行化操作，从而提高性能/吞吐量

分片的分布方式以及如何将其文档聚合回搜索请求的机制完全由 Elasticsearch 管理，对用户而言是透明的。

在可能随时发生故障的网络/云环境中，分片非常有用，建议使用故障转移机制，以防分片/节点以某种方式脱机或因任何原因消失。为此，Elasticsearch 允许您将索引的分片的一个或多个副本制作成所谓的副本分片或简称副本。



### 副本(Replicas)

**副本，是对分片的复制。**目的是为了当分片/节点发生故障时提供高可用性，它允许您扩展搜索量/吞吐量，因为可以在所有副本上并行执行搜索。

总而言之，每个索引可以拆分为多个分片。索引也可以复制为零次（表示没有副本）或更多次。复制之后，每个索引将具有主分片(从原始分片复制而来的)和复制分片(主分片的副本)。

可以在创建索引时为每个索引定义分片和副本的数量。创建索引后，您也可以随时动态更改副本数。您可以使用`_shrink` 和 `_splitAPI` 更改现有索引的分片数，但这不是一项轻松的任务，所以预先计划正确数量的分片是最佳方法。

复制之所以重要，主要有两个原因：

- 在shard/node失败的时候，它提供高可用性。正因为如此，复制的shard（简称shard的副本）绝不会跟原始shard在同一个节点上
- 它允许扩展搜索量/吞吐量，因为搜索可以并行地在所有副本上执行

例子：

![img](https://img2018.cnblogs.com/blog/874963/201812/874963-20181215233240381-1922192791.png)



默认情况下，Elasticsearch 中的每个索引都分配了5个主分片和1个副本，这意味着如果集群中至少有两个节点，则索引将包含5个主分片和另外5个副本分片（1个完整副本），总计为每个索引10个分片。



### 小结

1. **node**是一台服务器，表示集群中的节点
2. **document**表示索引记录
3. 一个**index**中不建议定义多个**type**
4. 一个**index**可以有多个**shard**，每个**shard**可以有**0**个或**多个**副本
5. **original shard** （原始shard，或者叫 primary shard）的复制成为副本shard，简称shard
6. 主分片和副本**决不会在同一个节点**
7. 分片的好处主要有两个：第一，突破单台服务器的硬件限制；第二，可以并行操作，从而提高性能和吞吐量；（PS：其实跟kafka差不多）
8. 副本的好处主要在于：第一，提供高可用；第二，并行提升性能和吞吐量
9. 一个索引包含一个或多个分片，索引记录（即文档）数据存储在这些shard中，且一个文档只会存在于一个分片中
10. 每个**shard**都是一个独立的功能完善的“**index**”，意思是它可以独立处理索引/搜索请求

（注意：本文中提到的分片指的是主分片（primary shard），而不是副本（replica shard））

## 参考

[Elasticsearch 快速开始 - 博客园](https://www.cnblogs.com/cjsblog/p/9439331.html)

[Elasticsearch Document - 博客园](https://www.cnblogs.com/cjsblog/p/10125342.html)

[Elasticsearch 技术分析（一）： 基础入门 - 博客园](https://www.cnblogs.com/jajian/p/9976900.html)

[Elasticsearch 系列文章 - 博客园](https://www.cnblogs.com/jajian/tag/Elasticsearch/)

