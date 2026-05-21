using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_dotnet.Models
{
    [Table("todos")]
    public class Todo
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("priority")]
        public string? Priority { get; set; }

        [Column("duedate")]
        public DateTime? DueDate 
        { 
        get => _dueDate;
        set => _dueDate = value?.ToUniversalTime();
        }
        private DateTime? _dueDate;

        [Column("completed")]
        public bool Completed { get; set; }
    }
}