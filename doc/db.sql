-- --------------------------------------------------------
-- 主机:                           192.168.0.130
-- 服务器版本:                        5.5.53-log - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win32
-- HeidiSQL 版本:                  9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- 导出 jif.cms 的数据库结构
CREATE DATABASE IF NOT EXISTS `jif.cms` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `jif.cms`;

-- 导出  表 jif.cms.articles 结构
CREATE TABLE IF NOT EXISTS `articles` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` longtext,
  `Content` longtext,
  `MarkdownContent` longtext,
  `AllowComments` tinyint(1) NOT NULL,
  `CoverImg` text,
  `IsPublished` bit(1) NOT NULL DEFAULT b'0',
  `IsDeleted` bit(1) NOT NULL DEFAULT b'0',
  `CategoryId` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateUserId` int(11) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  `PublishTime` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=447 DEFAULT CHARSET=utf8;

-- 正在导出表  jif.cms.articles 的数据：~21 rows (大约)
DELETE FROM `articles`;
/*!40000 ALTER TABLE `articles` DISABLE KEYS */;
INSERT INTO `articles` (`Id`, `Title`, `Content`, `MarkdownContent`, `AllowComments`, `CoverImg`, `IsPublished`, `IsDeleted`, `CategoryId`, `CreateTime`, `CreateUserId`, `UpdateTime`, `UpdateUserId`, `PublishTime`) VALUES
	(1, 'Explosions ', '<p>a</p>\n', 'a', 1, NULL, b'1', b'0', 2, '2016-12-11 15:48:04', 1, '2017-07-17 13:02:06', 1, '2017-07-19 13:23:44'),
	(20, '旅途·故乡', '纵梦里，还藏着那句来不及说的话 / 也不过，问句“是耶非耶”啊', NULL, 1, NULL, b'1', b'0', 15, '2016-12-11 16:00:15', 2, NULL, NULL, '2017-07-17 11:12:54'),
	(428, '暮春秋色', '<p>多开阔</p><p>幻生凋落</p><p>曙分</p><p>云落</p><p>冬穿梭</p><p>晚来经过</p><p>手挥</p><p>捕捉</p><p>起风了</p><p>骤雨下天</p><p>暮春</p><p>秋色</p><p>依清池</p><p>姽婳妩媚</p><p>万丘壑</p><p>锦缎绫罗</p><p>或多</p><p>已消融</p><p>光阴归来</p><p>变空白</p><p>染尘埃</p><p>一并殓埋</p>', NULL, 1, NULL, b'1', b'0', 1, '2016-12-11 16:00:36', 1, '2017-01-02 02:48:10', 1, '2017-07-17 11:12:54'),
	(429, '空谷幽兰', '<blockquote>\n<p><strong>循环依赖</strong> - 运行时组建相互依赖</p>\n</blockquote>\n<h2 id="h2-u540Cu4E3Au5C5Eu6027u6CE8u5165"><a name="同为属性注入" class="reference-link"></a><span class="header-link octicon octicon-link"></span>同为属性注入</h2><p>若互相关联的两个类，都是使用 <strong><code>属性注入</code></strong> 来引用对方，如下这种情况：</p>\n<pre><code class="lang-csharp">class ClassA\n{\n  // 注意：确保有公共的 set访问器\n  public ClassB B { get; set; }\n}\n\nclass ClassB\n{\n  // 注意：确保有公共的 set访问器\n  public ClassA A { get; set; }\n}\n</code></pre>\n<p>则须保证一下几点，则可：</p>\n<ul>\n<li><strong>依赖属性可设置</strong>, 即属性必须是可写的.</li><li><strong>使用PropertiesAutowired来注册类型</strong>, 以保证允许循环依赖</li><li><strong>这两种类型都不能注册为 <code>InstancePerDependency</code></strong>, 如果任意一个设置为 ‘factory scope(工厂作用域)’, 将得不到想要的结果。必须设置为其它非 ‘factory scope(工厂作用域)’ 的其它范围, 例如：<code>SingleInstance</code>, <code>InstancePerLifetimeScope</code>等</li></ul>\n<p>注册代码如下：</p>\n<pre><code class="lang-csharp">\n// 注册类型时，使用 `PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)`, 告诉autofac 允许循环依赖\nvar cb = new ContainerBuilder();\n\ncb.RegisterType&lt;ClassA&gt;()\n  .InstancePerLifetimeScope()\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\n\ncb.RegisterType&lt;ClassB&gt;()\n  .InstancePerLifetimeScope()\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\n</code></pre>\n<h2 id="h2--"><a name="构造函数/属性注入" class="reference-link"></a><span class="header-link octicon octicon-link"></span>构造函数/属性注入</h2><p>若互相关联的两个类，一个使用 <strong><code>属性注入</code></strong> 来引用对方， 另一个使用 <strong><code>构造函数注入</code></strong> 引用对方。如下这种情况：</p>\n<pre><code class="lang-csharp">class ClassA\n{\n  public ClassA (classB B) { }\n}\n\nclass ClassB\n{\n  // 注意：确保有公共的 set访问器\n  public ClassA A { get; set; }\n}\n</code></pre>\n<p>则须保证一下几点，则可：</p>\n<ul>\n<li><strong>依赖属性可设置</strong>, 即属性必须是可写的</li><li><strong>使用属性注入的类,须使用PropertiesAutowired来注册类型</strong>, 以保证允许循环依赖</li><li><strong>这两种类型都不能注册为 <code>InstancePerDependency</code></strong>, 如果任意一个设置为 ‘factory scope(工厂作用域)’, 将等不到想要的结果。必须设置为其它非 ‘factory scope(工厂作用域)’ 的其它范围, 例如：<code>SingleInstance</code>, <code>InstancePerLifetimeScope</code>等</li></ul>\n<p>注册代码如下：</p>\n<pre><code class="lang-csharp">var cb = new ContainerBuilder();\n\ncb.RegisterType&lt;ClassA&gt;()\n  .InstancePerLifetimeScope();\n\n// ClassB 中使用了属性注入来引用 ClassA, 则需要允许 ClassB 循环依赖\ncb.RegisterType&lt;ClassB&gt;()\n  .InstancePerLifetimeScope()\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\n</code></pre>\n<h2 id="h2-u540Cu4E3Au5171u9020u51FDu6570u6CE8u5165"><a name="同为共造函数注入" class="reference-link"></a><span class="header-link octicon octicon-link"></span>同为共造函数注入</h2><p>不支持同为 <strong><code>构造函数</code></strong>注入的循环依赖关系, 当你这样使用时，会抛出异常。<br>您可以使用DynamicProxy2扩展或其他方式引用来规避此问题</p>\n<h2 id="h2-u53C2u8003"><a name="参考" class="reference-link"></a><span class="header-link octicon octicon-link"></span>参考</h2><p><a href="http://docs.autofac.org/en/latest/advanced/circular-dependencies.html">Circular Dependencies - Autofac 循环依赖官方说明</a></p>\n', '> **循环依赖** - 运行时组建相互依赖\n\n## 同为属性注入\n\n若互相关联的两个类，都是使用 **`属性注入`** 来引用对方，如下这种情况：\n\n```csharp\nclass ClassA\n{\n  // 注意：确保有公共的 set访问器\n  public ClassB B { get; set; }\n}\n\nclass ClassB\n{\n  // 注意：确保有公共的 set访问器\n  public ClassA A { get; set; }\n}\n```\n\n则须保证一下几点，则可：\n\n- **依赖属性可设置**, 即属性必须是可写的.\n- **使用PropertiesAutowired来注册类型**, 以保证允许循环依赖\n- **这两种类型都不能注册为 `InstancePerDependency`**, 如果任意一个设置为 \'factory scope(工厂作用域)\', 将得不到想要的结果。必须设置为其它非 \'factory scope(工厂作用域)\' 的其它范围, 例如：`SingleInstance`, `InstancePerLifetimeScope`等\n\n注册代码如下：\n```csharp\n\n// 注册类型时，使用 `PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)`, 告诉autofac 允许循环依赖\nvar cb = new ContainerBuilder();\n\ncb.RegisterType<ClassA>()\n  .InstancePerLifetimeScope()\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\n\ncb.RegisterType<ClassB>()\n  .InstancePerLifetimeScope()\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\n```\n\n\n## 构造函数/属性注入\n\n\n若互相关联的两个类，一个使用 **`属性注入`** 来引用对方， 另一个使用 **`构造函数注入`** 引用对方。如下这种情况：\n\n```csharp\nclass ClassA\n{\n  public ClassA (classB B) { }\n}\n\nclass ClassB\n{\n  // 注意：确保有公共的 set访问器\n  public ClassA A { get; set; }\n}\n```\n\n则须保证一下几点，则可：\n\n- **依赖属性可设置**, 即属性必须是可写的\n- **使用属性注入的类,须使用PropertiesAutowired来注册类型**, 以保证允许循环依赖\n- **这两种类型都不能注册为 `InstancePerDependency`**, 如果任意一个设置为 \'factory scope(工厂作用域)\', 将等不到想要的结果。必须设置为其它非 \'factory scope(工厂作用域)\' 的其它范围, 例如：`SingleInstance`, `InstancePerLifetimeScope`等\n\n注册代码如下：\n```csharp\nvar cb = new ContainerBuilder();\n\ncb.RegisterType<ClassA>()\n  .InstancePerLifetimeScope();\n\n// ClassB 中使用了属性注入来引用 ClassA, 则需要允许 ClassB 循环依赖\ncb.RegisterType<ClassB>()\n  .InstancePerLifetimeScope()\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\n```\n\n## 同为共造函数注入\n\n不支持同为 **`构造函数`**注入的循环依赖关系, 当你这样使用时，会抛出异常。\n您可以使用DynamicProxy2扩展或其他方式引用来规避此问题\n\n\n## 参考\n[Circular Dependencies - Autofac 循环依赖官方说明](http://docs.autofac.org/en/latest/advanced/circular-dependencies.html)', 0, NULL, b'1', b'0', 1, '2016-12-11 18:35:07', 1, '2017-07-14 17:00:57', 1, '2017-07-17 11:12:54'),
	(430, 'ASP.NET Web API 中的 认证 与 授权', '<p>111</p>\n', '111', 1, NULL, b'1', b'0', 3, '2016-12-13 23:21:34', 2, '2017-07-14 12:54:50', 5, '2017-07-17 11:12:54'),
	(431, '山河故人', '<p><img src="http://static.cnbetacdn.com/article/2017/0716/47d8858530a66d0.gif" alt="熊猫" title="熊猫"></p>\n<p>潮升 月落 雨淋湿传说</p>\n<p>一张信纸 写不下许多</p>\n<p>你想起的我 在望着什么</p>\n<p>你张开的双手 在等什么</p>\n<p>你想我吧 在某一个刹那</p>\n<p>你正面对 一杯青涩的茶</p>\n<p>你已老去了吧 不再为梦疯狂</p>\n<p>你平静了吧 像海上的花</p>\n<p>曾经牵着手说的以后 那个普通路口</p>\n<p>如今月如钩 海棠消瘦 一叶知秋</p>\n<p>流星只听见一句誓愿 就落在你背后</p>\n<p>下雪的冬天 树梢上挂满流年</p>\n<p>潮升 月落 雨淋湿传说</p>\n<p>一张信纸 写不下许多</p>\n<p>你想起的我 在望着什么</p>\n<p>你张开的双手 在等什么</p>\n<p>你想我吧 在某一个刹那</p>\n<p>你正面对 一杯青涩的茶</p>\n<p>你已老去了吧 不再为梦疯狂</p>\n<p>你平静了吧 像海上的花</p>\n<p>曾经牵着手说的以后 那个普通路口</p>\n<p>如今月如钩 海棠消瘦 一叶知秋</p>\n<p>流星只听见一句誓愿 就落在你背后</p>\n<p>下雪的冬天 树梢上挂满流年</p>\n<p>曾经牵着手说的以后 那个普通路口</p>\n<p>如今月如钩 海棠消瘦 一叶知秋</p>\n<p>流星只听见一句誓愿 就落在你背后</p>\n<p>下雪的冬天 树梢上挂满流年</p>\n', '![熊猫](http://static.cnbetacdn.com/article/2017/0716/47d8858530a66d0.gif "熊猫")\n\n\n\n潮升 月落 雨淋湿传说\n\n一张信纸 写不下许多\n\n你想起的我 在望着什么\n\n你张开的双手 在等什么\n\n你想我吧 在某一个刹那\n\n你正面对 一杯青涩的茶\n\n你已老去了吧 不再为梦疯狂\n\n你平静了吧 像海上的花\n\n曾经牵着手说的以后 那个普通路口\n\n如今月如钩 海棠消瘦 一叶知秋\n\n流星只听见一句誓愿 就落在你背后\n\n下雪的冬天 树梢上挂满流年\n\n \n\n潮升 月落 雨淋湿传说\n\n一张信纸 写不下许多\n\n你想起的我 在望着什么\n\n你张开的双手 在等什么\n\n你想我吧 在某一个刹那\n\n你正面对 一杯青涩的茶\n\n你已老去了吧 不再为梦疯狂\n\n你平静了吧 像海上的花\n\n曾经牵着手说的以后 那个普通路口\n\n如今月如钩 海棠消瘦 一叶知秋\n\n流星只听见一句誓愿 就落在你背后\n\n下雪的冬天 树梢上挂满流年\n\n曾经牵着手说的以后 那个普通路口\n\n如今月如钩 海棠消瘦 一叶知秋\n\n流星只听见一句誓愿 就落在你背后\n\n下雪的冬天 树梢上挂满流年\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n', 1, NULL, b'0', b'0', 3, '2016-12-17 12:50:13', 1, '2017-10-15 07:39:08', 5, '2017-07-17 11:12:54'),
	(432, '香奈儿', '<p>作曲 : 王菲</p><p>作词 : 林夕</p><p>王子挑选宠儿</p><p>外套寻找它的模特儿</p><p>那么多的玻璃鞋</p><p>有很多人适合</p><p>没有独一无二</p><p>我是谁的安琪儿</p><p>你是谁的模特儿</p><p>亲爱的 亲爱的</p><p>让你我 好好 配合</p><p>让你我 慢慢 选择</p><p>你快乐 我也 快乐</p><p>你是模特儿我是</p><p>香奈儿 香奈儿 香奈儿</p><p>香奈儿 香奈儿</p><p>嘴唇挑选颜色</p><p>感情寻找它的模特儿</p><p>衣服挂在橱窗</p><p>有太多人适合</p><p>没有独一无二</p><p>我是谁的安琪儿</p><p>你是谁的模特儿</p><p>亲爱的 亲爱的</p><p>让你我 好好 配合</p><p>让你我 慢慢 选择</p><p>你快乐 我也 快乐</p><p>你是模特儿我是</p><p>香奈儿 香奈儿 香奈儿</p><p>香奈儿 香奈儿</p>', '# 香奈儿', 1, NULL, b'1', b'0', 1, '2016-12-18 17:52:39', 3, '2017-04-17 00:05:46', 3, '2017-07-17 11:12:54'),
	(433, '成都', '<p>作曲 : 赵雷</p>\n<p>作词 : 赵雷</p>\n<p>让我掉下眼泪的 不只昨夜的酒</p>\n<p>让我依依不舍的 不只你的温柔</p>\n<p>一路还要走多久 你攥着我的手</p>\n<p>让我感到为难的 是挣扎的自由</p>\n<p>分别总是在九月 回忆是思念的愁</p>\n<p>深秋嫩绿的垂柳 亲吻着我额头</p>\n<p>在那座阴雨的小城里</p>\n<p>我从未忘记你</p>\n<p>成都 带不走的 只有你</p>\n<p>和我在成都的街头走一走 喔哦</p>\n<p>直到所有的灯都熄灭了也不停留</p>\n<p>你会挽着我的衣袖</p>\n<p>我会把手揣进裤兜</p>\n', '作曲 : 赵雷\n\n作词 : 赵雷\n\n让我掉下眼泪的 不只昨夜的酒\n\n让我依依不舍的 不只你的温柔\n\n一路还要走多久 你攥着我的手\n\n让我感到为难的 是挣扎的自由\n\n分别总是在九月 回忆是思念的愁\n\n深秋嫩绿的垂柳 亲吻着我额头\n\n在那座阴雨的小城里\n\n我从未忘记你\n\n成都 带不走的 只有你\n\n和我在成都的街头走一走 喔哦\n\n直到所有的灯都熄灭了也不停留\n\n你会挽着我的衣袖\n\n我会把手揣进裤兜', 1, NULL, b'1', b'0', 3, '2016-12-24 15:06:09', 1, '2017-07-17 10:56:19', 1, '2017-07-17 11:12:54'),
	(434, 'phpstudy 访问速度慢解决办法', '<h2 id="h2-u95EEu9898u7F18u7531"><a name="问题缘由" class="reference-link"></a><span class="header-link octicon octicon-link"></span>问题缘由</h2><p>因为自己对 php + mysql 套装没有系统研究过，所以直接使用了 phpstudy 来配置 php + mysql 环境，数据库与网站程序在同一台服务器上。但是发现网站访问速度较慢，打开页面基本需要 5s 左右。 最终找到缘由，解决方案如下：</p>\n<blockquote>\n<p>PHP5.3以上，如果是链接localhost，会检测是IPV4还是IPV6，所以会比较慢。解决办法是：链接数据的时候，不要填写localhost，改为127.0.0.1</p>\n</blockquote>\n<p>直接找到网站数据库连接文件，修改数据库链接地址 localhost =&gt; 127.0.0.1</p>\n<p>例如，本网站基于typecho搭建的，故在网站根目录下找到 <code>config.inc.php</code>文件，修改如下：</p>\n<pre><code class="lang-php">$db-&gt;addServer(array (\n  &#39;host&#39; =&gt; &#39;127.0.0.1&#39;, // 之前此处若数据库在本地则为 localhost\n  ....其他配置\n)\n</code></pre>\n<h2 id="h2-u53C2u8003"><a name="参考" class="reference-link"></a><span class="header-link octicon octicon-link"></span>参考</h2><p><a href="http://bbs.csdn.net/topics/390983216#post-401035514">phpstudy打开网页很慢如何处理 - csdn</a></p>\n', '## 问题缘由\n因为自己对 php + mysql 套装没有系统研究过，所以直接使用了 phpstudy 来配置 php + mysql 环境，数据库与网站程序在同一台服务器上。但是发现网站访问速度较慢，打开页面基本需要 5s 左右。 最终找到缘由，解决方案如下：\n\n> PHP5.3以上，如果是链接localhost，会检测是IPV4还是IPV6，所以会比较慢。解决办法是：链接数据的时候，不要填写localhost，改为127.0.0.1\n\n直接找到网站数据库连接文件，修改数据库链接地址 localhost => 127.0.0.1\n\n例如，本网站基于typecho搭建的，故在网站根目录下找到 `config.inc.php`文件，修改如下：\n\n```php\n$db->addServer(array (\n  \'host\' => \'127.0.0.1\', // 之前此处若数据库在本地则为 localhost\n  ....其他配置\n)\n```\n\n## 参考\n\n[phpstudy打开网页很慢如何处理 - csdn](http://bbs.csdn.net/topics/390983216#post-401035514)', 1, NULL, b'1', b'0', 1, '2017-07-14 12:19:50', 5, '2017-08-13 14:28:04', 5, '2017-07-17 20:15:00'),
	(435, '历历万乡', '<p>作曲 : 陈粒</p>\n<p>作词 : 陈南西</p>\n<p>她住在七月的洪流上</p>\n<p>天台倾倒理想一万丈</p>\n<p>她午睡在北风仓皇途经的芦苇荡</p>\n<p>她梦中的草原白茫茫</p>\n<p>列车搭上悲欢去辗转</p>\n<p>她尝遍了每个异乡限时赠送的糖</p>\n<p>若我站在朝阳上 能否脱去昨日的惆怅</p>\n<p>单薄语言能否传达我所有的牵挂</p>\n<p>若有天我不复勇往 能否坚持走完这一场</p>\n<p>踏遍万水千山总有一地故乡</p>\n<p>城市慷慨亮整夜光</p>\n<p>如同少年不惧岁月长</p>\n<p>她想要的不多只是和别人的不一样</p>\n<p>烛光倒影为我添茶</p>\n<p>相逢太短不等茶水凉</p>\n<p>你扔下的习惯还顽强活在我身上</p>\n<p>若我站在朝阳上 能否脱去昨日的惆怅</p>\n<p>单薄语言能否传达我所有的牵挂</p>\n<p>若有天我不复勇往 能否坚持走完这一场</p>\n<p>踏遍万水千山总有一地故乡</p>\n<p>若我站在朝阳上 能否脱去昨日的惆怅</p>\n<p>单薄语言能否传达我所有的牵挂</p>\n<p>若有天我不复勇往 能否坚持走完这一场</p>\n<p>踏遍万水千山总有一地故乡</p>\n<p>她走在马蹄的余声中</p>\n<p>夕阳燃烧离别多少场</p>\n<p>她向陌生人们解说陌生人的风光</p>\n<p>等她归来坐下对我讲</p>\n<p>故人旧时容颜未沧桑</p>\n<p>我们仍旧想要当初想要的不一样</p>\n', '作曲 : 陈粒\n\n作词 : 陈南西\n\n她住在七月的洪流上\n\n天台倾倒理想一万丈\n\n她午睡在北风仓皇途经的芦苇荡\n\n她梦中的草原白茫茫\n\n列车搭上悲欢去辗转\n\n她尝遍了每个异乡限时赠送的糖\n\n若我站在朝阳上 能否脱去昨日的惆怅\n\n单薄语言能否传达我所有的牵挂\n\n若有天我不复勇往 能否坚持走完这一场\n\n踏遍万水千山总有一地故乡\n\n城市慷慨亮整夜光\n\n如同少年不惧岁月长\n\n她想要的不多只是和别人的不一样\n\n烛光倒影为我添茶\n\n相逢太短不等茶水凉\n\n你扔下的习惯还顽强活在我身上\n\n若我站在朝阳上 能否脱去昨日的惆怅\n\n单薄语言能否传达我所有的牵挂\n\n若有天我不复勇往 能否坚持走完这一场\n\n踏遍万水千山总有一地故乡\n\n若我站在朝阳上 能否脱去昨日的惆怅\n\n单薄语言能否传达我所有的牵挂\n\n若有天我不复勇往 能否坚持走完这一场\n\n踏遍万水千山总有一地故乡\n\n她走在马蹄的余声中\n\n夕阳燃烧离别多少场\n\n她向陌生人们解说陌生人的风光\n\n等她归来坐下对我讲\n\n故人旧时容颜未沧桑\n\n我们仍旧想要当初想要的不一样', 1, NULL, b'1', b'0', 3, '2017-07-17 18:54:56', 1, '2017-07-18 14:41:49', 1, '2017-07-20 22:00:28'),
	(436, '苏幕遮·碧云天', '<p>作曲 : 高旗</p>\n<p>作词 : 范仲淹</p>\n<p>合唱编写、编曲：高旗</p>\n<p>MIDI制作：黄少峰、高旗</p>\n<p>领唱：李健</p>\n<p>碧云天 黄叶地</p>\n<p>秋色连波</p>\n<p>波上寒烟翠</p>\n<p>山映斜阳天接水</p>\n<p>芳草无情</p>\n<p>更在斜阳外</p>\n<p>黯乡魂 追旅思</p>\n<p>夜夜除非</p>\n<p>好梦留人睡</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n<p>碧云天 黄叶地</p>\n<p>秋色连波</p>\n<p>波上寒烟翠</p>\n<p>山映斜阳天接水</p>\n<p>芳草无情</p>\n<p>更在斜阳外</p>\n<p>黯乡魂 追旅思</p>\n<p>夜夜除非</p>\n<p>好梦留人睡</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n', '作曲 : 高旗\n\n作词 : 范仲淹\n\n合唱编写、编曲：高旗\n\nMIDI制作：黄少峰、高旗\n\n领唱：李健\n\n碧云天 黄叶地\n\n秋色连波\n\n波上寒烟翠\n\n山映斜阳天接水\n\n芳草无情\n\n更在斜阳外\n\n黯乡魂 追旅思\n\n夜夜除非\n\n好梦留人睡\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪\n\n碧云天 黄叶地\n\n秋色连波\n\n波上寒烟翠\n\n山映斜阳天接水\n\n芳草无情\n\n更在斜阳外\n\n黯乡魂 追旅思\n\n夜夜除非\n\n好梦留人睡\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪', 1, NULL, b'1', b'0', 0, '2017-10-13 10:15:42', 5, '2017-10-15 08:15:38', 5, '2017-11-22 10:15:05'),
	(437, '苏幕遮·碧云天', '<p>作曲 : 高旗</p>\n<p>作词 : 范仲淹</p>\n<p>合唱编写、编曲：高旗</p>\n<p>MIDI制作：黄少峰、高旗</p>\n<p>领唱：李健</p>\n<p>碧云天 黄叶地</p>\n<p>秋色连波</p>\n<p>波上寒烟翠</p>\n<p>山映斜阳天接水</p>\n<p>芳草无情</p>\n<p>更在斜阳外</p>\n<p>黯乡魂 追旅思</p>\n<p>夜夜除非</p>\n<p>好梦留人睡</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n<p>碧云天 黄叶地</p>\n<p>秋色连波</p>\n<p>波上寒烟翠</p>\n<p>山映斜阳天接水</p>\n<p>芳草无情</p>\n<p>更在斜阳外</p>\n<p>黯乡魂 追旅思</p>\n<p>夜夜除非</p>\n<p>好梦留人睡</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n', '作曲 : 高旗\n\n作词 : 范仲淹\n\n合唱编写、编曲：高旗\n\nMIDI制作：黄少峰、高旗\n\n领唱：李健\n\n碧云天 黄叶地\n\n秋色连波\n\n波上寒烟翠\n\n山映斜阳天接水\n\n芳草无情\n\n更在斜阳外\n\n黯乡魂 追旅思\n\n夜夜除非\n\n好梦留人睡\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪\n\n碧云天 黄叶地\n\n秋色连波\n\n波上寒烟翠\n\n山映斜阳天接水\n\n芳草无情\n\n更在斜阳外\n\n黯乡魂 追旅思\n\n夜夜除非\n\n好梦留人睡\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪', 1, NULL, b'1', b'0', 0, '2017-10-13 10:15:47', 5, NULL, NULL, '2017-11-22 10:15:05'),
	(438, '苏幕遮·碧云天', '<p>作曲 : 高旗</p>\n<p>作词 : 范仲淹</p>\n<p>合唱编写、编曲：高旗</p>\n<p>MIDI制作：黄少峰、高旗</p>\n<p>领唱：李健</p>\n<p>碧云天 黄叶地</p>\n<p>秋色连波</p>\n<p>波上寒烟翠</p>\n<p>山映斜阳天接水</p>\n<p>芳草无情</p>\n<p>更在斜阳外</p>\n<p>黯乡魂 追旅思</p>\n<p>夜夜除非</p>\n<p>好梦留人睡</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n<p>碧云天 黄叶地</p>\n<p>秋色连波</p>\n<p>波上寒烟翠</p>\n<p>山映斜阳天接水</p>\n<p>芳草无情</p>\n<p>更在斜阳外</p>\n<p>黯乡魂 追旅思</p>\n<p>夜夜除非</p>\n<p>好梦留人睡</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n<p>明月楼高休独倚</p>\n<p>酒入愁肠</p>\n<p>化作相思泪</p>\n', '作曲 : 高旗\n\n作词 : 范仲淹\n\n合唱编写、编曲：高旗\n\nMIDI制作：黄少峰、高旗\n\n领唱：李健\n\n碧云天 黄叶地\n\n秋色连波\n\n波上寒烟翠\n\n山映斜阳天接水\n\n芳草无情\n\n更在斜阳外\n\n黯乡魂 追旅思\n\n夜夜除非\n\n好梦留人睡\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪\n\n碧云天 黄叶地\n\n秋色连波\n\n波上寒烟翠\n\n山映斜阳天接水\n\n芳草无情\n\n更在斜阳外\n\n黯乡魂 追旅思\n\n夜夜除非\n\n好梦留人睡\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪\n\n明月楼高休独倚\n\n酒入愁肠\n\n化作相思泪', 1, NULL, b'0', b'0', 0, '2017-10-13 10:15:48', 5, '2017-10-13 11:09:22', 5, '2017-11-22 10:15:05'),
	(439, '重返魔域', '<h3 id="h3-u6B4Cu8BCD"><a name="歌词" class="reference-link"></a><span class="header-link octicon octicon-link"></span>歌词</h3><p>重返魔域 (《魔域手游》主题曲) - 窦唯<br>词：窦唯<br>曲：窦唯<br>制作人：窦唯<br>录音：陈林<br>混音：陈林<br>制作执行：陈林<br>古来魔域世间<br>暴敛奴役混沌凶险<br>铁血黑暗杀伐浑天<br>拜神助力<br>拜神助力<br>拜神助力<br>绝地反击<br>绝地反击<br>绝地反击<br>拜神助力<br>拜神助力<br>拜神助力<br>绝地反击<br>亡灵巫师魔法师<br>战将勇士异能人<br>魔族血族神选者<br>血性魔域总新生<br>长生真母盖亚之名<br>崇光秩序撒拉之名<br>智慧神秘欧斯之名<br>混沌消逝奈落之名<br>伟力争战寇德之名</p>\n', '### 歌词\n重返魔域 (《魔域手游》主题曲) - 窦唯\n词：窦唯\n曲：窦唯\n制作人：窦唯\n录音：陈林\n混音：陈林\n制作执行：陈林\n古来魔域世间\n暴敛奴役混沌凶险\n铁血黑暗杀伐浑天\n拜神助力\n拜神助力\n拜神助力\n绝地反击\n绝地反击\n绝地反击\n拜神助力\n拜神助力\n拜神助力\n绝地反击\n亡灵巫师魔法师\n战将勇士异能人\n魔族血族神选者\n血性魔域总新生\n长生真母盖亚之名\n崇光秩序撒拉之名\n智慧神秘欧斯之名\n混沌消逝奈落之名\n伟力争战寇德之名', 0, NULL, b'0', b'0', 7, '2017-10-13 11:31:40', 5, NULL, NULL, '2017-10-13 11:24:10'),
	(440, '重返魔域', '<p>重返魔域</p>\n', '重返魔域', 0, NULL, b'0', b'0', 0, '2017-10-13 11:31:55', 5, '2017-10-13 11:33:38', 5, '2017-10-13 11:31:53'),
	(441, 'AAAAAAAAAAA', '<p>AAAAAAAAAAA</p>\n', 'AAAAAAAAAAA', 0, NULL, b'0', b'0', 0, '2017-10-13 11:40:10', 5, NULL, NULL, '2017-10-13 11:40:04'),
	(442, 'AAAAAAA', '<p>AAAAAAAA</p>\n', 'AAAAAAAA', 0, NULL, b'0', b'0', 0, '2017-10-13 11:40:18', 5, NULL, NULL, '2017-10-13 11:40:15'),
	(443, 'BBBBBB', '<p>BBBBBBBBBB</p>\n', 'BBBBBBBBBB', 0, NULL, b'0', b'0', 0, '2017-10-13 11:40:32', 5, NULL, NULL, '2017-10-13 11:40:29'),
	(444, 'CCCCCCCCCCC', '<p>CCCCCCCCCCCC</p>\n', 'CCCCCCCCCCCC', 0, NULL, b'0', b'0', 5, '2017-10-13 11:40:48', 5, NULL, NULL, '2017-10-13 16:00:42'),
	(445, 'DDDDDDD', '<p>DDDDDDDDDDDDDDD</p>\n', 'DDDDDDDDDDDDDDD', 0, NULL, b'0', b'0', 0, '2017-10-13 11:40:56', 5, NULL, NULL, '2017-10-13 11:40:53'),
	(446, 'QQQQQQ', '<p>QQQQQQQQQQQQ</p>\n', 'QQQQQQQQQQQQ', 0, NULL, b'0', b'0', 0, '2017-10-13 11:41:02', 5, '2017-10-15 08:06:51', 5, '2017-10-13 11:40:58');
/*!40000 ALTER TABLE `articles` ENABLE KEYS */;

-- 导出  表 jif.cms.article_attachments 结构
CREATE TABLE IF NOT EXISTS `article_attachments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `article_id` int(11) NOT NULL,
  `filepath` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='文章关联上传附件表';

-- 正在导出表  jif.cms.article_attachments 的数据：0 rows
DELETE FROM `article_attachments`;
/*!40000 ALTER TABLE `article_attachments` DISABLE KEYS */;
/*!40000 ALTER TABLE `article_attachments` ENABLE KEYS */;

-- 导出  表 jif.cms.article_categories 结构
CREATE TABLE IF NOT EXISTS `article_categories` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `OrderIndex` tinyint(4) NOT NULL DEFAULT '0',
  `ParentId` int(11) NOT NULL DEFAULT '0',
  `CoverImg` text,
  `Description` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8 COMMENT='文章分类表';

-- 正在导出表  jif.cms.article_categories 的数据：~18 rows (大约)
DELETE FROM `article_categories`;
/*!40000 ALTER TABLE `article_categories` DISABLE KEYS */;
INSERT INTO `article_categories` (`Id`, `Name`, `OrderIndex`, `ParentId`, `CoverImg`, `Description`) VALUES
	(1, '程序开发', 0, 0, '\\attachments\\ActicleCategoryCoverImgs\\dad56c53-effc-4fde-bb8e-fe0eb8e8404b.jpg', ''),
	(2, 'C# & .Net', 1, 9, '', ''),
	(3, '生活随笔', 2, 0, '', ''),
	(4, 'es6', 0, 9, '', ''),
	(5, 'quartz.net 翻译', 3, 2, '', ''),
	(6, '小宇', 0, 3, '', ''),
	(7, '音乐', 1, 0, '', ''),
	(8, '异步多线程', 0, 2, '', '111'),
	(9, '前端开发', 0, 1, '', ''),
	(10, '设计模式', 1, 2, '\\attachments\\ActicleCategoryCoverImgs\\5b22c94d-3b43-41af-9ccc-7502df967e8b.jpg', '设计模式 GOF\nHello world'),
	(11, 'asp.net mvc', 2, 2, '', ''),
	(12, '古典', 0, 7, '', ''),
	(13, '摇滚', 2, 7, '', ''),
	(14, '西方古典', 0, 12, '', ''),
	(15, '东方古典', 1, 12, '', ''),
	(16, '笛子', 1, 7, '\\attachments\\acticleCategoryCoverImgs\\0e37ec0b-8fa0-4011-8f46-c8836d0c54db.jpg', ''),
	(17, '竹笛', 0, 16, '', ''),
	(18, '竖笛', 1, 16, '', '');
/*!40000 ALTER TABLE `article_categories` ENABLE KEYS */;

-- 导出  表 jif.cms.article_tags 结构
CREATE TABLE IF NOT EXISTS `article_tags` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TagId` int(11) NOT NULL,
  `ArticleId` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=29 DEFAULT CHARSET=utf8 COMMENT='文章-标签 对照表';

-- 正在导出表  jif.cms.article_tags 的数据：3 rows
DELETE FROM `article_tags`;
/*!40000 ALTER TABLE `article_tags` DISABLE KEYS */;
INSERT INTO `article_tags` (`Id`, `TagId`, `ArticleId`) VALUES
	(27, 2, 435),
	(28, 1, 435),
	(26, 3, 435);
/*!40000 ALTER TABLE `article_tags` ENABLE KEYS */;

-- 导出  表 jif.cms.attachments 结构
CREATE TABLE IF NOT EXISTS `attachments` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SavePath` text NOT NULL,
  `Name` text NOT NULL,
  `Size` bigint(20) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateUserId` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='附件上传记录';

-- 正在导出表  jif.cms.attachments 的数据：~4 rows (大约)
DELETE FROM `attachments`;
/*!40000 ALTER TABLE `attachments` DISABLE KEYS */;
INSERT INTO `attachments` (`Id`, `SavePath`, `Name`, `Size`, `CreateTime`, `CreateUserId`) VALUES
	(1, 'E:\\JIF.CMS\\code\\JIF.CMS\\JIF.CMS.Management\\attachments\\02acc972639f121ca06c9961565a6daf\\firefox-48.0a2.zh-CN.win32.installer.exe', 'firefox-48.0a2.zh-CN.win32.installer.exe', 45524992, '2017-07-18 11:42:17', 1),
	(2, 'E:\\JIF.CMS\\code\\JIF.CMS\\JIF.CMS.Management\\attachments\\3c646efbab2e860bf996848fddd67718\\AdbeRdr11000_zh_CN.exe', 'AdbeRdr11000_zh_CN.exe', 56671376, '2017-07-18 11:42:18', 1),
	(3, 'E:\\JIF.CMS\\code\\JIF.CMS\\JIF.CMS.Management\\attachments\\52d39e1a5da863c49cea745d143b6398\\angle_3.2.zip', 'angle_3.2.zip', 151528828, '2017-07-18 11:42:23', 1),
	(4, 'F:\\WorkDocument\\JIF.CMS\\code\\JIF.CMS\\JIF.CMS.Management\\attachments\\33806127732_4fc20b444d_k.jpg', '33806127732_4fc20b444d_k.jpg', 561020, '2017-11-27 23:14:34', 1);
/*!40000 ALTER TABLE `attachments` ENABLE KEYS */;

-- 导出  表 jif.cms.sys_admin 结构
CREATE TABLE IF NOT EXISTS `sys_admin` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `account` varchar(50) NOT NULL DEFAULT '0',
  `password` varchar(50) NOT NULL DEFAULT '0',
  `email` varchar(50) NOT NULL DEFAULT '0',
  `cellphone` varchar(50) DEFAULT '0',
  `enable` bit(1) NOT NULL DEFAULT b'0',
  `createtime` datetime NOT NULL,
  `createuserid` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='系统管理员表';

-- 正在导出表  jif.cms.sys_admin 的数据：~6 rows (大约)
DELETE FROM `sys_admin`;
/*!40000 ALTER TABLE `sys_admin` DISABLE KEYS */;
INSERT INTO `sys_admin` (`id`, `account`, `password`, `email`, `cellphone`, `enable`, `createtime`, `createuserid`) VALUES
	(1, 'chenning', '049c219f6a76cbc65e54614449e01e14', 'cdmin207078@foxmail.com', '15618147952', b'1', '2017-03-19 13:25:49', 1),
	(2, 'lh', '761f3c4c19e5e8ca1c19415c4988b0b4', '290080604@qq.com', '13315569870', b'0', '2017-03-19 13:26:52', 1),
	(3, 'cdmin207078', 'fcb7f50e4dc86a70435dd230878ac3c7', '349121171@qq.com', '13347859909', b'1', '2017-03-20 12:35:05', 1),
	(4, 'zhangxiaofan', '182618f8c4be42ed38faaeac6ae44f1a', 'zhangxiaofan@qq.com', NULL, b'0', '2017-03-21 18:40:27', 1),
	(5, 'google', 'd389411d72551cc4e5df5564c865376c', 'google@baidu.com', '123123', b'1', '2017-03-24 00:01:26', 2),
	(6, 'qin', '732ccd007f7308388359330a0012b7b4', 'qin@qq.com', 'asd', b'1', '2017-06-04 10:55:47', 1);
/*!40000 ALTER TABLE `sys_admin` ENABLE KEYS */;

-- 导出  表 jif.cms.tags 结构
CREATE TABLE IF NOT EXISTS `tags` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='文章标签';

-- 正在导出表  jif.cms.tags 的数据：6 rows
DELETE FROM `tags`;
/*!40000 ALTER TABLE `tags` DISABLE KEYS */;
INSERT INTO `tags` (`Id`, `Name`) VALUES
	(1, '陈粒'),
	(2, '民谣'),
	(3, '江湖'),
	(4, '嗨起来'),
	(5, '我不知道'),
	(6, 'phpstudy');
/*!40000 ALTER TABLE `tags` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
