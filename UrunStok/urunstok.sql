-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Anamakine: 127.0.0.1
-- Üretim Zamanı: 05 May 2022, 14:27:43
-- Sunucu sürümü: 10.4.22-MariaDB
-- PHP Sürümü: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `urunstok`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `kategoriler`
--

CREATE TABLE `kategoriler` (
  `id` int(11) NOT NULL,
  `kategori` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `kategoriler`
--

INSERT INTO `kategoriler` (`id`, `kategori`) VALUES
(2, 'Mobilya'),
(8, 'Bilgisayar Parçası'),
(9, 'Giyim'),
(10, 'Süs Eşyası');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `musteriler`
--

CREATE TABLE `musteriler` (
  `id` int(11) NOT NULL,
  `ad` varchar(50) NOT NULL,
  `soyad` varchar(50) NOT NULL,
  `telefonno` bigint(11) NOT NULL,
  `gidecekurun` varchar(50) NOT NULL,
  `adres` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `musteriler`
--

INSERT INTO `musteriler` (`id`, `ad`, `soyad`, `telefonno`, `gidecekurun`, `adres`) VALUES
(2, 'Onur', 'Baları', 5536638224, 'Rtx3090', 'istanbul güngören merkez mah no 18 daire 1'),
(3, 'Aziz Emir', 'Yavuz', 5415412134, 'Siyah Tshirt', 'Dünyada bi yerde yaşıyo');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `urunler`
--

CREATE TABLE `urunler` (
  `id` int(11) NOT NULL,
  `urunadi` varchar(50) NOT NULL,
  `urunkategorisi` varchar(50) NOT NULL,
  `urunmodeli` varchar(50) NOT NULL,
  `urunserino` int(11) NOT NULL,
  `urunadedi` int(11) NOT NULL,
  `urunfiyati` int(11) NOT NULL,
  `aciklama` text NOT NULL,
  `uruntarihi` date NOT NULL,
  `resim` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Tablo döküm verisi `urunler`
--

INSERT INTO `urunler` (`id`, `urunadi`, `urunkategorisi`, `urunmodeli`, `urunserino`, `urunadedi`, `urunfiyati`, `aciklama`, `uruntarihi`, `resim`) VALUES
(1, 'Rtx3090', 'Bilgisayar Parçası', 'Nvidia Rtx3090 12gb 384bit', 12427542, 2, 45000, 'Nvidia Rtx ailesinin en güçlüsü', '2022-05-05', '4730geforce_rtx_3090_fe.png'),
(2, 'Rialto Koltuk Takımı', 'Mobilya', 'Rialto Koltuk Takımı ALFEMO MOBİLYA', 447875474, 22, 5000, 'rahat bi koltuk', '2022-05-05', '33480006327_rialto-koltuk-takimi_1440.jpeg'),
(3, 'Siyah Tshirt', 'Giyim', 'Pamuk', 4568546, 500, 20, 'acunun giydiği siyah tshirt', '2022-05-05', '13010231690821682.jpg');

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `kategoriler`
--
ALTER TABLE `kategoriler`
  ADD PRIMARY KEY (`id`);

--
-- Tablo için indeksler `musteriler`
--
ALTER TABLE `musteriler`
  ADD PRIMARY KEY (`id`);

--
-- Tablo için indeksler `urunler`
--
ALTER TABLE `urunler`
  ADD PRIMARY KEY (`id`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `kategoriler`
--
ALTER TABLE `kategoriler`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Tablo için AUTO_INCREMENT değeri `musteriler`
--
ALTER TABLE `musteriler`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Tablo için AUTO_INCREMENT değeri `urunler`
--
ALTER TABLE `urunler`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
