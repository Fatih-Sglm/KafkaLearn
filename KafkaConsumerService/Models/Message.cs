using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumerService.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
