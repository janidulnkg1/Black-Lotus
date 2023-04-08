/*
SQLyog Ultimate v8.55 
MySQL - 5.5.25a : Database - blacklotus
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`blacklotus` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `blacklotus`;

/*Table structure for table `category` */

DROP TABLE IF EXISTS `category`;

CREATE TABLE `category` (
  `categoryID` int(11) NOT NULL AUTO_INCREMENT,
  `categoryName` varchar(30) DEFAULT NULL,
  `categoryCost` int(11) DEFAULT NULL,
  PRIMARY KEY (`categoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=1004 DEFAULT CHARSET=utf8;

/*Data for the table `category` */

insert  into `category`(`categoryID`,`categoryName`,`categoryCost`) values (1001,'Anniversary',4000),(1002,'Birthday',3000),(1003,'Wedding',5000);

/*Table structure for table `flowerdetails` */

DROP TABLE IF EXISTS `flowerdetails`;

CREATE TABLE `flowerdetails` (
  `flowerID` int(11) NOT NULL AUTO_INCREMENT,
  `flowerName` varchar(30) DEFAULT NULL,
  `flowerPrice` int(11) DEFAULT NULL,
  `flowerStock` int(11) DEFAULT NULL,
  PRIMARY KEY (`flowerID`)
) ENGINE=InnoDB AUTO_INCREMENT=2005 DEFAULT CHARSET=utf8;

/*Data for the table `flowerdetails` */

insert  into `flowerdetails`(`flowerID`,`flowerName`,`flowerPrice`,`flowerStock`) values (2001,'Red Rose',60,150),(2002,'White Rose',35,200),(2003,'Lily',25,150);

/*Table structure for table `orderdetails` */

DROP TABLE IF EXISTS `orderdetails`;

CREATE TABLE `orderdetails` (
  `orderID` int(11) NOT NULL AUTO_INCREMENT,
  `orderDate` varchar(30) DEFAULT NULL,
  `orderCategory` varchar(30) DEFAULT NULL,
  `orderItemName` varchar(30) DEFAULT NULL,
  `orderQuantity` int(10) DEFAULT NULL,
  `orderAmount` int(10) DEFAULT NULL,
  `customerTitle` varchar(5) DEFAULT NULL,
  `customerFname` varchar(30) DEFAULT NULL,
  `customerLname` varchar(30) DEFAULT NULL,
  `customerContactno` varchar(12) DEFAULT NULL,
  `orderStatus` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`orderID`)
) ENGINE=InnoDB AUTO_INCREMENT=3004 DEFAULT CHARSET=utf8;

/*Data for the table `orderdetails` */

insert  into `orderdetails`(`orderID`,`orderDate`,`orderCategory`,`orderItemName`,`orderQuantity`,`orderAmount`,`customerTitle`,`customerFname`,`customerLname`,`customerContactno`,`orderStatus`) values (3001,'10-10-2022','Anniversary','Red Rose',30,4500,'Mr.','Janidu','Lankage','0762200954','Order Pending'),(3003,'11-10-2022','Wedding','White Rose',30,6050,'Mr.','Sachith','Kalhara','0777123456','Order Pending');

/*Table structure for table `userdetails` */

DROP TABLE IF EXISTS `userdetails`;

CREATE TABLE `userdetails` (
  `userID` int(11) NOT NULL AUTO_INCREMENT,
  `userFname` varchar(20) DEFAULT NULL,
  `userLname` varchar(20) DEFAULT NULL,
  `userLoginName` varchar(15) DEFAULT NULL,
  `userLoginPassword` varchar(15) DEFAULT NULL,
  `userType` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`userID`)
) ENGINE=InnoDB AUTO_INCREMENT=4006 DEFAULT CHARSET=utf8;

/*Data for the table `userdetails` */

insert  into `userdetails`(`userID`,`userFname`,`userLname`,`userLoginName`,`userLoginPassword`,`userType`) values (4001,'Ithira','Gunawardhana','IG','IG@BLS','Em'),(4005,'Janidu','Lankage','JL','JL@BLS','Em');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
