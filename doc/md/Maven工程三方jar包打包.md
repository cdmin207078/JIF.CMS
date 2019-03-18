# Maven 工程 引用三方 jar 包 打包问题



## 问题描述



## 解决方法



### 1. 添加本地引用依赖

```xml
<dependency>
	<groupId>xmap</groupId>
	<artifactId>xmap</artifactId>
	<scope>system</scope>
	<version>1.0</version>
	<systemPath>${project.basedir}/lib/xmap.jar</systemPath>
</dependency>
```

**${project.basedir}** 表示pom文件所在项目目录



### 2. 将jar包安装到本地maven库

#### ① 安装到本地maven 库

```shell
mvn install:install-file -Dfile=xmap.jar -DgroupId=xmap -DartifactId=xmap -Dversion=1.0.0 -Dpackaging=jar -DgeneratePom=true
```

其中：-DgroupId和-DartifactId的作用是指定了这个jar包在repository的安装路径，只是用来告诉项目去这个路径下寻找这个名称的jar包。

一般用的jar包都是在mven仓库中下载的，所以groupId和artifactId直接将复制maven仓库中的设置即可

#### ② 添加依赖

```xml
<!-- pom.xml -->
...
<dependency>
	<groupId>xmap</groupId>
	<artifactId>xmap</artifactId>
	<version>1.0.0</version>
</dependency>
...
```



> **注意：** 这样做有一个问题，如果团队协作的时候，就需要每人都在自己本地做一遍上面的操作，将三方jar包安装到各自的本地maven库中引用

### 3. 使用私有maven库, 维护三方jar 包





## 参考

[Maven项目引入本地第三方jar时出现 ClassNotFoundException](http://www.mcdao.com/mavenxiangmuyinrubendidisanfangjarshichuxian-classnotfoundexception.html)

[如何在maven的pom.xml中添加本地jar包](https://www.cnblogs.com/lixuwu/p/5855031.html)

[如何添加本地JAR文件到Maven项目中](https://blog.csdn.net/ShuSheng0007/article/details/78547264)

