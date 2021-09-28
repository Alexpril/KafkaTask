using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Kafka
{
    public interface IProducerKafka
    {
        public void ProduceMessage(string message, string[] topics = null);
    }
}
