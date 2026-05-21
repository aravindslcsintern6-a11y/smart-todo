// This file represents database table
// This represents the postgreSql table

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("networkscans")]
    public class NetworkScan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("target")]
        public string Target { get; set; } = string.Empty;

        [Column("scanresult")]
        public string? ScanResult { get; set; }

        [Column("scannedat")]
        public DateTime ScannedAt { get; set; }
    }
