-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        5.5.53 - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win32
-- HeidiSQL 版本:                  9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 导出 jif.cms 的数据库结构
DROP DATABASE IF EXISTS `jif.cms`;
CREATE DATABASE IF NOT EXISTS `jif.cms` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `jif.cms`;


-- 导出  表 jif.cms.article 结构
DROP TABLE IF EXISTS `article`;
CREATE TABLE IF NOT EXISTS `article` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` longtext,
  `Content` longtext,
  `MarkdownContent` longtext,
  `AllowComments` tinyint(1) NOT NULL,
  `Published` tinyint(1) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `CreateUserId` int(11) NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `UpdateUserId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=434 DEFAULT CHARSET=utf8;

-- 正在导出表  jif.cms.article 的数据：~8 rows (大约)
DELETE FROM `article`;
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article` (`Id`, `Title`, `Content`, `MarkdownContent`, `AllowComments`, `Published`, `IsDeleted`, `CategoryId`, `CreateTime`, `CreateUserId`, `UpdateTime`, `UpdateUserId`) VALUES
	(1, 'Explosions ', 'But my love you know its coming', NULL, 1, 1, 0, 2, '2016-12-11 15:48:04', 1, NULL, NULL),
	(20, '旅途·故乡', '纵梦里，还藏着那句来不及说的话 / 也不过，问句“是耶非耶”啊', NULL, 1, 1, 0, 15, '2016-12-11 16:00:15', 2, NULL, NULL),
	(428, '暮春秋色', '<p>多开阔</p><p>幻生凋落</p><p>曙分</p><p>云落</p><p>冬穿梭</p><p>晚来经过</p><p>手挥</p><p>捕捉</p><p>起风了</p><p>骤雨下天</p><p>暮春</p><p>秋色</p><p>依清池</p><p>姽婳妩媚</p><p>万丘壑</p><p>锦缎绫罗</p><p>或多</p><p>已消融</p><p>光阴归来</p><p>变空白</p><p>染尘埃</p><p>一并殓埋</p>', NULL, 1, 1, 0, 1, '2016-12-11 16:00:36', 1, '2017-01-02 02:48:10', 1),
	(429, '空谷幽兰', '<p><img alt="Image" src="http://img.xiami.net/images/album/img46/1046/5619561472119930.jpg" width="707" height="707"><br></p><p><br></p><p>纵有红颜 百生千劫</p><p>难消君心 万古情愁</p><p>青峰之巅 山外之山</p><p>晚霞寂照 星夜无眠</p><p>&nbsp;</p><p>如幻大千 惊鸿一瞥</p><p>一曲终了 悲欣交集</p><p>夕阳之间 天外之天</p><p>梅花清幽 独立春寒</p><p>&nbsp;</p><p>红尘中 你的无上清凉</p><p>寂静光明 默默照耀世界</p><p>行如风 如君一骑绝尘</p><p>空谷绝响 至今谁在倾听</p><p>&nbsp;</p><p>一念净心 花开遍世界</p><p>每临绝境 峰回路又转</p><p>但凭净信 自在出乾坤</p><p>恰似如梦初醒 归途在眼前</p><p>&nbsp;</p><p>行尽天涯 静默山水间</p><p>倾听晚风 拂柳笛声残</p><p>踏破芒鞋 烟雨任平生</p><p>慧行坚勇 究畅恒无极</p>', '> **循环依赖** - 运行时组建相互依赖\r\n\r\n## 同为属性注入\r\n\r\n若互相关联的两个类，都是使用 **`属性注入`** 来引用对方，如下这种情况：\r\n\r\n```csharp\r\nclass ClassA\r\n{\r\n  // 注意：确保有公共的 set访问器\r\n  public ClassB B { get; set; }\r\n}\r\n\r\nclass ClassB\r\n{\r\n  // 注意：确保有公共的 set访问器\r\n  public ClassA A { get; set; }\r\n}\r\n```\r\n\r\n则须保证一下几点，则可：\r\n\r\n- **依赖属性可设置**, 即属性必须是可写的.\r\n- **使用PropertiesAutowired来注册类型**, 以保证允许循环依赖\r\n- **这两种类型都不能注册为 `InstancePerDependency`**, 如果任意一个设置为 \'factory scope(工厂作用域)\', 将得不到想要的结果。必须设置为其它非 \'factory scope(工厂作用域)\' 的其它范围, 例如：`SingleInstance`, `InstancePerLifetimeScope`等\r\n\r\n注册代码如下：\r\n```csharp\r\n\r\n// 注册类型时，使用 `PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)`, 告诉autofac 允许循环依赖\r\nvar cb = new ContainerBuilder();\r\n\r\ncb.RegisterType<ClassA>()\r\n  .InstancePerLifetimeScope()\r\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\r\n\r\ncb.RegisterType<ClassB>()\r\n  .InstancePerLifetimeScope()\r\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\r\n```\r\n\r\n\r\n## 构造函数/属性注入\r\n\r\n\r\n若互相关联的两个类，一个使用 **`属性注入`** 来引用对方， 另一个使用 **`构造函数注入`** 引用对方。如下这种情况：\r\n\r\n```csharp\r\nclass ClassA\r\n{\r\n  public ClassA (classB B) { }\r\n}\r\n\r\nclass ClassB\r\n{\r\n  // 注意：确保有公共的 set访问器\r\n  public ClassA A { get; set; }\r\n}\r\n```\r\n\r\n则须保证一下几点，则可：\r\n\r\n- **依赖属性可设置**, 即属性必须是可写的\r\n- **使用属性注入的类,须使用PropertiesAutowired来注册类型**, 以保证允许循环依赖\r\n- **这两种类型都不能注册为 `InstancePerDependency`**, 如果任意一个设置为 \'factory scope(工厂作用域)\', 将等不到想要的结果。必须设置为其它非 \'factory scope(工厂作用域)\' 的其它范围, 例如：`SingleInstance`, `InstancePerLifetimeScope`等\r\n\r\n注册代码如下：\r\n```csharp\r\nvar cb = new ContainerBuilder();\r\n\r\ncb.RegisterType<ClassA>()\r\n  .InstancePerLifetimeScope();\r\n\r\n// ClassB 中使用了属性注入来引用 ClassA, 则需要允许 ClassB 循环依赖\r\ncb.RegisterType<ClassB>()\r\n  .InstancePerLifetimeScope()\r\n  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);\r\n```\r\n\r\n## 同为共造函数注入\r\n\r\n不支持同为 **`构造函数`**注入的循环依赖关系, 当你这样使用时，会抛出异常。\r\n您可以使用DynamicProxy2扩展或其他方式引用来规避此问题\r\n\r\n\r\n## 参考\r\n[Circular Dependencies - Autofac 循环依赖官方说明](http://docs.autofac.org/en/latest/advanced/circular-dependencies.html)', 1, 0, 0, 1, '2016-12-11 18:35:07', 1, '2017-01-03 23:47:18', 1),
	(430, 'ASP.NET Web API 中的 认证 与 授权', '<h1><span style="color: rgb(51, 51, 51); font-size: 16px;">当你已经创建好一个 Web API， 你现在想要控制对其的访问。在本系列文章中，我们将介绍针对未授权用户访问的一些设定选项，用来保护 web API。 这一系列将要涵盖</span><span style="color: rgb(51, 51, 51); font-size: 16px;">&nbsp;</span><strong style="color: rgb(51, 51, 51);">认证</strong><span style="color: rgb(51, 51, 51); font-size: 16px;">&nbsp;</span><code>Authentication</code><span style="color: rgb(51, 51, 51); font-size: 16px;">&nbsp;</span><span style="color: rgb(51, 51, 51); font-size: 16px;">与</span><span style="color: rgb(51, 51, 51); font-size: 16px;">&nbsp;</span><strong style="color: rgb(51, 51, 51);">授权</strong><span style="color: rgb(51, 51, 51); font-size: 16px;">&nbsp;</span><code>Authorization</code><br></h1><ul><li><strong>认证</strong>&nbsp;(<code>Authentication</code>) - 确定用户身份。例如：Alice 用她的 用户名 和 密码 登录，服务器使用密码来认证 Alice。</li><li><strong>授权</strong>&nbsp;(<code>Authorization</code>) - 决定是否允许用户执行操作。例如：Alice 有权限 获取资源， 但不能创建资源。</li></ul><p>本系列中的第一篇文章概述了ASP.NET Web API中的&nbsp;<strong>认证</strong>(<code>Authentication</code>) 和&nbsp;<strong>授权</strong>(<code>Authorization</code>)。 其它主题描述Web API的常见身份验证方案。</p><h2>认证</h2><p>Web API 假定该认证发生在 host(寄宿主机) 中。对于 web-hosting(web托管)而言, 主机是 IIS， 它将会使用&nbsp;<code>HTTP Modules</code>来进行认证。你可以配置你的项目使用 IIS 或 ASP.NET 内置的其它 认证模块， 或者也可以编写的&nbsp;<code>HTTP moduel</code>&nbsp;来进行自定义认证。</p><p>当宿主程序对用户进行认证时，将会创建一个&nbsp;<code>principal</code>&nbsp;对象。这是一个&nbsp;<code>IPrincipal</code>对象，它表示当前代码运行时的一个安全上下文。宿主程序通过设置&nbsp;<code>Thread.CurrentPrincipal</code>，将&nbsp;<code>principal</code>&nbsp;附加到当前线程。<code>principal</code>&nbsp;包含了一个表示关联用户信息的&nbsp;<code>Identity</code>&nbsp;对象。如果用户认证通过，则&nbsp;<code>Identity.IsAuthenticated = true</code>。对于匿名请求<code>IsAuthenticated = false</code>。</p><blockquote><p>更多关于&nbsp;<code>principas</code>&nbsp;请参见：<a href="http://msdn.microsoft.com/en-us/library/shz8h065.aspx" target="_blank">Role-Based Security</a></p></blockquote><h3>使用HTTP Message Handlers 进行身份验证</h3><p>你可以在&nbsp;<a href="https://www.asp.net/web-api/overview/working-with-http/http-message-handlers" target="_blank">HTTP message handler</a>中创建认证逻辑来替代使用宿主的身份认证机制。在这种情况下，<code>message handler</code>&nbsp;检查 Http请求 并 设置<code>principal</code>。</p><p>何时应该使用&nbsp;<code>message handlers</code>&nbsp;来做 认证呢？ 这里了给出一些参考权衡：</p><ul><li>An HTTP module sees all requests that go through the ASP.NET pipeline. A message handler only sees requests that are routed to Web API.</li><li>You can set per-route message handlers, which lets you apply an authentication scheme to a specific route.</li><li>HTTP modules are specific to IIS. Message handlers are host-agnostic, so they can be used with both web-hosting and self-hosting.</li><li>HTTP modules participate in IIS logging, auditing, and so on.</li><li>HTTP modules run earlier in the pipeline. If you handle authentication in a message handler, the principal does not get set until the handler runs. Moreover, the principal reverts back to the previous principal when the response leaves the message handler.</li></ul><p>一般来说，如果你不需要支持 self-hosting(自托管)，http module 是一个更好的选择。 如果需要支持 self-hosting(自托管)，建议使用 message handler。</p><h3>设置 Principal</h3><p>如果你的应用程序执行任何自定义认证逻辑，则必须在两个地方设置 principal：</p><ul><li><strong>Thread.CurrentPrincipal</strong>&nbsp;- 此属性是在.NET中设置线程主体的标准方法</li><li><strong>HttpContext.Current.User</strong>&nbsp;- 此属性是ASP.NET 专用的</li></ul><blockquote><p>对于 web-hosting(web托管)，必须在两个地方设置 principal， 否则&nbsp;<strong><code>安全上下文</code></strong>&nbsp;可能会变得不一致。 对于 self-hosting(自托管)，<code>HttpContext.Current</code>&nbsp;为 null。为了让你的代码跟寄宿环境无关，因此，在分配给<code>HttpContext.Current</code>&nbsp;之前检查是否为 null</p></blockquote><pre><code><span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">private</span> <span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">void</span> <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">SetPrincipal</span>(<span class="hljs-params" style="line-height: 1.6; color: rgb(248, 248, 242); outline: none !important;">IPrincipal principal</span>)<br style="outline: none !important;"></span>{<br style="outline: none !important;">    Thread.CurrentPrincipal = principal;<br style="outline: none !important;">    <span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">if</span> (HttpContext.Current != <span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">null</span>)<br style="outline: none !important;">    {<br style="outline: none !important;">        HttpContext.Current.User = principal;<br style="outline: none !important;">    }<br style="outline: none !important;">}<br style="outline: none !important;"></code></pre><h2>授权</h2><p><strong>授权</strong>&nbsp;发生在 pipeline(管线) 的后期, 更接近于 Controller(控制器)。这可以让更细粒度的控制授权资源.</p><ul><li><code>Authorization filters</code>(授权过滤器)，在&nbsp;<code>Controller - Action</code>&nbsp;之前运行。 如果请求未被授权，filter 将返回错误响应，并且不会调用&nbsp;<code>Action</code>。</li><li>在 Controller, Action 内部，你可以从&nbsp;<code>ApiController.User</code>&nbsp;获得当前&nbsp;<code>principal</code>&nbsp;对象。例如：你可以基于一个用户名的筛选列表，仅返回属于该用户的资源。</li></ul><p><img src="https://media-www-asp.azureedge.net/media/3994461/webapi_auth01.png" alt=""></p><h3>使用 [Authorize] 属性</h3><p>Web API提供了一个内置的授权过滤器&nbsp;<code>AuthorizeAttribute</code>。 此过滤器检查用户是否已通过身份验证。 如果没有，则返回HTTP状态代码401（Unauthorized），而不调用操作。&nbsp;<br>你你已将过滤器应用到 globally(全局), 具体controller(控制器), 具体 action(动作)。</p><p><strong>Globally</strong>&nbsp;(全局)：若要限制每个 web api 的访问，则在全局的过滤器列表中添加&nbsp;<code>AuthorizeAttribute</code></p><pre><code><span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">public</span> <span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">static</span> <span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">void</span> <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">Register</span>(<span class="hljs-params" style="line-height: 1.6; color: rgb(248, 248, 242); outline: none !important;">HttpConfiguration config</span>)<br style="outline: none !important;"></span>{<br style="outline: none !important;">    config.Filters.Add(<span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">new</span> AuthorizeAttribute());<br style="outline: none !important;">}<br style="outline: none !important;"></code></pre><p><strong>Controller</strong>&nbsp;(控制器级别)：若要限制针对某个 controller 的访问，则在具体的 controller 上添加&nbsp;<code>AuthorizeAttribute</code></p><pre><code>[Authorize]<br style="outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">public</span> <span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">class</span> <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ValuesController</span> : <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ApiController</span><br style="outline: none !important;">{<br style="outline: none !important;">    <span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">public</span> HttpResponseMessage <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">Get</span>(<span class="hljs-params" style="line-height: 1.6; color: rgb(248, 248, 242); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">int</span> id</span>) </span>{ ... }<br style="outline: none !important;">    <span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">public</span> HttpResponseMessage <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">Post</span>() </span>{ ... }<br style="outline: none !important;">}<br style="outline: none !important;"></code></pre><p><strong>Action</strong>：若要限制某个 action 的访问，则在具体的 action 上添加&nbsp;<code>AuthorizeAttribute</code></p><pre><code><br style="outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">public</span> <span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">class</span> <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ValuesController</span> : <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ApiController</span><br style="outline: none !important;">{<br style="outline: none !important;">    <span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">public</span> HttpResponseMessage <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">Get</span>() </span>{ ... }<br style="outline: none !important;"><br style="outline: none !important;">    <span class="hljs-comment" style="line-height: 1.6; color: rgb(117, 113, 94); outline: none !important;">// Require authorization for a specific action.</span><br style="outline: none !important;">    [Authorize]<br style="outline: none !important;">    <span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">public</span> HttpResponseMessage <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">Post</span>() </span>{ ... }<br style="outline: none !important;">}<br style="outline: none !important;"></code></pre><p>另外，你还可以约束一个 controller 的访问, 但允许匿名访问指定的 Action，这需要使用&nbsp;<code>[AllowAnonymous]</code>&nbsp;属性。在下面的示例中，Post 方法被约束了，而 Get 方法允许被匿名访问：</p><pre><code>[Authorize]<br style="outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">public</span> <span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">class</span> <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ValuesController</span> : <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ApiController</span><br style="outline: none !important;">{<br style="outline: none !important;">    [AllowAnonymous]<br style="outline: none !important;">    <span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">public</span> HttpResponseMessage <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">Get</span>() </span>{ ... }<br style="outline: none !important;"><br style="outline: none !important;">    <span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">public</span> HttpResponseMessage <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">Post</span>() </span>{ ... }<br style="outline: none !important;">}<br style="outline: none !important;"></code></pre><p>在之前的例子中，过滤器只允许认证通过的用户访问限制性资源，未通过的则被禁止访问。你也可以针对&nbsp;<strong>特定用户</strong>&nbsp;或者&nbsp;<strong>特定角色</strong>&nbsp;来做访问限制。</p><pre><code><span class="hljs-comment" style="line-height: 1.6; color: rgb(117, 113, 94); outline: none !important;">// 访问用户限制:</span><br style="outline: none !important;">[Authorize(Users = <span class="hljs-string" style="line-height: 1.6; color: rgb(230, 219, 116); outline: none !important;">"Alice,Bob"</span>)]<br style="outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">public</span> <span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">class</span> <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ValuesController</span> : <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ApiController</span> { ... }<br style="outline: none !important;"><br style="outline: none !important;"><span class="hljs-comment" style="line-height: 1.6; color: rgb(117, 113, 94); outline: none !important;">// 访问角色限制:</span><br style="outline: none !important;">[Authorize(Roles = <span class="hljs-string" style="line-height: 1.6; color: rgb(230, 219, 116); outline: none !important;">"Administrators"</span>)]<br style="outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">public</span> <span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">class</span> <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ValuesController</span> : <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">ApiController</span> { ... }<br style="outline: none !important;"></code></pre><blockquote><p>Web API控制器的 AuthorizeAttribute 过滤器位于 System.Web.Http 命名空间中。 对于 System.Web.Mvc 命名空间中的MVC控制器有一个类似的过滤器，它与Web API控制器不兼容。</p></blockquote><h3>自定义 Authorization Filters</h3><p>一个自定义 authorization filter 需要派生自以下几个类型：</p><ul><li><strong>AuthorizeAttribute</strong>&nbsp;派生自此类，基于当前用户 和 用户的角色 执行授权逻辑。</li><li><strong>AuthorizationFilterAttribute</strong>&nbsp;派生自此类，以执行不必基于 当前用户 或 角色 的同步授权逻辑。</li><li><strong>IAuthorizationFilter</strong>&nbsp;实现此接口，以执行异步授权逻辑逻辑。例如：如果您的授权逻辑需要进行异步I/O或网络调用。(如果你的授权逻辑是 CPU-bound(cpu密集型)的，那么直接继承自 AuthorizationFilterAttribute将会更简单，这样便不需要编写异步方法了。)</li></ul><p>下图展示了AuthorizeAttribute类的类层次结构</p><p><img alt="" src="blob:https%3A//maxiang.io/ea29e1c9-6bc5-4dec-8af5-057bbdae7a11"></p><h3>在Controller Action 中授权</h3><p>在很多情况下，您可能允许请求继续，然后根据&nbsp;<code>principal</code>&nbsp;来确定改变接下来的行为。例如：您返回的信息可能会根据用户的角色而有所不同。在 controller 的方法中，你可以通过&nbsp;<code>ApiController.user</code>&nbsp;属性得到当前的<code>principal</code>&nbsp;对象。</p><pre><code><span class="hljs-function" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;"><span class="hljs-keyword" style="line-height: 1.6; color: rgb(102, 217, 239); outline: none !important;">public</span> HttpResponseMessage <span class="hljs-title" style="line-height: 1.6; color: rgb(166, 226, 46); outline: none !important;">Get</span>()<br style="outline: none !important;"></span>{<br style="outline: none !important;">    <span class="hljs-keyword" style="line-height: 1.6; color: rgb(249, 38, 114); outline: none !important;">if</span> (User.IsInRole(<span class="hljs-string" style="line-height: 1.6; color: rgb(230, 219, 116); outline: none !important;">"Administrators"</span>))<br style="outline: none !important;">    {<br style="outline: none !important;">        <span class="hljs-comment" style="line-height: 1.6; color: rgb(117, 113, 94); outline: none !important;">// ...</span><br style="outline: none !important;">    }<br style="outline: none !important;">}<br style="outline: none !important;"></code></pre><h2>总结</h2><p>这篇文章属于 ASP.NET&nbsp;<strong>授权</strong>&nbsp;与&nbsp;<strong>认证</strong>&nbsp;的概览文章，其中第一部分讲述了认证流程：何时认证、在何处认证。认证的方法，设置 principal(安全主体) 对象。第二部分讲述了认证之后，授权的时间，授权方法。全局授权过滤，controller级授权过滤，以及具体action级授权过滤，或者也可以在 action 中根据 principal 来做逻辑判断。</p><hr><h2>参考文献</h2><p>原文链接:&nbsp;<a href="https://www.asp.net/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api" target="_blank">https://www.asp.net/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api</a></p><p>[翻译参考] ASP.NET Web API身份验证和授权:&nbsp;<a href="http://www.cnblogs.com/youring2/archive/2013/03/09/2950992.html" target="_blank">http://www.cnblogs.com/youring2/archive/2013/03/09/2950992.html</a></p><p>HttpModule的认识:&nbsp;<a href="http://www.cnblogs.com/tangself/archive/2011/03/28/1998007.html" target="_blank">http://www.cnblogs.com/tangself/archive/2011/03/28/1998007.html</a></p><p>[0].&nbsp;<strong>principal (安全主体)</strong>&nbsp;：<a href="http://www.cnblogs.com/artech/archive/2011/06/30/principal_Identity02.html" target="_blank">http://www.cnblogs.com/artech/archive/2011/06/30/principal_Identity02.html</a>&nbsp;<br>[1].&nbsp;<strong>安全上下文</strong>&nbsp;：<a href="http://www.cnblogs.com/fish-li/archive/2012/05/07/2486840.html#_label4" target="_blank">http://www.cnblogs.com/fish-li/archive/2012/05/07/2486840.html#_label4</a></p>', NULL, 1, 1, 0, 3, '2016-12-13 23:21:34', 2, '2016-12-15 21:14:31', 1),
	(431, '山河故人', '<p>山自山 水自水</p><p>&nbsp;</p><p>时光轻轻催</p><p>&nbsp;</p><p>花自落 不等不追</p><p>&nbsp;</p><p>心无畏 自由泪</p><p>&nbsp;</p><p>情爱满空杯</p><p>&nbsp;</p><p>天尽头 不醉不归</p><p>&nbsp;</p><p>去已去 来又来</p><p>&nbsp;</p><p>曾盼故人归</p><p>&nbsp;</p><p>那一刻 不离不分</p><p>&nbsp;</p><p>还记得 却不认得</p><p>&nbsp;</p><p>一种老滋味</p><p>&nbsp;</p><p>怕只怕 物是人非</p><p>&nbsp;</p><p>秋风起 云的回乡曲</p><p>&nbsp;</p><p>为相聚 不远万里</p><p>&nbsp;</p><p>天注定 未来总有人缺席</p><p>还好是 深爱过你</p><p>&nbsp;</p><p>细水流 拂清风一缕</p><p>&nbsp;</p><p>看上去 漫无目的</p><p>&nbsp;</p><p>等看透 过去总会将过去</p><p>&nbsp;</p><p>又何妨 心里有你</p><p>&nbsp;</p><p>山自山 水自水</p><p>&nbsp;</p><p>时光轻轻催</p><p>&nbsp;</p><p>花自落 不等不追</p><p>&nbsp;</p><p>心无畏 自由泪</p><p>&nbsp;</p><p>情爱满空杯</p><p>&nbsp;</p><p>天尽头 不醉不归</p><p>&nbsp;</p><p>天尽头 不醉不归</p>', NULL, 1, 1, 0, 1, '2016-12-17 12:50:13', 1, '2017-01-02 02:48:51', 2),
	(432, '香奈儿', '<p>作曲 : 王菲</p><p>作词 : 林夕</p><p>王子挑选宠儿</p><p>外套寻找它的模特儿</p><p>那么多的玻璃鞋</p><p>有很多人适合</p><p>没有独一无二</p><p>我是谁的安琪儿</p><p>你是谁的模特儿</p><p>亲爱的 亲爱的</p><p>让你我 好好 配合</p><p>让你我 慢慢 选择</p><p>你快乐 我也 快乐</p><p>你是模特儿我是</p><p>香奈儿 香奈儿 香奈儿</p><p>香奈儿 香奈儿</p><p>嘴唇挑选颜色</p><p>感情寻找它的模特儿</p><p>衣服挂在橱窗</p><p>有太多人适合</p><p>没有独一无二</p><p>我是谁的安琪儿</p><p>你是谁的模特儿</p><p>亲爱的 亲爱的</p><p>让你我 好好 配合</p><p>让你我 慢慢 选择</p><p>你快乐 我也 快乐</p><p>你是模特儿我是</p><p>香奈儿 香奈儿 香奈儿</p><p>香奈儿 香奈儿</p>', NULL, 1, 1, 0, 1, '2016-12-18 17:52:39', 3, NULL, NULL),
	(433, '成都', '<p>作曲 : 赵雷</p><p>作词 : 赵雷</p><p>让我掉下眼泪的 不止昨夜的酒</p><p>让我依依不舍的 不止你的温柔</p><p>余路还要走多久 你攥着我的手</p><p>让我感到为难的 是挣扎的自由</p><p>分别总是在九月 回忆是思念的愁</p><p>深秋嫩绿的垂柳 亲吻着我额头</p><p>在那座阴雨的小城里 我从未忘记你</p><p>成都 带不走的 只有你</p><p>和我在成都的街头走一走</p><p>直到所有的灯都熄灭了也不停留</p><p>你会挽着我的衣袖 我会把手揣进裤兜</p><p>走到玉林路的尽头 坐在(走过)小酒馆的门口</p><p>分别总是在九月 回忆是思念的愁</p><p>深秋嫩绿的垂柳 亲吻着我额头</p><p>在那座阴雨的小城里 我从未忘记你</p><p>成都 带不走的 只有你</p><p>和我在成都的街头走一走</p><p>直到所有的灯都熄灭了也不停留</p><p>你会挽着我的衣袖 我会把手揣进裤兜</p><p>走到玉林路的尽头 坐在(走过)小酒馆的门口</p><p>和我在成都的街头走一走</p><p>直到所有的灯都熄灭了也不停留</p><p>和我在成都的街头走一走</p><p>直到所有的灯都熄灭了也不停留</p><p>你会挽着我的衣袖 我会把手揣进裤兜</p><p>走到玉林路的尽头 坐在(走过)小酒馆的门口</p><p>和我在成都的街头走一走</p><p>直到所有的灯都熄灭了也不停留</p>', NULL, 1, 1, 0, 2, '2016-12-24 15:06:09', 1, '2017-01-04 00:26:18', 1);
/*!40000 ALTER TABLE `article` ENABLE KEYS */;


-- 导出  表 jif.cms.article_category 结构
DROP TABLE IF EXISTS `article_category`;
CREATE TABLE IF NOT EXISTS `article_category` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Order` tinyint(4) NOT NULL DEFAULT '0',
  `ParentCategoryId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='文章分类表';

-- 正在导出表  jif.cms.article_category 的数据：~3 rows (大约)
DELETE FROM `article_category`;
/*!40000 ALTER TABLE `article_category` DISABLE KEYS */;
INSERT INTO `article_category` (`Id`, `Name`, `Order`, `ParentCategoryId`) VALUES
	(1, '程序开发', 1, 0),
	(2, 'C#, .Net', 2, 0),
	(3, '生活随笔', 3, 0);
/*!40000 ALTER TABLE `article_category` ENABLE KEYS */;


-- 导出  表 jif.cms.sys_admin 结构
DROP TABLE IF EXISTS `sys_admin`;
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='系统管理员表';

-- 正在导出表  jif.cms.sys_admin 的数据：~5 rows (大约)
DELETE FROM `sys_admin`;
/*!40000 ALTER TABLE `sys_admin` DISABLE KEYS */;
INSERT INTO `sys_admin` (`id`, `account`, `password`, `email`, `cellphone`, `enable`, `createtime`, `createuserid`) VALUES
	(1, 'chenning', '049c219f6a76cbc65e54614449e01e14', 'cdmin207078@foxmail.com', '15618147952', b'1', '2017-03-19 13:25:49', 1),
	(2, 'lh', 'bcc571a4222c7716422286a19ebeeafd', '290080604@qq.com', '13315569870', b'0', '2017-03-19 13:26:52', 1),
	(3, 'cdmin207078', '6416035efee569dd747225825cdbde81', '349121171@qq.com', '13347859909', b'1', '2017-03-20 12:35:05', 1),
	(4, 'zhangxiaofan', '6cf66698dcb4407ae156fd5e27894b36', 'zhangxiaofan@qq.com', NULL, b'0', '2017-03-21 18:40:27', 1),
	(5, 'google', '81c78e53547b6bc05e483fdbaa3f538c', 'google@baidu.com', '123123', b'1', '2017-03-24 00:01:26', 2);
/*!40000 ALTER TABLE `sys_admin` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
