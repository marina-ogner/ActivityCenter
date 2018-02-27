-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: entityproject
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `activities`
--

DROP TABLE IF EXISTS `activities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `activities` (
  `ActivityId` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) NOT NULL,
  `Datetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Duration` int(11) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedAt` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `UserId` int(11) NOT NULL,
  `Address` varchar(255) NOT NULL,
  PRIMARY KEY (`ActivityId`),
  KEY `fk_activities_users1_idx` (`UserId`),
  CONSTRAINT `fk_activities_users1` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `activities`
--

LOCK TABLES `activities` WRITE;
/*!40000 ALTER TABLE `activities` DISABLE KEYS */;
INSERT INTO `activities` VALUES (1,'Soccer in the Park','2018-04-14 15:00:00',120,'Play soccer in the park.','2018-02-26 19:59:39','2018-02-26 19:59:39',2,'14230 Bel-Red Rd, Bellevue, WA 98007'),(3,'Game Night','2018-04-15 18:00:00',180,'Play board games','2018-02-26 20:54:49','2018-02-26 20:54:49',3,'10777 Main St, Bellevue, WA 98004'),(4,'Bonfire Beach Retreat','2018-02-27 09:00:00',45,'A walkable, oceanfront town on the Olympic Peninsula with shops, dining, tennis courts, indoor pool, and much more.','2018-02-26 21:13:29','2018-02-26 21:13:29',1,'Pacific Beach, WA 98571'),(5,'Squirt Gun Fight','2018-04-14 15:30:00',60,'What better way to cool off than a good old-fashioned water fight?','2018-02-26 21:43:34','2018-02-26 21:43:34',3,'Seattle, WA'),(6,'Bonfire Beach Retreat','2019-03-01 15:00:00',120,'Relax at our family’s beach retreat, centrally located in Seabrook, WA across the street from Crescent Park','2018-02-27 10:47:49','2018-02-27 10:47:49',1,'Pacific Beach, WA 98571'),(7,'Ski lessons','2019-03-01 14:00:00',180,'The best way to learn to ski or snowboard in the Northwest.','2018-02-27 10:51:01','2018-02-27 10:51:01',2,'1001 State Route 906 Snoqualmie Pass, WA 98068');
/*!40000 ALTER TABLE `activities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `participants`
--

DROP TABLE IF EXISTS `participants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `participants` (
  `ParticipantId` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `ActivityId` int(11) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ParticipantId`),
  KEY `fk_users_has_activities_activities1_idx` (`ActivityId`),
  KEY `fk_users_has_activities_users_idx` (`UserId`),
  CONSTRAINT `fk_users_has_activities_activities1` FOREIGN KEY (`ActivityId`) REFERENCES `activities` (`ActivityId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_users_has_activities_users` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `participants`
--

LOCK TABLES `participants` WRITE;
/*!40000 ALTER TABLE `participants` DISABLE KEYS */;
INSERT INTO `participants` VALUES (4,3,4,'2018-02-26 21:40:06','2018-02-26 21:40:06'),(27,1,3,'2018-02-27 11:52:09','2018-02-27 11:52:09'),(30,2,3,'2018-02-27 11:57:31','2018-02-27 11:57:31'),(31,2,5,'2018-02-27 11:57:32','2018-02-27 11:57:32'),(33,3,6,'2018-02-27 11:57:55','2018-02-27 11:57:55'),(34,3,1,'2018-02-27 11:58:04','2018-02-27 11:58:04'),(35,1,1,'2018-02-27 11:58:55','2018-02-27 11:58:55');
/*!40000 ALTER TABLE `participants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(255) NOT NULL,
  `LastName` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Marina','Ogner','marina.ogner@gmail.com','1marina.ogner@gmail.com','2018-02-26 19:54:08','2018-02-26 19:54:08'),(2,'Tim','Team','tim@gmail.com','1tim@gmail.com','2018-02-26 19:56:32','2018-02-26 19:56:32'),(3,'Ann','Smith','ann@gmail.com','1ann@gmail.com','2018-02-26 20:27:05','2018-02-26 20:27:05');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-02-27 14:37:16
