using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.Application.Common.Interfaces.MessageBroker;
public interface IRabbitMqService
{
    void CreateTopicExchange(string exchangeName);
}