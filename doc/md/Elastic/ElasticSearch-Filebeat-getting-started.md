# ElasticSearch - Filebeat 起步

[TOC]



## 安装 Filebeat





## 配置 Filebeat

通过编辑 Filebeat 配置文件来配置使用 Filebeat ， 默认配置文件名为 **filebeat.yml**

> **filebeat.reference.yml** 示例配置文件，显示所有不推荐的选项。

下面开始配置

### 第一步：定义日志文件路径

对于最基本的Filebeat配置，你可以使用单个路径。例如：

```yaml
filebeat.inputs:
- type: log
  enabled: true
  paths:
    - /var/log/*.log
```

在这个例子中，获取在/var/log/*.log路径下的所有文件作为输入，这就意味着Filebeat将获取/var/log目录下所有以.log结尾的文件。

为了从预定义的子目录级别下抓取所有文件，可以使用以下模式：/var/log/*/*.log。这将抓取/var/log的子文件夹下所有的以.log结尾的文件。它不会从/var/log文件夹本身抓取。目前，不可能递归地抓取这个目录下的所有子目录下的所有.log文件。

> 注意：
>
> 假设配置的输入路径是 **/var/log/\*/*.log**，假设目录结构如下
>
> ![img](https://images2018.cnblogs.com/blog/874963/201808/874963-20180808182426196-188738357.png)
>
> 那么只会抓取到 **2.log** 和 **3.log** ，而**不会**抓到 **1.log** 和 **4.log** 。**因为目前，无法以递归方式获取目录的所有子目录中的所有文件。它不从/ var / log文件夹本身获取日志文件。**



### 第二步：配置输出

Filebeat 支持多种输出。但通常情况下，可以将数据直接发送到 Elasticsearch 或者发送到 Logstash 做一些额外的处理

如果想直接将数据发送到 Elasticsearch，则只需要设置 Elasticsearch 的服务地址和服务端口即可

```yaml
output.elasticsearch:
  hosts: ["172.19.159.1:9200"]
```

发送到 Logstash，参考： [Configure the Logstash output](https://www.elastic.co/guide/en/beats/filebeat/7.2/logstash-output.html)
发送到 其它输出，参考： [Configure the output](https://www.elastic.co/guide/en/beats/filebeat/7.2/configuring-output.html)

### 第三步：配置Kibana(可选)

如果使用随Filebeat提供的示例Kibana仪表板，可以配置Kibana端点。如果Kibana与Elasticsearch在同一主机上运行，则可以跳过此步骤。

```yaml
setup.kibana:
  host: "172.19.159.1:5601"
```

### 第四步：配置 Elasticsearch 和 Kibana安全策略(可选)

如果你的Elasticsearch和Kibana配置了安全策略，那么在你启动Filebeat之前需要在配置文件中指定访问凭据

```yaml
output.elasticsearch:
  hosts: ["172.19.159.1:9200"]
  username: "filebeat_internal"
  password: "YOUR_PASSWORD" 
setup.kibana:
  host: "172.19.159.1:5601"
  username: "my_kibana_user"  
  password: "YOUR_PASSWORD"
```



### 第五步：测试Filebeat配置(可选)

若要测试配置文件是否正确，运行下面命令来检查。其中，**-c** 用来指定配置文件路径

```sh
./filebeat test config -e -c ./filebeat.yml
```



有关Filebeat 配置选项的更多信息，请参见配置 [Configuring Filebeat](https://www.elastic.co/guide/en/beats/filebeat/7.2/configuring-howto-filebeat.html)

## 索引模板 (可选)

索引模板用于定义确定如何分析字段的设置和映射。(*相当于定义索引文档的数据结构，因为要把采集的数据转成标准格式输出*)

Filebeat包已经安装了推荐的索引模板。如果你接受filebeat.yml中的默认配置，那么Filebeat在成功连接到Elasticsearch以后会自动加载模板。如果模板已经存在，不会覆盖，除非你配置了必须这样做。

通过在Filebeat配置文件中配置模板加载选项，你可以禁用自动模板加载，或者自动加载你自己的目标。

### 加载配置模板

默认情况下，如果Elasticsearch输出是启用的，那么Filebeat会自动加载推荐的模板文件 **fields.yml**，如果要使用默认索引模板的话，则不需要其它配置。否则，可以在 **filebeat.yml** 中修改。

**加载不同模板**

```yaml
setup.template.name: "your_template_name"
setup.template.fields: "path/to/fields.yml"
```

**覆盖已存在的模板**

```yaml
setup.template.overwrite: true
```

**禁用自动模板加载**

```yaml
setup.template.enabled: false
```

  **更改索引名称**

默认情况下，Filebeat 写事件到名为 filebeat-7.2.1-yyyy.MM.dd 的索引，其中yyyy.MM.dd是事件被索引的日期。为了用一个不同的名字，你可以在Elasticsearch输出中设置index选项。例如：

```yaml
output.elasticsearch.index: "customname-%{[agent.version]}-%{+yyyy.MM.dd}"
setup.template.name: "customname"
setup.template.pattern: "customname-*"
```

If you’re using pre-built Kibana dashboards, also set the `setup.dashboards.index` option. For example:

```yaml
setup.dashboards.index: "customname-*"
```



### 手动指定配置模板

```sh
./filebeat setup --template -E output.logstash.enabled=false -E 'output.elasticsearch.hosts=["localhost:9200"]'
```



## 设置 Kibana dashboards (可选)

Filebeat附带了Kibana仪表盘、可视化示例。在你用dashboards之前，你需要创建索引模式，filebeat-*，并且加载dashboards到Kibana中。为此，你可以运行setup命令或者在filebeat.yml配置文件中配置dashboard加载。

```sh
./filebeat setup --dashboards
```



## 启动 Filebeat

```yaml
./filebeat -e
```





## 参考

[Filebeat Reference - 官方文档](https://www.elastic.co/guide/en/beats/filebeat/current/index.html)

[开始使用Filebeat - 博客园](https://www.cnblogs.com/cjsblog/p/9445792.html)

[Filebeat 模块与配置(续 <开始使用Filebeat>) - 博客园](https://www.cnblogs.com/cjsblog/p/9495024.html)

[Filebeat安装部署及配置详解](https://cloud.tencent.com/developer/article/1006051)