using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTask.Models
{
    public class Task
    {
        public enum Importance
        {
            Low,
            Medium,
            High
        }
        public enum Stare
        {
            Open,
            InProgress,
            Closed
        }


        //[Key()]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime Added { get; set; }
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }
        [EnumDataType(typeof(Importance))]
        public Importance Important { get; set; }
        [EnumDataType(typeof(Stare))]
        public Stare State { get; set; }
        [DataType(DataType.Date)]
        public DateTime CloseAt { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
