USE [parkir]
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_access_control_rule]    Script Date: 07/04/2021 16:33:57 PM ******/
CREATE TYPE [dbo].[TVP_access_control_rule] AS TABLE(
	[kd petugas] [nchar](10) NULL,
	[ac_id] [int] NULL,
	[access] [nchar](1) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_jenis_member_detail]    Script Date: 07/04/2021 16:33:57 PM ******/
CREATE TYPE [dbo].[TVP_jenis_member_detail] AS TABLE(
	[kdj member] [nchar](3) NULL,
	[kd jenis] [nchar](10) NULL,
	[tarif] [numeric](18, 0) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TVP_member_detail]    Script Date: 07/04/2021 16:33:57 PM ******/
CREATE TYPE [dbo].[TVP_member_detail] AS TABLE(
	[kode member] [nchar](25) NULL,
	[kd jenis] [nchar](10) NULL,
	[detail] [nchar](50) NULL,
	[nopol] [nchar](10) NULL
)
GO
/****** Object:  Table [dbo].[access_control]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[access_control](
	[parent] [nchar](50) NULL,
	[process] [nchar](50) NULL,
	[ac_id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[access_control_rule]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[access_control_rule](
	[kd petugas] [nchar](10) NULL,
	[ac_id] [int] NULL,
	[access] [nchar](1) NULL CONSTRAINT [DF_access_control_rule_access]  DEFAULT ('T'),
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[gerbang]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gerbang](
	[nama gerbang] [nchar](20) NULL,
	[jenis] [nchar](3) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[info]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[info](
	[area_nopol] [nchar](2) NULL,
	[kode_lokasi] [nchar](5) NULL,
	[perusahaan] [nchar](50) NULL,
	[alamat] [nchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[info_kendaraan]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[info_kendaraan](
	[nopol] [nchar](10) NULL,
	[keterangan] [text] NULL,
	[exp] [date] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[jenis_kendaraan]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jenis_kendaraan](
	[kd jenis] [nchar](10) NULL,
	[keterangan] [nchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[jenis_member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jenis_member](
	[kdj member] [nchar](3) NULL,
	[jenis member] [nchar](50) NULL,
	[jenis tarif] [nchar](1) NULL,
	[persen tarif] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[jenis_member_detail]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jenis_member_detail](
	[kdj member] [nchar](3) NULL,
	[kd jenis] [nchar](10) NULL,
	[tarif] [numeric](18, 0) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[level_petugas]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[level_petugas](
	[kd level] [nchar](10) NULL,
	[nama level] [nchar](50) NULL,
	[akses] [nchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[member](
	[kode member] [nchar](25) NULL,
	[kdj member] [nchar](3) NULL,
	[nama member] [nchar](50) NULL,
	[alamat member] [nchar](50) NULL,
	[no telpon] [nchar](15) NULL,
	[unit kerja] [nchar](50) NULL,
	[exp date] [date] NULL,
	[pin] [nchar](7) NULL,
	[kd petugas] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[member_detail]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[member_detail](
	[kode member] [nchar](25) NULL,
	[kd jenis] [nchar](10) NULL,
	[detail] [nchar](50) NULL,
	[nopol] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[parkir]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parkir](
	[nomor transaksi] [nchar](25) NULL,
	[nopol] [nchar](10) NULL,
	[kode kunci] [nchar](6) NULL,
	[gerbang masuk] [nchar](50) NULL,
	[gerbang keluar] [nchar](50) NULL,
	[tgl masuk] [datetime] NULL,
	[jam masuk] [time](7) NULL,
	[tgl keluar] [datetime] NULL,
	[jam keluar] [time](7) NULL,
	[jam] [int] NULL,
	[menit] [int] NULL,
	[detik] [int] NULL,
	[tarif_masuk] [numeric](18, 0) NULL,
	[tarif_keluar] [numeric](18, 0) NULL,
	[overtime] [int] NULL,
	[tarif overtime] [numeric](18, 0) NULL,
	[denda] [numeric](18, 0) NULL,
	[info] [nchar](2) NULL,
	[jenis] [nchar](1) NULL,
	[kode member] [nchar](25) NULL,
	[petugas masuk] [nchar](10) NULL,
	[petugas keluar] [nchar](10) NULL,
	[kd jenis] [nchar](10) NULL,
	[status] [nchar](1) NULL CONSTRAINT [DF_parkir_status]  DEFAULT (N'T'),
	[kd shift in] [nchar](10) NULL,
	[kd shift out] [nchar](10) NULL,
	[img_in] [image] NULL,
	[img_out] [image] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[parkir_denda]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parkir_denda](
	[nopol] [nchar](10) NULL,
	[kd jenis] [nchar](10) NULL,
	[tgl] [datetime] NULL,
	[jam] [time](7) NULL,
	[denda] [decimal](18, 0) NULL,
	[nomor stnk] [nchar](25) NULL,
	[nama pemilik] [nchar](50) NULL,
	[alamat] [nchar](50) NULL,
	[img] [image] NULL,
	[user id] [nchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[parkir_image]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parkir_image](
	[nomor transaksi] [nchar](25) NULL,
	[tgl] [datetime] NULL,
	[img_in] [image] NULL,
	[img_out] [image] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pengaturan]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pengaturan](
	[member overtime] [nchar](1) NULL,
	[member tarif overtime] [nchar](7) NULL,
	[member by nopol] [nchar](1) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[petugas]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[petugas](
	[kd petugas] [nchar](10) NULL,
	[nama petugas] [nchar](50) NULL,
	[kd level] [nchar](10) NULL,
	[password] [nchar](32) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sesi_login]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sesi_login](
	[kd petugas] [nchar](10) NULL,
	[status] [nchar](1) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[shift]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shift](
	[kd shift] [nchar](10) NULL,
	[keterangan] [nchar](50) NULL,
	[jam mulai] [time](7) NULL,
	[jam akhir] [time](7) NULL,
	[status] [nchar](1) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tarif]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tarif](
	[kode tarif] [nchar](10) NULL,
	[kd jenis] [nchar](10) NULL,
	[tarif] [numeric](18, 0) NULL,
	[overtime] [int] NULL,
	[periode overtime] [int] NULL,
	[maksimal overtime] [int] NULL,
	[tarif overtime] [numeric](18, 0) NULL,
	[tarif denda] [numeric](18, 0) NULL,
	[grass periode] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[add_jenis_kendaraan]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[add_jenis_kendaraan]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kd_jenis nchar(10),
   @keterangan nchar(50)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'ADD'
begin
   begin transaction
   insert into jenis_kendaraan([kd jenis],[keterangan])
   values (@kd_jenis,@keterangan)
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada penambahan data jenis kendaraan'
        return (1)
      End
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End



GO
/****** Object:  StoredProcedure [dbo].[add_jenis_member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_jenis_member]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kdj_member nchar(3),
   @jenis_member nchar(50),
   @jenis_tarif nchar(1),
   @persen_tarif int,
   @jenis_member_detail_TVP TVP_jenis_member_detail ReadOnly
AS

declare @mERROR_NO int
--
if @mPROCESS = 'ADD'
begin

   begin transaction
   insert into jenis_member ([kdj member],[jenis member],[jenis tarif],[persen tarif]) 
   values (@kdj_member,@jenis_member,@jenis_tarif,@persen_tarif)                   
   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada parkir masuk'
        return (1)
      End


   insert into jenis_member_detail ([kdj member],[kd jenis],[tarif]) 
   select [kdj member],[kd jenis],[tarif] from @jenis_member_detail_TVP

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada parkir masuk'
        return (1)
      End

   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[add_member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_member]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kode_member nchar(25),
   @kdj_member nchar(3),
   @nama_member nchar(50),
   @alamat_member nchar(50),
   @no_telpon nchar(15),
   @exp_date nchar(10),
   @pin nchar(7),
   @kd_petugas nchar(10),
   @member_detail_TVP TVP_member_detail ReadOnly
AS

declare @mERROR_NO int
--
if @mPROCESS = 'ADD'
begin

   begin transaction
   insert into member ([kode member],[kdj member],[nama member],
   [alamat member],[no telpon],[exp date],[pin],[kd petugas]) 
   values (@kode_member,@kdj_member,@nama_member,@alamat_member,
   @no_telpon,@exp_date,@pin,@kd_petugas)                   
   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada input data member'
        return (1)
      End


   insert into member_detail ([kode member],[kd jenis],[detail],[nopol] ) 
   select [kode member],[kd jenis],[detail],[nopol] from @member_detail_TVP

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada input data member detail'
        return (1)
      End

   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[add_parkir_denda]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_parkir_denda]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @nopol char(10),
   @kd_jenis char(10),
   @denda decimal(18,0),
   @nomor_stnk char(35),
   @nama_pemilik char(50),
   @alamat_pemilik char(50),
   @img image,
   @user_id char(10)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'ADD'
begin
   begin transaction
   --- insert data ranap
   insert into parkir_denda ([nopol],[kd jenis],[tgl],[jam],[denda],[nomor stnk],[nama pemilik],[alamat],[img],[user id]) 
   values (@nopol,@kd_jenis,getdate(),getdate(),@denda,@nomor_stnk,@nama_pemilik,@alamat_pemilik,@img,@user_id)                          
   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada entry denda parkir'
        return (1)
      End

   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[add_parkir_keluar]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_parkir_keluar]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @nomor_transaksi nchar(25),
   @nopol nchar(10),
   @gerbang_keluar nchar(50),
   @tgl_keluar nchar(10),
   @jam_keluar nchar(10),
   @jam int,
   @menit int,
   @detik int,
   @tarif_keluar decimal(18,0),
   @overtime int,
   @tarif_overtime decimal(18,0),
   @denda decimal(18,0),
   @info nchar(2),
   @jenis nchar(1),
   @kode_member nchar(25),
   @petugas_keluar nchar(10),
   @kd_shift_out nchar(10),
   @img_out image
AS

declare @mERROR_NO int
--
if @mPROCESS = 'UPDATE'
begin
   begin transaction
   --- insert data ranap
   update parkir set [nopol] = @nopol,
   [gerbang keluar] = @gerbang_keluar,
   [tgl keluar] = getdate(),
   [jam keluar] = getdate(),
   [jam] = @jam,
   [menit] = @menit,
   [detik] = @detik,
   [tarif_keluar] = @tarif_keluar,
   [overtime] = @overtime,
   [tarif overtime] = @tarif_overtime,
   [denda] = @denda,
   [info] = @info,
   [jenis] = @jenis,
   [kode member] = @kode_member,
   [petugas keluar] = @petugas_keluar,
   [kd shift out] = @kd_shift_out,
   [status] = 'Y'
   where [nomor transaksi] = @nomor_transaksi                        
   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update parkir keluar'
        return (1)
      End

   update parkir_image set [img_out] = @img_out 
   where [nomor transaksi] = @nomor_transaksi
                           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update parkir image keluar'
        return (1)
      End

   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[add_parkir_keluar_member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_parkir_keluar_member]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @nomor_transaksi nchar(25),
   @nopol nchar(10),
   @gerbang_keluar nchar(50),
   @tgl_keluar nchar(10),
   @jam_keluar nchar(10),
   @jam int,
   @menit int,
   @detik int,
   @tarif_keluar decimal(18,0),
   @overtime int,
   @tarif_overtime decimal(18,0),
   @denda decimal(18,0),
   @info nchar(2),
   @jenis nchar(1),
   @kode_member nchar(25),
   @petugas_keluar nchar(10),
   @kd_shift_out nchar(10),
   @img_out image
AS

declare @mERROR_NO int
--
if @mPROCESS = 'UPDATE'
begin
   begin transaction
   --- insert data ranap
   update parkir set [nopol] = @nopol,
   [gerbang keluar] = @gerbang_keluar,
   [tgl keluar] = getdate(),
   [jam keluar] = getdate(),
   [jam] = @jam,
   [menit] = @menit,
   [detik] = @detik,
   [tarif_keluar] = @tarif_keluar,
   [overtime] = @overtime,
   [tarif overtime] = @tarif_overtime,
   [denda] = @denda,
   [info] = @info,
   [jenis] = @jenis,
   [kode member] = @kode_member,
   [petugas keluar] = @petugas_keluar,
   [kd shift out] = @kd_shift_out,
   [status] = 'Y'
   where [nomor transaksi] = @nomor_transaksi                        
   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update parkir keluar'
        return (1)
      End

   update parkir_image set [img_out] = @img_out 
   where [nomor transaksi] = @nomor_transaksi
                           
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update parkir image keluar'
        return (1)
      End

   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[add_parkir_masuk]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_parkir_masuk]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @nomor_transaksi char(25),
   @kode_kunci char(6),
   @gerbang_masuk char(50),
   @tgl_masuk char(10),
   @jam_masuk char(10),
   @kd_jenis char(10),
   @petugas_masuk char(10),
   @kd_shift_in char(10),
   @img_in image
AS

declare @mERROR_NO int
--
if @mPROCESS = 'ADD'
begin
   begin transaction
   --- insert data parkir
   insert into parkir ([nomor transaksi],[kode kunci],[gerbang masuk],[tgl masuk],[jam masuk],[kd jenis],[petugas masuk],[kd shift in]) 
   values (@nomor_transaksi,@kode_kunci,@gerbang_masuk,getdate(),getdate(),@kd_jenis,@petugas_masuk,@kd_shift_in)                          
   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada parkir masuk'
        return (1)
      End

   insert into parkir_image ([nomor transaksi],[tgl],[img_in]) values (@nomor_transaksi,GETDATE(),@img_in)
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada image parkir masuk'
        return (1)
      End
   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[add_tarif]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[add_tarif]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kode_tarif nchar(10),
   @kd_jenis nchar(10),
   @tarif decimal(18,0),
   @overtime int,
   @periode_overtime int,
   @maksimal_overtime int,
   @tarif_overtime decimal(18,0),
   @tarif_denda decimal(18,0),
   @grass_periode int
AS

declare @mERROR_NO int
--
if @mPROCESS = 'ADD'
begin
   begin transaction
   
   insert into tarif ([kode tarif],[kd jenis],[tarif],
                      [overtime],[periode overtime],[maksimal overtime],
                      [tarif overtime],[tarif denda],[grass periode]) 
   values (@kode_tarif,@kd_jenis,@tarif,@overtime,@periode_overtime,
           @maksimal_overtime,@tarif_overtime,@tarif_denda,@grass_periode)

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada penambahan data jenis kendaraan'
        return (1)
      End
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End



GO
/****** Object:  StoredProcedure [dbo].[add_users]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_users]
   @mERROR_MESSAGE nchar(50) output,
   @rowid int output, 
   @mPROCESS nchar(6), 
   @kd_petugas nchar(10),
   @kd_level nchar(10),
   @password nchar(32),
   @access_control_rule_TVP TVP_access_control_rule ReadOnly
AS

declare @mERROR_NO int
--
if @mPROCESS = 'ADD'
begin

   begin transaction
   insert into petugas ([kd petugas],[kd level],[password]) 
   values (@kd_petugas,@kd_level,@password)                   
   
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada input data petugas'
        return (1)
      End


   insert into access_control_rule ([kd petugas],[ac_id],[access]) 
   select [kd petugas],[ac_id],[access] from @access_control_rule_TVP

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada input access control rule'
        return (1)
      End

   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'

   DECLARE @rowid_Cursor CURSOR
   SET @rowid_Cursor = CURSOR LOCAL SCROLL FOR 
   SELECT IDENT_CURRENT('petugas') as rowid 
   OPEN @rowid_Cursor
   FETCH NEXT FROM @rowid_Cursor INTO @rowid
   CLOSE @rowid_Cursor
   DEALLOCATE @rowid_Cursor

   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[delete_jenis_kendaraan]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[delete_jenis_kendaraan]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kd_jenis nchar(10)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'DELETE'
begin
   begin transaction
   delete from jenis_kendaraan where [kd jenis] = @kd_jenis
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update data jenis kendaraan'
        return (1)
      End
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End



GO
/****** Object:  StoredProcedure [dbo].[delete_jenis_member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_jenis_member]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kdj_member nchar(3)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'DELETE'
begin

   begin transaction
   delete from jenis_member  
   where [kdj member] = @kdj_member 
             
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada hapus jenis_member'
        return (1)
      End

   delete from jenis_member_detail  where [kdj member] = @kdj_member
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada hapus jenis_member_detail'
        return (1)
      End 

   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[delete_member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_member]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kode_member nchar(25)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'DELETE'
begin
   begin transaction
   delete from member 
   where [kode member] = @kode_member 
     
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update data member'
        return (1)
      End

   delete from member_detail where [kode member] = @kode_member
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada hapus data member detail'
        return (1)
      End
	    
   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[delete_tarif]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[delete_tarif]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kode_tarif nchar(10)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'DELETE'
begin
   begin transaction
   
   delete from tarif where [kode tarif] = @kode_tarif

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada penambahan data jenis kendaraan'
        return (1)
      End
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End



GO
/****** Object:  StoredProcedure [dbo].[delete_users]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete_users]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kd_petugas nchar(10)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'DELETE'
begin
   begin transaction
   delete from petugas 
   where [kd petugas] = @kd_petugas 
     
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada delete data petugas'
        return (1)
      End

   delete from access_control_rule where [kd petugas] = @kd_petugas
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada hapus data access control'
        return (1)
      End
	    
   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[update_jenis_kendaraan]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[update_jenis_kendaraan]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kd_jenis nchar(10),
   @keterangan nchar(50)
AS

declare @mERROR_NO int
--
if @mPROCESS = 'UPDATE'
begin
   begin transaction
   update jenis_kendaraan set [keterangan] = @keterangan
   where [kd jenis] = @kd_jenis
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update data jenis kendaraan'
        return (1)
      End
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End



GO
/****** Object:  StoredProcedure [dbo].[update_jenis_member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[update_jenis_member]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kdj_member nchar(3),
   @jenis_member nchar(50),
   @jenis_tarif nchar(1),
   @persen_tarif int,
   @jenis_member_detail_TVP TVP_jenis_member_detail ReadOnly
AS

declare @mERROR_NO int
--
if @mPROCESS = 'UPDATE'
begin

   begin transaction
   update jenis_member set [jenis member] = @jenis_member,
   [jenis tarif] = @jenis_tarif,[persen tarif] = @persen_tarif 
   where [kdj member] = @kdj_member 
             
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update jenis_member'
        return (1)
      End

   delete from jenis_member_detail  where [kdj member] = @kdj_member
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada hapus jenis_member_detail'
        return (1)
      End

   insert into jenis_member_detail ([kdj member],[kd jenis],[tarif]) 
   select [kdj member],[kd jenis],[tarif] from @jenis_member_detail_TVP

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada add jenis_member_detail'
        return (1)
      End

   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[update_member]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[update_member]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kode_member nchar(25),
   @kdj_member nchar(3),
   @nama_member nchar(50),
   @alamat_member nchar(50),
   @no_telpon nchar(15),
   @exp_date nchar(10),
   @pin nchar(7),
   @member_detail_TVP TVP_member_detail ReadOnly
AS

declare @mERROR_NO int
--
if @mPROCESS = 'UPDATE'
begin
   begin transaction
   update member set [kdj member] = @kdj_member, 
   [nama member] = @nama_member,
   [alamat member] = @alamat_member,
   [no telpon] = @no_telpon,
   [exp date] = @exp_date,
   [pin] = @pin
   where [kode member] = @kode_member 
     
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada update data member'
        return (1)
      End

   delete from member_detail where [kode member] = @kode_member
   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada hapus data member detail'
        return (1)
      End

   insert into member_detail ([kode member],[kd jenis],[detail],[nopol] ) 
   select [kode member],[kd jenis],[detail],[nopol] from @member_detail_TVP

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada input data member detail'
        return (1)
      End

   --- simpan transaksi jika tidak ada error
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End





GO
/****** Object:  StoredProcedure [dbo].[update_tarif]    Script Date: 07/04/2021 16:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[update_tarif]
   @mERROR_MESSAGE nchar(50) output, 
   @mPROCESS nchar(6), 
   @kode_tarif nchar(10),
   @kd_jenis nchar(10),
   @tarif decimal(18,0),
   @overtime int,
   @periode_overtime int,
   @maksimal_overtime int,
   @tarif_overtime decimal(18,0),
   @tarif_denda decimal(18,0),
   @grass_periode int
AS

declare @mERROR_NO int
--
if @mPROCESS = 'UPDATE'
begin
   begin transaction
   
   update tarif set [kd jenis] = @kd_jenis,
          [tarif] = @tarif,
          [overtime] = @overtime,
          [periode overtime] = @periode_overtime,
          [maksimal overtime] = @maksimal_overtime,
          [tarif overtime] = @tarif_overtime,
          [tarif denda] =@tarif_denda,
          [grass periode] = @grass_periode
   where [kode tarif] = @kode_tarif

   set @mERROR_NO = @@ERROR
   if @mERROR_NO <> 0
      begin
        Rollback transaction
        set @mERROR_MESSAGE = ' Terjadi kesalahan pada penambahan data jenis kendaraan'
        return (1)
      End
   commit transaction
   set @mERROR_MESSAGE = 'Y'
   return (0)
End



GO
