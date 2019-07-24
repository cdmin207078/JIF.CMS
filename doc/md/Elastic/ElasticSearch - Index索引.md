# ElasticSearch - Index 索引

[TOC]



本节讲述Elasticsearch 中的索引API。索引API 用于管理各个索引，索引设置，别名，映射和索引模板。



## 索引维护

### 新建索引

**索引命名规范**

- 仅限小写字母

- 不能包含`\`、`/`、 `*`、`?`、`"`、`<`、`>`、`|`、#以及空格符等特殊符号

- 从7.0版本开始不再包含冒号

- 不能以`-`、`_`或`+`开头

- 不能超过255个字节（注意这里是字节，因此多字节字符将计入255个限制）

  

基本命令如下：

```
PUT weibo
```

结果

```json
{
  "acknowledged" : true,
  "shards_acknowledged" : true,
  "index" : "weibo"
}
```

上面这样，就创建了一个具有默认配置的名为 **weibo** 的索引。



### 索引配置

创建索引时，可以制定相关设置，比如设置索引的 分片数 **number_of_shards** 和 副本数**number_of_replicas**

**默认情况下 分片数(number_of_shards) 与 副本数 (number_of_replicas) 都是 1**

例子：创建一个名为 weibo 的索引，它具有 三个 分片，与 两个 副本

```sh
PUT douban_book
{
    "settings" : {
        "index" : {
            "number_of_shards" : 3,
            "number_of_replicas" : 1
        }
    }
}
```

也可以简化为

```sh
PUT douban_book
{
    "settings" : {
        "number_of_shards" : 3,
        "number_of_replicas" : 2
    }
}
```



### 查看索引

例子：查看名为 weibo 的索引

```sh
GET douban_book
```

结果

```json
{
  "douban_book" : {
    "aliases" : { },
    "mappings" : { },
    "settings" : {
      "index" : {
        "creation_date" : "1563809485541",
        "number_of_shards" : "3",
        "number_of_replicas" : "1",
        "uuid" : "NJUtxdxbT_OWFTC3PQ3wbw",
        "version" : {
          "created" : "7020099"
        },
        "provided_name" : "douban_book"
      }
    }
  }
}
```



### 查看索引列表

```sh
GET /_cat/indices?v
```

结果如下，其中带有`.kibana`是Kibana自带样例索引。值得注意的是，新建的索引默认情况下 分片数(pri) 与 副本数 (rep) 都是 1

![1563809880638](ElasticSearch - Index索引的使用.assets/1563809880638.png)



### 判断索引是否存在

```sh
HEAD douban_book
```

结果

```sh
# 存在
200 - OK
# 不存在
404 - Not Found
```



### 更新副本数

```sh
PUT douban_book/_settings
{
  "number_of_replicas": 5
}
```



### 更新分片数

> 索引分片数，不可修改

**尝试修改将会得到以下错误**

```sh
PUT douban_book/_settings
{
  "number_of_shards": 5
}
```

结果

```json
{
  "error": {
    "root_cause": [
      {
        "type": "illegal_argument_exception",
        "reason": "Can't update non dynamic settings [[index.number_of_shards]] for open indices [[weibo/NJUtxdxbT_OWFTC3PQ3wbw]]"
      }
    ],
    "type": "illegal_argument_exception",
    "reason": "Can't update non dynamic settings [[index.number_of_shards]] for open indices [[weibo/NJUtxdxbT_OWFTC3PQ3wbw]]"
  },
  "status": 400
}
```

这是因为当索引一个文档的时候，文档会被存储到一个主分片中。 Elasticsearch 如何知道一个文档应该存放到哪个分片中呢？当我们创建文档时，它如何决定这个文档应当被存储在分片 `1` 还是分片 `2` 中呢？

首先这肯定不会是随机的，否则将来要获取文档的时候我们就不知道从何处寻找了。实际上，这个过程是根据下面这个公式决定的：

```
shard = hash(routing) % number_of_primary_shards
```

`routing` 是一个可变值，默认是文档的 `_id` ，也可以设置成一个自定义的值。 `routing` 通过 hash 函数生成一个数字，然后这个数字再除以 `number_of_primary_shards` （主分片的数量）后得到 **余数** 。这个分布在 `0` 到 `number_of_primary_shards-1` 之间的余数，就是我们所寻求的文档所在分片的位置。

这就解释了为什么我们要在创建索引的时候就确定好主分片的数量 并且永远不会改变这个数量：因为如果数量变化了，那么所有之前路由的值都会无效，文档也再也找不到了。

> 参考： https://www.elastic.co/guide/cn/elasticsearch/guide/current/routing-value.html



### 查看索引配置信息

```sh
GET douban_book/_settings/
```

结果

```json
{
  "weibo" : {
    "settings" : {
      "index" : {
        "creation_date" : "1563809485541",
        "number_of_shards" : "3",
        "number_of_replicas" : "5",
        "uuid" : "NJUtxdxbT_OWFTC3PQ3wbw",
        "version" : {
          "created" : "7020099"
        },
        "provided_name" : "weibo"
      }
    }
  }
}
```



### 删除索引

```sh
DELETE douban_book
```

结果

```json
{
  "acknowledged" : true
}
```



### 清空索引数据

>TODO

### 索引的关闭与打开

一个关闭的索引几乎不占用系统资源。我们可以临时关闭某个索引，在需要时再重新打开该索引。

```sh
# 关闭
POST douban_book/_close
# 打开
POST douban_book/_open
```



### 指定 type 的 mapping

> TODO

### 索引别名

索引别名不仅仅可以关联一个索引，它能聚合多个索引。此外，一个别名也可以与一个过滤器相关联， 这个过滤器在搜索和路由的时候被自动应用。



## 索引文档

### 新增文档

#### 指定ID

```sh
POST /douban_book/_doc/1
{
  "title":"借我"
}
```

返回

```json
{
  "_index" : "douban_book",
  "_type" : "_doc",
  "_id" : "1",
  "_version" : 1,
  "result" : "created",
  "_shards" : {
    "total" : 2,
    "successful" : 1,
    "failed" : 0
  },
  "_seq_no" : 0,
  "_primary_term" : 1
}

```

其中 **_shards** 节点展示有关索引操作的复制过程的信息：

- **`total`**

  Indicates how many shard copies (primary and replica shards) the index operation should be executed on.

- **`successful`**

  Indicates the number of shard copies the index operation succeeded on.

- **`failed`**

  An array that contains replication-related errors in the case an index operation failed on a replica shard.

The index operation is successful in the case `successful` is at least 1.

> 注意：Replica shards may not all be started when an indexing operation successfully returns (by default, only the primary is required, but this behavior can be changed). In that case, total will be equal to the total shards based on the number_of_replicas setting and successful will be equal to the number of shards started (primary plus replicas). If there were no failures, the failed will be 0.

#### 自动生成ID

可以在不指定id的情况下执行索引操作。 在这种情况下，将自动生成id。 此外，op_type将自动设置为create。 这是一个例子（注意这里使用的是 **POST** 而不是 **PUT**)

```sh
POST /douban_book/_doc
{
  "name":"借我"
}
```

#### Operation Type - 操作类型

索引操作还接受可用于强制创建操作的**op_type**，允许**“put-if-absent”**行为。 使用**create**时，如果索引中已存在该id的文档，则索引操作将失败。

```sh
PUT /douban_book/_doc/1?op_type=create
# 另一种写法
# PUT /douban_book/_create/1
{
  "name":"借我"
}
```

若已经存在记录则会报错

```json
{
  "error": {
    "root_cause": [
      {
        "type": "version_conflict_engine_exception",
        "reason": "[1]: version conflict, document already exists (current version [1])",
        "index_uuid": "tvh8h76FTceuL_9Ixm8qsw",
        "shard": "2",
        "index": "douban_book"
      }
    ],
    "type": "version_conflict_engine_exception",
    "reason": "[1]: version conflict, document already exists (current version [1])",
    "index_uuid": "tvh8h76FTceuL_9Ixm8qsw",
    "shard": "2",
    "index": "douban_book"
  },
  "status": 409
}
```

### 删除文档

```sh
DELETE /douban_book/_doc/1
```

> https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-delete.html

#### 条件删除文档

```sh
POST /douban_book/_delete_by_query
{
  "query": {
    "match":{
      "name":"借我"
    }
  }
}
```

> https://www.elastic.co/guide/en/elasticsearch/reference/current/docs-delete-by-query.html

### 修改文档



### 查找文档





### 批量操作

```sh
# 多条添加添加数据
POST _bulk
{ "index" : { "_index" : "douban_book" } }
{ "<field>":"<value>",...}
{ "index" : { "_index" : "douban_book" } }
{ "<field>":"<value>",...}
...
# 修改
# 删除
```

> 注意：
>
> **json 数据必须书写在一行**
> **create、update这些必须每行都加上一个申明索引**





## 综合实例

```sh

# 删除索引
DELETE /douban_book

# 创建索引
PUT douban_book
{
  "settings": {
    "number_of_shards": 3,
    "number_of_replicas": 1
  }
}

# 修改索引 - 修改副本数量
PUT douban_book/_settings
{
  "number_of_replicas": 5
}

# 新增文档 - 指定ID
POST /douban_book/_doc/1
# PUT /douban_book/_doc/1?op_type=create
# PUT /douban_book/_create/1
{
  "title":"借我"
}

# 新增文档 - 随机ID
POST /douban_book/_doc
{
  "name":"借我"
}

# 删除文档 - 指定ID 删除
DELETE /douban_book/_doc/1

# 删除文档 - 条件删除


# 修改文档



# 查询文档是否存在
HEAD /douban_book/_doc/1

# 查询文档 - 指定 ID 查询
GET /douban_book/_doc/1

# 查询文档 - 条件查询




# 清空索引

# 关闭索引
POST douban_book/_close

# 开启索引
POST douban_book/_open

# 批量操作文档
POST _bulk
{ "index" : { "_index" : "douban_book" } }
{ "title":"夏夜","auhtor":"玛格丽特·杜拉斯","pub_date":"2019-06-01T00:00:00","desc":"《夏夜：杜拉斯全集2》为《杜拉斯全集》的第二卷，收入法国作家杜拉斯写于1952至1960年间写的三部长篇小说《直布罗陀水手》《塔尔奎尼亚的小马》《夏夜十点半钟》，是作家处于转型期，从传统往个人风格化写作转变的作品。杜拉斯不再着意于平铺直叙讲故事，而是在看似寡淡的对话中道出微妙的心境与情感。小说仍有比较完整的情节，但加入魔幻、黑色悬疑小说色彩，虚实界限开始模糊，语言节奏、叙述节奏开始变得断裂。这三部作品仍属早期创作，风格却开始转变，三个故事都以意大利、西班牙作为地点，都以夏天的炎热作为气氛烘托。","price":72.00,"ISBN":"9787532779222"}
{ "index" : { "_index" : "douban_book" } }
{ "title":"人间失格","auhtor":"太宰治","pub_date":"2009-09-01T00:00:00","desc":"《人间失格》是日本著名小说家太宰治最具影响力的小说作品，同时也是糸色望（注：动漫《再见！绝望先生》的主角）老师日常生活必备的读物之一。另外在日本轻小说《文学少女》第一卷中被大量提及。《人间失格》（又名《丧失为人的资格》）发表于1948年，是一部自传体的小说，纤细的自传体中透露出极致的颓废，毁灭式的绝笔之作。太宰治巧妙地将自己的人生与思想，隐藏于主角叶藏的人生遭遇，藉由叶藏的独白，窥探太宰治的内心世界，一个“充满了可耻的一生”。在发表这部作品的同年，太宰治就自杀身亡。","price":16.00,"ISBN":"9787546302393"}
{ "index" : { "_index" : "douban_book" } }
{ "title":"朝花夕拾","auhtor":"鲁迅","pub_date":"1972-04-01T00:00:00","desc":"《朝花夕拾》原名《旧事重提》，是现代文学家鲁迅的散文集，收录鲁迅于1926年创作的10篇回忆性散文，1928年由北京未名社出版，现编入《鲁迅全集》第2卷。此文集作为“回忆的记事”，多侧面地反映了作者鲁迅青少年时期的生活，形象地反映了他的性格和志趣的形成经过。前七篇反映他童年时代在绍兴的家庭和私塾中的生活情景，后三篇叙述他从家乡到南京，又到日本留学，然后回国教书的经历；揭露了半封建半殖民地社会种种丑恶的不合理现象，同时反映了有抱负的青年知识分子在旧中国茫茫黑夜中，不畏艰险，寻找光明的困难历程，以及抒发了作者对往日亲友、师长的怀念之情。文集以记事为主，饱含着浓烈的抒情气息，往往又夹以议论，做到了抒情、叙事和议论融为一体，优美和谐，朴实感人。作品富有诗情画意，又不时穿插着幽默和讽喻；形象生动，格调明朗，有强烈的感染力。","price":0.25,"ISBN":"10019-1985" }
{ "index" : { "_index" : "douban_book" } }
{ "title":"呐喊","auhtor":"鲁迅","pub_date":"1973-03-01T00:00:00","desc":"《呐喊》收录作者1918年至1922年所作小说十四篇。1923年8月由北京新潮社出版，原收十五篇，列为该社《文艺丛书》之一。1924年5月第三次印刷时起，改由北京北新书局出版，列为作者所编的《乌合丛书》之一。1930年1 月第十三次印刷时，由作者抽去其中的《不周山》一篇(后改名为《补天》，收入《故事新编》)。作者生前共印行二十二版次。","price":0.36,"ISBN":"10019-1979" }
{ "index" : { "_index" : "douban_book" } }
{ "title":"彷徨","auhtor":"鲁迅","pub_date":"1973-03-01T00:00:00","desc":"《彷徨》收入鲁迅1924年至1925年所作的小说，首篇《祝福》写于1924年2月16日，末篇《离婚》写于1925年11月6日，实际的时间跨度是一年半多，1926年由北京北新书局出版，列为作者所编的《乌合丛书》之一。此后印行的版本都与初版同。书的扉页，有作者的题记：朝发轫于苍梧兮，夕余至乎县圃；欲少留此灵琐兮，日忽忽将其暮。吾令羲和弭节兮，望崦嵫而勿迫；路漫漫其修远兮，吾将上下而求索。《彷徨》鲁迅先生写作于“五·四”运动后新文化阵营分化的时期。原来参加过新文化运动的人，“有的退隐，有的高升，有的前进”，鲁迅当时象布不成阵的游勇那样“孤独”和 “彷徨”。《彷徨》表现了他在这一时期在革命征途上探索的心情。","price":0.37,"ISBN":"10019-1982" }

```



## 参考

[Elasticsearch 7.x：2、索引管理 -csdn](https://blog.csdn.net/chengyuqiang/article/details/86000472)

