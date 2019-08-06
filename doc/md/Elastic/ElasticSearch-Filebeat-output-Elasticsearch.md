# ElasticSearch-Filebeat Output to ElasticSearch

[TOC]

## Input 配置项

### paths

数据源路径列表, 可以设置多个

```yaml
filebeat.inputs:
- type: log 
  paths:
    - /var/log/system.log
    - /var/log/wifi.log
    - /var/log/*/*.log
```

其中 **/var/log/\*/*.log** 将会抓取**/var/log**目录中子目录中的所有.log文件。
**注意：它不会抓取 /var/log 本身目录下的日志文件。**
如果你想要的是获取 **/var/log/** 目录中递归所有下层.log 文件, 则需要设置 **recursive_glob**

### recursive_glob.enabled

> 默认值 - `true`

允许将**扩展为递归glob模式。**
启用这个特性后，每个路径中最右边的 \*\* 被扩展为固定数量的glob模式。
例如：/foo/\*\*   扩展到/foo， /foo/\*， /foo/\*\*，等等。
如果启用，它将单个\*\*扩展为8级深度*模式。
这个特性默认是启用的，设置**recursive_glob.enabled**为 **false** 可以禁用它

### encoding

读取的文件的编码

下面是一些W3C推荐的简单的编码：

- plain, latin1, utf-8, utf-16be-bom, utf-16be, utf-16le, big5, gb18030, gbk, hz-gb-2312
- euc-kr, euc-jp, iso-2022-jp, shift-jis, 等等

plain编码是特殊的，因为它不校验或者转换任何输入。

> 完整编码列表：https://www.elastic.co/guide/en/beats/filebeat/current/filebeat-input-log.html#_literal_encoding_literal

### exclude_lines

一个正则表达式列表，用于匹配您希望Filebeat **排除**的行。Filebeat **排除**列表中匹配正则表达式匹配到的行。
默认情况下，不会删除任何行。空行将会被忽略。

如果指定了multiline，那么在用exclude_lines过滤之前会将每个多行消息合并成一个单行。**（PS：也就是说，多行合并成单行后再支持排除行的过滤）**

下面的示例配置Filebeat以删除任何以DBG开头的行

```yaml
filebeat.inputs:
- type: log
  ...
  exclude_lines: ['^DBG']
```

有关受支持的regexp模式列表，请参阅正则表达式支持  [*Regular expression support*](https://www.elastic.co/guide/en/beats/filebeat/current/regexp-support.html) 

### include_lines

一个正则表达式列表，用于匹配您希望Filebeat **包含**的行。Filebeat只导出与列表中的正则表达式匹配的行。默认情况下，导出所有行。空行被忽略。

如果指定了multipline设置，每个多行消息先被合并成单行以后再执行include_lines过滤。

下面是一个例子，配置Filebeat导出以ERR或者WARN开头的行

```/yaml
filebeat.inputs:
- type: log
  ...
  include_lines: ['^ERR', '^WARN']
```

> 如果同时定义了 **include_lines** 和 **exclude_lines**，则Filebeat首先执行include_lines，然后执行exclude_lines。 定义两个选项的顺序无关紧要。 include_lines选项将始终在exclude_lines选项之前执行，即使exclude_lines出现在配置文件中的include_lines之前也是如此。

下面的示例导出包含sometext的行，但以DBG开头的行将被排除

```yaml
filebeat.inputs:
- type: log
  ...
  include_lines: ['sometext']
  exclude_lines: ['^DBG']
```

有关受支持的regexp模式列表，请参阅正则表达式支持  [*Regular expression support*](https://www.elastic.co/guide/en/beats/filebeat/current/regexp-support.html) 

### harvester_buffer_size

> 默认值 - `16384`

每个 harvester 在读取文件时使用的缓冲区的字节大小。

### max_bytes

> 默认值 - `10M(10485760)`

**单个日志消息可以拥有的最大字节数**
**最大字节之后的所有字节将被丢弃，并且不发送。**
这个设置对于多行日志消息特别有用，因为多行日志消息可能会变大。

### json

使 Filebeat将日志作为JSON消息来解析。例如：

```yaml
json.keys_under_root: true
json.add_error_key: true
json.message_key: log
```

为了启用JSON解析模式，你必须至少指定下列设置项中的一个：

#### keys_under_root

> 默认值 - `false`

默认情况下，解码后的JSON被放置在一个以"json"为key的输出文档中。如果你启用这个设置，那么这个key在文档中被复制为顶级。

#### overwrite_keys

如果启用了 **keys_under_root** 和此设置，则解码的JSON对象中的值将覆盖Filebeat通常添加的字段（类型，来源，偏移等），以防发生冲突。

#### add_error_key

如果启用此设置，Filebeat会在JSON解组错误或在配置中定义message_key但无法使用时添加 **error.message** 和 **error.type:json** 键。

#### message_key

**[可选]** 用于在应用行过滤和多行设置的时候指定一个JSON key。指定的这个key必须在JSON对象中是顶级的，而且其关联的值必须是一个字符串，否则没有过滤或者多行聚集发送

#### ignore_decoding_error

**[可选]** 用于指定是否JSON解码错误应该被记录到日志中。如果设为true，错误将被记录。默认是false。

### multiline

控制Filebeat如何处理跨越多行的日志消息的选项。 请参阅  [*Manage multiline messages*](https://www.elastic.co/guide/en/beats/filebeat/current/multiline-examples.html)











## Output 配置项

### enable

> 默认值 - `true`

启用或禁用该输出。默认true。

### hosts

要连接的Elasticsearch节点列表
事件以循环顺序分发到这些节点,  如果一个节点无法访问, 则会自动将该事件发送到另一个节点
每个Elasticsearch节点都可以定义为 **URL** 或 **IP:PORT**
例如：**http://192.15.3.2**, **https://es.found.io:9230**, **192.24.3.2:9300**

**如果未指定端口，则使用9200**

```yaml
output.elasticsearch:
  hosts: ["10.45.3.2:9220", "10.45.3.1:9230"]
  protocol: https
  path: /elasticsearch
```

最终得到的节点链接为: **https://10.45.3.2:9220/elasticsearch**, **https://10.45.3.1:9230/elasticsearch**

### compression_level

> 默认值 - `0`

gzip压缩级别。 将此值设置为0将禁用压缩。 压缩级别必须在**1（最佳速度）**到 **9（最佳压缩）**的范围内。
**增加压缩级别将减少网络使用，但会增加CPU使用率。**

### escape_html

> 默认值 - `false`

配置字符串中的HTML转义。 设置为true以启用转义。

### worker

> 默认值 - `1`

每个配置主机向Elasticsearch发布事件的工作器数。 这最适用于启用负载平衡模式 
示例：如果您有2个主机和3个工作程序，则总共启动6个工作程序（每个主机3个）

### username

连接到Elasticsearch的基本身份验证用户名

### password

连接到Elasticsearch的基本身份验证密码

### parameters

使用索引操作在url中传递的HTTP参数字典

### protocol

> 默认值：`http`

选项是: **http** 或 **https** 
**如果为主机指定URL, 则协议的值将被URL中指定的任何方案覆盖**

### path

HTTP API调用前面的HTTP路径前缀。 这对于Elasticsearch监听在自定义前缀下导出API的HTTP反向代理的情况非常有用

### headers

自定义HTTP头, 以添加到Elasticsearch输出创建的每个请求中, 通常可以通过用逗号分隔多个头值来为相同的头名指定多个头值. 例如: 

```yaml
output.elasticsearch.headers:
  X-My-Header: Header contents
```

### proxy_url

连接到Elasticsearch服务器时要使用的代理的URL。 该值可以是完整的URL或“host [:port]”，在这种情况下，假设为“http”方案。 如果未通过配置文件指定值，则使用代理环境变量。 有关环境变量的更多信息，请参阅 [*Go文档*](https://golang.org/pkg/net/http/#ProxyFromEnvironment)。

### index

索引名字, 指定发送到哪个索引中去
默认是 **filebeat-%{[beat.version]}-%{+yyyy.MM.dd}**（例如，"filebeat-6.3.2-2017.04.26"）
如果你想改变这个设置，你需要配置 **setup.template.name** 和 **setup.template.pattern** 选项
具体参考:  [*Load the Elasticsearch index template*](https://www.elastic.co/guide/en/beats/filebeat/current/configuration-template.html)

如果你用内置的 Kibana dashboards，你也需要设置 **setup.dashboards.index** 选项。

启用索引生命周期管理时，将忽略索引设置。 如果要将事件发送到支持索引生命周期管理的集群，何更改索引名称, 请参阅 [*Configure index lifecycle management*](https://www.elastic.co/guide/en/beats/filebeat/current/ilm.html)

你也可以使用索引文档中的指定字段来动态设置索引名称， 例如：

```yaml
output.elasticsearch:
  hosts: ["http://localhost:9200"]
  index: "%{[fields.log_type]}-%{[agent.version]}-%{+yyyy.MM.dd}" 
```

依照上面设置, **log_type:normal** 将会发送到 **normal-7.3.0-2019-08-02** 索引中. **log_type:critical** 将会发送到 **critical-7.3.0-2019-08-02** 中

### indices

索引选择器规则的数组
每个规则指定用于与规则匹配的事件的索引, Filebeat使用数组中的第一个匹配规则。规则可以包含条件，格式化基于字符串的字段和名称映射
**如果没有匹配的规则, 则使用 *index* 配置**

**规则定义**

#### index

要使用的索引格式字符串。如果此字符串包含字段引用，例如 **%{[fields.name]}**，则必须存在字段，否则规则将失败。

#### mapping



#### default

如果映射没有找到匹配项，则使用默认索引名称

#### when

具体规则条件, 支持的条件有:  [conditions](https://www.elastic.co/guide/en/beats/filebeat/current/defining-processors.html#conditions)

下面的示例根据 *message* 字段是否包含指定的字符串设置索引

```yaml
output.elasticsearch:
  hosts: ["http://localhost:9200"]
  indices:
    - index: "warning-%{[agent.version]}-%{+yyyy.MM.dd}"
      when.contains:
        message: "WARN"
    - index: "error-%{[agent.version]}-%{+yyyy.MM.dd}"
      when.contains:
        message: "ERR"
```

### ilm

配置索引的生命周期, 参考: [*Configure index lifecycle management*](https://www.elastic.co/guide/en/beats/filebeat/current/ilm.html) 

### pipeline

> TODO: https://www.elastic.co/guide/en/beats/filebeat/current/elasticsearch-output.html#pipeline-option-es

### pipelines

> TODO: https://www.elastic.co/guide/en/beats/filebeat/current/elasticsearch-output.html#pipelines-option-es

### max_retries

> *Filebeat 忽略此设置, 并无限重试*

### bulk_max_size

> 默认值: 50

单个Elasticsearch批量API索引请求中要批量处理的最大事件数。
事件可以分批收集。 Filebeat会将大于bulk_max_size的批次拆分为多个批次。
指定更大的批处理大小可以通过降低发送事件的开销来提高性能。 但是，大批量大小也会增加处理时间，这可能会导致API错误，连接中断，超时发布请求，以及最终导致吞吐量降低。
将**bulk_max_size**设置为小于或等于 **0** 的值将禁用批处理的拆分。 禁用拆分时，队列将决定批处理中包含的事件数。

### backoff.init

> 默认值: 1 秒

在网络错误后尝试重新连接到Elasticsearch的间隔秒数
等待backoff.init秒后，Filebeat尝试重新连接。 
**如果尝试失败，则退避定时器以指数方式增加到backoff.max。 连接成功后，将重置退避定时器**

### backoff.max

> 默认值: 60 秒

在网络错误后尝试连接到Elasticsearch之前等待的最大秒数。 默认值为60秒。

### timeout

> 默认值: 90 秒

Elasticsearch请求的http请求超时(以秒为单位)。默认值是90。

### ssl

> TODO: https://www.elastic.co/guide/en/beats/filebeat/current/elasticsearch-output.html#_literal_ssl_literal



## 配置 Https

当你指定Elasticsearch作为output时，Filebeat通过Elasticsearch提供的HTTP API向其发送数据。例如：

```yaml
output.elasticsearch:
  hosts: ["https://localhost:9200"]
  index: "filebeat-%{[agent.version]}-%{+yyyy.MM.dd}"
  ssl.certificate_authorities: ["/etc/pki/root/ca.pem"]
  ssl.certificate: "/etc/pki/client/cert.pem"
  ssl.key: "/etc/pki/client/cert.key"
```



## 参考



[Configure the Elasticsearch Input - 官网文档](https://www.elastic.co/guide/en/beats/filebeat/current/filebeat-input-log.html)

[Configure the Elasticsearch Output - 官网文档](https://www.elastic.co/guide/en/beats/filebeat/current/elasticsearch-output.html)

[Filebeat 模块与配置(续 <开始使用Filebeat>) - 博客园](https://www.cnblogs.com/cjsblog/p/9495024.html)

[Filebeat安装部署及配置详解](https://cloud.tencent.com/developer/article/1006051)

