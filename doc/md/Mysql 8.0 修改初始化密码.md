# MySql 8.0  初始化/修改 密码



## 问题描述

运行环境

**System** - `CentOS 7`

**mysql** - `mysql Ver 8.0.13 for Linux on x86_64 (MySQL Community Server - GPL)`



MySql 8.0 版本，使用下方 sql 语句来修改用户密码，会报错

```sql
update user set password = password('123456') where user = 'root' ;

# 报错内容
# ERROR 1064 (42000): You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near '('123456') where user = 'root'' at line 1

```

原因是：**在mysql 5.7.9以后废弃了password字段和password()函数, 并且在5.7中存储密码的字段不再是password了，变成了authentication_string**



## 解决方法

1. 修改mysql配置文件为免密登陆

   ```shell
   # /etc/my.cnf
   
   [mysqld]
   datadir=/var/lib/mysql
   socket=/var/lib/mysql/mysql.sock
   
   log-error=/var/log/mysqld.log
   pid-file=/var/run/mysqld/mysqld.pid
   skip-grant-tables # 添加免密登陆，数据库启动的时候 跳跃权限表的限制，不用验证密码，直接登录
   ```

   **使用 `whereis` 命令来查找配置文件位置**

   ```shell
   [root@localhost ~]# whereis my.cnf
   my: /etc/my.cnf
   ```

 2. 重启`mysql `数据库，进入数据库，将 `root@localhost` 用户密码设置为空

    ```shell
    [root@localhost ~]# systemctl restart mysqld 	# 重启数据库
    [root@localhost ~]# mysql 						# 免密进入数据库
    mysql> use mysql 								# 切换数据库
    mysql> update user 								# 修改密码为空
    	-> set authentication_string = '' 
    	-> where user = 'root';
    Query OK, 1 row affected (0.05 sec)
    Rows matched: 1  Changed: 1  Warnings: 0
    
    ```

 3. 退出mysql， 删除 `/etc/my.cnf` 文件中添加的 `skip-grant-tables`，重启mysql服务

4. 使用root用户进行登录，因为上面设置了authentication_string为空，所以可以免密码登录

   ```shell
   [root@localhost ~]# mysql -u root -p
   Enter password: （此处直接回车）
   ```

5. 使用ALTER修改root用户密码

   ```shell
   mysql> ALTER user 'root'@'localhost' IDENTIFIED BY '123456';
   Query OK, 0 rows affected (0.01 sec)
   ```



   > 注意：使用 `IDENTIFIED BY '123456'` 修改密码之后，其它机器使用此密码连接数据库时，有可能会出现 `Authentication plugin 'caching_sha2_password' cannot be loaded` 错误。
   >
   > [ERROR 2059 (HY000): Authentication plugin 'caching_sha2_password' cannot be loaded](https://stackoverflow.com/questions/49194719/authentication-plugin-caching-sha2-password-cannot-be-loaded)
   >
   > 解决方法：
   >
   > 修改密码的语句修改为
   >
   > ALTER user 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY  '123456';

6. 至此修改成功， 以后便可使用用户名密码登录

   ```shell
   [root@localhost ~]# mysql -u root -p
   Enter password: (输入上面设置的密码 '123456')
   Welcome to the MySQL monitor.  Commands end with ; or \g.
   Your MySQL connection id is 10
   Server version: 8.0.13 MySQL Community Server - GPL
   
   Copyright (c) 2000, 2018, Oracle and/or its affiliates. All rights reserved.
   
   Oracle is a registered trademark of Oracle Corporation and/or its
   affiliates. Other names may be trademarks of their respective
   owners.
   
   Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.
   
   mysql> 
   
   ```



## 参考

https://www.cnblogs.com/jjg0519/p/9034713.html



