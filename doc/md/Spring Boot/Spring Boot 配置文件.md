# Spring Boot 配置文件

## 1. 配置文件

Spring Boot 使用一个全局配置文件，用来修改 Spring Boot 底层自动配置的默认值，**配置文件名称是固定的**。

配置文件支持下面两种文件格式：

* application.properties
* application.yml


其中，properties 格式：

```properties
# application.properties
server.port=9090
```

yaml 格式：

```yaml
# application.yaml
server:
  port: 9090
```

或，之前 xml 格式：

```xml
<server>
	<port>9090</port>
</server>
```

## 2. YAML 语法

### 1.  基本语法

`<key>:[空格]<value>` 表示一对键值对，其中分隔冒号后面，value 前面的**空格必须有**

YAML 配置 以空格的缩进来控制层级关系，只要左对齐的一列数据，都是同一层级的

```yaml
# application.yaml
server:
  port: 9091
  error:
    path: /error
```

**注意：属性和值都是大小写敏感的**



### 2. 值的写法



#### 1. 字面量：普通的值 (数字，字符串，布尔)

字面量直接来写

其中，用 单引号 或 双引号包裹字符串字面量，会有不同效果，具体如下：

`" "`： 双引号：**不会转义字符串里的特殊符号，特殊字符会表示其本身含义**。例如：

```yaml
name: "hello \n world"
# == 输出 == 
# hello [换行↓]
# world
```

`' '`：单引号：**会转义字符串其中的特殊字符，特殊字符会当作普通字符串，原样输出**。例如：

```yaml
name: "hello \n world"
# == 输出 ==
# hello \n world
```

**默认情况下，字符串不需要加 单引号 或者 双引号**



#### 2. 对象，Map (键值对，属性和值)

当值为对象的时候，只需在key 的下一行来写对象的属性和值，注意保持缩进

```properties
user:
  name: 不凡
  age: 29
```

还有一种行内写法，二者效果一样：

```yaml
user: { name: 不凡, age: 29 }
```

#### 3.  数组

用 `-[空格][value]` 来表示数组中的一个元素，例如：

```yaml
pets:
 - cat
 - dog
 - fish
```

或 行内写法：

```yaml
pets: [cat, dog, fish]
```



## 3.  配置映射

创建自定义类

```java
/* Person.java */
public class Person {

    private String lastName;
    private Integer age;
    private Boolean boss;
    private Date birth;

    private Map<String, Object> maps;

    private List<Object> lists;

    private Dog dog;

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public Integer getAge() {
        return age;
    }

    public void setAge(Integer age) {
        this.age = age;
    }

    public Boolean getBoss() {
        return boss;
    }

    public void setBoss(Boolean boss) {
        this.boss = boss;
    }

    public Date getBirth() {
        return birth;
    }

    public void setBirth(Date birth) {
        this.birth = birth;
    }

    public Map<String, Object> getMaps() {
        return maps;
    }

    public void setMaps(Map<String, Object> maps) {
        this.maps = maps;
    }

    public List<Object> getLists() {
        return lists;
    }

    public void setLists(List<Object> lists) {
        this.lists = lists;
    }

    public Dog getDog() {
        return dog;
    }

    public void setDog(Dog dog) {
        this.dog = dog;
    }


    @Override
    public String toString() {
        return "Person{" +
                "lastName='" + lastName + '\'' +
                ", age=" + age +
                ", boss=" + boss +
                ", birth=" + birth +
                ", maps=" + maps +
                ", lists=" + lists +
                ", dog=" + dog +
                '}';
    }
}
```

```java
/* Dog.java */
public class Dog {
    private String name;

    private String age;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getAge() {
        return age;
    }

    public void setAge(String age) {
        this.age = age;
    }

    @Override
    public String toString() {
        return "Dog{" +
                "name='" + name + '\'' +
                ", age='" + age + '\'' +
                '}';
    }
}
```



配置文件

```yaml
person:
  lastName: 不凡
  age: 29
  boss: false
  birth: 1990-08-08
  maps: { k1: v1, k2: v2}
  lists:
    - 东坡
    - 介甫
  dog:
    name: Dog
    age: 2
```

接下来，为了让配置文件中的配置，能被映射到 Person 对象上，则需要给 Person 添加注解，

`@ConfigurationProperties(prefix = "person")`

**@ConfigurationProperties** 注解用来告诉 Spring Boot 将本类中的所有属性和配置文件相关配置进行绑定，其中 **prefix = "person"** 参数指明使用配置文件中的哪个节点进行映射

并且，只有容器中的组件，才能使用容器提供的 @ConfigurationProperties 功能，所以须添加 **@Component** 注解

添加好注解Person 类整体如下：

```java
@Component
@ConfigurationProperties(prefix = "person")
public class Person {

    private String lastName;
    private Integer age;
    private Boolean boss;
    private Date birth;

    private Map<String, Object> maps;
    private List<Object> lists;
    private Dog dog;
}
```

------

**IDEA 注意**，若使用 IDEA 开发工具，可能会出现下图提示，表示 Spring Boot 配置注解处理器没有被配置到 **classpath**

![IDEA Spring Boot](Spring Boot 配置文件.assets/1543906667976.png)

点击 Open Documentation 链接，跳转至 [Spring Boot 官网文档 Annotation Processor](https://docs.spring.io/spring-boot/docs/2.1.1.RELEASE/reference/html/configuration-metadata.html#configuration-metadata-annotation-processor) 按照说明，将 依赖添加至 项目 pom 文件中，如下：

```xml
/* pom.xml */
...
<dependencies>
    ...
    <dependency>
        <groupId>org.springframework.boot</groupId>
        <artifactId>spring-boot-configuration-processor</artifactId>
        <optional>true</optional>
    </dependency>
</dependencies>
...
```

依赖添加完毕之后，回到 Person 类中，上方红色提示变为绿色：

![1543907206824](Spring Boot 配置文件.assets/1543907206824.png)

提示说明，重新运行Spring Boot 程序，以来更新以来配置

以后再在配置文件中书写配置，则会有智能提示，如下：

![yaml 配置智能提示](Spring Boot 配置文件.assets/1543916604912.png)

其中提示的 `last-name` 与 `lastName` 写法效果相等

------

最后，测试输出

```java
@RunWith(SpringRunner.class)
@SpringBootTest
public class HelloSpringBootQuickApplicationTests {
    
    @Autowired
    Person person;

    @Test
    public void contextLoads() {
        System.out.println(person);
    }
}
```

结果：

![1543909624599](Spring Boot 配置文件.assets/1543909624599.png)



## 4. Properties 语法配置

```properties
# person 信息配置
person.last-name=张三
person.age=18
person.birth=2000/02/24
person.boss=true
person.dog.name=佩奇
person.dog.age=5
person.lists=乔治,苏西
person.maps.k1=小猪
person.maps.k2=Pig
```

结果 :

![1543917215952](Spring Boot 配置文件.assets/1543917215952.png)

**注意**：出现这种情况时，表明所使用的 IDEA `.properties` 文件默认编码不是 `UTF-8`，设置一下默认编码格式即可， 具体设置方法如下：

![设置 IDEA .properties 文件编码格式为 UTF-8](Spring Boot 配置文件.assets/1543917632197.png)

设置之后，结果如下：

![1543917572817](Spring Boot 配置文件.assets/1543917572817.png)



## 5. 使用 @Value 取值





```java
@Component
//@ConfigurationProperties(prefix = "person")
public class Person {

    /**
     * 获取配置文件中的属性配置
     */
    @Value("${person.last-name}")
    private String lastName;

    /**
     * 执行 SpEL 表达式
     */
    @Value("#{11*2}")
    private Integer age;
    
    ...
}
```

结果：

![1543918504391](Spring Boot 配置文件.assets/1543918504391.png)



## 6. @Value 取值与 @ConfigurationProperties 取值比较

| Feature            | @ConfigurationProperties | @Value     |
| ------------------ | ------------------------ | :--------- |
| 指定方式           | 批量注入文件中的属性     | 一个个指定 |
| 松散绑定(松散语法) | 支持                     | 不支持     |
| SpEL               | 不支持                   | 支持       |
| JSR303数据校验     | 支持                     | 不支持     |
| 复杂类型封装       | 支持                     | 不支持     |

> 总结：
>
> 如果仅仅在某个业务逻辑中需要获取配置文件中的某个配置项，则使用 **@Value**
>
> 如果专门写了一个 JavaBean来和配置文件进行映射，则直接使用 **@ConfigurationProperties ** 更方便合适

---

**JSR303数据校验**

```java
@Component
@ConfigurationProperties(prefix = "person")
@Validated  // 新增启用校验注解
public class Person {

    /**
     * 获取配置文件中的属性配置
     */
    // @Value("${person.last-name}")
    // Email 格式校验
    @Email  
    private String lastName;
    ...
}
```

结果

![1543919616473](Spring Boot 配置文件.assets/1543919616473.png)

---

## 7 . @PropertySource 与 @ImportResource

**@PropertySource** : 加载指定位置配置文件

```
@PropertySource(value = {"classpath:person.properties"})
@Component
@ConfigurationProperties(prefix = "person")
public class Person {
	...
}
```

**注意** 使用指定配置文件时，须确保 `application.properties` 与 `application.yaml` 里面同样配置移出，否则则不会使用指定配置文件配置值

**@ImportResource**：导入Spring 的配置文件，

## 参考

[尚硅谷_SpringBoot_配置-yaml简介-视频 - P9-P13](https://www.bilibili.com/video/av36291265/?p=9)