using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IDbConnector
    {
        /// <summary>
        /// Cоединение с базой данной
        /// </summary>
        void Connect();

        /// <summary>
        /// Чтение котоое работает по правилу "ближайший нужный"
        /// </summary>
        WorkNotification ReadData(DateTimeOffset date);

        /// <summary>
        /// Чтение в бд
        /// </summary>
        void WriteData(WorkNotification item);
    }
}
