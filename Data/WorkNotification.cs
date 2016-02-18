using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable]
    [DataContract]
    public class WorkNotification
    {
        public ObjectId id { get; set; }

        [DataMember]
        public WorkNotificationStatus Status { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        public DateTimeOffset NotificationDate { get; set; }

        public WorkNotification()
        {
            NotificationDate = DateTime.Now.Date;
        }

        public WorkNotification(
            WorkNotificationStatus status)
        {
            Status = status;
            NotificationDate = DateTime.Now.Date;
        }

        public override string ToString()
        {
            if (Status == WorkNotificationStatus.Ready)
            {
                return "Все работает штатно, работы по обновлению не ведутся";
            }
            else if (Status == WorkNotificationStatus.Busy && NotificationDate.Date <= DateTime.Now.Date)
            {
                return "Сервис недоступен, ведутся технические работы";
            }
            else
            {
                return String.Format("Сейчас все работает штатно, но на {0:dd.MM.yyyy} запланированы работы", NotificationDate.Date);
            }
        }
    }
}
