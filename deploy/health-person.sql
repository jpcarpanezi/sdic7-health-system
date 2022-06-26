-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: 192.168.86.155    Database: HealthPerson
-- ------------------------------------------------------
-- Server version	8.0.27

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `HealthPerson` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `HealthPerson`;

--
-- Table structure for table `Address`
--

DROP TABLE IF EXISTS `Address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Address` (
  `aUUID` binary(16) NOT NULL,
  `pUUID` binary(16) NOT NULL,
  `aStreet` varchar(255) NOT NULL,
  `aComplement` varchar(255) DEFAULT NULL,
  `aCity` varchar(100) NOT NULL,
  `aState` varchar(64) NOT NULL,
  `aZipCode` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`aUUID`),
  KEY `fk_Address_Person` (`pUUID`),
  CONSTRAINT `fk_Address_Person` FOREIGN KEY (`pUUID`) REFERENCES `Person` (`pUUID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Address`
--

LOCK TABLES `Address` WRITE;
/*!40000 ALTER TABLE `Address` DISABLE KEYS */;
/*!40000 ALTER TABLE `Address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `EmergencyContacts`
--

DROP TABLE IF EXISTS `EmergencyContacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `EmergencyContacts` (
  `ecUUID` binary(16) NOT NULL,
  `pUUID` binary(16) NOT NULL,
  `ecName` varchar(255) NOT NULL,
  `ecPhone` varchar(16) NOT NULL,
  `ecKinship` varchar(255) NOT NULL,
  PRIMARY KEY (`ecUUID`),
  KEY `fk_EmergencyContacts_Person` (`pUUID`),
  CONSTRAINT `fk_EmergencyContacts_Person` FOREIGN KEY (`pUUID`) REFERENCES `Person` (`pUUID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `EmergencyContacts`
--

LOCK TABLES `EmergencyContacts` WRITE;
/*!40000 ALTER TABLE `EmergencyContacts` DISABLE KEYS */;
/*!40000 ALTER TABLE `EmergencyContacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MedicalInformations`
--

DROP TABLE IF EXISTS `MedicalInformations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MedicalInformations` (
  `pUUID` binary(16) NOT NULL,
  `miBloodType` enum('A-','A+','AB+','AB-','B+','B-','O-','O+') NOT NULL,
  `miMedicalConditions` text,
  `miAllergies` text,
  `miObservations` text,
  PRIMARY KEY (`pUUID`),
  CONSTRAINT `fk_MedicalInformations_Person` FOREIGN KEY (`pUUID`) REFERENCES `Person` (`pUUID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MedicalInformations`
--

LOCK TABLES `MedicalInformations` WRITE;
/*!40000 ALTER TABLE `MedicalInformations` DISABLE KEYS */;
/*!40000 ALTER TABLE `MedicalInformations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Person`
--

DROP TABLE IF EXISTS `Person`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Person` (
  `pUUID` binary(16) NOT NULL,
  `pName` varchar(255) NOT NULL,
  `pCPF` varchar(14) NOT NULL,
  `pPhone` varchar(16) NOT NULL,
  `pBirthDate` date NOT NULL,
  `pEmail` varchar(120) NOT NULL,
  `pBirthCity` varchar(100) NOT NULL,
  PRIMARY KEY (`pUUID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Person`
--

LOCK TABLES `Person` WRITE;
/*!40000 ALTER TABLE `Person` DISABLE KEYS */;
/*!40000 ALTER TABLE `Person` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-06-26 17:06:41
