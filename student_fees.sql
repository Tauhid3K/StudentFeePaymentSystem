-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 04, 2026 at 02:37 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `student_fees`
--

-- --------------------------------------------------------

--
-- Table structure for table `admins`
--

CREATE TABLE `admins` (
  `Id` int(11) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `admins`
--

INSERT INTO `admins` (`Id`, `Email`, `Password`, `CreatedAt`, `UpdatedAt`) VALUES
(2, 'admin@gmail.com', 'admin123', '2025-12-27 05:33:21', '2025-12-27 05:33:21');

-- --------------------------------------------------------

--
-- Table structure for table `fees`
--

CREATE TABLE `fees` (
  `Id` int(11) NOT NULL,
  `Semester` varchar(50) NOT NULL,
  `Amount` decimal(10,2) NOT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `fees`
--

INSERT INTO `fees` (`Id`, `Semester`, `Amount`, `CreatedAt`, `UpdatedAt`) VALUES
(1, 'Semester 1', 25000.00, '2025-12-27 05:21:54', '2025-12-27 05:21:54'),
(2, 'Semester 2', 26000.00, '2025-12-27 05:21:54', '2025-12-27 05:21:54'),
(3, 'Semester 3', 27000.00, '2025-12-27 05:21:54', '2025-12-27 05:21:54'),
(4, 'Semester 4', 28000.00, '2025-12-27 05:21:54', '2025-12-27 05:21:54'),
(5, 'Semester 5', 29000.00, '2025-12-27 05:21:54', '2025-12-27 05:21:54'),
(6, 'Semester 6', 30000.00, '2025-12-27 05:21:54', '2025-12-27 05:21:54'),
(7, 'Semester 7', 31000.00, '2025-12-27 05:21:54', '2025-12-27 05:21:54'),
(8, 'Semester 8', 32000.00, '2025-12-27 05:21:54', '2025-12-27 05:21:54');

-- --------------------------------------------------------

--
-- Table structure for table `payments`
--

CREATE TABLE `payments` (
  `Id` int(11) NOT NULL,
  `StudentId` int(11) NOT NULL,
  `Semester` varchar(50) NOT NULL,
  `PaymentType` varchar(50) DEFAULT NULL,
  `AccountNumber` varchar(50) DEFAULT NULL,
  `Amount` decimal(10,2) NOT NULL,
  `TransactionId` varchar(100) DEFAULT NULL,
  `Status` enum('Pending','Completed','Failed','Approved') DEFAULT 'Pending',
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `payments`
--

INSERT INTO `payments` (`Id`, `StudentId`, `Semester`, `PaymentType`, `AccountNumber`, `Amount`, `TransactionId`, `Status`, `CreatedAt`, `UpdatedAt`) VALUES
(2, 4, 'Semester 4', 'Nagad', '01786343902', 28000.00, 'TRX-639025303257389351', 'Approved', '2025-12-27 05:31:58', '2025-12-28 08:52:05'),
(3, 5, 'Semester 2', 'Nagad', '01856566219', 27000.00, 'TRX-639025302088369357', 'Completed', '2025-12-27 06:13:22', '2025-12-28 08:50:23'),
(6, 9, 'Semester 4', 'Nagad', '01763044123', 28000.00, 'TRX-639025294433506867', 'Completed', '2025-12-28 08:35:57', '2025-12-28 08:49:15'),
(7, 9, 'Semester 5', NULL, NULL, 29000.00, NULL, 'Pending', '2025-12-28 08:37:30', '2025-12-28 08:37:30'),
(8, 5, 'Semester 3', NULL, NULL, 27000.00, NULL, 'Pending', '2025-12-28 08:50:23', '2025-12-28 08:50:23');

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `Id` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Session` varchar(50) DEFAULT NULL,
  `Roll` varchar(50) NOT NULL,
  `Semester` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `PendingFees` decimal(10,2) DEFAULT 0.00,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`Id`, `Name`, `Session`, `Roll`, `Semester`, `Email`, `Password`, `PendingFees`, `CreatedAt`, `UpdatedAt`) VALUES
(4, 'Tauhid Shahriar', '22-23', '75', '5th', 'tauhid@gmail.com', 'password123', 0.00, '2025-12-27 05:31:58', '2025-12-28 06:39:45'),
(5, 'Jhone', '21-22', '08', '3rd', 'john@gmail.com', 'password123', 0.00, '2025-12-27 06:13:22', '2025-12-28 08:50:23'),
(9, 'Nafis Islam', '21-22', '76', '5th', 'nafis@gmail.com', 'password123', 0.00, '2025-12-28 08:35:57', '2025-12-28 08:37:30');

--
-- Triggers `students`
--
DELIMITER $$
CREATE TRIGGER `trg_auto_payment_after_student_insert` AFTER INSERT ON `students` FOR EACH ROW BEGIN
    DECLARE fee_amount DECIMAL(10,2);
    DECLARE sem_name VARCHAR(50);

    -- Map student input to fees.Semester
    CASE NEW.Semester
        WHEN '1st' THEN SET sem_name = 'Semester 1';
        WHEN '2nd' THEN SET sem_name = 'Semester 2';
        WHEN '3rd' THEN SET sem_name = 'Semester 3';
        WHEN '4th' THEN SET sem_name = 'Semester 4';
        WHEN '5th' THEN SET sem_name = 'Semester 5';
        WHEN '6th' THEN SET sem_name = 'Semester 6';
        WHEN '7th' THEN SET sem_name = 'Semester 7';
        WHEN '8th' THEN SET sem_name = 'Semester 8';
        ELSE SET sem_name = NULL;
    END CASE;

    IF sem_name IS NOT NULL THEN
        SELECT Amount INTO fee_amount
        FROM fees
        WHERE Semester = sem_name
        LIMIT 1;

        IF fee_amount IS NOT NULL THEN
            INSERT INTO payments (StudentId, Semester, Amount, Status, CreatedAt, UpdatedAt)
            VALUES (NEW.Id, sem_name, fee_amount, 'Pending', NOW(), NOW());
        END IF;
    END IF;
END
$$
DELIMITER ;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admins`
--
ALTER TABLE `admins`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- Indexes for table `fees`
--
ALTER TABLE `fees`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Semester` (`Semester`);

--
-- Indexes for table `payments`
--
ALTER TABLE `payments`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `StudentId` (`StudentId`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Roll` (`Roll`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admins`
--
ALTER TABLE `admins`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `fees`
--
ALTER TABLE `fees`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `payments`
--
ALTER TABLE `payments`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `payments`
--
ALTER TABLE `payments`
  ADD CONSTRAINT `payments_ibfk_1` FOREIGN KEY (`StudentId`) REFERENCES `students` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
