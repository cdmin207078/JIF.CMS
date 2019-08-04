# ElasticSearch-Filebeat Output to ElasticSearch

[TOC]

## 配置项

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

#### ilm

配置索引的生命周期, 参考: [*Configure index lifecycle management*](https://www.elastic.co/guide/en/beats/filebeat/current/ilm.html) 

#### pipeline

> TODO: https://www.elastic.co/guide/en/beats/filebeat/current/elasticsearch-output.html#pipeline-option-es

#### pipelines

> TODO: https://www.elastic.co/guide/en/beats/filebeat/current/elasticsearch-output.html#pipelines-option-es

#### max_retries

> *Filebeat 忽略此设置, 并无限重试*

#### bulk_max_size









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

[Configure the Elasticsearch output - 官网文档](https://www.elastic.co/guide/en/beats/filebeat/current/elasticsearch-output.html)

[Filebeat 模块与配置(续 <开始使用Filebeat>) - 博客园](https://www.cnblogs.com/cjsblog/p/9495024.html)

[Filebeat安装部署及配置详解](https://cloud.tencent.com/developer/article/1006051)

