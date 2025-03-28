﻿using kutuphane_otomasyou.Models.table.kisiler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using kutuphane_otomasyou.Models.table.kitaplar;

namespace kutuphane_otomasyou.Models.table
{
    [Table("AlinanKitap")]
    public class AlinanKitaplar
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [StringLength(50), Required]
        public string kitap_adi { get; set; }


        [StringLength(50), Required]
        public string yazar { get; set; }


        [Required]
        public int turuId { get; set; }



        [StringLength(300), Required]
        public string ozet { get; set; }


        [Required]
        public int sayfa_sayisi { get; set; }



        [StringLength(500), Required]
        public string resimi { get; set; }

        [Required]
        public int yili { get; set; }

      
        public int kullanici_ıd { get; set; }



    }
}