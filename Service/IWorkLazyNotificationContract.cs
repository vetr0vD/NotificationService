using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ServiceContract]
    public interface IWorkLazyNotificationContract
    {
        /// <summary>
        /// Возвращаем статус работ актуальный для даты указаной в параметре
        /// Если на текущий момент статус Ready, то возвращаем ближайший статус отличный от Ready, если такой есть.
        /// </summary>
        [OperationContract]
        WorkNotification GetWorkStatus(DateTimeOffset date);

        /// <summary>
        /// Задаем статус работ на указанную дату. Перезаписываем статус если такой уже создан за указанную дату.
        /// </summary>
        [OperationContract]
        void SetWorkStatus(WorkNotification notification);
    }
}
