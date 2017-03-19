-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        5.7.16-log - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- 导出 jif.cms 的数据库结构
DROP DATABASE IF EXISTS `jif.cms`;
CREATE DATABASE IF NOT EXISTS `jif.cms` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `jif.cms`;

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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='系统管理员表';

-- 正在导出表  jif.cms.sys_admin 的数据：~2 rows (大约)
DELETE FROM `sys_admin`;
/*!40000 ALTER TABLE `sys_admin` DISABLE KEYS */;
INSERT INTO `sys_admin` (`id`, `account`, `password`, `email`, `cellphone`, `enable`, `createtime`, `createuserid`) VALUES
	(1, 'chenning', 'fe0e99962702c02b6e0508352a2dba8d', 'cdmin207078@foxmail.com', '15618147952', b'1', '2017-03-19 13:25:49', 1),
	(2, 'lh', '86c74b307aa08b89439667b7d31ff8a5', '290080604@qq.com', '13315569870', b'0', '2017-03-19 13:26:52', 1);
/*!40000 ALTER TABLE `sys_admin` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
