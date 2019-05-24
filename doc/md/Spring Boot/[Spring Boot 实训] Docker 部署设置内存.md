# [Spring Boot 实训] Docker 部署设置内存



## jar 包方式运行

jar 包启动运行时，直接指定：

```dockerfile
...
ENTRYPOINT ["java","-jar","-Xms500m", "-Xmx1g","app.jar"]
...
```



## war 包方式运行

**docker run** 时指定：

启动时传递 **JAVA_OPTS** 环境变量即可

```shell
[root@vultr ~]# docker run --rm -e JAVA_OPTS='-Xms500m -Xmx1g' tomcat
```

**使用 compose.yml** 时指定：

```yaml
...
environment:
        JAVA_OPTS: '-Xms500m -Xmx1g'
...
```



## 参考

[How to set Java heap size (Xms/Xmx) inside Docker container?](https://stackoverflow.com/questions/29923531/how-to-set-java-heap-size-xms-xmx-inside-docker-container)