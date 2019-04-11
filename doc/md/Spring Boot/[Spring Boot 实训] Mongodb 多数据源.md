# [Spring Boot 实训] Mongodb 多数据源



**代码**



> MultiMongoConfigure.java

```java
public abstract class MultiMongoConfigure {

    // 变量名跟配置的参数对应
    private String host;
    private String authentication_database;
    private String database;
    private String username;
    private String password;
    private int port;
    
    // get; set; ....

    public MongoDbFactory mongoDbFactory() throws Exception {

        // 无认证的初始化方法
        // return new SimpleMongoDbFactory(new MongoClient(host, port), database);

        // 有认证的初始化方法
        ServerAddress serverAddress = new ServerAddress(host, port);
        List<MongoCredential> mongoCredentialList = new ArrayList<>();
        // 注意这里的 授权数据库 - authentication_database 与 业务数据库 - database 不同
  mongoCredentialList.add(MongoCredential.createScramSha1Credential(username, authentication_database, password.toCharArray()));
        return new SimpleMongoDbFactory(new MongoClient(serverAddress, mongoCredentialList), database);
    }

    /*
     * Factory method to create the MongoTemplate
     */
    abstract public MongoTemplate getMongoTemplate() throws Exception;

}
```



> MongoConfigRobot.java

```java
@Configuration
@ConfigurationProperties(prefix = "spring.data.mongodb.robot")
public class MongoConfigRobot extends MultiMongoConfigure {

    // 需要注意的是 多个数据源中有一个需要设置bean名为mongoTemplate，而且注释为@Primary
    // 否则WebMvcConfigurationSupport.class等会报错找不到mongoTemplate。
    // 参考：https://blog.csdn.net/zzq900503/article/details/81103917

    // 可能遇到的问题-more than one ‘primary’ bean found among candidates
    // 原因:
    // Spring Boot会自动注入一个默认的mongoTemplate或者我们设置了多个@Primary数据源。
    // 解决方式:
    // 排除Spring Boot自动注入的类，我们自动重写的mongoTemplate需要且只能设置一个为@Primary。
    // @Primary 只用设置一个即可
    @Primary
    @Bean(name = "RobotMongoTemplate")
    @Override
    public MongoTemplate getMongoTemplate() throws Exception {
        return new MongoTemplate(mongoDbFactory());
    }
}
```

同理另一个数据库类似代码



**配置文件**

```properties
#mongoDB
spring.data.mongodb.robot.host=192.168.20.72
spring.data.mongodb.robot.database=robot
spring.data.mongodb.robot.port=27017
spring.data.mongodb.robot.username=admin
spring.data.mongodb.robot.password=123456

# 授权数据库
spring.data.mongodb.robot.authentication-database=admin


spring.data.mongodb.robotcrm.host=192.168.20.72
spring.data.mongodb.robotcrm.authentication-database=admin
spring.data.mongodb.robotcrm.database=robot-crm
spring.data.mongodb.robotcrm.port=27017
spring.data.mongodb.robotcrm.username=admin
spring.data.mongodb.robotcrm.password=123456

# 授权数据库
spring.data.mongodb.robotcrm.authentication-database=admin
```



**使用**

```java
@Autowired
@Qualifier(value = "RobotMongoTemplate")
private MongoTemplate robotMongo;

@Autowired
@Qualifier(value = "RobotCRMMongoTemplate")
private MongoTemplate crmMongo;

```







## 参考

[Spring Boot配置MongoDB多数据源](https://blog.csdn.net/zzq900503/article/details/81103917)

